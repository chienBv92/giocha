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

        public ActionResult GioHangPartialTop()
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
            return lstGioHang.Count();
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
                if (sanpham.Quantity < sanphamcheck.SoLuong + 1)
                {
                    return View("ThongBao");
                    //return Content("<script>alert(\"Sản phẩm đã hết hàng!\")</script>");
                }
                sanphamcheck.SoLuong++;
                sanphamcheck.TienHang = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                return Redirect(strUrl);
            }
            ItemGioHang itemGH = new ItemGioHang(IdSanPham);
            if (sanpham.Quantity < itemGH.SoLuong)
            {
                return View("ThongBao");
                //return Content("<script>alert(\"Sản phẩm đã hết hàng!\")</script>");
            }
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
            int response = 0;
            if (sanphamcheck != null)
            {
                 //kiểm tra số lượng sản phẩm tồn
                if (sanpham.Quantity < sanphamcheck.SoLuong + 1)
                {
                    response = -1;
                }
                else
                {
                    sanphamcheck.SoLuong++;
                    sanphamcheck.TienHang = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                    response = 1;
                }
            }
            else
            {
                ItemGioHang itemGH = new ItemGioHang(Id);
                if (sanpham.Quantity < itemGH.SoLuong)
                {
                    response = -1;
                }
                else
                {
                    response = 1;
                    lstGioHang.Add(itemGH);
                }
            }
            
            Session["GioHang"] = lstGioHang;
            double TongSoLuong = TinhTongSoLuong();
            decimal TotalMoney = TongTien();

            JsonResult result = Json(new { TongSoLuong = TongSoLuong, TotalMoney = TotalMoney, response = response }, JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult SaveSessionCart(long Id = 0, int qty = 0)
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
            int response = 0;

            if (sanphamcheck != null)
            {
                //kiểm tra số lượng sản phẩm tồn
                if (sanpham.Quantity < qty)
                {
                    response = -1;
                }
                else
                {
                    sanphamcheck.SoLuong = qty;
                    sanphamcheck.TienHang = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                    response = 1;
                }
            }

            Session["GioHang"] = lstGioHang;
            double TongSoLuong = TinhTongSoLuong();
            decimal TotalMoney = TongTien();

            JsonResult result = Json(new { TongSoLuong = TongSoLuong, TotalMoney = TotalMoney, response = response }, JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult XoaGioHang(long Id, string strUrl)
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
                lstGioHang.Remove(sanphamcheck);
            }

            Session["GioHang"] = lstGioHang;
            double TongSoLuong = TinhTongSoLuong();
            decimal TotalMoney = TongTien();

            return Redirect(strUrl);
        }

        #region VIEW
        // Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(lstGioHang);
        }
        #endregion

        #region Order by cart
        // Xem giỏ hàng
        [HttpGet]
        public ActionResult DatHang()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;
            OrderModel model = new OrderModel();
            ManageDistrictDa dataAccess = new ManageDistrictDa();

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int HaNoiCity = 1;
                model.DISTRICT_LIST = dataAccess.getDistrictList(HaNoiCity).ToList().Select(
                f => new SelectListItem
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
                model.DISTRICT_LIST.Insert(0, new SelectListItem { Value = Constant.DEFAULT_VALUE, Text = "Chọn Quận/huyện" });

                model.UserID = currentUser.ID;
                model.UserName = currentUser.UserName;
                model.Receive_District = currentUser.USER_DISTRICT;
                model.Receive_Address = currentUser.USER_ADDRESS;
                model.Email = currentUser.Email;
                model.Receive_Phone = currentUser.Phone;

                List<ItemGioHang> lstGioHang = LayGioHang();
                ViewBag.lstGioHangItem = lstGioHang;

                model.TongSoLuong = TinhTongSoLuong();
                model.TongTienHang = TongTien();
                if (model.Receive_District > 0)
                {
                    var getDistrict = dataAccess.getCityByID(model.Receive_District);
                    model.PriceShip = getDistrict != null ? (int)getDistrict.PriceShip.Value : 0;
                }
                ManagePromotionDa promotionDa = new ManagePromotionDa();

                var getPromotion = promotionDa.getPromotionForDiscount(model.TongTienHang);
                if (getPromotion != null)
                {
                    var discount = getPromotion.Discount.HasValue ? getPromotion.Discount.Value : 0;
                    model.PriceDiscountTotal = (int)(model.TongTienHang * discount / 100);
                }
                model.PriceTotal = model.TongTienHang + model.PriceShip - model.PriceDiscountTotal;
            }

            return View(model);
        }
        #endregion

        #region REGISTER/ UPDATE
        [HttpPost]
        public ActionResult CartSubmit(OrderModel model)
        {
            try
            {
                using (OrderCartService service = new OrderCartService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        List<ItemGioHang> lstGioHang = LayGioHang();
                        if (lstGioHang.Count() > 0 && model.UserID > 0)
                        {
                            isNew = true;
                            var orderId = service.InsertOrder(model, lstGioHang);
                            lstGioHang = new List<ItemGioHang>();
                            Session["GioHang"] = lstGioHang;

                            JsonResult result = Json(new { orderId = orderId, isNew = isNew }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            ModelState.AddModelError("error", "Thông tin đăng kí không đúng!");
                        }
                    }
                    else
                    {
                        var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    }

                    return new EmptyResult();

                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                System.Web.HttpContext.Current.Session["ERROR"] = ex;
                return new EmptyResult();

            }
        }
        #endregion
    }
}