using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Models;

namespace Web_Gio_Cha.Controllers
{
    public class ManageUserController : BaseController
    {
        // GET: User
        //#region LIST
        //[HttpGet]
        //public ActionResult UserList()
        //{
        //    CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

        //    if (currentUser == null || currentUser.IsAdmin == false)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }

        //    UserAccountModel model = new UserAccountModel();

        //    CommonService comService = new CommonService();
        //    ManageUserDa dataAccess = new ManageUserDa();

        //    var tmpCondition = GetRestoreData() as UserAccountModel;
        //    if (tmpCondition != null)
        //    {
        //        model = tmpCondition;
        //    }
        //    model.CITY_LIST = comService.GetCityList().ToList().Select(
        //    f => new SelectListItem
        //    {
        //        Value = f.CITY_CD.ToString(),
        //        Text = f.CITY_NAME
        //    }).ToList();
        //    model.CITY_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "" });

        //    model.DISTRICT_LIST = comService.GetDistrictList().ToList().Select(
        //    f => new SelectListItem
        //    {
        //        Value = f.CITY_CD.ToString() + "_" + f.DISTRICT_CD.ToString(),
        //        Text = f.DISTRICT_NAME
        //    }).ToList();

        //    model.TOWN_LIST = comService.GetTownList().ToList().Select(
        //    f => new SelectListItem
        //    {
        //        Value = f.CITY_CD.ToString() + "_" + f.DISTRICT_CD.ToString() + "_" + f.TOWN_CD.ToString(),
        //        Text = f.TOWN_NAME
        //    }).ToList();

        //    model.HAS_CONDITION = false;
        //    ViewBag.UserAuthority = new SelectList(UtilityServices.UtilityServices.GetAuthorityUser(), "Value", "Text");

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult UserList(DataTableModel dt, UserAccountModel condition)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (ManageUserService service = new ManageUserService())
        //        {
        //            int total_row = 0;
        //            var dataList = service.SearchUserList(dt, condition, out total_row);

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
        //                            i.USER_ID,
        //                            order++,
        //                            i.USER_EMAIL != null ? HttpUtility.HtmlEncode(i.USER_EMAIL) : String.Empty,
        //                            i.USER_NAME != null ? HttpUtility.HtmlEncode(i.USER_NAME) : String.Empty,
        //                            i.CITY_NAME != null ? HttpUtility.HtmlEncode(i.CITY_NAME) : String.Empty,
        //                            i.DISTRICT_NAME != null ? HttpUtility.HtmlEncode(i.DISTRICT_NAME) : String.Empty,
        //                            i.TOWN_NAME != null ? HttpUtility.HtmlEncode(i.TOWN_NAME) : String.Empty,
        //                            i.USER_ADDRESS != null ? HttpUtility.HtmlEncode(i.USER_ADDRESS) : String.Empty,
        //                            i.USER_PHONE != null ? HttpUtility.HtmlEncode(i.USER_PHONE) : String.Empty,
        //                            i.INS_DATE != null ? i.INS_DATE.Value.ToString("dd/MM/yyyy") : String.Empty,
        //                            i.STATUS =="1"? "Đã kích hoạt" : "Chưa kích hoạt",
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

    }
}