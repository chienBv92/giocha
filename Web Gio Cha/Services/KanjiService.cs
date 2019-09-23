//using Web_Gio_Cha.Da;
//using Web_Gio_Cha.EF;
//using Web_Gio_Cha.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Transactions;
//using System.Web;
//using Web_Gio_Cha.Resources;
//using Web_Gio_Cha.Services;

//namespace KanjiWeb.Services
//{
//    public class KanjiService : BaseService
//    {
//        #region REGIST/ UPDATE
//        public long InsertKanjiTotal(KanjiModel model)
//        {
//            int res = 0;
//            // Declare new DataAccess object
//            KanjiDa dataAccess = new KanjiDa();

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    Kanji Kanji = new Kanji();
//                    Kanji.level = model.level;
//                    Kanji.NumK = model.NumK;
//                    Kanji.kanji1 = model.kanji1.Length > 0 ? model.kanji1.Trim() : "";
//                    Kanji.HanViet = model.HanViet.Length > 0 ? model.HanViet.Trim() : "";
//                    Kanji.meaning = model.meaning.Length > 0 ? model.meaning.Trim() : "";
//                    Kanji.Image = model.Image;
//                    Kanji.draw = model.draw;
//                    Kanji.Mind = model.Mind;
//                    Kanji.Note = model.Note;
//                    Kanji.display = model.display;
//                    Kanji.del_flg = Constant.DeleteFlag.NON_DELETE;
//                    Kanji.INS_DATE = DateTime.Now;
//                    Kanji.INS_USER_ID = SessionModel.USER_ID;

//                    res = dataAccess.InsertKanji(Kanji);

//                    if (res > 0)
//                    {
//                        foreach (var kun in model.listAmKun)
//                        {
//                            kun.Kanid = res;
//                            var getMaxAmKun = dataAccess.getMaxAmKun(kun);
//                            kun.order_disp = getMaxAmKun + 1;
//                            kun.del_flg = Constant.DeleteFlag.NON_DELETE;
//                            kun.INS_DATE = DateTime.Now;
//                            kun.INS_USER_ID = SessionModel.USER_ID;
//                            dataAccess.InsertAmKun(kun);
//                        }

//                        foreach (var on in model.listAmOn)
//                        {
//                            on.Kanid = res;
//                            var getMaxAmOn = dataAccess.getMaxAmOn(on);
//                            on.order_disp = getMaxAmOn + 1;
//                            on.del_flg = Constant.DeleteFlag.NON_DELETE;
//                            on.INS_DATE = DateTime.Now;
//                            on.INS_USER_ID = SessionModel.USER_ID;
//                            dataAccess.InsertAmOn(on);
//                        }

//                        foreach (var content in model.listContent)
//                        {
//                            content.Kanid = res;
//                            var getMaxAmOn = dataAccess.getMaxContentKanji(content);
//                            content.order_disp = getMaxAmOn + 1;
//                            content.del_flg = Constant.DeleteFlag.NON_DELETE;
//                            content.INS_DATE = DateTime.Now;
//                            content.INS_USER_ID = SessionModel.USER_ID;
//                            dataAccess.InsertContentKanji(content);
//                        }

//                        foreach (var exam in model.listExample)
//                        {
//                            exam.Kanid = res;
//                            var getMaxAmOn = dataAccess.getMaxExampleKanji(exam);
//                            exam.order_disp = getMaxAmOn + 1;
//                            exam.del_flg = Constant.DeleteFlag.NON_DELETE;
//                            exam.INS_DATE = DateTime.Now;
//                            exam.INS_USER_ID = SessionModel.USER_ID;
//                            dataAccess.InsertExampleKanji(exam);
//                        }
//                    }

//                    if (res <= 0)
//                        transaction.Dispose();
//                    transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }
//            return res;
//        }

//        public long UpdateKanjiTotal(KanjiModel model)
//        {
//            int res = 0;
//            // Declare new DataAccess object
//            KanjiDa dataAccess = new KanjiDa();

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    Kanji Kanji = new Kanji();
//                    Kanji.id = model.id;
//                    Kanji.level = model.level;
//                    Kanji.NumK = model.NumK;
//                    Kanji.kanji1 = model.kanji1.Length > 0 ? model.kanji1.Trim() : "";
//                    Kanji.HanViet = model.HanViet.Length > 0 ? model.HanViet.Trim() : "";
//                    Kanji.meaning = model.meaning.Length > 0 ? model.meaning.Trim() : "";
//                    Kanji.Image = model.Image;
//                    Kanji.draw = model.draw;
//                    Kanji.Mind = model.Mind;
//                    Kanji.Note = model.Note;
//                    Kanji.display = model.display;
//                    Kanji.del_flg = Constant.DeleteFlag.NON_DELETE;
//                    Kanji.UPD_DATE = DateTime.Now;
//                    Kanji.UPD_USER_ID = SessionModel.USER_ID;

//                    res = dataAccess.UpdateKanji(Kanji);

//                    if (res > 0)
//                    {
//                        foreach (var kun in model.listAmKun)
//                        {
//                            if (kun.Kanid > 0)
//                            {
//                                kun.INS_DATE = DateTime.Now;
//                                kun.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.UpdateAmKun(kun);
//                            }
//                            else
//                            {
//                                kun.Kanid = res;
//                                var getMaxAmKun = dataAccess.getMaxAmKun(kun);
//                                kun.order_disp = getMaxAmKun + 1;
//                                kun.del_flg = Constant.DeleteFlag.NON_DELETE;
//                                kun.INS_DATE = DateTime.Now;
//                                kun.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.InsertAmKun(kun);
//                            }
//                        }

