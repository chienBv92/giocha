using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Models
{
    public class ViewOrderModel: Order
    {
        public string ORDER_STATUS_TEXT { get; set; }
        public string Product_Name { get; set; }
        public string UserName { get; set; }
        public string DistrictName { get; set; }
        public string AddressCustomer { get; set; }

        public string CREATE_DATE_STRING { get; set; }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }

        public string ProductDetailTotal { get; set; }
    }
}