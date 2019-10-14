using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Gio_Cha.Da;
using Web_Gio_Cha.EF;
using Web_Gio_Cha.Models;
using Web_Gio_Cha.Models.Define;
using Web_Gio_Cha.Resources;
using Web_Gio_Cha.Services;
using WebDuhoc.Models.Define;
using PagedList;

namespace Web_Gio_Cha.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        #region REGISTER/ UPDATE
        public ActionResult SliderEdit(long newsId = 0)
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            Slide model = new Slide();
            CommonService comService = new CommonService();
            SliderDa dataAccess = new SliderDa();
            model.Status = Constant.Status.ACTIVE;

            if (newsId > 0)
            {
                Slide infor = new Slide();
                infor = dataAccess.getInfoSlider(newsId);
                model = infor != null ? infor : model;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide model)
        {
            try
            {
                using (SlideService service = new SlideService())
                {
                    if (ModelState.IsValid)
                    {
                        bool isNew = false;
                        bool success = false;
                        if (model.ID == 0)
                        {
                            isNew = true;
                            success = service.InsertSlide(model) > 0;
                            JsonResult result = Json(new { isNew = isNew, success = success }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                        else
                        {
                            isNew = false;
                            success = service.UpdateSlide(model) > 0;
                            JsonResult result = Json(new { isNew = isNew, success = success }, JsonRequestBehavior.AllowGet);
                            return result;
                        }
                    }
                    else
                    {
                        var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    }

                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                System.Web.HttpContext.Current.Session["ERROR"] = ex;
                return new EmptyResult();
            }
        }

        #endregion

        #region LIST
        [HttpGet]
        public ActionResult SlideList()
        {
            CmnEntityModel currentUser = Session["CmnEntityModel"] as CmnEntityModel;

            if (currentUser == null || currentUser.IsAdmin == false)
            {
                return RedirectToAction("Login", "Login");
            }

            Slide model = new Slide();

            CommonService comService = new CommonService();

            return View(model);
        }


        [HttpPost]
        public ActionResult SlideList(DataTableModel dt, Slide condition)
        {
            if (ModelState.IsValid)
            {
                using (SlideService service = new SlideService())
                {
                    int total_row = 0;
                    var dataList = service.SearchSlideList(dt, condition, out total_row);

                    int order = 1;
                    int lastItem = dt.iDisplayLength + dt.iDisplayStart;

                    var result = Json(
                        new
                        {
                            sEcho = dt.sEcho,
                            iTotalRecords = total_row,
                            iTotalDisplayRecords = total_row,
                            aaData = (from i in dataList
                                      select new object[]
                                {
                                    i.ID,
                                    order++,
                                    i.Type == 1 ? "Slide Top": "Slide Left",
                                    i.DisplayOrder,
                                    i.Status == true? "Hiển thị" : "Ẩn",
                                    i.CreatedDate != null ? i.CreatedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.ModifiedDate != null ? i.ModifiedDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                                    i.del_flg
                                })

                        });

                    result.MaxJsonLength = Int32.MaxValue;
                    return result;
                }
            }
            else
            {
                var ErrorMessages = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            }
            return new EmptyResult();
        }

        #endregion

        #region VIEW SLIDER
        public ActionResult RenderSlideTop()
        {
            SliderDa dataAccess = new SliderDa();

            var listSlideTop = dataAccess.getListSlideView(Constant.SlideType.Top);

            return PartialView(listSlideTop);
        }

        public ActionResult RenderSlideLeft()
        {
            SliderDa dataAccess = new SliderDa();

            
            var listSlideTop = dataAccess.getListSlideView(Constant.SlideType.Left);

            return PartialView(listSlideTop);
        }
        #endregion

    }
}