﻿using ShipOnline.UtilityService;
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
    public class UserManageOrderController : Controller
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: UserManageOrder
        #region LIST ORDER USER
        public ActionResult ManageOrderList(AdminOrderList condition)
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

            AdminOrderDa da = new AdminOrderDa();
            UserOrderService service = new UserOrderService();
            DataTableModel dt = new DataTableModel();
            dt.iDisplayStart = 0;
            dt.iDisplayLength = 25;
            int total_row = 0;
            condition.UserID = currentUser.ID;
            var dataList = service.SearchOrderListbyUser(dt, condition, out total_row).ToList();
            var totalDataNoneStatus = service.SearchOrderListTotalNoneStatus(dt, condition);
            for (int i = 0; i < dataList.Count(); i++)
            {
                dataList[i].ORDER_STATUS_TEXT = OrderStatus.Items[dataList[i].Status].ToString();
                dataList[i].CREATE_DATE_STRING = dataList[i].CreatedDate.HasValue ? dataList[i].CreatedDate.Value.ToString("dd/MM/yyyy hh:mm") : "";

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

            return View(dataList);
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
                                    i.Paid == true ?  true : false,
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

        #region LIST PRODUCT DETAIL
        public ActionResult OrderDetail(string Code)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            AdminOrderDa da = new AdminOrderDa();
            UserOrderService service = new UserOrderService();

            IList<OrderDetail> listDetail = new List<OrderDetail>();
            var getOrder = da.getOrderByCode(Code);
            if (getOrder != null)
            {
                var getOrderDetail = da.getOrderDetail(getOrder.ID);
                listDetail = getOrderDetail;
            }

            return View(listDetail);
        }

        #endregion
    }
}