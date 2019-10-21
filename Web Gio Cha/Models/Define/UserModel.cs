using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Web_Gio_Cha.EF;
using System.Web.Mvc;
using Web_Gio_Cha.Resources;


namespace Web_Gio_Cha.Models
{
    public class UserModel: TblUser
    {
        public int USER_ID { get; set; }

        public string DistrictName { get; set; }

        public List<SelectListItem> DISTRICT_LIST { get; set; }

        public string NEW_PASSWORD { get; set; }
        public string NEW_PASSWORD_REPEAT { get; set; }

        public UserModel()
        {
            Status = Constant.Status.ACTIVE; ;
            del_flg = Constant.DeleteFlag.NON_DELETE;
        }
    }
}