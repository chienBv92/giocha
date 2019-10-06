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
    public class OrderCartDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertOrder(Order entity)
        {
            entity.CreatedDate = DateTime.Now;

            da.Order.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long InsertOrderDetail(OrderDetail entity)
        {
            da.OrderDetail.Add(entity);
            da.SaveChanges();
            return entity.OrderID;
        }

        public long UpdateQuantityProduct(long ProductID, int quantity)
        {
            var product = da.Product.Find(ProductID);
            if (product != null)
            {
                product.Quantity = product.Quantity - quantity;
                da.SaveChanges();
            }
            return ProductID;
        }

        public Order getOrderByID(long Id)
        {
            Order product = da.Order.Where(s => s.ID == Id).SingleOrDefault();
            return product;
        }

        public TblUser getUserByID(long Id)
        {
            TblUser user = da.TblUser.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        public int getMaxOrder()
        {
            int countOrder = da.Order.Count();
            return countOrder;
        }
        #endregion
    }

}