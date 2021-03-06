﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Da
{
    public class UserOrderDa : BaseDa
    {
        #region LIST
        public IEnumerable<AdminOrderList> SearchOrderListbyUser(DataTableModel dt, AdminOrderList model, out int total_row)
        {
            model.TO_DATE = model.FROM_DATE.HasValue ? model.TO_DATE.Value.AddDays(1) : model.TO_DATE;

            var lstOrder = (from pro in da.Order
                            join user in da.TblUser on pro.UserID equals user.ID
                            join dis in da.TblCity on pro.Receive_District equals dis.ID

                            where ((!String.IsNullOrEmpty(model.Code) == true ? pro.Code.Contains(model.Code) : 1 == 1))
                            //where (model.FROM_DATE.HasValue ? pro.CreatedDate >= model.FROM_DATE : 1 == 1)
                            //where (model.TO_DATE.HasValue ? pro.CreatedDate <= model.TO_DATE : 1 == 1)
                            //where ((model.PaymentMethod.HasValue && model.PaymentMethod > 0) ? pro.PaymentMethod == model.PaymentMethod.Value : 1 == 1)
                            //where (model.Paid.HasValue ? pro.Paid == model.Paid.Value : 1 == 1)
                            //where (!String.IsNullOrEmpty(model.ORDER_STATUS_LIST) == true ? pro.Status.ToString().Contains(model.ORDER_STATUS_LIST) : 1 == 1)

                            where ((model.UserID.HasValue && model.UserID > 0) ? pro.UserID == model.UserID : 1 == 1)

                            select new AdminOrderList
                            {
                                ID = pro.ID,
                                Code = pro.Code,
                                Status = pro.Status,
                                TongTienHang = pro.TongTienHang,
                                PriceDiscountTotal = pro.PriceDiscountTotal,
                                PriceShip = pro.PriceShip,
                                PriceTotal = pro.PriceTotal,
                                UserName = user.UserName,
                                Receive_Phone = pro.Receive_Phone,
                                DistrictName = dis.Name,
                                Receive_Address = pro.Receive_Address,
                                CreatedDate = pro.CreatedDate,
                                //CREATE_DATE_STRING = pro.CreatedDate.HasValue ? pro.CreatedDate.Value.ToString("dd/MM/yyyy hh:mm") : "",
                                //ORDER_STATUS_TEXT = OrderStatus.Items[pro.Status.Value].ToString(),
                                PaymentMethod = pro.PaymentMethod,
                                Paid = pro.Paid,
                                del_flg = pro.del_flg
                            });

            total_row = lstOrder.Count();

            // Sắp xếp theo lựa chọn
            //if (!String.IsNullOrEmpty(model.SORT_COL))
            //{
            //    switch (model.SORT_COL)
            //    {
            //        case SortAdminOrderShipList.CREATE_DATE:
            //            if (model.SORT_TYPE == SortType.ASC)
            //            {
            //                lstOrder = lstOrder.OrderBy(i => i.CreatedDate).ThenBy(i => i.Code).ThenBy(i => i.PriceTotal).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            else
            //            {
            //                lstOrder = lstOrder.OrderByDescending(i => i.CreatedDate).ThenByDescending(i => i.Code).ThenByDescending(i => i.PriceTotal).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            break;

            //        case SortAdminOrderShipList.ORDER_CODE:
            //            if (model.SORT_TYPE == SortType.ASC)
            //            {
            //                lstOrder = lstOrder.OrderBy(i => i.Code).ThenBy(i => i.CreatedDate).ThenBy(i => i.PriceTotal).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            else
            //            {
            //                lstOrder = lstOrder.OrderByDescending(i => i.Code).ThenByDescending(i => i.CreatedDate).ThenByDescending(i => i.PriceTotal).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            break;

            //        case SortAdminOrderShipList.TOTAL_MONEY:
            //            if (model.SORT_TYPE == SortType.ASC)
            //            {
            //                lstOrder = lstOrder.OrderBy(i => i.PriceTotal).ThenBy(i => i.CreatedDate).ThenBy(i => i.Code).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            else
            //            {
            //                lstOrder = lstOrder.OrderByDescending(i => i.PriceTotal).ThenByDescending(i => i.CreatedDate).ThenByDescending(i => i.Code).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);
            //            }
            //            break;
            //    }
            //}
            //else
            //{
            //    lstOrder = lstOrder.OrderByDescending(i => i.CreatedDate); ;
            //}

            return lstOrder.OrderByDescending(i => i.CreatedDate);
        }

        public IEnumerable<AdminOrderList> SearchOrderListTotalNoneStatus(DataTableModel dt, AdminOrderList model)
        {
            model.TO_DATE = model.FROM_DATE.HasValue ? model.TO_DATE.Value.AddDays(1) : model.TO_DATE;

            var lstOrder = (from pro in da.Order
                            join user in da.TblUser on pro.UserID equals user.ID
                            join dis in da.TblCity on pro.Receive_District equals dis.ID

                            where (pro.del_flg == model.del_flg && (!String.IsNullOrEmpty(model.Code) == true ? pro.Code.Contains(model.Code) : 1 == 1))
                            where (model.FROM_DATE.HasValue ? pro.CreatedDate >= model.FROM_DATE : 1 == 1)
                            where (model.TO_DATE.HasValue ? pro.CreatedDate <= model.TO_DATE : 1 == 1)
                            where ((model.Receive_District.HasValue && model.Receive_District > 0) ? pro.Receive_District == model.Receive_District : 1 == 1)
                            where ((model.UserID.HasValue && model.UserID > 0) ? pro.UserID == model.UserID : 1 == 1)
                            where ((model.PaymentMethod.HasValue && model.PaymentMethod > 0) ? pro.PaymentMethod == model.PaymentMethod.Value : 1 == 1)
                            where (model.Paid.HasValue ? pro.Paid == model.Paid.Value : 1 == 1)

                            select new AdminOrderList
                            {
                                ID = pro.ID,
                                Code = pro.Code,
                                Status = pro.Status,
                                Product_Name = "",
                                TongTienHang = pro.TongTienHang,
                                PriceDiscountTotal = pro.PriceDiscountTotal,
                                PriceShip = pro.PriceShip,
                                PriceTotal = pro.PriceTotal,
                                UserName = user.UserName,
                                Receive_Phone = pro.Receive_Phone,
                                DistrictName = dis.Name,
                                Receive_Address = pro.Receive_Address,
                                CreatedDate = pro.CreatedDate,
                                //CREATE_DATE_STRING = pro.CreatedDate.HasValue ? pro.CreatedDate.Value.ToString("dd/MM/yyyy hh:mm") : "",
                                PaymentMethod = pro.PaymentMethod,
                                Paid = pro.Paid,
                                del_flg = pro.del_flg
                            });

            return lstOrder;
        }

        public List<Product> GetHotProducts(int maxItem)
        {
            var lstProduct = da.Product.Where(x => x.Is_Hot.Value && x.Status.Value && x.del_flg.Equals("0")).OrderByDescending(o => o.ModifiedDate).OrderByDescending(o => o.CreatedDate).Take(maxItem);
            return lstProduct.ToList();
        }
        #endregion
    }
}