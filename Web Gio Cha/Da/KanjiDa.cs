//using KanjiWeb.EF;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using KanjiWeb.Resources;
//using KanjiWeb.Models;
//using System.Text;
//using System.Data.Entity;

//namespace KanjiWeb.Da
//{
//    public class KanjiDa : BaseDa
//    {
//        public SessionModel sessionModel = (SessionModel)HttpContext.Current.Session["USER_SESSION"];

//        #region INSERT/UPDATE

//        public bool CheckExistKanji(string Kanji)
//        {
//            Kanji = Kanji.Trim();
//            var result = da.Kanji.Where(x => x.kanji1 == Kanji && x.del_flg == Constant.DeleteFlag.NON_DELETE).SingleOrDefault();

//            if (result != null)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public int InsertKanji(Kanji entity)
//        {
//            da.Kanji.Add(entity);
//            da.SaveChanges();
//            return entity.id;
//        }

//        public long InsertAmKun(AmKun entity)
//        {
//            da.AmKun.Add(entity);
//            da.SaveChanges();
//            return entity.order_disp;
//        }

//        public long InsertAmOn(AmOn entity)
//        {
//            da.AmOn.Add(entity);
//            da.SaveChanges();
//            return entity.order_disp;
//        }

//        public long InsertContentKanji(ContentKanji entity)
//        {
//            da.ContentKanji.Add(entity);
//            da.SaveChanges();
//            return entity.order_disp;
//        }

//        public long InsertExampleKanji(ExampleKanji entity)
//        {
//            da.ExampleKanji.Add(entity);
//            da.SaveChanges();
//            return entity.order_disp;
//        }

//        public int getMaxAmKun(AmKun entity)
//        {
//            int max = 0;
//            var existAmKun = da.AmKun.Any(x => x.Kanid == entity.Kanid);
//            if (existAmKun)
//            {
//                max = da.AmKun.Where(x => x.Kanid == entity.Kanid).Max(x => x.order_disp);
//            }
//            return max;
//        }

//        public int getMaxAmOn(AmOn entity)
//        {
//            int max = 0;
//            var existAmOn = da.AmOn.Any(x => x.Kanid == entity.Kanid);
//            if (existAmOn)
//            {
//                max = da.AmOn.Where(x => x.Kanid == entity.Kanid).Max(x => x.order_disp);
//            }
//            return max;
//        }

//        public int getMaxContentKanji(ContentKanji entity)
//        {
//            int max = 0;
//            var existContent = da.ContentKanji.Any(x => x.Kanid == entity.Kanid);
//            if (existContent)
//            {
//                max = da.ContentKanji.Where(x => x.Kanid == entity.Kanid).Max(x => x.order_disp);
//            }
//            return max;
//        }

//        public int getMaxExampleKanji(ExampleKanji entity)
//        {
//            int max = 0;
//            var exist = da.ExampleKanji.Any(x => x.Kanid == entity.Kanid);
//            if (exist)
//            {
//                max = da.ExampleKanji.Where(x => x.Kanid == entity.Kanid).Max(x => x.order_disp);
//            }
//            return max;
//        }

//        // Update
//        public int UpdateKanji(Kanji entity)
//        {
//            var kan = da.Kanji.Find(entity.id);
//            if (kan != null)
//            {
//                //kan.HanViet = entity.HanViet;
//                try
//                {
//                    // set data
//                    kan.kanji1 = entity.kanji1;
//                    kan.level = entity.level;
//                    kan.NumK = entity.NumK;
//                    kan.HanViet = entity.HanViet;
//                    kan.meaning = entity.meaning;
//                    kan.Image = entity.Image;
//                    kan.draw = entity.draw;
//                    kan.Mind = entity.Mind;
//                    kan.Note = entity.Note;
//                    kan.display = entity.display;
//                    kan.del_flg = Constant.DeleteFlag.NON_DELETE;
//                    kan.UPD_DATE = entity.UPD_DATE;
//                    kan.UPD_USER_ID = entity.UPD_USER_ID;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return entity.id;
//        }

