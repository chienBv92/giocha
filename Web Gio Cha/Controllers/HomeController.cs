using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;
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
                    SliderDa dataAccess = new SliderDa();

                    var listSlideTop = dataAccess.getListSlideView(Constant.SlideType.Top);
                    ViewBag.listSlideTop = listSlideTop;
                    return View(dataList);
                }
            }
            return View();
        }

        public ActionResult Intro(int MenuCd = MenuCode.Intro)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();
            TblMenuContent model = new TblMenuContent();
            model = dataAccess.getMenuContentDisplay(MenuCd);

            return View(model);
        }

        public ActionResult Help(int MenuCd = MenuCode.Help)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();
            TblMenuContent model = new TblMenuContent();
            model = dataAccess.getMenuContentDisplay(MenuCd);

            return View(model);
        }

        public ActionResult About(int MenuCd = 4)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();
            TblMenuContent model = new TblMenuContent();
            model = dataAccess.getMenuContentDisplay(MenuCd);

            return View(model);
        }

        public ActionResult Contact(int MenuCd = MenuCode.Contact)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();
            TblMenuContent model = new TblMenuContent();
            model = dataAccess.getMenuContentDisplay(MenuCd);

            return View(model);
        }

        public ActionResult RenderFooter()
        {
            SliderDa dataAccess = new SliderDa();
            string myCompany = "00000";

            var infoCompany = dataAccess.getInfoCompany(myCompany);

            return PartialView(infoCompany);
        }
    }
}