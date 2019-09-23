using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;

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
    }
}