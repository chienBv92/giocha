using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;

namespace Web_Gio_Cha.Models.Define
{
    public class ItemGioHang
    {
        public long IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal TienHang { get; set; }
        public string HinhAnh { get; set; }

        public ItemGioHang(long IdSanPham)
        {
            ProductDa db = new ProductDa();
            this.IdSanPham = IdSanPham;
            Product sanpham = db.getProductByID(IdSanPham);
            this.TenSanPham = sanpham.Name;
            this.HinhAnh = sanpham.Image;
            this.DonGia = sanpham.Price.Value;
            this.SoLuong = 1;
            this.TienHang = DonGia * SoLuong;
        }

        public ItemGioHang(long IdSanPham, int SL)
        {
            ProductDa db = new ProductDa();
            this.IdSanPham = IdSanPham;
            Product sanpham = db.getProductByID(IdSanPham);
            this.TenSanPham = sanpham.Name;
            this.HinhAnh = sanpham.Image;
            this.DonGia = sanpham.Price.Value;
            this.SoLuong = SL;
            this.TienHang = DonGia * SoLuong;
        }
    }
}