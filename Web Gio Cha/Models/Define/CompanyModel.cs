using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;

namespace Web_Gio_Cha.Models.Define
{
    public class CompanyModel : TblCompany
    {
        public long COMPANY_ID_HIDDEN { get; set; }
    }
}