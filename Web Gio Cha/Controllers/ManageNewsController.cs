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
    public class ManageNewsController : BaseController
    {
        // GET: ManageNews
        #region REGISTER/ UPDATE
        public ActionResult NewsEdit(long newsId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblNews model = new TblNews();
            CommonService comService = new CommonService();
            ManageNewsDa dataAccess = new ManageNewsDa();
            model.Status = Constant.Status.ACTIVE;

            if (newsId > 0)
            {
                TblNews infor = new TblNews();
                infor = dataAccess.getInfoNews(newsId);
                model = infor != null ? infor : model;
                model.Content = HttpUtility.HtmlDecode(model.Content);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblNews model)
        {
            try
            {
                using (NewsService service = new NewsService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;

                        if (model.ID == 0)
                        {
                            isNew = true;
                            service.InsertNews(model);
                            JsonResult result = Json(new { isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;
                            service.UpdateNews(model);
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
        public ActionResult NewsList()
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
        public ActionResult NewsList(DataTableModel dt, NewsModel condition)
        {
            if (ModelState.IsValid)
            {
                using (NewsService service = new NewsService())
                {
                    int total_row = 0;
                    var dataList = service.SearchNewsList(dt, condition, out total_row);

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
                                    HttpUtility.HtmlEncode(i.Name),
                                    i.Status == true? "Hiển thị" : "Ẩn",
                                    i.CreatedDate != null ? i.CreatedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.CreateName,
                                    i.ModifiedDate != null ? i.ModifiedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.ModifiedName,
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
        public ActionResult DeleteNews(long NEWS_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (NEWS_ID > 0)
                {
                    using (var service = new NewsService())
                    {
                        var deleteResult = service.DeleteNews(NEWS_ID);

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