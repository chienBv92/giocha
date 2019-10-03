using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Da
{
    public class ManagePromotionDa : BaseDa
    {
        public TblPromotion getPromotionByID(long Id)
        {
            TblPromotion pro = da.TblPromotion.Where(s => s.id == Id).SingleOrDefault();
            return pro;
        }

        public TblPromotion getPromotionForDiscount(decimal priceTotal)
        {
            TblPromotion pro = da.TblPromotion.Where(s => s.Status == true && s.del_flg == Constant.DeleteFlag.NON_DELETE && s.PriceMin <= priceTotal && s.PriceMax > priceTotal).SingleOrDefault();
            return pro;
        }

        public List<TblCity> getCityList(int level)
        {
            List<TblCity> cityList = da.TblCity.Where(s => s.Level == level).ToList();
            return cityList;
        }

        public List<TblCity> getDistrictList(int city)
        {
            List<TblCity> distrcitList = da.TblCity.Where(s => s.Level == Constant.CityLevel.District && s.Parent_Code == city && s.del_flg == Constant.DeleteFlag.NON_DELETE && s.Status == Constant.Status.ACTIVE).ToList();
            return distrcitList;
        }

        #region INSERT/UPDATE
        public long InsertPromotion(TblPromotion entity)
        {
            entity.del_flg = Constant.DeleteFlag.NON_DELETE;

            da.TblPromotion.Add(entity);
            da.SaveChanges();
            return entity.id;
        }

        public long UpdatePromotion(TblPromotion entity)
        {
            var pro = da.TblPromotion.Find(entity.id);
            if (pro != null)
            {
                try
                {
                    // set data
                    pro.PromotionName = entity.PromotionName;
                    pro.PriceMin = entity.PriceMin;
                    pro.PriceMax = entity.PriceMax;
                    pro.Discount = entity.Discount;

                    pro.del_flg = Constant.DeleteFlag.NON_DELETE;

                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return entity.id;
        }

        public Product getProductByID(long Id)
        {
            Product product = da.Product.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public TblUser getUserByID(long Id)
        {
            TblUser user = da.TblUser.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        #endregion

        #region LIST
        public IEnumerable<TblPromotion> SearchPromotionList(DataTableModel dt, TblPromotion model, out int total_row)
        {
            var lstPromotion = da.TblPromotion.Where(i => i.del_flg == model.del_flg);
            if (model.Status.HasValue)
            {
                lstPromotion = lstPromotion.Where(i => i.Status == model.Status);
            }
            // check giá nằm tron khoảng nào
            if (model.PriceMin > 0)
            {
                lstPromotion = lstPromotion.Where(i => i.PriceMin <= model.PriceMin && i.PriceMax > model.PriceMin);
            }
            if (!String.IsNullOrEmpty(model.PromotionName))
            {
                lstPromotion = lstPromotion.Where(i => i.PromotionName.Contains(model.PromotionName));
            }

            total_row = lstPromotion.Count();

            lstPromotion = lstPromotion.OrderBy(i => i.del_flg).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return lstPromotion;
        }
        #endregion

        #region DELETE
        public long DeletePromotion(long PROMOTION_ID = 0)
        {
            var pro = da.TblPromotion.Where(x => x.id == PROMOTION_ID).SingleOrDefault();
            if (pro != null)
            {
                try
                {
                    // set data
                    pro.del_flg = Constant.DeleteFlag.DELETE;

                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return pro.id;
        }

        #endregion

    }
}