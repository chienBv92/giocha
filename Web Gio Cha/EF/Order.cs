//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Gio_Cha.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public long ID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> PriceTotal { get; set; }
        public Nullable<decimal> PriceDiscountTotal { get; set; }
        public Nullable<decimal> PriceShip { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public Nullable<bool> Paid { get; set; }
        public string ReasonCancel { get; set; }
        public string Note { get; set; }
        public Nullable<int> Receive_City { get; set; }
        public Nullable<int> Receive_District { get; set; }
        public Nullable<int> Receive_Town { get; set; }
        public string Receive_Address { get; set; }
        public string Receive_Phone { get; set; }
        public string del_flg { get; set; }
        public string Code { get; set; }
        public string Success { get; set; }
    }
}
