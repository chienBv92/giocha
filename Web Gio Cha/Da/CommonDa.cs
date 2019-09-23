using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Da
{
    public class CommonDa : BaseDa
    {
        public IEnumerable<Product_Category> GetCategoryList()
        {
            var cate = da.Product_Category.Where(x => x.del_flg == Constant.DeleteFlag.NON_DELETE && x.Status == Constant.Status.ACTIVE).OrderBy(x => x.DisplayOrder);

            return cate;
        }
    }
}