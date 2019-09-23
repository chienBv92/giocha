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
    public class ManageCategoryProductController : Controller
    {
        // GET: ManageCategoryProduct
        public ActionResult Index()
        {
            return View();
        }

        #region REGISTER/ UPDATE
        public ActionResult ProductCategoryEdit(int CATEGORY_ID = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            ProductCategoryModel model = new ProductCategoryModel();
            ProductCategoryService service = new ProductCategoryService();
            if (CATEGORY_ID > 0)
            {
                model = service.getProductCategory(CATEGORY_ID);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryModel model)
        {
            try
            {
                using (ProductCategoryService service = new ProductCategoryService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        long res = 0;
                        if (model.CATEGORY_ID_HIDDEN == 0)
                        {
                            isNew = true;

                            res = service.InsertProductCategory(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;

                            res = service.UpdateProductCategory(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
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

        //check exist
        public ActionResult CheckExistCategoryProduct(int category_id, int categoryId_Old)
        {
            if (Request.IsAjaxRequest())
            {
                // Declare new DataAccess object
                ProductCategoryDa dataAccess = new ProductCategoryDa();

                var exist = dataAccess.FindCategoryProduct(category_id, categoryId_Old);
                JsonResult result = Json(new
                {
                    exist
                }, JsonRequestBehavior.AllowGet);

                return result;

            }
            return new EmptyResult();
        }

        #endregion

        #region List
        public ActionResult ProductCategoryList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            ProductCategoryModel model = new ProductCategoryModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataTableModel dt, ProductCategoryModel condition)
        {
            if (ModelState.IsValid)
            {
                using (ProductCategoryService service = new ProductCategoryService())
                {
                    int total_row = 0;
                    var dataList = service.SearchProductCategoryList(dt, condition, out total_row);

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
                                    order++,
                                    i.ID,
                                    i.Name != null ? HttpUtility.HtmlEncode(i.Name) : String.Empty,
                                    i.DisplayOrder != null? i.DisplayOrder : 0,
                                    i.Status == true? "Hiển thị" : "Ẩn",
                                    i.del_flg,
                                })
                        });

                    result.MaxJsonLength = Int32.MaxValue;
                    return result;
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region DELETE
        public ActionResult Delete(int CATEGORY_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid && CATEGORY_ID > 0)
                {
                    using (var service = new ProductCategoryService())
                    {
                        var deleteResult = service.DeleteProductCategory(CATEGORY_ID);

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