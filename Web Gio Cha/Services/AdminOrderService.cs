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

        #region UPDATE STATUS AND PAYMENT
        public bool UpdatePayment(long OrderId)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.UpdatePayment(OrderId);

                    if (result > 0)
                        transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    result = -1;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return (result > 0);
        }

        public bool UpdateStatusOrder(AdminOrderList model)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.UpdateStatusOrder(model);

                    if (result > 0)
                        transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    result = -1;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return (result > 0);
        }

        public bool UpdateNoteOrder(AdminOrderList model)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.UpdateNoteOrder(model);

                    if (result > 0)
                        transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    result = -1;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return (result > 0);
        }
        #endregion

        #region CANCEL
        public bool UpdateCancelOrder(AdminOrderList model)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.UpdateCancelOrder(model);

                    if (result > 0)
                        transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    result = -1;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return (result > 0);
        }

        #endregion

        #region UPDATE INFO ORDER
        public bool UpdateInfo(AdminOrderList model)
        {
            AdminOrderDa dataAccess = new AdminOrderDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.UpdateInfo(model);

                    if (result > 0)
                        transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    result = -1;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return (result > 0);
        }

        #endregion
    }

}