using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Services
{
    public class ManageDistrictService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertCity(CityModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManageDistrictDa dataAccess = new ManageDistrictDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    TblCity city = new TblCity();
                    city.ID = model.ID;
                    city.Level = model.Level;
                    city.Parent_Code = model.CITY_ID;
                    city.Name = model.Name;
                    city.PriceShip = model.PriceShip;
                    city.Status = model.Status;
                    city.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.InsertCity(city);

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

        public long UpdateCity(CityModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManageDistrictDa dataAccess = new ManageDistrictDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    TblCity city = new TblCity();

                    city.Name = model.Name;
                    city.Level = model.Level;
                    city.Status = model.Status;
                    city.PriceShip = model.PriceShip;
                    city.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.UpdateCity(city);

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

        public ProductModel getProductByID(long PRODUCT_ID)
        {
            ProductDa dataAccess = new ProductDa();
            ProductModel model = new ProductModel();

            var product = dataAccess.getProductByID(PRODUCT_ID);

            if (product != null)
            {
                model.ID = product.ID;
                model.CategoryID = product.CategoryID;
                model.Name = product.Name;
                model.MetaTitle = product.MetaTitle;
                model.Detail = product.Detail;
                model.PriceBefore = product.PriceBefore;
                model.Price = product.Price;
                model.Promotion = product.Promotion;
                model.Quantity = product.Quantity;
                model.Is_Hot = product.Is_Hot;
                model.Discount = product.Discount;
                model.Image = product.Image;
                model.Status = product.Status;
                model.Unit = product.Unit;
                model.CreatedDate = product.CreatedDate;
                model.CreatedBy = product.CreatedBy;
                model.ModifiedDate = product.ModifiedDate;

                long userCreateID = product.CreatedBy.HasValue ? product.CreatedBy.Value : 0;
                var userCreate = dataAccess.getUserByID(userCreateID);
                model.CreateName = userCreate != null ? userCreate.UserName : "";
                long userUpdateID = product.ModifiedBy.HasValue ? product.ModifiedBy.Value : 0;
                var userUpdate = dataAccess.getUserByID(userUpdateID);
                model.ModifiedName = userUpdate != null ? userUpdate.UserName : "";

                model.del_flg = Constant.DeleteFlag.NON_DELETE;
            }
            return model;
        }
        #endregion

    }
}