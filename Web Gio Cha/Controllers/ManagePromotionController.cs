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
    public class ManagePromotionController : BaseController
    {
        // GET: ManagePromotion
        #region REGISTER/ UPDATE
        public ActionResult PromotionEdit(long promotionId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblPromotion model = new TblPromotion();

            CommonService comService = new CommonService();
            ManagePromotionDa dataAccess = new ManagePromotionDa();
            if (promotionId > 0)
            {
                var info = dataAccess.getPromotionByID(promotionId);
                if (info != null)
                {
                    info.Discount = (int)info.Discount;
                    model = info;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TblPromotion model)
        {
            try
            {
                using (ManagePromotionService service = new ManagePromotionService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        TblPromotion entity = new TblPromotion();

                        if (model.id == 0)
                        {
                            isNew = true;

                            service.InsertPromotion(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;

                            service.UpdatePromotion(model);
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

        #endregion

        #region LIST
        // GET: /AdminManageDistrict/
        public ActionResult PromotionList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblPromotion model = new TblPromotion();
            CommonService comService = new CommonService();

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataTableModel dt, TblPromotion condition)
        {
            if (ModelState.IsValid)
            {
                using (ManagePromotionService service = new ManagePromotionService())
                {
                    int total_row = 0;
                    var dataList = service.SearchPromotionList(dt, condition, out total_row);

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
                                    i.id,
                                    order++,
                                    i.PromotionName != null ? HttpUtility.HtmlEncode(i.PromotionName) : String.Empty,
                                    i.PriceMin,
                                    i.PriceMax,
                                    i.Discount.HasValue ? i.Discount.Value + "%" : "",
                                    i.Status == true? "Hiển thị" : "Ẩn",
                                    i.del_flg
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
        public ActionResult DeletePromotion(long PROMOTION_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid && PROMOTION_ID > 0)
                {
                    using (var service = new ManagePromotionService())
                    {
                        var deleteResult = service.DeletePromotion(PROMOTION_ID);

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

        #region FIND DISTRCIT
        public ActionResult getDictrictByID(long districtID)
        {
            if (Request.IsAjaxRequest())
            {
                // Declare new DataAccess object
                ManageDistrictDa dataAccess = new ManageDistrictDa();

                var exist = dataAccess.getCityByID(districtID);
                JsonResult result = Json(new
                {
                    exist
                }, JsonRequestBehavior.AllowGet);

                return result;

            }
            return new EmptyResult();
        }
        #endregion
    }
}