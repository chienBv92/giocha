using ShipOnline.UtilityService;
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
    public class OrderCartService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertOrder(OrderModel model, List<ItemGioHang> lstGioHang)
        {
            long res = 0;
            // Declare new DataAccess object
            OrderCartDa dataAccess = new OrderCartDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    Order order = new Order();
                    order.UserID = model.UserID;
                    int maxOrder = dataAccess.getMaxOrder() + 1;

                    order.Code = "VB" + DateTime.Now.ToString("ddMM") + maxOrder.ToString("D6");
                    order.LINK_QRCODE = QRCodeServices.creatQR(order.Code);

                    order.TongTienHang = model.TongTienHang;
                    order.PriceTotal = model.PriceTotal;
                    order.PriceShip = model.PriceShip;
                    order.PriceDiscountTotal = model.PriceDiscountTotal;
                    order.PaymentMethod = model.METHOD_TYPE;
                    order.Paid = PayStatus.None;
                    order.Note = model.Note;
                    order.Receive_District = model.Receive_District;
                    order.Receive_Address = model.Receive_Address;
                    order.Receive_Phone = model.Receive_Phone;
                    order.Receive_Address = model.Receive_Address;
                    order.Status = OrderStatus.Create;
                    order.del_flg = Constant.DeleteFlag.NON_DELETE;

                    res = dataAccess.InsertOrder(order);

                    if (res <= 0)
                        transaction.Dispose();

                    if (res > 0)
                    {
                        long res1 = 0;
                        foreach (var data in lstGioHang)
                        {
                            OrderDetail detail = new OrderDetail();
                            detail.OrderID = res;
                            detail.ProductID = data.IdSanPham;
                            detail.ProductName = data.TenSanPham;
                            detail.Quantity = data.SoLuong;
                            detail.Price = data.TienHang;
                            detail.del_flg = Constant.DeleteFlag.NON_DELETE;

                            res1 = dataAccess.InsertOrderDetail(detail);
                            if (res1 <= 0)
                                transaction.Dispose();

                            res1 = dataAccess.UpdateQuantityProduct(detail.ProductID, detail.Quantity.Value);
                            if (res1 <= 0)
                                transaction.Dispose();
                        }
                    }

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
                    product.MetaTitle = model.Name.Contains("/") ? model.Name.Replace("/", "") : model.Name;
                    product.MetaTitle = UtilityServices.UtilityServices.ConvertToUnsign(product.MetaTitle);
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
    }
}