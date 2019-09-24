using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Models.Define
{
    public class NewsModel: TblNews
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CreateName { get; set; }
        public string ModifiedName { get; set; }

        public NewsModel()
        {
             Status = Constant.Status.ACTIVE;
             del_flg = Constant.DeleteFlag.NON_DELETE;
        }
    }
}