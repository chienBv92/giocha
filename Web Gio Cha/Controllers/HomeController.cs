using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Services;

namespace Web_Gio_Cha.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                using (ProductService service = new ProductService())
                {
                    var dataList = service.SearchProductList();
                    return View(dataList);
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}