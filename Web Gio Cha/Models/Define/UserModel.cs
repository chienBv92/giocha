using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Web_Gio_Cha.EF;


namespace Web_Gio_Cha.Models
{
    public class UserModel: User
    {
        public int USER_ID { get; set; }
    }
}