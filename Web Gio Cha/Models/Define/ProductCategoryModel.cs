using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Models.Define
{
    public class ProductCategoryModel: Product_Category
    {
        public long CATEGORY_ID_HIDDEN { get; set; }
        public ProductCategoryModel()
        {
            Status = Constant.Status.ACTIVE;
            del_flg = Constant.DeleteFlag.NON_DELETE;
        }

        public string CreateName { get; set; }
        public string ModifiedName { get; set; }
    }
}