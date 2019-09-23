using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Models.Define
{
    public class ProductModel: Product
    {
        public string CATEGORY_NAME { get; set; }
        public List<SelectListItem> CATEGORY_LIST { get; set; }

        public string CreateName { get; set; }
        public string ModifiedName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public ProductModel()
        {
            PriceBefore = 0;
            Price = 0;
            Discount = 0;
            Status = Constant.Status.ACTIVE; ;
            Is_Hot = true;
            del_flg = Constant.DeleteFlag.NON_DELETE;
        }
    }
}