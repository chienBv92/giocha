using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Da
{
    public class ManageDistrictDa:BaseDa
    {
        public TblCity getCityByID(long Id)
        {
            TblCity product = da.TblCity.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public List<TblCity> getCityList(int level)
        {
            List<TblCity> cityList = da.TblCity.Where(s => s.Level == level).ToList();
            return cityList;
        }

        #region INSERT/UPDATE
        public long InsertCity(TblCity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CmnEntityModel.ID;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CmnEntityModel.ID;

            da.TblCity.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateCity(TblCity entity)
        {
            var city = da.Product.Find(entity.ID);
            if (city != null)
            {
                try
                {
                    // set data
                    city.del_flg = Constant.DeleteFlag.NON_DELETE;
                    city.ModifiedDate = DateTime.Now;
                    city.ModifiedBy = CmnEntityModel.ID;

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

        public Product getProductByID(long Id)
        {
            Product product = da.Product.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public User getUserByID(long Id)
        {
            User user = da.User.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        #endregion

    }
}