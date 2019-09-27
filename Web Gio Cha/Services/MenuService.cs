using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Services
{
    public class MenuService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertMenu(TblMenuContent model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManageMenuDa dataAccess = new ManageMenuDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    res = dataAccess.InsertMenu(model);

                    if (res <= 0)
                        transaction.Dispose();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return res;
        }

        public long UpdateMenu(TblMenuContent model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManageMenuDa dataAccess = new ManageMenuDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    res = dataAccess.UpdateMenu(model);

                    if (res <= 0)
                        transaction.Dispose();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return res;
        }

        #endregion

        #region LIST
        public List<TblMenuContent> SearchMenuList(DataTableModel dt, TblMenuContent model, out int total_row)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();
            List<TblMenuContent> results = dataAccess.SearchMenuList(dt, model, out total_row);
            return results;
        }
        #endregion

        #region DELETE
        public bool DeleteMenu(long NEWS_ID = 0)
        {
            ManageMenuDa dataAccess = new ManageMenuDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.DeleteMenu(NEWS_ID);

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