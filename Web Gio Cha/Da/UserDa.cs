using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Da
{
    public class UserDa:BaseDa
    {
        public long InsertUser(TblUser entity)
        {
            da.TblUser.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        // Update
        public long UpdateUser(TblUser entity)
        {
            var user = da.TblUser.Find(entity.ID);
            if (user != null)
            {
                //kan.HanViet = entity.HanViet;
                try
                {
                    // set data
                    user.UserName = entity.UserName;
                    user.Name = entity.UserName;
                    user.Phone = entity.Phone;

                    user.del_flg = Constant.DeleteFlag.NON_DELETE;
                    user.ModifiedDate = entity.ModifiedDate;

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

        public bool Login(string Email, string password)
        {
            var result = da.TblUser.Count(i => i.Email == Email && i.Password == password);
            return result > 0 ? true : false;
        }

        public TblUser getUserByEmail(string Email)
        {
            var result = da.TblUser.SingleOrDefault(i => i.Email == Email);
            return result;
        }

        public UserModel getInfoUser(long userId)
        {
            UserModel model = new UserModel();

            TblUser user = da.TblUser.Where(s => s.ID == userId).SingleOrDefault();
            if (user != null)
            {
                model.ID = user.ID;

            }
            return model;
        }

        public bool CheckExistUserEmail(string Email)
        {
            Email = Email.Trim();
            var result = da.TblUser.Where(x => x.Email == Email).SingleOrDefault();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}