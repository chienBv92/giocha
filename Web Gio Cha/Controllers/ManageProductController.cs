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
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Controllers
{
    public class ManageProductController : BaseController
    {
        // GET: ManageProduct
        #region REGISTER/ UPDATE
        public ActionResult ProductEdit(long ProductId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            ProductModel model = new ProductModel();
            CommonService comService = new CommonService();
            ProductService service = new ProductService();

            if (ProductId > 0)
            {
                ProductModel infor = new ProductModel();
                infor = service.getProductByID(ProductId);
                model = infor != null ? infor : model;
                model.Detail = HttpUtility.HtmlDecode(model.Detail);
            }

            model.CATEGORY_LIST = comService.GetCategoryList().ToList().Select(
            f => new SelectListItem
            {
                Value = f.ID.ToString(),
                Text = f.Name
            }).ToList();

            model.CATEGORY_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "Chọn danh mục" });

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductModel model)
        {
            try
            {
                using (ProductService service = new ProductService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        long res = 0;
                        if (model.ID == 0)
                        {
                            isNew = true;
                            res = service.InsertProduct(model);
                            JsonResult result = Json(new { isNew = isNew, res = res }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;
                            res = service.UpdateProduct(model);
                            JsonResult result = Json(new { isNew = isNew, res = res }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                    }
                    else
                    {
                        var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    }

                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                System.Web.HttpContext.Current.Session["ERROR"] = ex;
                return new EmptyResult();
            }
        }

        #endregion

        #region LIST
        [HttpGet]
        public ActionResult ProductList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.ID == 0 || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            ProductModel model = new ProductModel();
            CommonService comService = new CommonService();

            var tmpCondition = GetRestoreData() as ProductModel;
            if (tmpCondition != null)
            {
                model = tmpCondition;
            }

            model.CATEGORY_LIST = comService.GetCategoryList().ToList().Select(
           f => new SelectListItem
           {
               Value = f.ID.ToString(),
               Text = f.Name
           }).ToList();

            model.CATEGORY_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "Chọn danh mục" });

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataTableModel dt, ProductModel condition)
        {
            if (ModelState.IsValid)
            {
                using (ProductService service = new ProductService())
                {
                    int total_row = 0;
                    var dataList = service.SearchProductList(dt, condition, out total_row);

                    int order = 1;
                    int totalRowCount = dataList.Count();
                    int lastItem = dt.iDisplayLength + dt.iDisplayStart;

                    var result = Json(
                        new
                        {
                            sEcho = dt.sEcho,
                            iTotalRecords = total_row,
                            iTotalDisplayRecords = total_row,
                            aaData = (from i in dataList
                                      select new object[]
                                {
                                    i.ID,
                                    order++,
                                    HttpUtility.HtmlEncode(i.Name),
                                    i.Image,
                                    i.PriceBefore != null ? i.PriceBefore.Value : 0,
                                    i.Price != null ? i.Price.Value : 0,
                                    i.Quantity != null ? i.Quantity.Value : 0,
                                    i.Status == true ? "Hiển thị": "Ẩn",
                                    i.CreatedDate != null ? i.CreatedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.CreateName,
                                    i.ModifiedDate != null ? i.ModifiedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.ModifiedName,
                                    i.del_flg,
                                    i.Is_Hot
                                })

                        });
                    SaveRestoreData(condition);

                    result.MaxJsonLength = Int32.MaxValue;
                    return result;
                }
            }
            else
            {
                var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            }
            return new EmptyResult();
        }
        #endregion

        #region DELETE
        public ActionResult DeleteProduct(long PRODUCT_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid && PRODUCT_ID > 0)
                {
                    using (var service = new ProductService())
                    {
                        var deleteResult = service.DeleteProduct(PRODUCT_ID);

                        JsonResult result = Json(new
                        {
                            statusCode = deleteResult ? Constant.SUCCESSFUL : Constant.INTERNAL_SERVER_ERROR
                        }, JsonRequestBehavior.AllowGet);

                        return result;
                    }
                }
                else
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                }
            }

            return new EmptyResult();
        }

        #endregion

    }
}