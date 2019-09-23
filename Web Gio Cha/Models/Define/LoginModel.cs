using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Web_Gio_Cha.Models
{
    public class LoginModel
    {
        public int USER_ID { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string USER_EMAIL { get; set; }

        public string USER_NAME { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string USER_PASSWORD { get; set; }
    }
}