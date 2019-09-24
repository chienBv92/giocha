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
            var city = da.TblCity.Find(entity.ID);
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

        #region LIST
        public IEnumerable<TblCity> SearchDistrictList(DataTableModel dt, CityModel model, out int total_row)
        {
            var lstDistrict = da.TblCity.Where(i => i.Level == Constant.CityLevel.District && i.del_flg == model.del_flg);
            if (model.Status.HasValue)
            {
                lstDistrict = lstDistrict.Where(i => i.Status == model.Status);
            }
            if (model.CITY_ID > 0)
            {
                lstDistrict = lstDistrict.Where(i => i.Parent_Code == model.CITY_ID);
            }
            if (!String.IsNullOrEmpty(model.DISTRICT_NAME))
            {
                lstDistrict = lstDistrict.Where(i => i.Name.Contains(model.DISTRICT_NAME));
            }

            total_row = lstDistrict.Count();

            lstDistrict = lstDistrict.OrderByDescending(i => i.ModifiedDate).ThenByDescending(i => i.CreatedDate).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return lstDistrict;
        }
        #endregion

        #region DELETE
        public long DeleteDistrict(long DISTRICT_ID = 0)
        {
            var city = da.TblCity.Where(x => x.ID == DISTRICT_ID).SingleOrDefault();
            if (city != null)
            {
                try
                {
                    // set data
                    city.del_flg = Constant.DeleteFlag.DELETE;

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

            return city.ID;
        }

        #endregion


    }
}