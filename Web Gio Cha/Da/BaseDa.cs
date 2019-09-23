using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;

namespace Web_Gio_Cha.Da
{
    public class BaseDa
    {
        private CmnEntityModel cmnEntityModel = null;

        public GioChaEntities da = null;
        public BaseDa()
        {
            da = new GioChaEntities();
        }

        public CmnEntityModel CmnEntityModel
        {
            get
            {
                if (cmnEntityModel == null)
                {
                    if (HttpContext.Current.Session["CmnEntityModel"] == null)
                    {
                        HttpContext.Current.Session["CmnEntityModel"] = new CmnEntityModel();
                    }
                    cmnEntityModel = (CmnEntityModel)HttpContext.Current.Session["CmnEntityModel"];
                }
                return cmnEntityModel;
            }
        }
    }
}