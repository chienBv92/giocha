using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.EF;

namespace Web_Gio_Cha.Models.Define
{
    public class CityModel:TblCity
    {
        public int CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public int DISTRICT_ID { get; set; }
        public string DISTRICT_NAME { get; set; }
        public int DISTRICT_ID_HIDDEN { get; set; }

        public List<SelectListItem> CITY_LIST { get; set; }

        public List<SelectListItem> DISTRICT_LIST { get; set; }
    }
}