//        public long UpdateAmKun(AmKun entity)
//        {
//            var kan = da.AmKun.Where(x => x.Kanid == entity.Kanid && x.order_disp == entity.order_disp).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.AmKun1 = entity.AmKun1;
//                    kan.display = entity.display;
//                    kan.UPD_DATE = entity.UPD_DATE;
//                    kan.UPD_USER_ID = entity.UPD_USER_ID;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public long UpdateAmOn(AmOn entity)
//        {
//            var kan = da.AmOn.Where(x => x.Kanid == entity.Kanid && x.order_disp == entity.order_disp).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.AmOn1 = entity.AmOn1;
//                    kan.display = entity.display;
//                    kan.UPD_DATE = entity.UPD_DATE;
//                    kan.UPD_USER_ID = entity.UPD_USER_ID;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public long UpdateContentKanji(ContentKanji entity)
//        {
//            var kan = da.ContentKanji.Where(x => x.Kanid == entity.Kanid && x.order_disp == entity.order_disp).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.Bo = entity.Bo;
//                    kan.Name = entity.Name;
//                    kan.Meaning = entity.Meaning;
//                    kan.display = entity.display;
//                    kan.UPD_DATE = entity.UPD_DATE;
//                    kan.UPD_USER_ID = entity.UPD_USER_ID;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public long UpdateExampleKanji(ExampleKanji entity)
//        {
//            var kan = da.ExampleKanji.Where(x => x.Kanid == entity.Kanid && x.order_disp == entity.order_disp).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.word = entity.word;
//                    kan.hiragara = entity.hiragara;
//                    kan.HanViet = entity.HanViet;
//                    kan.Meaning = entity.Meaning;
//                    kan.display = entity.display;
//                    kan.UPD_DATE = entity.UPD_DATE;
//                    kan.UPD_USER_ID = entity.UPD_USER_ID;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }


//        #endregion

//        #region LIST
//        public IEnumerable<KanjiModel> SearchKanjiList(DataTableModel dt, KanjiModel model, out int total_row)
//        {
//            StringBuilder sql = new StringBuilder();

//            List<KanjiModel> dataList = new List<KanjiModel>();
//            List<Kanji> ListKanji = da.Kanji.Where(s => s.del_flg == model.del_flg && s.display == model.display).ToList();

//            if (!String.IsNullOrEmpty(model.level))
//            {
//                ListKanji = ListKanji.Where(s => s.level.Contains(model.level)).ToList();
//            }
//            if (model.NumK > 0)
//            {
//                ListKanji = ListKanji.Where(s => s.NumK == model.NumK).ToList();
//            }
//            if (!String.IsNullOrEmpty(model.kanji1))
//            {
//                ListKanji = ListKanji.Where(s => s.kanji1.Equals(model.kanji1)).ToList();
//            }
//            if (!String.IsNullOrEmpty(model.HanViet))
//            {
//                ListKanji = ListKanji.Where(s => s.HanViet.Contains(model.HanViet)).ToList();
//            }
//            if (!String.IsNullOrEmpty(model.meaning))
//            {
//                ListKanji = ListKanji.Where(s => s.meaning.Contains(model.meaning)).ToList();
//            }

//            foreach (var data in ListKanji)
//            {
//                KanjiModel kanji = new KanjiModel();

//                // set data
//                kanji.id = data.id;
//                kanji.kanji1 = data.kanji1;
//                kanji.level = data.level;
//                kanji.NumK = data.NumK;
//                kanji.HanViet = data.HanViet;
//                kanji.meaning = data.meaning;
//                kanji.Image = data.Image;
//                kanji.draw = data.draw;
//                kanji.Mind = data.Mind;
//                kanji.Note = data.Note;
//                kanji.del_flg = data.del_flg;
//                kanji.display = data.display;
//                kanji.INS_DATE = data.INS_DATE;
//                kanji.INS_USER_ID = data.INS_USER_ID;
//                kanji.UPD_DATE = data.UPD_DATE;
//                kanji.UPD_USER_ID = data.UPD_USER_ID;

//                var listAmOn = da.AmOn.Where(i => i.Kanid == data.id).ToList();
//                kanji.listAmOn = listAmOn;
//                var listAmKun = da.AmKun.Where(i => i.Kanid == data.id).ToList();
//                kanji.listAmKun = listAmKun;
//                var listContent = da.ContentKanji.Where(i => i.Kanid == data.id).ToList();
//                kanji.listContent = listContent;
//                var listExample = da.ExampleKanji.Where(i => i.Kanid == data.id).ToList();
//                kanji.listExample = listExample;

//                dataList.Add(kanji);
//            }

