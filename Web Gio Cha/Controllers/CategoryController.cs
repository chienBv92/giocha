using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Services;

namespace Web_Gio_Cha.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CategoryProductPartial()
        {
            ProductCategoryService service = new ProductCategoryService();
            int maxItem = 10;
            IEnumerable<Product_Category> model = service.GetListCategory(maxItem);
            return PartialView(model);
        }

        public ActionResult GetProductByCategory(long categoryId = 0)
        {
            ProductCategoryService service = new ProductCategoryService();
            ProductCategoryDa da = new ProductCategoryDa();
            int maxItem = 10;
            ViewBag.CategoryName = da.getCategoryName(categoryId);
            IEnumerable<Product> model = service.GetListProductByCategory(categoryId);
            return View(model);
        }
    }
}