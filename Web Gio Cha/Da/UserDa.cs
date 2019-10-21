using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.UtilityServices.SafePassword;
using WebDuhoc.Models.Define;

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
                    user.Receive_District = entity.Receive_District;
                    user.Receive_Address = entity.Receive_Address;

                    user.del_flg = Constant.DeleteFlag.NON_DELETE;
                    user.ModifiedDate = DateTime.Now;

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

        public long UpdatePassword(TblUser entity)
        {
            var user = da.TblUser.Find(entity.ID);
            if (user != null)
            {
                //kan.HanViet = entity.HanViet;
                try
                {
                    // set data
                    user.Password = entity.Password;

                    user.del_flg = Constant.DeleteFlag.NON_DELETE;
                    user.ModifiedDate = DateTime.Now;

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

        public long ConfirmEmail(TblUser entity)
        {
            var user = da.TblUser.Find(entity.ID);
            if (user != null)
            {
                //kan.HanViet = entity.HanViet;
                try
                {
                    // set data
                    user.Email_Confirm = Constant.ConfirmEmail.CONFIRMED;
                    user.Status = Constant.Status.ACTIVE;
                    user.ModifiedDate = DateTime.Now;

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

        public long ReSetPassword(TblUser entity)
        {
            var user = da.TblUser.Find(entity.ID);
            if (user != null)
            {
                //kan.HanViet = entity.HanViet;
                try
                {
                    // set data
                    user.Password = SafePassword.GetSaltedPassword(Constant.DEFAULT_PASSWORD); 
                    user.Email_Confirm = Constant.ConfirmEmail.RESET_PASSWORD;
                    user.Status = Constant.Status.NONE;
                    user.ModifiedDate = DateTime.Now;

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

        public UserModel getInfoUser(long userId)
        {
            
            var data = (from user in da.TblUser
                        from dis in da.TblCity.Where(x => x.ID == user.Receive_District).DefaultIfEmpty()
                        where (user.ID == userId)

                            select new UserModel
                            {
                                ID = user.ID,
                                Email = user.Email,
                                UserName = user.UserName,
                                Phone = user.Phone,
                                Password = user.Password,
                                Receive_District = user.Receive_District,
                                DistrictName = dis.Name,
                                Receive_Address = user.Receive_Address,
                                Status = user.Status,
                                CreatedDate = user.CreatedDate,
                                ModifiedDate = user.ModifiedDate,
                                del_flg = user.del_flg
                            }).SingleOrDefault();

            return data;
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

        #region LIST
        public IEnumerable<UserModel> SearchUserList(DataTableModel dt, UserModel model, out int total_row)
        {
            var lstUser = (from user in da.TblUser
                              from dis in da.TblCity.Where(x => x.ID == user.Receive_District).DefaultIfEmpty()
                              where (user.del_flg == model.del_flg && (!String.IsNullOrEmpty(model.Name) == true ? user.Name.Contains(model.Name) : 1 == 1))
                              where (model.Status.HasValue ? user.Status == model.Status : 1 == 1)
                              where (!String.IsNullOrEmpty(model.Receive_Address) == true ? user.Receive_Address.Contains(model.Receive_Address) : 1 == 1)
                           where (!String.IsNullOrEmpty(model.Email) == true ? user.Email.Contains(model.Email) : 1 == 1)
                           where (!String.IsNullOrEmpty(model.UserName) == true ? user.UserName.Contains(model.UserName) : 1 == 1)
                           where (!String.IsNullOrEmpty(model.Phone) == true ? user.Phone.Contains(model.Phone) : 1 == 1)
                             where (model.Receive_District > 0 ? user.Receive_District == model.Receive_District : 1 == 1)

                              select new UserModel
                              {
                                  ID = user.ID,
                                  Email = user.Email,
                                  UserName = user.UserName,
                                  Phone = user.Phone,
                                  Password = user.Password,
                                  Receive_District = user.Receive_District,
                                  DistrictName = dis.Name,
                                  Receive_Address = user.Receive_Address,
                                  Status = user.Status,
                                  CreatedDate = user.CreatedDate,
                                  ModifiedDate = user.ModifiedDate,
                                  del_flg = user.del_flg
                              });

            total_row = lstUser.Count();

            lstUser = lstUser.OrderByDescending(i => i.ModifiedDate).ThenByDescending(i => i.CreatedDate).Skip(dt.iDisplayStart).Take(dt.iDisplayLength);

            return lstUser;
        }

        #endregion

        #region DELETE
        public long DeleteUser(long USER_ID = 0)
        {
            var user = da.TblUser.Where(x => x.ID == USER_ID).SingleOrDefault();
            if (user != null)
            {
                try
                {
                    // set data
                    user.del_flg = Constant.DeleteFlag.DELETE;

                    user.ModifiedDate = DateTime.Now;
                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return user.ID;
        }

        #endregion
    }
}