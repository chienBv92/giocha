﻿using System;
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
        public ActionResult MenuContentEdit(long menuId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            TblMenuContent model = new TblMenuContent();
            CommonService comService = new CommonService();
            ManageMenuDa dataAccess = new ManageMenuDa();
            model.Status = Constant.Status.ACTIVE;

            if (menuId > 0)
            {
                TblMenuContent infor = new TblMenuContent();
                infor = dataAccess.getInfoMenuContent(menuId);
                model = infor != null ? infor : model;
                model.Menu_Content = HttpUtility.HtmlDecode(model.Menu_Content);
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

        public ActionResult CheckExistMenuCode(int MenuCd)
        {
            if (Request.IsAjaxRequest())
            {
                // Declare new DataAccess object
                ManageMenuDa dataAccess = new ManageMenuDa();

                var exist = dataAccess.CheckExistMenuCode(MenuCd);
                JsonResult result = Json(new
                {
                    exist
                }, JsonRequestBehavior.AllowGet);

                return result;

            }
            return new EmptyResult();
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

            TblMenuContent model = new TblMenuContent();

            CommonService comService = new CommonService();
            ManageMenuDa dataAccess = new ManageMenuDa();

            var tmpCondition = GetRestoreData() as TblMenuContent;
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
        public ActionResult DeleteMenu(long MENU_ID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                if (MENU_ID > 0)
                {
                    using (var service = new MenuService())
                    {
                        var deleteResult = service.DeleteMenu(MENU_ID);

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