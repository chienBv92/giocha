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
    public class ProductService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertProduct(ProductModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ProductDa dataAccess = new ProductDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    Product product = new Product();
                    product.CategoryID = model.CategoryID;
                    product.Name = model.Name;
                    product.MetaTitle = UtilityServices.UtilityServices.ConvertToUnsign(model.MetaTitle);
                    product.Detail = model.Detail;
                    product.PriceBefore = model.PriceBefore;
                    product.Price = model.Price;
                    product.Promotion = model.Promotion;
                    product.Quantity = model.Quantity;
                    product.Is_Hot = model.Is_Hot;
                    product.Discount = model.Discount;
                    product.Image = model.Image;
                    product.Unit = model.Unit;
                    product.Status = model.Status;
                    product.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.InsertProduct(product);

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

        public long UpdateProduct(ProductModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ProductDa dataAccess = new ProductDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    Product product = new Product();
                    product.ID = model.ID;
                    product.CategoryID = model.CategoryID;
                    product.Name = model.Name;
                    product.MetaTitle = UtilityServices.UtilityServices.ConvertToUnsign(model.MetaTitle);
                    product.Detail = model.Detail;
                    product.PriceBefore = model.PriceBefore;
                    product.Price = model.Price;
                    product.Promotion = model.Promotion;
                    product.Quantity = model.Quantity;
                    product.Is_Hot = model.Is_Hot;
                    product.Discount = model.Discount;
                    product.Image = model.Image;
                    product.Unit = model.Unit;
                    product.Status = model.Status;
                    product.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.UpdateProduct(product);

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
        public IEnumerable<ProductModel> SearchProductList(DataTableModel dt, ProductModel model, out int total_row)
        {
            ProductDa dataAccess = new ProductDa();
            IEnumerable<ProductModel> results = dataAccess.SearchProductList(dt, model, out total_row);
            return results;
        }
        #endregion

        #region DELETE
        public bool DeleteProduct(long PRODUCT_ID)
        {
            ProductDa dataAccess = new ProductDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.DeleteProduct(PRODUCT_ID);

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