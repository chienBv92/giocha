using Web_Gio_Cha.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.Models;

namespace Web_Gio_Cha.Services
{
    public class BaseService: IDisposable
    {
        private CmnEntityModel sessionModel = null;

        public CmnEntityModel SessionModel
        {
            get
            {
                if (sessionModel == null)
                {
                    if (HttpContext.Current.Session["USER_SESSION"] == null)
                    {
                        HttpContext.Current.Session["USER_SESSION"] = new CmnEntityModel();
                    }
                    sessionModel = (CmnEntityModel)HttpContext.Current.Session["USER_SESSION"];
                }
                return sessionModel;
            }
        }

        public void Dispose()
        {

        }
    }
}