using ShipOnline.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;
using Web_Gio_Cha.UtilityService;
using WebDuhoc.Models.Define;
using Rotativa;
using Rotativa.MVC;

namespace Web_Gio_Cha.Controllers
{
    public class AdminManageOrderController : BaseController
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: AdminOrderShip
        #region LIST
        [HttpGet]
        public ActionResult OrderList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            ManageDistrictDa dataAccess = new ManageDistrictDa();

            AdminOrderList model = new AdminOrderList();
            CommonService comService = new CommonService();
            CommonController comControl = new CommonController();

            int HaNoiCity = 1;
            model.DISTRICT_LIST = dataAccess.getDistrictList(HaNoiCity).ToList().Select(
            f => new SelectListItem
            {
                Value = f.ID.ToString(),
                Text = f.Name
            }).ToList();
            model.DISTRICT_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "Chọn Quận/huyện" });

            model.SORT_TYPE_LIST = UtilityServices.UtilityServices.SortTypeList();
            model.STATUS_SELECT_LIST = UtilityServices.UtilityServices.getListStatusAll();
            model.TO_DATE = DateTime.Now;
            model.FROM_DATE = model.TO_DATE.Value.AddDays(-15);

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataTableModel dt, AdminOrderList condition)
        {
            if (ModelState.IsValid)
            {
                using (AdminOrderService service = new AdminOrderService())
                {
                    try
                    {
                        int total_row = 0;
                        var OrderSummaryInfo = new AdminOrderList();
                        AdminOrderDa da = new AdminOrderDa();
                        if (!string.IsNullOrEmpty(condition.ORDER_STATUS_LIST))
                        {
                            List<string> statusList = condition.ORDER_STATUS_LIST.Split(',').ToList();
                            if (statusList.Count > 0)
                            {
                                condition.ORDER_STATUS_LIST = string.Join("','", statusList.ToArray());
                            }
                        }

                        var dataList = service.SearchOrderList(dt, condition, out total_row).ToList();
                        var totalDataNoneStatus = service.SearchOrderListTotalNoneStatus(dt, condition);

                        OrderSummaryInfo.TotalStatus_0 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Create).ToList().Count();
                        OrderSummaryInfo.TotalStatus_1 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.TakingOrder).ToList().Count();
                        OrderSummaryInfo.TotalStatus_2 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Shiping).ToList().Count();
                        OrderSummaryInfo.TotalStatus_3 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Delivery).ToList().Count();
                        OrderSummaryInfo.TotalStatus_4 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Finished).ToList().Count();
                        OrderSummaryInfo.TotalStatus_5 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Cancel).ToList().Count();
                        OrderSummaryInfo.TotalStatus_6 = totalDataNoneStatus.Where(i => i.Status == OrderStatus.Error).ToList().Count();

                        OrderSummaryInfo.TotalStatusAll = totalDataNoneStatus.Count();
                        for (int i = 0; i < dataList.Count(); i++)
                        {
                            var getProductDetail = da.getlistProductByOrder(dataList[i].ID).Take(3);
                            List<string> listName = new List<string>();
                            foreach (var det in getProductDetail)
                            {
                                det.Name = det.Name.Length > 10 ? det.Name.Substring(0, 10) : det.Name;
                                listName.Add(det.Name);
                            }
                            if (listName.Count > 0)
                            {
                                dataList[i].Product_Name = string.Join(", ", listName);
                            }
                        }

                        OrderSummaryInfo.thisMonth = Utility.GetCurrentDateOnly().Month;
                        //var thismonth = Utility.GetCurrentDateOnly().ToString("MM/yyyy");
                        //var dataThisMonth = service.SearchOrderByMonth(thismonth);
                        //OrderSummaryInfo.TotalOrderthisMonth = dataThisMonth.Count();

                        int totalRowCount = dataList.Count();
                        int lastItem = dt.iDisplayLength + dt.iDisplayStart;

                        var tableData = (from i in dataList
                                         select new object[]
                                {
                                    i.ID,
                                    i.Code,
                                    OrderStatus.Items[i.Status.Value].ToString(),
                                    i.Product_Name,
                                    i.TongTienHang,
                                    i.PriceDiscountTotal,
                                    i.PriceShip,
                                    i.PriceTotal,
                                    i.UserName,
                                    i.Receive_Phone,
                                    i.DistrictName,
                                    i.Receive_Address,
                                    i.CreatedDate.HasValue ? i.CreatedDate.Value.ToString("dd/MM/yyyy hh:mm") : "",
                                    PaymentMethodType.Items[i.PaymentMethod],
                                    i.Paid == false ? PayStatus.Items[0] : PayStatus.Items[1],
                                    i.Status,
                                    i.del_flg
                                });

                        var result = Json(new
                        {
                            sEcho = dt.sEcho,
                            iTotalRecords = total_row,
                            iTotalDisplayRecords = total_row,
                            objOrderSummaryInfo = OrderSummaryInfo,
                            aaData = tableData
                        });
                        Session["SearchAdminOrderShipList"] = condition;
                        
                        result.MaxJsonLength = Int32.MaxValue;
                        return result;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            else
            {
                var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            }
            return new EmptyResult();
        }
        #endregion

        #region VIEW ORDER SHIP
        public ActionResult ViewOrderDetail(long OrderId = 0)
        {
            ViewOrderModel model = new ViewOrderModel();
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            CommonService comService = new CommonService();
            CommonDa da = new CommonDa();
            AdminOrderDa dataAccess = new AdminOrderDa();
            if (OrderId > 0)
            {
                ViewOrderModel infor = new ViewOrderModel();
                infor = dataAccess.getInforOrder(OrderId);
                model = infor != null ? infor : model;
                string CompanyCd = "00000";
                var inforCompany = da.getInfoCompany(CompanyCd);
                if (inforCompany != null)
                {
                    model.CompanyName = inforCompany.COMPANY_NAME;
                    model.CompanyAddress = inforCompany.COMPANY_ADDRESS;
                    model.CompanyEmail = inforCompany.COMPANY_EMAIL;
                    model.CompanyPhone = inforCompany.COMPANY_PHONE;
                }

                model.AddressCustomer = model.DistrictName + ", " + model.Receive_Address;
                model.ORDER_STATUS_TEXT = OrderStatus.Items[model.Status].ToString();

                var getOrderDetail = dataAccess.getOrderDetail(model.ID).Take(3);
                ViewBag.listOrderDetail = getOrderDetail.ToList();
                foreach (var data in getOrderDetail)
                {
                    model.ProductDetailTotal += data.ProductName + " (Số lượng: " + data.Quantity.ToString() + "), ";
                }

                if (String.IsNullOrEmpty(model.LINK_QRCODE))
                {
                    model.LINK_QRCODE = QRCodeServices.creatQR(model.Code);
                    dataAccess.UpdateQRLink(OrderId, model.LINK_QRCODE);
                }
                if (!String.IsNullOrEmpty(model.LINK_QRCODE))
                {
                    model.LINK_QRCODE = model.LINK_QRCODE.Replace("~", "");
                }

            }

            return this.PartialView("ViewOrderDetail", model);
        }

        #endregion

        #region PRINT PDF
        [HttpPost]
        public ActionResult PrintOrderCustom(string ORDER_ID_STRING)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            CommonService comService = new CommonService();
            CommonDa da = new CommonDa();
            AdminOrderDa dataAccess = new AdminOrderDa();
            List<ViewOrderModel> listOrderDetail = new List<ViewOrderModel>();
            ViewOrderModel model = new ViewOrderModel();

            if (!string.IsNullOrEmpty(ORDER_ID_STRING))
            {
                List<long> ORDER_ID_LIST = ORDER_ID_STRING.Split(',').Select(long.Parse).ToList();
                if (ORDER_ID_LIST.Count > 0)
                {
                    for (int i = 0; i < ORDER_ID_LIST.Count(); i++)
                    {
                        if (ORDER_ID_LIST[i] > 0)
                        {
                            ViewOrderModel infor = new ViewOrderModel();
                            infor = dataAccess.getInforOrder(ORDER_ID_LIST[i]);
                            model = infor != null ? infor : model;
                            string CompanyCd = "00000";
                            var inforCompany = da.getInfoCompany(CompanyCd);
                            if (inforCompany != null)
                            {
                                model.CompanyName = inforCompany.COMPANY_NAME;
                                model.CompanyAddress = inforCompany.COMPANY_ADDRESS;
                                model.CompanyEmail = inforCompany.COMPANY_EMAIL;
                                model.CompanyPhone = inforCompany.COMPANY_PHONE;
                            }

                            model.AddressCustomer = model.DistrictName + ", " + model.Receive_Address;
                            model.ORDER_STATUS_TEXT = OrderStatus.Items[model.Status].ToString();

                            var getOrderDetail = dataAccess.getOrderDetail(model.ID).Take(3);
                            ViewBag.listOrderDetail = getOrderDetail.ToList();
                            foreach (var data in getOrderDetail)
                            {
                                model.ProductDetailTotal += data.ProductName + " (Số lượng: " + data.Quantity.ToString() + "), ";
                            }

                            if (String.IsNullOrEmpty(model.LINK_QRCODE))
                            {
                                model.LINK_QRCODE = QRCodeServices.creatQR(model.Code);
                                dataAccess.UpdateQRLink(ORDER_ID_LIST[i], model.LINK_QRCODE);
                            }
                            if (!String.IsNullOrEmpty(model.LINK_QRCODE))
                            {
                                model.LINK_QRCODE = model.LINK_QRCODE.Replace("~", "");
                            }
                            listOrderDetail.Add(model);
                        }
                    }
                }
                //return new ViewAsPdf("ViewOrderDetailPDF", listOrderDetail);
                return PartialView("ViewOrderDetailPDF", listOrderDetail);

            }

            return new EmptyResult();
        }

        #endregion
    }
}