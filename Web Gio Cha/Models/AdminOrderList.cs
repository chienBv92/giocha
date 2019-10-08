using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Models
{
    public class AdminOrderList: Order
    {
        public string ORDER_STATUS_TEXT { get; set; }
        public string Product_Name { get; set; }
        public string UserName { get; set; }
        public string DistrictName { get; set; }

        public string CREATE_DATE_STRING { get; set; }

        public List<SelectListItem> DISTRICT_LIST { get; set; }

        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }

        public string SORT_COL { get; set; }
        public string SORT_TYPE { get; set; }
        public IList<SelectListItem> SORT_TYPE_LIST { get; set; }

        public string ORDER_STATUS_LIST { get; set; }

        public long? STATUS_SELECT { get; set; }
        public IList<SelectListItem> STATUS_SELECT_LIST { get; set; }
        public long TotalStatus_0 { get; set; }
        public long TotalStatus_1 { get; set; }
        public long TotalStatus_2 { get; set; }
        public long TotalStatus_3 { get; set; }
        public long TotalStatus_4 { get; set; }
        public long TotalStatus_5 { get; set; }
        public long TotalStatus_6 { get; set; }
        public long TotalStatus_7 { get; set; }
        public long TotalStatus_8 { get; set; }
        public long TotalStatus_9 { get; set; }
        public long TotalStatus_10 { get; set; }
        public long TotalStatusAll { get; set; }
        public long TotalOrderForMonth { get; set; }
        public int thisMonth { set; get; }
        public int TotalOrderthisMonth { set; get; }

        public int OldStatus { get; set; }

        public AdminOrderList()
        {
            SORT_TYPE = SortType.ASC;
        }
    }
}