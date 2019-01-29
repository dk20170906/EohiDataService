using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class WebAppController : Controller
    {
        EFDBHelper<Models.api_webapp> dbhelper = new EFDBHelper<Models.api_webapp>();
        //
        // GET: /Admin/WebApp/

        public ActionResult Index()
        {
           
            return View();
        }

        /// <summary>
        /// 获取列表;
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public JsonResult getlist(int page, int limit)
        {
            //int rows = 0;
            //var list = ApiItemService.GetList();
            //Models.kailifonEntities E = new Models.kailifonEntities();
            //var list = E.api_items.Select();

            int rows = 0;
            var list = dbhelper.Page(page, limit, out rows, u => u.webappname, null);

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
        [ValidateInput(false)]
        public JsonResult dataSave(Models.api_webapp entity)
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


        public ActionResult Edit(int id)
        {
            Models.api_webapp entity = dbhelper.FindById(id); 
            if (entity==null )
            {
                entity = new Models.api_webapp();
                //新增;
                ViewBag.entity = entity;
            }
            else
            {
                ViewBag.entity = entity;
            }
            return View();//
        }

        
        public JsonResult Del(int id)
        {
            try
            {
                Models.api_webapp item = dbhelper.FindById(id);
                dbhelper.Delete(item);
                dbhelper.SaveChanges();
                return Json(new { access = true });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }

    }
}
