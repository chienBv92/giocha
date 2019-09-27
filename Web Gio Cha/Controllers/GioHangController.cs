using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Gio_Cha.Controllers
{
    public class GioHangController : Controller
    {
        // xây dựng giỏ hàng partial
        public ActionResult GioHangPartial()
        {
            //if (TinhTongSoLuong() == 0)
            //{
            //    ViewBag.TongSoLuong = 0;
            //    ViewBag.TongTien = 0;
            //    return PartialView();
            //}
            //ViewBag.TongSoLuong = TinhTongSoLuong();
            //ViewBag.TongTien = TongTien();
            return PartialView();
        }
    }
}