using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;
namespace Web_Gio_Cha.Da
{
    public class ManageMenuDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertMenu(TblMenuContent entity)
        {
            entity.del_flg = Constant.DeleteFlag.NON_DELETE;

            da.TblMenuContent.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateMenu(TblMenuContent entity)
        {
            var news = da.TblMenuContent.Find(entity.ID);
            if (news != null)
            {
                try
                {
                    // set data
                    news.Menu_Content = entity.Menu_Content;
                    news.Status = entity.Status;
                    news.del_flg = Constant.DeleteFlag.NON_DELETE;

                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return entity.ID;
        }

        public TblMenuContent getInfoMenuContent(long Id)
        {
            TblMenuContent product = da.TblMenuContent.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public TblUser getUserByID(long Id)
        {
            TblUser user = da.TblUser.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        public bool CheckExistMenuCode(int MenuCd)
        {
            var result = da.TblMenuContent.Where(x => x.MenuCd == MenuCd).SingleOrDefault();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region LIST
        public List<TblMenuContent> SearchMenuList(DataTableModel dt, TblMenuContent model, out int total_row)
        {
            var listMenu = da.TblMenuContent.Where(i=>i.del_flg == model.del_flg).ToList();
            total_row = listMenu.Count();
            return listMenu;
        }

        #endregion

        #region DELETE
        public long DeleteMenu(long MENU_ID = 0)
        {
            var news = da.TblMenuContent.Where(x => x.ID == MENU_ID).SingleOrDefault();
            if (news != null)
            {
                try
                {
                    // set data
                    news.del_flg = Constant.DeleteFlag.DELETE;
                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return news.ID;
        }

        #endregion
    }
}