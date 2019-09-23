using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.UtilityServices.SafePassword;

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

            using (var transaction = new TransactionScope())
            {
                try
                {
                    User User = new User();
                    User.Email = model.Email;
                    User.UserName = model.UserName;
                    User.Password = SafePassword.GetSaltedPassword(model.Password);
                    User.Phone = model.Phone;
                    User.IsAdmin = false;
                    // Tạm thời cho active trước, sau làm xác nhận email 
                    User.Email_Confirm = Constant.ConfirmEmail.CONFIRMED;
                    User.Status = Constant.Status.ACTIVE;

                    User.del_flg = Constant.DeleteFlag.NON_DELETE;
                    User.CreatedDate = DateTime.Now;

                    res = dataAccess.InsertUser(User);

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

        public long UpdateUser(UserModel model)
        {
            long res = 0;
            // Declare new DataAccess object
            UserDa dataAccess = new UserDa();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    User User = new User();
                    User.UserName = model.UserName;
                    User.Name = model.Name;
                    User.Phone = model.Phone;
                    User.del_flg = Constant.DeleteFlag.NON_DELETE;
                    User.ModifiedDate = DateTime.Now;

                    res = dataAccess.UpdateUser(User);

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

        #endregion
    }
}