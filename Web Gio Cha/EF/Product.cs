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
    
    public partial class Product
    {
        public long ID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string MoreImages { get; set; }
        public Nullable<decimal> PriceBefore { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Promotion { get; set; }
        public Nullable<bool> IncludedVAT { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string NoteDelivery { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Is_Hot { get; set; }
        public Nullable<System.DateTime> TopHot { get; set; }
        public string Unit { get; set; }
        public string del_flg { get; set; }
    }
}
