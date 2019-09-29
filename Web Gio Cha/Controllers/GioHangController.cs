using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;

namespace Web_Gio_Cha.Controllers
{
    public class GioHangController : Controller
    {
        // xây dựng giỏ hàng partial
        public ActionResult GioHangPartial()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        // phương thức tính tiền
        public decimal TongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.TienHang);
        }

        // thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult ThemGioHang(long IdSanPham, string strUrl)
        {
            ProductDa da = new ProductDa();
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Product sanpham = da.getProductByID(IdSanPham);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng 
            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đã tồn tại một sản phẩm trong giỏ hàng
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanphamcheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                //if (sanpham.SoLuongTon < sanphamcheck.SoLuong)
                //{
                //    return View("ThongBao");
                //}
                sanphamcheck.SoLuong++;
                sanphamcheck.TienHang = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                return Redirect(strUrl);
            }
            ItemGioHang itemGH = new ItemGioHang(IdSanPham);
            //if (sanpham.SoLuongTon < itemGH.SoLuong)
            //{
            //    return View("ThongBao");
            //}
            lstGioHang.Add(itemGH);

            Session["GioHang"] = lstGioHang;
            //db.SaveChanges();
            return Redirect(strUrl);
        }

        public List<ItemGioHang> LayGioHang()
        {
            // giỏ hàng đả tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                // nếu session giỏ hàng đã tồn tại
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }

        public ActionResult ThemGioHangAjax(long Id = 0)
        {
            ProductDa da = new ProductDa();
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Product sanpham = da.getProductByID(Id);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng 
            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đã tồn tại một sản phẩm trong giỏ hàng
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == Id);
            if (sanphamcheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                //if (sanpham.SoLuongTon < sanphamcheck.SoLuong)
                //{
                //    return View("ThongBao");
                //}
                sanphamcheck.SoLuong++;
                sanphamcheck.TienHang = sanphamcheck.SoLuong * sanphamcheck.DonGia;
            }
            else
            {
                ItemGioHang itemGH = new ItemGioHang(Id);
                //if (sanpham.SoLuongTon < itemGH.SoLuong)
                //{
                //    return View("ThongBao");
                //}
                lstGioHang.Add(itemGH);
            }
            
            Session["GioHang"] = lstGioHang;
            double TongSoLuong = TinhTongSoLuong();
            decimal TotalMoney = TongTien();

            JsonResult result = Json(new { TongSoLuong = TongSoLuong, TotalMoney = TotalMoney }, JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}