//            total_row = ListKanji.Count();

//            return dataList;

//        }

//        public KanjiModel getInfoKanji(long kanjiId)
//        {
//            KanjiModel model = new KanjiModel();

//            Kanji kanji = da.Kanji.Where(s => s.id == kanjiId).SingleOrDefault();
//            if (kanji != null)
//            {
//                // set data
//                model.id = kanji.id;
//                model.kanji1 = kanji.kanji1;
//                model.level = kanji.level;
//                model.NumK = kanji.NumK;
//                model.HanViet = kanji.HanViet;
//                model.meaning = kanji.meaning;
//                model.Image = kanji.Image;
//                model.draw = kanji.draw;
//                model.Mind = kanji.Mind;
//                model.Note = kanji.Note;
//                model.del_flg = kanji.del_flg;
//                model.display = kanji.display;
//                model.INS_DATE = kanji.INS_DATE;
//                model.INS_USER_ID = kanji.INS_USER_ID;
//                model.UPD_DATE = kanji.UPD_DATE;
//                model.UPD_USER_ID = kanji.UPD_USER_ID;

//                var listAmOn = da.AmOn.Where(i => i.Kanid == model.id && i.del_flg == Constant.DeleteFlag.NON_DELETE).ToList();
//                model.listAmOn = listAmOn;
//                var listAmKun = da.AmKun.Where(i => i.Kanid == model.id && i.del_flg == Constant.DeleteFlag.NON_DELETE).ToList();
//                model.listAmKun = listAmKun;
//                var listContent = da.ContentKanji.Where(i => i.Kanid == model.id && i.del_flg == Constant.DeleteFlag.NON_DELETE).ToList();
//                model.listContent = listContent;
//                var listExample = da.ExampleKanji.Where(i => i.Kanid == model.id && i.del_flg == Constant.DeleteFlag.NON_DELETE).ToList();
//                model.listExample = listExample;
//            }

//            return model;
//        }
//        #endregion

//        #region DELETE
//        public int DeleteKanji(long KANJI_ID = 0)
//        {
//            var sessionModel = (SessionModel)HttpContext.Current.Session["USER_SESSION"];

//            var kan = da.Kanji.Where(x => x.id == KANJI_ID).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.del_flg = Constant.DeleteFlag.DELETE;

//                    kan.UPD_DATE = DateTime.Now;
//                    kan.UPD_USER_ID = sessionModel != null ? sessionModel.USER_ID : 0;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.id;
//        }

//        public int DeleteAmOn(long KanjiId = 0, long OnId = 0)
//        {

//            var kan = da.AmOn.Where(x => x.Kanid == KanjiId && x.order_disp == OnId).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.del_flg = Constant.DeleteFlag.DELETE;

//                    kan.UPD_DATE = DateTime.Now;
//                    kan.UPD_USER_ID = sessionModel != null ? sessionModel.USER_ID : 0;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public int DeleteAmKun(long KanjiId = 0, long KunId = 0)
//        {

//            var kan = da.AmKun.Where(x => x.Kanid == KanjiId && x.order_disp == KunId).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.del_flg = Constant.DeleteFlag.DELETE;

//                    kan.UPD_DATE = DateTime.Now;
//                    kan.UPD_USER_ID = sessionModel != null ? sessionModel.USER_ID : 0;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public int DeleteContent(long KanjiId = 0, long ContentId = 0)
//        {

//            var kan = da.ContentKanji.Where(x => x.Kanid == KanjiId && x.order_disp == ContentId).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.del_flg = Constant.DeleteFlag.DELETE;

//                    kan.UPD_DATE = DateTime.Now;
//                    kan.UPD_USER_ID = sessionModel != null ? sessionModel.USER_ID : 0;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        public int DeleteExample(long KanjiId = 0, long ExampleId = 0)
//        {

//            var kan = da.ExampleKanji.Where(x => x.Kanid == KanjiId && x.order_disp == ExampleId).SingleOrDefault();
//            if (kan != null)
//            {
//                try
//                {
//                    // set data
//                    kan.del_flg = Constant.DeleteFlag.DELETE;

//                    kan.UPD_DATE = DateTime.Now;
//                    kan.UPD_USER_ID = sessionModel != null ? sessionModel.USER_ID : 0;
//                    da.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//                return 0;

//            return kan.order_disp;
//        }

//        #endregion
//    }
//}