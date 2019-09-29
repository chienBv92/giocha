using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ViewProductDetail(long id = 0)
        {
            CommonService comService = new CommonService();
            ProductService service = new ProductService();

            Product model = service.getProductDetail(id);
            ViewBag.ProductName = model != null ? model.Name.ToUpper() : "";
               
            return View(model);
        }
    }
}