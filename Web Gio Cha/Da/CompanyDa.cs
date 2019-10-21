using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.Da
{
    public class CompanyDa : BaseDa
    {
        public TblCompany getCompanyByID(long Id)
        {
            TblCompany company = da.TblCompany.Where(s => s.COMPANY_ID == Id).SingleOrDefault();
            return company;
        }

        public TblCompany getCompanyByCd(string CompanyCd)
        {
            TblCompany company = da.TblCompany.Where(s => s.COMPANY_CD == CompanyCd).SingleOrDefault();
            return company;
        }

        public bool CheckExistCompanyCd(string CompanyCd)
        {
            var result = da.TblCompany.Where(x => x.COMPANY_CD == CompanyCd).SingleOrDefault();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region INSERT/UPDATE
        public long InsertCompany(TblCompany entity)
        {
            entity.DEL_FLG = Constant.DeleteFlag.NON_DELETE;
            entity.INS_DATE = DateTime.Now;
            entity.INS_USER_ID = CmnEntityModel.ID;
            entity.UPD_DATE = DateTime.Now;
            entity.UPD_USER_ID = CmnEntityModel.ID;

            da.TblCompany.Add(entity);
            da.SaveChanges();
            return entity.COMPANY_ID;
        }

        public long UpdateCompany(TblCompany entity)
        {
            var company = da.TblCompany.Find(entity.COMPANY_ID);
            if (company != null)
            {
                try
                {
                    // set data
                    company.COMPANY_ADDRESS = entity.COMPANY_ADDRESS;
                    company.COMPANY_EMAIL = entity.COMPANY_EMAIL;
                    company.COMPANY_LAT = entity.COMPANY_LAT;
                    company.COMPANY_LNG = entity.COMPANY_LNG;
                    company.COMPANY_NAME = entity.COMPANY_NAME;
                    company.COMPANY_PHONE = entity.COMPANY_PHONE;
                    company.FACE_PAGE = entity.FACE_PAGE;
                    company.LINK_MAP = entity.LINK_MAP;
                    company.LOGO = entity.LOGO;
                    company.DEL_FLG = Constant.DeleteFlag.NON_DELETE;
                    company.UPD_DATE = DateTime.Now;
                    company.UPD_USER_ID = CmnEntityModel.ID;

                    da.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return 0;

            return entity.COMPANY_ID;
        }

        #endregion
    }
}