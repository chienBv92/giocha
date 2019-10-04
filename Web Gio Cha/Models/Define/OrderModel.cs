using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Gio_Cha.Models.Define
{
    public class OrderModel
    {
        public double TongSoLuong { get; set; }
        public decimal TongTienHang { get; set; }

        //public IList<ItemGioHang> ItemGioHangList { get; set; }

        public long UserID { get; set; }
        public decimal PriceShip { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal PriceDiscountTotal { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public int Receive_District { get; set; }
        public List<SelectListItem> DISTRICT_LIST { get; set; }
        public string Receive_Address { get; set; }
        public string Receive_Phone { get; set; }
        public int METHOD_TYPE { get; set; }
        public string Note { get; set; }


    }
}