using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;
using Web_Gio_Cha.UtilityService;

namespace Web_Gio_Cha.Controllers
{
    public class CommonController : BaseController
    {
        // GET: Common
        [HttpGet]
        public ActionResult CheckTimeOut()
        {
            CommonService comService = new CommonService();
            //var POPUP_INFOR = comService.GetPopupInfor();
            if ((base.GetCache<CmnEntityModel>("CmnEntityModel") == null) || (base.GetCache<CmnEntityModel>("CmnEntityModel").ID == 0))
            {
                this.Response.StatusCode = Constant.TIME_OUT;
                //logger.Fatal(Messages.E044);
                return new EmptyResult();
            }

            JsonResult result = Json(
                "Success",
                JsonRequestBehavior.AllowGet
            );
            return result;
        }

        public ActionResult Error()
        {
            Exception ex = (Exception)System.Web.HttpContext.Current.Session["ERROR"];
            FormsAuthentication.SignOut();
            Session.Clear();
            if (ex != null)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Data = ex.Data;
                ViewBag.HelpLink = ex.HelpLink;
                ViewBag.HResult = ex.HResult;
                ViewBag.InnerException = ex.InnerException;
                ViewBag.Message = ex.Message;
                ViewBag.Source = ex.Source;
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.TargetSite = ex.TargetSite;
                ViewBag.CurrentTime = Utility.GetCurrentDateTime().ToString("yyyy/MM/dd HH:mm:ss");
                try
                {
                    var httpException = (System.Web.HttpException)ex;
                    var httpCode = httpException.GetHttpCode();
                    if (httpCode == Constant.NOT_FOUND)
                    {
                        return View("NotFound");
                    }
                }
                catch (Exception)
                {
                }

            }
            return View("Error");
        }
    }
}