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
    
    public partial class TblOrderHistory
    {
        public long HISTORY_ID { get; set; }
        public Nullable<long> ORDER_ID { get; set; }
        public Nullable<int> DSP_ORDER { get; set; }
        public Nullable<int> STATUS_HISTORY { get; set; }
        public Nullable<System.DateTime> DATE_HISTORY { get; set; }
        public Nullable<System.DateTime> USER_HISTORY { get; set; }
    }
}