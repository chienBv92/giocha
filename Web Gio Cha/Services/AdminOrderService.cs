using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Services
{
    public class AdminOrderService : BaseService
    {
        #region Search
        public IEnumerable<AdminOrderList> SearchOrderList(DataTableModel dt, AdminOrderList model, out int total_row)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();
            IEnumerable<AdminOrderList> results = dataAccess.SearchOrderList(dt, model, out total_row);
            return results;
        }

        public IEnumerable<AdminOrderList> SearchOrderListTotalNoneStatus(DataTableModel dt, AdminOrderList model)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();
            IEnumerable<AdminOrderList> results = dataAccess.SearchOrderListTotalNoneStatus(dt, model);
            return results;
        }
        #endregion
    }

}