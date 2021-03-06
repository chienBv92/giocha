﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.UtilityServices.SafePassword;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Services
{
    public class UserService : BaseService
    {
        #region REGIST/ UPDATE
        public long InsertUser(UserModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();

            TblUser User = new TblUser();
            User.Email = model.Email;
            User.UserName = model.UserName;
            User.Password = SafePassword.GetSaltedPassword(model.Password);
            User.Phone = model.Phone;
            User.IsAdmin = false;
            // Chưa xác nhận email
            User.Email_Confirm = Constant.ConfirmEmail.CONFIRMED;
            User.Status = Constant.Status.ACTIVE;

            User.del_flg = Constant.DeleteFlag.NON_DELETE;
            User.CreatedDate = DateTime.Now;

            res = dataAccess.InsertUser(User);
            return res;
        }

        public long UpdateUser(UserModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();

            TblUser User = new TblUser();
            User.ID = model.ID;
            User.UserName = model.UserName;
            User.Name = model.Name;
            User.Phone = model.Phone;
            User.Receive_District = model.Receive_District;
            User.Receive_Address = model.Receive_Address;
            User.del_flg = Constant.DeleteFlag.NON_DELETE;
            User.ModifiedDate = DateTime.Now;

            res = dataAccess.UpdateUser(User);
            return res;
        }

        #endregion

        #region CHANGE PASSWORD
        public long UpdatePassword(UserModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();
            using (var transaction = new TransactionScope())
            {
                TblUser entity = new TblUser();

                entity.ID = model.ID;
                entity.Password = SafePassword.GetSaltedPassword(model.NEW_PASSWORD);

                res = dataAccess.UpdatePassword(entity);
                if (res <= 0)
                    transaction.Dispose();
                transaction.Complete();
            }
            return res;
        }
        #endregion

        #region Search
        public IEnumerable<UserModel> SearchUserList(DataTableModel dt, UserModel model, out int total_row)
        {
            UserDa dataAccess = new UserDa();
            IEnumerable<UserModel> results = dataAccess.SearchUserList(dt, model, out total_row);
            return results;
        }

        #endregion

        #region DELETE
        public bool DeleteUser(long USER_ID)
        {
            UserDa dataAccess = new UserDa();

            long result = 0;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    result = dataAccess.DeleteUser(USER_ID);

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