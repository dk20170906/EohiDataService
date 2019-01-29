using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class ApiLinkController : Controller
    {
        EFDBHelper<Models.api_links> dbhelper = new EFDBHelper<Models.api_links>();
        //
        // GET: /Admin/ApiLink/

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
            int rows = 0;
            var list = dbhelper.Page(page, limit, out rows, u => u.id, null);
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

        public ActionResult Edit(int id)
        {
            Models.api_links item = dbhelper.FindById(id);
            //Models.Model_ApiItem entity = new Models.Model_ApiItem();
            if (item == null)
            {
                //新增;
                item = new Models.api_links();
                item.linkname = "";
                item.linkstring = "";
                item.linktype = "";
                ViewBag.entity = item;
            }
            else
            {
                // ApiItemService.GetById(id);
                ViewBag.entity = item;
            }
            return View();//
        }


        [HttpPost]
        public JsonResult dataSave(Models.api_links entity)
        {
            try
            {
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

        public JsonResult Del(int id)
        {
            try
            {
                Models.api_links item = dbhelper.FindById(id);
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
