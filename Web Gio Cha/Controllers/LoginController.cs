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
using System.Transactions;

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
                                    res = sendMailRegisterAccount(model);
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

        #region SEND MAIL
        // send email confirm account
        public int sendMailRegisterAccount(UserModel model)
        {
            SendMailService sendMailservice = new SendMailService();
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Common/Sendmail.cshtml"));

            content = content.Replace("{{UserEmail}}", model.Email);
            content = content.Replace("{{UserName}}", model.UserName);
            content = content.Replace("{{Phone}}", model.Phone);
            string subject = "Xác nhận tài khoản mới";
            var callbackUrl = Url.Action("ConfirmEmail", "Login", new { UserEmail = model.Email }, protocol: Request.Url.Scheme);
            content = content.Replace("{{callback}}", callbackUrl);
            //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            //sendMailservice.SendMail(toEmail, subject, content);
            var res = sendMailservice.SendMail(model.Email, subject, content); // Gửi email đến tài khoản đăng kí
            return res;
        }

        // send mail reset password
        public int sendMailResetPassword(string UserEmail)
        {
            SendMailService sendMailservice = new SendMailService();
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Common/ResetPassword.cshtml"));

            content = content.Replace("{{UserEmail}}", UserEmail);
            string subject = "Xác nhận thiết lập mật khẩu";
            var callbackUrl = Url.Action("ConfirmResetPassword", "Login", new { UserEmail = UserEmail }, protocol: Request.Url.Scheme);
            content = content.Replace("{{callback}}", callbackUrl);

            var res = sendMailservice.SendMail(UserEmail, subject, content); // Gửi email đến tài khoản đăng kí
            return res;
        }

        // check exist user
        public ActionResult ConfirmEmail(string UserEmail)
        {
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();

            var user = dataAccess.getUserByEmail(UserEmail);
            if (user != null)
            {
                long suscess = dataAccess.ConfirmEmail(user);
                if (suscess > 0)
                {
                    ViewBag.confirmEmailSuccess = "Xác nhận Email thành công! Vui lòng đăng nhập!";
                    return this.RedirectToAction("Login", "Login");
                }
            }

            return new EmptyResult();
        }

        // check exist user
        public ActionResult ConfirmResetPassword(string UserEmail)
        {
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();

            var user = dataAccess.getUserByEmail(UserEmail);
            if (user != null)
            {
                var suscess = dataAccess.ConfirmEmail(user);
                if (suscess > 0)
                {
                    ViewBag.confirmPasswordSuccess = "Mật khẩu đã được thay đổi! Vui lòng đăng nhập!";
                    return this.RedirectToAction("Login", "Login");
                }
            }

            return new EmptyResult();
        }
        #endregion

        #region PASSWORD
        [HttpGet]
        public ActionResult ResetPassword()
        {
            UserModel model = new UserModel();

            return View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ResetPassword(UserModel model)
        {
            if (ModelState.IsValid)
            {
                using (UserService service = new UserService())
                {
                    UserDa dataAccess = new UserDa();

                    var user = dataAccess.getUserByEmail(model.Email);
                    if (user != null)
                    {
                        var suscess = dataAccess.ReSetPassword(user);
                        suscess = sendMailResetPassword(model.Email);
                        if (suscess > 0)
                        {
                            ViewBag.sendMailSuccess = "Yêu cầu reset mật khẩu của bạn đã được gửi tới email:" + model.Email;
                        }
                        else
                        {
                            ModelState.AddModelError("ResetPass", "Có lỗi xảy ra, vui lòng thực hiện lại!");
                            ViewBag.sendMailError = "Có lỗi xảy ra, vui lòng thực hiện lại!";
                        }
                        return this.View();
                    }
                }
            }
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            }

            return View();
        }
        #endregion

    }
}