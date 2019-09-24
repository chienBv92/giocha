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
    public class ProductDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertProduct(Product entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CmnEntityModel.ID;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CmnEntityModel.ID;

            da.Product.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateProduct(Product entity)
        {
            var product = da.Product.Find(entity.ID);
            if (product != null)
            {
                try
                {
                    // set data
                    product.CategoryID = entity.CategoryID;
                    product.Name = entity.Name;
                    product.MetaTitle = UtilityServices.UtilityServices.ConvertToUnsign(entity.MetaTitle);
                    product.Detail = entity.Detail;
                    product.PriceBefore = entity.PriceBefore;
                    product.Price = entity.Price;
                    product.Promotion = entity.Promotion;
                    product.Quantity = entity.Quantity;
                    product.Is_Hot = entity.Is_Hot;
                    product.Discount = entity.Discount;
                    product.Image = entity.Image;
                    product.Status = entity.Status;
                    product.Unit = entity.Unit;
                    product.del_flg = Constant.DeleteFlag.NON_DELETE;
                    product.ModifiedDate = DateTime.Now;
                    product.ModifiedBy = CmnEntityModel.ID;

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
        public IEnumerable<ProductModel> SearchProductList(DataTableModel dt, ProductModel model, out int total_row)
        {
            var lstProduct = (from pro in da.Product
                              join cate in da.Product_Category on pro.CategoryID equals cate.ID
                              join user in da.User on pro.CreatedBy equals user.ID
                              from userB in da.User.Where(i=>i.ID == pro.ModifiedBy).DefaultIfEmpty()
                              //join userB in da.User.DefaultIfEmpty on pro.ModifiedBy equals userB.ID
                              where (pro.del_flg == model.del_flg && (!String.IsNullOrEmpty(model.Name) == true ? pro.Name.Contains(model.Name) : 1 == 1))
                              where (pro.Status.HasValue ? pro.Status == model.Status : 1 == 1)
                              where (model.CategoryID > 0 ? pro.CategoryID == model.CategoryID : 1 == 1)
                              where (model.PriceBefore > 0 ? pro.PriceBefore == model.PriceBefore : 1 == 1)
                              where (model.Price > 0 ? pro.Price == model.Price : 1 == 1)
                              where (model.FromDate.HasValue ? pro.CreatedDate >= model.FromDate : 1 == 1)
                              where (model.ToDate.HasValue ? pro.CreatedDate <= model.ToDate : 1 == 1)

                               select new ProductModel
                               {
                                   ID = pro.ID,
                                   CategoryID = pro.CategoryID,
                                   CATEGORY_NAME = cate.Name,
                                   Name = pro.Name,
                                   MetaTitle = pro.MetaTitle,
                                   Detail = pro.Detail,
                                   Image = pro.Image,
                                   PriceBefore = pro.PriceBefore,
                                   Price = pro.Price,
                                   Promotion = pro.Promotion,
                                   Quantity = pro.Quantity,
                                   Discount = pro.Discount,
                                   Is_Hot = pro.Is_Hot,
                                   Unit = pro.Unit,
                                   TopHot = pro.TopHot,
                                   del_flg = pro.del_flg,
                                   Status = cate.Status,
                                   CreatedDate = pro.CreatedDate,
                                   CreateName = user.UserName,
                                   ModifiedDate = pro.ModifiedDate,
                                   ModifiedName = userB.UserName
                               });

            total_row = lstProduct.Count();

            lstProduct = lstProduct.OrderByDescending(i => i.ModifiedDate).ThenByDescending(i=>i.CreatedDate).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return lstProduct;
        }
        #endregion

        #region DELETE
        public long DeleteProduct(long PRODUCT_ID = 0)
        {
            var product = da.Product.Where(x => x.ID == PRODUCT_ID).SingleOrDefault();
            if (product != null)
            {
                try
                {
                    // set data
                    product.del_flg = Constant.DeleteFlag.DELETE;

                    product.ModifiedDate = DateTime.Now;
                    product.ModifiedBy = CmnEntityModel.ID;
                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return product.ID;
        }

        #endregion

    }
}