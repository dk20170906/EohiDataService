using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class DataBoardDesignerController : Controller
    {
        EFDBHelper<Models.api_databoard> boardDbHelper = new EFDBHelper<Models.api_databoard>();
        EFDBHelper<Models.api_databoard_items> itemsDbHelper = new EFDBHelper<Models.api_databoard_items>();
        //
        // GET: /Admin/DataBoardDesigner/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Design(string  boardno)
        {
            //跳转到设计页面;
            ViewBag.boardno = boardno;
            return View();
        }

        public ActionResult Design2(string boardno)
        {
            //跳转到设计页面;
            ViewBag.boardno = boardno;
            return View();
        }
        //编辑
        public ActionResult Edit(int id)
        {
            Models.api_databoard item = boardDbHelper.FindById(id);
            //Models.Model_ApiItem entity = new Models.Model_ApiItem();
            if (item == null)
            {
                //新增;
                item = new Models.api_databoard();
                item.boardno = Guid.NewGuid().ToString("N");
                ViewBag.entity = item;
            }
            else
            {
                //修改
                ViewBag.entity = item;
            }
            return View();//
        }


        public JsonResult boardList(int page, int limit)
        {
            try
            {
                int rows = 0;
                var list = boardDbHelper.Page(page, limit, out rows, u => u.id, null);
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
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message },JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult boardGet(string boardno)
        {
            try
            {
                Models.api_databoard board = boardDbHelper.GetAll(u => u.boardno == boardno, u => u.id).FirstOrDefault();
                if (board != null)
                {

                    return Json(new { access = true, msg = "", result = board });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + boardno + "]对应的记录", result = board });
                }
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }
        public JsonResult boardSavconfig(string boardno, string mainoption)
        {
            try
            {
                Models.api_databoard board = boardDbHelper.GetAll(u => u.boardno == boardno, u => u.id).FirstOrDefault();
                if (board != null)
                {
                    board.mainoption = mainoption;
                    boardDbHelper.Update(board);
                    boardDbHelper.SaveChanges();
                    return Json(new { access = true, result = board });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + boardno + "]对应的记录", result = board });
                }
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }
        [HttpPost]
        public JsonResult boardSave(Models.api_databoard entity)
        {
            try
            {
                if (entity.id <= 0)
                {

                    entity.mod_man = "";
                    entity.mod_date = DateTime.Now;
                    entity.cre_date = DateTime.Now;
                    entity.cre_man = "";
                    entity.boardno = Guid.NewGuid().ToString();
                    boardDbHelper.Insert(entity);
                    boardDbHelper.SaveChanges();
                    //dbhelper.SaveChanges();
                }
                else
                {
                    entity.mod_date = DateTime.Now;
                    boardDbHelper.Update(entity);
                    boardDbHelper.SaveChanges();
                }
                return Json(new { access = true, result = entity });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }

        }

       [HttpPost]
        public JsonResult boardDelete(int id)
        {
            try
            {

                Models.api_databoard board = boardDbHelper.FindById(id);//.GetAll(u => u.itemno == itemno, u => u.id).FirstOrDefault();
                if (board != null)
                {
                    boardDbHelper.Delete(board);
                    boardDbHelper.SaveChanges();
                    return Json(new { access = true, msg = "" });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + id + "]对应的记录", result = board });
                }

                // return Json(new { access = true });
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }

        }

       [HttpPost]
       public JsonResult boardcopy(string boardno)
       {
           try
           {

               Models.api_databoard board = boardDbHelper.GetAll(u => u.boardno == boardno, u => u.id).FirstOrDefault();
               if (board != null)
               {

                   string newboardno = Guid.NewGuid().ToString("N");
                   //执行存储过程； 复制;
                   string sql = "exec pr_app_databoard_copy @boardno,@newboardno,@userid";
                   SqlParameter[] pars = new SqlParameter[]
                       {
                           new SqlParameter("@boardno",board.boardno),
                             new SqlParameter("@newboardno",newboardno),
                            new SqlParameter("@userid",""),
                       };
                   DBHelper.ExecuteNonQuery(sql, pars);


                   return Json(new { access = true, result = board, msg = newboardno });
               }
               else
               {
                   return Json(new { access = false, msg = "未找到[" + boardno + "]对应的记录", result = board });
               }
               // return Json(new { access = true });
           }
           catch (Exception exp)
           {
               return Json(new { access = false, msg = exp.Message });
           }

       }


       [HttpPost]
       public JsonResult boardpub(string boardno)
       {
           try
           {

               Models.api_databoard board = boardDbHelper.GetAll(u => u.boardno == boardno, u => u.id).FirstOrDefault();
               if (board != null)
               {
                   //
                   if (board.pubno == null || board.pubno == "")
                   {
                       //
                       var pubo = Guid.NewGuid().ToString(); ;

                       board.pubno = pubo;
                       boardDbHelper.Update(board);
                       boardDbHelper.SaveChanges();



                   }


                   //执行存储过程； 发布;
                   string sql = "exec pr_app_databoard_pub @boardno,@userid";
                   SqlParameter[] pars = new SqlParameter[]
                       {
                           new SqlParameter("@boardno",board.boardno),
                            new SqlParameter("@userid",""),
                       };
                   DBHelper.ExecuteNonQuery(sql, pars);

                  
                   return Json(new { access = true, result = board });
               }
               else
               {
                   return Json(new { access = false, msg = "未找到[" + boardno + "]对应的记录", result = board });
               }
               // return Json(new { access = true });
           }
           catch (Exception exp)
           {
               return Json(new { access = false, msg = exp.Message });
           }

       }
        


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemSave(Models.api_databoard_items entity)
        {
            try
            {
                if (entity.id <= 0)
                {
                    itemsDbHelper.Insert(entity);
                    itemsDbHelper.SaveChanges();
                }
                else
                {
                    itemsDbHelper.Update(entity);
                    itemsDbHelper.SaveChanges();
                }
                return Json(new { access = true, result = entity });
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }


        /// <summary>
        /// 更新位置
        /// </summary>
        /// <param name="itemno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="widht"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemLocationSave(string itemno,int left,int top,int width,int height,int itemindex)
        {
            try
            {
                Models.api_databoard_items item = itemsDbHelper.GetAll(u => u.itemno == itemno,u=>u.id).FirstOrDefault();
                if (item != null)
                {
                    item.itemx = left;
                    item.itemy = top;
                    item.itemw = width;
                    item.itemh = height;
                    item.itemindex = itemindex;
                    //更新
                    itemsDbHelper.Update(item);
                    itemsDbHelper.SaveChanges();
                    return Json(new { access = true,msg="", result = item });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + itemno + "]对应的记录", result = item });
                }
                
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }

        /// <summary>
        /// 更新选项
        /// </summary>
        /// <param name="itemno"></param>
        /// <param name="itemoption"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemOptionSave(string itemno, string itemoption)
        {
            try
            {
                Models.api_databoard_items item = itemsDbHelper.GetAll(u => u.itemno == itemno, u => u.id).FirstOrDefault();
                if (item != null)
                {
                    item.itemoption = itemoption;
                    //更新
                    itemsDbHelper.Update(item);
                    itemsDbHelper.SaveChanges();
                    return Json(new { access = true, msg = "", result = item });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + itemno + "]对应的记录", result = item });
                }

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }


        /// <summary>
        /// 更新选项
        /// </summary>
        /// <param name="itemno"></param>
        /// <param name="itemoption"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemDataSave(string itemno, string itemdata)
        {
            try
            {
                Models.api_databoard_items item = itemsDbHelper.GetAll(u => u.itemno == itemno, u => u.id).FirstOrDefault();
                if (item != null)
                {
                    item.itemdata = itemdata;
                    //更新
                    itemsDbHelper.Update(item);
                    itemsDbHelper.SaveChanges();
                    return Json(new { access = true, msg = "", result = item });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + itemno + "]对应的记录", result = item });
                }

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult itemList(string boardno)
        {
            try
            {

                int rows = 0;
                var list = itemsDbHelper.GetAll(u => u.boardno == boardno,u=>u.itemindex );

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

        [HttpPost]
        public JsonResult itemDelete(string itemno)
        {
            try
            {

                Models.api_databoard_items item = itemsDbHelper.GetAll(u => u.itemno == itemno, u => u.id).FirstOrDefault();
                if (item != null)
                {
                    itemsDbHelper.Delete(item);
                    itemsDbHelper.SaveChanges();
                    return Json(new { access = true, msg = "" });
                }
                else
                {
                    return Json(new { access = false, msg = "未找到[" + itemno + "]对应的记录", result = item });
                }
               
               // return Json(new { access = true });
            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }
        }
    }
}
