using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;

namespace Web_Gio_Cha.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        #region VIEW ACCOUNT
        public ActionResult ViewAccount()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }

            UserModel model = new UserModel();

            UserDa dataAccess = new UserDa();
            if (currentUser.ID > 0)
            {
                UserModel infor = new UserModel();
                infor = dataAccess.getInfoUser(currentUser.ID);

                model = infor != null ? infor : model;
            }

            return View(model);
        }
        #endregion

        #region REGISTER/ UPDATE
        public ActionResult UpdateUser()
        {
            UserModel model = new UserModel();
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }

            //CommonService comService = new CommonService();
            UserDa dataAccess = new UserDa();
            if (currentUser.ID > 0)
            {
                UserModel infor = new UserModel();
                infor = dataAccess.getInfoUser(currentUser.ID);

                model = infor != null ? infor : model;
            }
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
        [AllowAnonymous]
        public ActionResult Edit(UserModel model)
        {
            try
            {
                using (UserService service = new UserService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        long res = 0;
                        //SendMailService sendMailservice = new SendMailService();
                        using (var transaction = new TransactionScope())
                        {
                            try
                            {
                                if (model.ID == 0)
                                {
                                    isNew = true;

                                    res = service.InsertUser(model);
                                    if (res <= 0)
                                        transaction.Dispose();
                                    // send mail confirm account
                                    //res = sendMailRegisterAccount(model);
                                    transaction.Complete();
                                }
                                else
                                {
                                    isNew = false;
                                    res = service.UpdateUser(model);
                                    if (res <= 0)
                                        transaction.Dispose();
                                    transaction.Complete();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message, ex);
                            }
                            finally
                            {
                                transaction.Dispose();
                            }
                            JsonResult result = Json(new { isNew = isNew, res = res }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                    }
                    else
                    {
                        var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                        foreach (var mes in ErrorMessages)
                        {
                            ModelState.AddModelError(mes.Key, mes.Errors.ToString());
                        }
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

        // check exist user
        public ActionResult CheckExistUserEmail(string userEmail)
        {
            if (Request.IsAjaxRequest())
            {
                // Declare new DataAccess object
                UserDa dataAccess = new UserDa();

                var exist = dataAccess.CheckExistUserEmail(userEmail);
                JsonResult result = Json(new
                {
                    exist
                }, JsonRequestBehavior.AllowGet);

                return result;

            }
            return new EmptyResult();
        }
        #endregion

        #region CHANGE PASSWORD
        public ActionResult ChangePassword()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            UserModel model = new UserModel();

            UserDa dataAccess = new UserDa();
            if (currentUser.ID > 0)
            {
                UserModel infor = new UserModel();
                infor = dataAccess.getInfoUser(currentUser.ID);

                model = infor != null ? infor : model;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(UserModel model)
        {
            {
                try
                {
                    using (UserService service = new UserService())
                    {
                        if (ModelState.IsValid)
                        {
                            var res = service.UpdatePassword(model);
                            if (res > 0)
                            {
                                TempData.Clear();
                                FormsAuthentication.SignOut();
                                //base.RemoveCache("CmnEntityModel");
                                Session.Clear();
                            }
                            var success = res > 0 ? true : false;
                            JsonResult result = Json(new { success = success }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                            foreach (var mes in ErrorMessages)
                            {
                                ModelState.AddModelError(mes.Key, mes.Errors.ToString());
                            }
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
        }
        #endregion
    }
}