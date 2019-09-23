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
    public class ProductCategoryService: BaseService
    {
        #region REGIST/ UPDATE
        public long InsertProductCategory(ProductCategoryModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ProductCategoryDa dataAccess = new ProductCategoryDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    Product_Category cate = new Product_Category();
                    cate.ID = model.ID;
                    cate.Name = model.Name;
                    cate.DisplayOrder = model.DisplayOrder;
                    cate.Status = model.Status;
                    cate.del_flg = Constant.DeleteFlag.NON_DELETE;
                    res = dataAccess.InsertProductCategory(cate);

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

        public long UpdateProductCategory(ProductCategoryModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            ProductCategoryDa dataAccess = new ProductCategoryDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    Product_Category cate = new Product_Category();
                    cate.ID = model.ID;
                    cate.Name = model.Name;
                    cate.DisplayOrder = model.DisplayOrder;
                    cate.Status = model.Status;
                    cate.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.UpdateProductCategory(cate);

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

        public ProductCategoryModel getProductCategory(long CATEGORY_ID)
        {
            ProductCategoryDa dataAccess = new ProductCategoryDa();
            ProductCategoryModel model = new ProductCategoryModel();

            var productCate = dataAccess.getProductCategory(CATEGORY_ID);
            if (productCate != null)
            {
                model.ID = productCate.ID;
                model.Name = productCate.Name;
                model.DisplayOrder = productCate.DisplayOrder;
                model.Status = productCate.Status;
                model.del_flg = productCate.del_flg;
            }
            return model;
        }
        #endregion

        #region Search
        public IEnumerable<ProductCategoryModel> SearchProductCategoryList(DataTableModel dt, ProductCategoryModel model, out int total_row)
        {
            ProductCategoryDa dataAccess = new ProductCategoryDa();
            IEnumerable<ProductCategoryModel> results = dataAccess.SearchProductCategoryList(dt, model, out total_row);
            return results;
        }
        #endregion

        #region DELETE
        public bool DeleteProductCategory(int CATEGORY_ID)
        {
            ProductCategoryDa dataAccess = new ProductCategoryDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.DeleteProductCategory(CATEGORY_ID);

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