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
    public class ManageMenuContentController : BaseController
    {
        // GET: ManageMenuContent
        #region REGISTER/ UPDATE
        public ActionResult MenuContentEdit(long newsId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblMenuContent model = new TblMenuContent();
            CommonService comService = new CommonService();
            MenuContentDa dataAccess = new MenuContentDa();
            model.Status = Constant.Status.ACTIVE;

            if (newsId > 0)
            {
                TblMenuContent infor = new TblMenuContent();
                //infor = dataAccess.getInfoNews(newsId);
                //model = infor != null ? infor : model;
                //model.Content = HttpUtility.HtmlDecode(model.Content);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblMenuContent model)
        {
            try
            {
                using (MenuService service = new MenuService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;

                        if (model.ID == 0)
                        {
                            isNew = true;
                            service.InsertMenu(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;
                            service.UpdateMenu(model);
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
        [HttpGet]
        public ActionResult MenuList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            NewsModel model = new NewsModel();

            CommonService comService = new CommonService();
            ManageNewsDa dataAccess = new ManageNewsDa();

            var tmpCondition = GetRestoreData() as NewsModel;
            if (tmpCondition != null)
            {
                model = tmpCondition;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataTableModel dt, TblMenuContent condition)
        {
            if (ModelState.IsValid)
            {
                using (MenuService service = new MenuService())
                {
                    int total_row = 0;
                    var dataList = service.SearchMenuList(dt, condition, out total_row);

                    int order = 1;
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
                                    MenuCode.Items[i.MenuCd].ToString(),
                                    i.Status == true? "Hiển thị" : "Ẩn",
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
        public ActionResult DeleteMenu(long NEWS_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (NEWS_ID > 0)
                {
                    using (var service = new MenuService())
                    {
                        var deleteResult = service.DeleteMenu(NEWS_ID);

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