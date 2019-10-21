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

namespace Web_Gio_Cha.Controllers
{
    public class AdminManageCompanyController : BaseController
    {
        // GET: AdminManageCompany
        #region REGISTER/ UPDATE
        public ActionResult CompanyEdit()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;
            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblCompany model = new TblCompany();

            CommonService comService = new CommonService();
            CompanyDa dataAccess = new CompanyDa();
            string CompanyCd = "00000";
            TblCompany infor = new TblCompany();

            infor = dataAccess.getCompanyByCd(CompanyCd);
            model = infor != null ? infor : model;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblCompany model)
        {
            try
            {
                using (CompanyService service = new CompanyService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;

                        if (model.COMPANY_ID == 0)
                        {
                            isNew = true;

                            service.InsertCompany(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;

                            service.UpdateCompany(model);
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

        // check exist CompanyCd
        public ActionResult CheckExistCompanyCd(string companyCd)
        {
            if (Request.IsAjaxRequest())
            {
                // Declare new DataAccess object
                CompanyDa dataAccess = new CompanyDa();

                var exist = dataAccess.CheckExistCompanyCd(companyCd);
                JsonResult result = Json(new
                {
                    exist
                }, JsonRequestBehavior.AllowGet);

                return result;

            }
            return new EmptyResult();
        }

        #endregion

        //#region LIST
        //[HttpGet]
        //public ActionResult CompanyList()
        //{
        //    CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;
        //    var authorityList = currentUser != null ? currentUser.USER_AUTHORITY : 0;

        //    if (currentUser == null || authorityList != User_Authority.Set_Admin)
        //    {
        //        return RedirectToAction("Login", "UserAccount");
        //    }

        //    CompanyModel model = new CompanyModel();
        //    CommonService comService = new CommonService();

        //    var tmpCondition = GetRestoreData() as CompanyModel;
        //    if (tmpCondition != null)
        //    {
        //        model = tmpCondition;
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CompanyList(DataTableModel dt, CompanyModel condition)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (CompanyService service = new CompanyService())
        //        {
        //            int total_row = 0;
        //            var dataList = service.SearchCompanyList(dt, condition, out total_row);

        //            int order = 1;
        //            int totalRowCount = dataList.Count();
        //            int lastItem = dt.iDisplayLength + dt.iDisplayStart;

        //            var result = Json(
        //                new
        //                {
        //                    sEcho = dt.sEcho,
        //                    iTotalRecords = total_row,
        //                    iTotalDisplayRecords = total_row,
        //                    aaData = (from i in dataList
        //                              select new object[]
        //                        {
        //                            i.COMPANY_ID,
        //                            order++,
        //                            i.COMPANY_CD,
        //                            i.COMPANY_NAME != null ? HttpUtility.HtmlEncode(i.COMPANY_NAME) : String.Empty,
        //                            i.COMPANY_ADDRESS,
        //                            i.COMPANY_PHONE,
        //                            i.COMPANY_EMAIL != null ? HttpUtility.HtmlEncode(i.COMPANY_EMAIL) : String.Empty,
        //                            i.FACE_PAGE != null ? HttpUtility.HtmlEncode(i.FACE_PAGE) : String.Empty,
        //                            i.EMPLOYEE_NUMS,
        //                            i.INS_DATE != null ? i.INS_DATE.Value.ToString("dd/MM/yyyy") : String.Empty,
        //                            i.INS_USER_NAME,
        //                            i.DEL_FLG
        //                        })

        //                });
        //            SaveRestoreData(condition);

        //            result.MaxJsonLength = Int32.MaxValue;
        //            return result;
        //        }
        //    }
        //    else
        //    {
        //        var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //    }
        //    return new EmptyResult();
        //}
        //#endregion

        //#region DELETE
        //public ActionResult DeleteCompany(long COMPANY_ID = 0)
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        if (COMPANY_ID > 0)
        //        {
        //            using (var service = new CompanyService())
        //            {
        //                var deleteResult = service.DeleteCompany(COMPANY_ID);

        //                JsonResult result = Json(new
        //                {
        //                    statusCode = deleteResult ? Constant.SUCCESSFUL : Constant.INTERNAL_SERVER_ERROR
        //                }, JsonRequestBehavior.AllowGet);

        //                return result;
        //            }
        //        }
        //        else
        //        {
        //            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //        }
        //    }

        //    return new EmptyResult();
        //}

        //#endregion
    }
}