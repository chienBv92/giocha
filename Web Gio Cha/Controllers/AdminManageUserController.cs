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
    public class AdminManageUserController : BaseController
    {
        #region LIST
        [HttpGet]
        public ActionResult UserList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;
            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            UserModel model = new UserModel();

            CommonService comService = new CommonService();

            var tmpCondition = GetRestoreData() as UserModel;
            if (tmpCondition != null)
            {
                model = tmpCondition;
            }

            UserDa dataAccess = new UserDa();

            int HaNoiCity = 1;
            ManageDistrictDa daDistrict = new ManageDistrictDa();
            model.DISTRICT_LIST = daDistrict.getDistrictList(HaNoiCity).ToList().Select(
            f => new SelectListItem
            {
                Value = f.ID.ToString(),
                Text = f.Name
            }).ToList();
            model.DISTRICT_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "Chọn Quận/huyện" });

            return View(model);
        }

        [HttpPost]
        public ActionResult UserList(DataTableModel dt, UserModel condition)
        {
            if (ModelState.IsValid)
            {
                using (UserService service = new UserService())
                {
                    int total_row = 0;
                    var dataList = service.SearchUserList(dt, condition, out total_row);

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
                                    i.Email != null ? HttpUtility.HtmlEncode(i.Email) : String.Empty,
                                    i.UserName != null ? HttpUtility.HtmlEncode(i.UserName) : String.Empty,
                                    i.DistrictName != null ? HttpUtility.HtmlEncode(i.DistrictName) : String.Empty,
                                    i.Receive_Address != null ? HttpUtility.HtmlEncode(i.Receive_Address) : String.Empty,
                                    i.Phone != null ? HttpUtility.HtmlEncode(i.Phone) : String.Empty,
                                    i.CreatedDate != null ? i.CreatedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.Status == true? "Đã kích hoạt" : "Chưa kích hoạt",
                                    i.del_flg
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
        public ActionResult DeleteUser(long USER_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid && USER_ID > 0)
                {
                    using (var service = new UserService())
                    {
                        var deleteResult = service.DeleteUser(USER_ID);

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