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
    public class ManagePromotionService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertPromotion(TblPromotion model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManagePromotionDa dataAccess = new ManagePromotionDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    res = dataAccess.InsertPromotion(model);

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

        public long UpdatePromotion(TblPromotion model)
        {
            long res = 0;
            // Declare new DataAccess object
            ManagePromotionDa dataAccess = new ManagePromotionDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    res = dataAccess.UpdatePromotion(model);

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

        #region Search
        public IEnumerable<TblPromotion> SearchPromotionList(DataTableModel dt, TblPromotion model, out int total_row)
        {
            ManagePromotionDa dataAccess = new ManagePromotionDa();
            IEnumerable<TblPromotion> results = dataAccess.SearchPromotionList(dt, model, out total_row);
            return results;
        }
        #endregion

        #region DELETE
        public bool DeletePromotion(long PROMOTION_ID)
        {
            ManagePromotionDa dataAccess = new ManagePromotionDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.DeletePromotion(PROMOTION_ID);

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