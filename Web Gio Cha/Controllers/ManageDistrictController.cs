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

namespace Web_Gio_Cha.Controllers
{
    public class ManageDistrictController : Controller
    {
        // GET: ManageDistrict
        #region REGISTER/ UPDATE
        public ActionResult DistrictEdit(int CityCd = 0, int DistrictCd = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            CityModel model = new CityModel();

            CommonService comService = new CommonService();
            ManageDistrictDa dataAccess = new ManageDistrictDa();
            if (DistrictCd > 0)
            {
                var info = dataAccess.getCityByID(DistrictCd);
                if (info != null)
                {
                    model = (CityModel)info;
                }
            }

            model.CITY_LIST = dataAccess.getCityList(Constant.CityLevel.City).ToList().Select(
            f => new SelectListItem
            {
                Value = f.ID.ToString(),
                Text = f.Name
            }).ToList();
            model.CITY_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "" });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CityModel model)
        {
            try
            {
                using (ManageDistrictService service = new ManageDistrictService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        TblCity entity = new TblCity();

                        if (model.DISTRICT_ID_HIDDEN == 0)
                        {
                            isNew = true;

                            service.InsertCity(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;

                            //service.UpdateDistrict(model);
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
        //public ActionResult CheckExistDistrictCd(int cityCd, int districtCd, int districtCdOld)
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        // Declare new DataAccess object
        //        ManageDistrictDa dataAccess = new ManageDistrictDa();

        //        var exist = dataAccess.CheckExistDistrictCd(cityCd, districtCd, districtCdOld);
        //        JsonResult result = Json(new
        //        {
        //            exist
        //        }, JsonRequestBehavior.AllowGet);

        //        return result;

        //    }
        //    return new EmptyResult();
        //}
        #endregion
    }
}