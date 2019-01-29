using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class QuartzNetController : Controller
    {
        EFDBHelper<Models.api_quartz> dbhelper = new EFDBHelper<Models.api_quartz>();
        //
        // GET: /Admin/QuartzNet/

        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// 获取api列表;
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public JsonResult getlist(int page, int limit)
        {
            //int rows = 0;
            int rows = 0;
            var list = dbhelper.Page(page, limit, out rows, u => u.quartzname, null);
            //Models.api_items
            return Json(new
            {
                code = 0,
                msg = "",
                count = rows,
                data = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult dataSave(Models.api_quartz entity)
        {
            try
            {
                if (entity != null)
                {
                    entity.mod_date = DateTime.Now;
                }

                if (entity.id <= 0)
                {
                    dbhelper.Insert(entity);
                    dbhelper.SaveChanges();
                    dbhelper.SaveChanges();
                }
                else
                {
                    dbhelper.Update(entity);
                    dbhelper.SaveChanges();
                }
                return Json(new { access = true });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }

        }



        public ActionResult Start()
        {
            return RedirectToAction("Index", "QuartzNet");//跳转
        }

        public ActionResult Stop()
        {

            return RedirectToAction("Index", "QuartzNet");//跳转
        }

        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            //Models.Model_QuartzNetItem entity = new Models.Model_QuartzNetItem();
            Models.api_quartz entity = dbhelper.FindById(id);
            if (entity ==null )
            {
                entity = new Models.api_quartz(); 
                //新增;
                entity.quartzname = "新建";
               
                entity.crontrigger = "/5 * * ? * *";
                entity.quartzstatus = "停止";
                entity.quartznote = "每5秒钟执行一次";

                entity.jobtype = "Http";
                entity.jobpars = "http://";
                ViewBag.entity = entity;
            }
            else
            {
                ViewBag.entity = entity;
            }
            return View();//
        }


        [ValidateInput(false)]
        public ActionResult Save(Models.Model_QuartzNetItem entity)
        {
            if (entity != null)
            {
                if (entity.Quartzname == null)
                    entity.Quartzname = "";
            }

            if (entity.Id <= 0)
            {
                //新增;
                QuartzNetService.Add(entity);
            }
            else
            {
                QuartzNetService.Update(entity);
            }


            return RedirectToAction("Index", "QuartzNet");//跳转
        }
        public ActionResult Join(int id)
        {
              Models.Model_QuartzNetItem entity = QuartzNetService.GetById(id);
              if (entity.Quartzstatus == "启动")
              {
                  entity.Quartzstatus = "停止";
                  QuartzNetService.UpdateStatus(entity);
              }
              else
              {
                  entity.Quartzstatus = "启动";
                  QuartzNetService.UpdateStatus(entity);
              }
            //

            return RedirectToAction("Index", "QuartzNet");//跳转
        }


        public JsonResult QzJoin(int id)
        {
            try
            {
                 Models.api_quartz entity = dbhelper.FindById(id);
                 if (entity != null)
                 {
                     if (entity.quartzstatus == "启动")
                     {
                         entity.quartzstatus = "停止";
                         dbhelper.Update(entity);
                         dbhelper.SaveChanges();
                     }
                     else
                     {
                         entity.quartzstatus = "启动";
                         dbhelper.Update(entity);
                         dbhelper.SaveChanges();
                     }

                 }

                return Json(new { access = true });
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }
        

        public ActionResult Del(int id)
        {
            Models.Model_QuartzNetItem entity = QuartzNetService.GetById(id);
            if (entity == null)
            {
                return RedirectToAction("Index", "QuartzNet");//跳转
            }
            //
            QuartzNetService.Delete(entity);


            return RedirectToAction("Index", "QuartzNet");//跳转
        }

    }
}
