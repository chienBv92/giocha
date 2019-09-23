using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;

namespace Web_Gio_Cha.Services
{
    public class CommonService : BaseService
    {
        public IEnumerable<Product_Category> GetCategoryList()
        {
            // Declare new DataAccess object
            CommonDa dataAccess = new CommonDa();
            IEnumerable<Product_Category> results;

            results = dataAccess.GetCategoryList();

            return results;
        }
    }
}