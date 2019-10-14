using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using WebDuhoc.Models.Define;

namespace Web_Gio_Cha.Da
{
    public class SliderDa : BaseDa
    {
        #region INSERT/UPDATE
        public long InsertSlide(Slide entity)
        {
            entity.del_flg = Constant.DeleteFlag.NON_DELETE;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CmnEntityModel.ID;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CmnEntityModel.ID;

            da.Slide.Add(entity);
            da.SaveChanges();
            return entity.ID;
        }

        public long UpdateSlide(Slide entity)
        {
            var slide = da.Slide.Find(entity.ID);
            if (slide != null)
            {
                try
                {
                    // set data
                    slide.Type = entity.Type;
                    slide.DisplayOrder = entity.DisplayOrder;
                    slide.Image = entity.Image;
                    slide.Link = entity.Link;
                    slide.Status = entity.Status;
                    slide.del_flg = Constant.DeleteFlag.NON_DELETE;
                    slide.ModifiedDate = DateTime.Now;
                    slide.ModifiedBy = CmnEntityModel.ID;

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

        public Slide getInfoSlider(long Id)
        {
            Slide slider = da.Slide.Where(s => s.ID == Id).SingleOrDefault();
            return slider;
        }

        public IEnumerable<Slide> getListSlideView(int SlideType = 0)
        {
            var slider = da.Slide.Where(s => s.Type == SlideType && s.del_flg == Constant.DeleteFlag.NON_DELETE && s.Status == true).OrderBy(i=>i.DisplayOrder).ThenByDescending(i=>i.ModifiedDate).ToList();
            return slider;
        }

        public TblUser getUserByID(long Id)
        {
            TblUser user = da.TblUser.Where(s => s.ID == Id).SingleOrDefault();
            return user;
        }

        public TblCompany getInfoCompany(string COMPANY_CD)
        {
            TblCompany slider = da.TblCompany.Where(s => s.COMPANY_CD == COMPANY_CD).SingleOrDefault();
            return slider;
        }

        #endregion

        #region LIST
        public IEnumerable<Slide> SearchSlideList(DataTableModel dt, Slide model, out int total_row)
        {
            var list = da.Slide.Where(i=>i.del_flg == model.del_flg).ToList();
            if(model.Type > 0){
                list = list.Where(i=>i.Type == model.Type).ToList();
            }
            if(model.Status.HasValue){
                list = list.Where(i=>i.Status == model.Status).ToList();
            }

            total_row = list.Count();
            list = list.OrderBy(i => i.DisplayOrder).ThenByDescending(i => i.ModifiedDate).ToList();

            return list;
        }

        #endregion
    }
}