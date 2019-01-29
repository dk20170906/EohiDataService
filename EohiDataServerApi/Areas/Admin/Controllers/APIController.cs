using EohiDataServerApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EohiDataServerApi.Areas.Admin.Controllers
{
    [ValidateInput(false)]
    public class APIController : Controller
    {
        //
        // GET: /Admin/API/
        EFDBHelper<Models.api_items> dbhelper = new EFDBHelper<Models.api_items>();
        EFDBHelper<Models.api_type_htmlhelp> dbAPITypeHelp = new EFDBHelper<api_type_htmlhelp>();

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
            //var list = ApiItemService.GetList();
            //Models.kailifonEntities E = new Models.kailifonEntities();
            //var list = E.api_items.Select();


            int rows = 0;
            var list = dbhelper.Page(page, limit, out rows, u => u.apiname, null);

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
        public JsonResult dataSave(Models.api_items entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.apiname == null)
                        entity.apiname = "";
                    if (entity.apinote == null)
                        entity.apinote = "";
                    if (entity.apipars == null)
                        entity.apipars = "";
                    if (entity.apiscript == null)
                        entity.apiscript = "";
                    if (entity.apistatus == null)
                        entity.apistatus = "";
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


        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            Models.api_items item = dbhelper.FindById(id);
            //Models.Model_ApiItem entity = new Models.Model_ApiItem();
            if (item == null)
            {
                //新增;
                item = new Models.api_items();
                item.apistatus = "未启用";
                item.apinote = "";
                item.apipars = "";
                item.apiname = "";
                ViewBag.entity = item;
            }
            else
            {
                // ApiItemService.GetById(id);
                ViewBag.entity = item;
            }
            return View();//
        }


        public JsonResult Del(int id)
        {
            try
            {
                Models.api_items item = dbhelper.FindById(id);
                dbhelper.Delete(item);
                dbhelper.SaveChanges();
                return Json(new { access = true });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }


        public ActionResult APIShowHelp(int typeid)
        {
            ViewBag.TypeId = typeid;
            return View();
        }
        public JsonResult APIShowHelpAJAX(int typeid)
        {
            if (typeid > 0)
            {
                var typehelp = dbAPITypeHelp.FirstOrDefault(u => u.TypeId == typeid);
                if (typehelp != null)
                {
                    return Json(new
                    {
                        code = 0,
                        tId = typehelp.id,
                        typeId = typehelp.TypeId,
                        htmlStr = typehelp.HtmlStr,
                        msg = "请求成功"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        code = 1,
                        tId = 0,
                        typeId = typeid,
                        htmlStr = "暂无此项帮助",
                        msg = "暂无此项帮助"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }


        [HttpPost]
        public JsonResult APIShowHelpSave(string HtmlStr, int TypeId, int TId)
        {
            if (TId > 0)
            {

                if (TypeId >= 0)
                {
                    api_type_htmlhelp aPITypeHelp = dbAPITypeHelp.FirstOrDefault(u => u.id == TId);
                    aPITypeHelp.TypeId = TypeId;
                    aPITypeHelp.HtmlStr = HtmlStr;
                    dbAPITypeHelp.Update(aPITypeHelp);

                }
            }
            else
            {
                api_type_htmlhelp aPITypeHelp = new api_type_htmlhelp();
                aPITypeHelp.TypeId = TypeId;
                aPITypeHelp.HtmlStr = HtmlStr;
                dbAPITypeHelp.Insert(aPITypeHelp);
            }
            int codeint = dbAPITypeHelp.SaveChanges();
            if (codeint > 0)
            {
                return Json(new
                {
                    code = codeint,
                    msg = "保存成功",
                },
       JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    code = codeint,
                    msg = "保存失败",
                },
JsonRequestBehavior.AllowGet);
            }
        }



    }
}

