using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.Services;
using Web_Gio_Cha.UtilityServices.SafePassword;
using System.Web.Security;

namespace Web_Gio_Cha.Controllers
{
    public class LoginController : Controller
    {
        #region LOGIN
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName)
        {
            try
            {
                LoginModel model = new LoginModel();
                TryUpdateModel(model);

                if (ModelState.IsValid)
                {
                    UserDa da = new UserDa();
                    model.USER_PASSWORD = SafePassword.GetSaltedPassword(model.USER_PASSWORD);
                    var exist = da.Login(model.USER_EMAIL, model.USER_PASSWORD);
                    if (exist)
                    {
                        CmnEntityModel session = new CmnEntityModel();
                        var user = da.getUserByEmail(model.USER_EMAIL);
                        session.UserName = user.UserName;
                        session.Email = user.Email;
                        session.ID = user.ID;
                        session.IsAdmin = user.IsAdmin;
                        session.Phone = user.Phone;
                        session.Status = user.Status;
                        Session.Add("CmnEntityModel", session);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email hoặc mật khẩu không đúng!");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                System.Web.HttpContext.Current.Session["ERROR"] = ex;
                return new EmptyResult();
            }
        }

        #endregion

        #region LOGOUT
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            TempData.Clear();
            FormsAuthentication.SignOut();
            //base.RemoveCache("CmnEntityModel");
            Session.Clear();

            return this.RedirectToAction("Index", "Home");
        }

        #endregion

        #region REGISTER/ UPDATE
        public ActionResult Register(long UserId = 0)
        {
            UserModel model = new UserModel();

            //CommonService comService = new CommonService();
            UserDa dataAccess = new UserDa();
            if (UserId > 0)
            {
                UserModel infor = new UserModel();
                infor = dataAccess.getInfoUser(UserId);

                model = infor != null ? infor : model;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserModel model)
        {
            try
            {
                using (UserService service = new UserService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        //SendMailService sendMailservice = new SendMailService();
                        if (model.ID == 0)
                        {
                            isNew = true;

                            var res = service.InsertUser(model);
                            // send mail confirm account
                            //sendMailRegisterAccount(model);

                            JsonResult result = Json(new { isNew = isNew, res =  res}, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;

                            var res = service.UpdateUser(model);
                            JsonResult result = Json(new { isNew = isNew, res =  res }, JsonRequestBehavior.AllowGet);
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

    }
}