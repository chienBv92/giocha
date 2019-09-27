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
    public class ManageNewsDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertNews(TblNews entity)
        {
            entity.del_flg = Constant.DeleteFlag.NON_DELETE;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CmnEntityModel.ID;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CmnEntityModel.ID;

            da.TblNews.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateNews(TblNews entity)
        {
            var news = da.TblNews.Find(entity.ID);
            if (news != null)
            {
                try
                {
                    // set data
                    news.Name = entity.Name;
                    news.MetaTitle = entity.MetaTitle;
                    news.Content = entity.Content;
                    news.Image = entity.Image;
                    news.Status = entity.Status;
                    news.del_flg = Constant.DeleteFlag.NON_DELETE;
                    news.ModifiedDate = DateTime.Now;
                    news.ModifiedBy = CmnEntityModel.ID;

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

        public TblNews getInfoNews(long Id)
        {
            TblNews product = da.TblNews.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public TblUser getUserByID(long Id)
        {
            TblUser user = da.TblUser.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        #endregion

        #region LIST
        public IEnumerable<NewsModel> SearchNewsList(DataTableModel dt, NewsModel model, out int total_row)
        {
//            StringBuilder sql = new StringBuilder();
//            sql.Append(@" Select A.*, B.UserName as CreateName, C.UserName as ModifiedName from TblNews A
//                        left join TblUser B
//                        on A.CreatedBy = B.ID
//                        left join TblUser C
//                        on A.ModifiedBy = C.ID ");

//            sql.AppendFormat("Where A.del_flg = '{0}' ", model.del_flg);
//            if(model.Status.HasValue){
//                sql.AppendFormat(" AND A.Status = {0} ", model.Status == true ? "1" : "0");
//            }
//            if (!String.IsNullOrEmpty(model.Name))
//            {
//                sql.AppendFormat(" AND A.Name like '{0}' ", "%" + model.Name + "%");
//            }
            //if (model.FromDate.HasValue)
            //{
            //    sql.AppendFormat(" AND A.ModifiedDate >= '{0}' ", model.FromDate);
            //}
            //if (model.ToDate.HasValue)
            //{
            //    sql.AppendFormat(" AND A.ModifiedDate <= '{0}' ", model.ToDate);
            //}
            //sql.Append("Order by A.ModifiedDate desc, A.CreatedDate desc ");

            var listNews = (from cate in da.TblNews
                               from userB in da.TblUser.Where(i => i.ID == cate.CreatedBy).DefaultIfEmpty()
                               from userC in da.TblUser.Where(i => i.ID == cate.ModifiedBy).DefaultIfEmpty()
                               where (cate.del_flg == model.del_flg && (!String.IsNullOrEmpty(model.Name) == true ? cate.Name.Contains(model.Name) : 1 == 1))
                               where (model.Status.HasValue ? cate.Status == model.Status : 1 == 1)
                               where (model.FromDate.HasValue ? cate.ModifiedDate >= model.FromDate : 1 == 1)
                               where (model.ToDate.HasValue ? cate.ModifiedDate <= model.ToDate : 1 == 1)
                               select new NewsModel
                               {
                                   ID = cate.ID,
                                   Name = cate.Name,
                                   Status = cate.Status,
                                   CreatedDate = cate.CreatedDate,
                                   ModifiedDate = cate.ModifiedDate,
                                   CreateName = userB.UserName,
                                   ModifiedName = userC.UserName,
                                   del_flg = cate.del_flg
                               });

            total_row = listNews.Count();
            listNews = listNews.OrderByDescending(i=>i.ModifiedDate).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return listNews;
        }

        #endregion

        #region DELETE
        public long DeleteNews(long NEWS_ID = 0)
        {
            var news = da.TblNews.Where(x => x.ID == NEWS_ID).SingleOrDefault();
            if (news != null)
            {
                try
                {
                    // set data
                    news.del_flg = Constant.DeleteFlag.DELETE;

                    news.ModifiedDate = DateTime.Now;
                    news.ModifiedBy = CmnEntityModel.ID;
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