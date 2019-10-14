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
    public class ProductCategoryDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertProductCategory(Product_Category entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CmnEntityModel.ID;

            da.Product_Category.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateProductCategory(Product_Category entity)
        {
            var cate = da.Product_Category.Find(entity.ID);
            if (cate != null)
            {
                try
                {
                    // set data
                    cate.ID = entity.ID;
                    cate.Name = entity.Name;
                    cate.DisplayOrder = entity.DisplayOrder;
                    cate.Status = entity.Status;
                    cate.del_flg = Constant.DeleteFlag.NON_DELETE;
                    cate.ModifiedDate = DateTime.Now;
                    cate.ModifiedBy = CmnEntityModel.ID;

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

        public Product_Category getProductCategory(long Id)
        {
            Product_Category cate = da.Product_Category.Where(s => s.ID == Id).SingleOrDefault();
            return cate;
        }

        public string getCategoryName(long Id)
        {
            Product_Category cate = da.Product_Category.Where(s => s.ID == Id).SingleOrDefault();
            string CategoryName = cate != null ? cate.Name.ToUpper() : "";
            return CategoryName;
        }

        public bool FindCategoryProduct(int category_id, int categoryId_Old)
        {
            var category = da.Product_Category.Where(x => x.ID == category_id).SingleOrDefault();

            if (category != null && categoryId_Old == 0)
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
        public IEnumerable<ProductCategoryModel> SearchProductCategoryList(DataTableModel dt, ProductCategoryModel model, out int total_row)
        {
            var lstCategory = (from cate in da.Product_Category
                               join user in da.TblUser on cate.CreatedBy equals user.ID
                               where (cate.del_flg == model.del_flg && (!String.IsNullOrEmpty(model.Name) == true ? cate.Name.Contains(model.Name) : 1 == 1))
                               select new ProductCategoryModel
                               {
                                   Name = cate.Name,
                                   ID = cate.ID,
                                   DisplayOrder = cate.DisplayOrder,
                                   Status = cate.Status,
                                   CreateName = user.UserName
                               });

            total_row = lstCategory.Count();

            lstCategory = lstCategory.OrderBy(i=>i.DisplayOrder).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return lstCategory;
        }
        #endregion

        #region DELETE
        public long DeleteProductCategory(long CATEGORY_ID = 0)
        {
            var cate = da.Product_Category.Where(x => x.ID == CATEGORY_ID).SingleOrDefault();
            if (cate != null)
            {
                try
                {
                    // set data
                    cate.del_flg = Constant.DeleteFlag.DELETE;

                    cate.ModifiedDate = DateTime.Now;
                    cate.ModifiedBy = CmnEntityModel.ID;
                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return cate.ID;
        }

        #endregion

        #region GET DATA
        public List<Product_Category> GetListCategory(int maxItem)
        {
            var lstCategory = da.Product_Category.Where(x=> x.Status == true && x.del_flg.Equals("0")).OrderBy(o=>o.DisplayOrder).ThenByDescending(o => o.ModifiedDate).Take(maxItem);
            return lstCategory.ToList();
        }

        public List<Product> GetListProductByCategory(long categoryID)
        {
            var lstProduct = da.Product.Where(x => x.CategoryID == categoryID && x.Status == true && x.del_flg.Equals("0")).OrderByDescending(o => o.ModifiedDate);
            return lstProduct.ToList();
        }

        public List<Product> GetListProductAllCategory()
        {
            var lstProduct = da.Product.Where(x=> x.Status == true && x.del_flg.Equals("0")).OrderByDescending(o => o.ModifiedDate);
            return lstProduct.ToList();
        }
        #endregion
    }
}