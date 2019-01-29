using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Controllers
{
    public class DataViewController : Controller
    {
        EFDBHelper<Models.api_databoard_pub> boardDbHelper = new EFDBHelper<Models.api_databoard_pub>();
        EFDBHelper<Models.api_databoard_items_pub> itemsDbHelper = new EFDBHelper<Models.api_databoard_items_pub>();

        //
        // GET: /DataView/

        public ActionResult Index(string pn)
        { //跳转到设计页面;
            ViewBag.pubno = pn;
            return View();
        }

        public ActionResult IndexV2(string pn)
        { //跳转到设计页面;
            ViewBag.pubno = pn;
            return View();
        }
        [HttpPost]
        public JsonResult boardGet(string pubno)
        {
            try
            {
                Models.api_databoard_pub board = boardDbHelper.GetAll(u => u.pubno == pubno, u => u.id).FirstOrDefault();
                if (board != null)
                {

                    return Json(new { access = true, msg = "", result = board });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + pubno + "]对应的记录", result = board });
                }
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemList(string pubno)
        {
            try
            {

                int rows = 0;
                var list = itemsDbHelper.GetAll(u => u.pubno == pubno, u => u.itemindex);

                //Models.api_items
                return Json(new
                {
                    access = true,
                    code = 0,
                    msg = "",
                    count = rows,
                    data = list
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }
       
    }
}
