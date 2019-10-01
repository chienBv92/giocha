using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Gio_Cha.Models.Define
{
    public class GioHang
    {
        public double TongSoLuong { get; set; }
        public decimal TongTien { get; set; }

        public IList<ItemGioHang> ItemGioHangList { get; set; }

    }
}