//                        foreach (var on in model.listAmOn)
//                        {
//                            if (on.Kanid > 0)
//                            {
//                                on.INS_DATE = DateTime.Now;
//                                on.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.UpdateAmOn(on);
//                            }
//                            else
//                            {
//                                on.Kanid = res;
//                                var getMaxAmOn = dataAccess.getMaxAmOn(on);
//                                on.order_disp = getMaxAmOn + 1;
//                                on.del_flg = Constant.DeleteFlag.NON_DELETE;
//                                on.INS_DATE = DateTime.Now;
//                                on.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.InsertAmOn(on);
//                            }
//                        }

//                        foreach (var content in model.listContent)
//                        {
//                            if (content.Kanid > 0)
//                            {
//                                content.INS_DATE = DateTime.Now;
//                                content.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.UpdateContentKanji(content);
//                            }
//                            else
//                            {
//                                content.Kanid = res;
//                                var getMaxAmOn = dataAccess.getMaxContentKanji(content);
//                                content.order_disp = getMaxAmOn + 1;
//                                content.del_flg = Constant.DeleteFlag.NON_DELETE;
//                                content.INS_DATE = DateTime.Now;
//                                content.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.InsertContentKanji(content);
//                            }
//                        }

//                        foreach (var exam in model.listExample)
//                        {
//                            if (exam.Kanid > 0)
//                            {
//                                exam.INS_DATE = DateTime.Now;
//                                exam.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.UpdateExampleKanji(exam);
//                            }
//                            else
//                            {
//                                exam.Kanid = res;
//                                var getMaxAmOn = dataAccess.getMaxExampleKanji(exam);
//                                exam.order_disp = getMaxAmOn + 1;
//                                exam.del_flg = Constant.DeleteFlag.NON_DELETE;
//                                exam.INS_DATE = DateTime.Now;
//                                exam.INS_USER_ID = SessionModel.USER_ID;
//                                dataAccess.InsertExampleKanji(exam);
//                            }
//                        }
//                    }

//                    if (res <= 0)
//                        transaction.Dispose();
//                    transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }
//            return res;
//        }

//        //public long UpdateUser(UserAccountModel model)
//        //{
//        //    long res = 0;
//        //    // Declare new DataAccess object
//        //    UserAccountDa dataAccess = new UserAccountDa();
//        //    using (var transaction = new TransactionScope())
//        //    {
//        //        TblUserAccount entity = new TblUserAccount();

//        //        entity.USER_ID = model.USER_ID = model.USER_ID_HIDDEN;
//        //        entity.USER_NAME = model.USER_NAME;
//        //        entity.SHOP_NAME = model.SHOP_NAME;
//        //        entity.AREA = model.AREA;
//        //        entity.USER_CITY = model.USER_CITY;
//        //        entity.USER_DISTRICT = model.USER_DISTRICT;
//        //        entity.USER_TOWN = model.USER_TOWN;
//        //        entity.USER_ADDRESS = model.USER_ADDRESS;
//        //        entity.USER_PHONE = model.USER_PHONE;
//        //        entity.INS_DATE = Utility.GetCurrentDateTime();
//        //        entity.PASSWORD_LAST_UPDATE_DATE = Utility.GetCurrentDateTime();
//        //        entity.UPD_DATE = Utility.GetCurrentDateTime();

//        //        res = dataAccess.UpdateUser(entity);
//        //        if (res <= 0)
//        //            transaction.Dispose();
//        //        transaction.Complete();
//        //    }
//        //    return res;
//        //}
//        #endregion

//        #region LIST
//        public IEnumerable<KanjiModel> SearchKanjiList(DataTableModel dt, KanjiModel model, out int total_row)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            IEnumerable<KanjiModel> results = dataAccess.SearchKanjiList(dt, model, out total_row);
//            return results;
//        }

//        #endregion

//        #region DELETE
//        public bool DeleteKanji(long KANJI_ID = 0)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            int result = 0;

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    result = dataAccess.DeleteKanji(KANJI_ID);

//                    if (result > 0)
//                        transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Dispose();
//                    result = -1;
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }

//            return (result > 0);
//        }

//        public bool DeleteAmOn(long KanjiId = 0, long OnId = 0)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            int result = 0;

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    result = dataAccess.DeleteAmOn(KanjiId, OnId);

//                    if (result > 0)
//                        transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Dispose();
//                    result = -1;
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }

//            return (result > 0);
//        }

//        public bool DeleteAmKun(long KanjiId = 0, long KunId = 0)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            int result = 0;

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    result = dataAccess.DeleteAmKun(KanjiId, KunId);

//                    if (result > 0)
//                        transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Dispose();
//                    result = -1;
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }

//            return (result > 0);
//        }

//        public bool DeleteContent(long KanjiId = 0, long ContentId = 0)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            int result = 0;

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    result = dataAccess.DeleteContent(KanjiId, ContentId);

//                    if (result > 0)
//                        transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Dispose();
//                    result = -1;
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }

//            return (result > 0);
//        }

//        public bool DeleteExample(long KanjiId = 0, long ExampleId = 0)
//        {
//            KanjiDa dataAccess = new KanjiDa();

//            int result = 0;

//            using (var transaction = new TransactionScope())
//            {
//                try
//                {
//                    result = dataAccess.DeleteExample(KanjiId, ExampleId);

//                    if (result > 0)
//                        transaction.Complete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Dispose();
//                    result = -1;
//                    throw new Exception(ex.Message, ex);
//                }
//                finally
//                {
//                    transaction.Dispose();
//                }
//            }

//            return (result > 0);
//        }

//        #endregion

//    }
//}