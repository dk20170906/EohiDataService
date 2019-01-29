using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.ThreeD.Controllers
{      
    /// <summary>
///   数据看板
/// </summary>
    public class DataReadingBoardController : Controller
    {
        EFDBHelper<Models.api_databoard> boardDbHelper = new EFDBHelper<Models.api_databoard>();
        EFDBHelper<Models.api_databoard_items> itemsDbHelper = new EFDBHelper<Models.api_databoard_items>();
        #region 看板定义
         /// <summary>
         /// 看板定义的分页列表
         /// </summary>
         /// <param name="page"></param>
         /// <param name="rows"></param>
         /// <returns></returns>
        public JsonResult DRBList(int page,int rows)
        {
            int total = 0;
            var list = boardDbHelper.Page(page, rows, out total, u => u.id, null);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("total", total);
            dic.Add("rows", list);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }
           /// <summary>
           /// 新增/修改看板定义对像
           /// </summary>
           /// <param name="api_Databoard"></param>
           /// <returns></returns>
        public JsonResult DRBAdd(Models.api_databoard api_Databoard)
        {
            api_Databoard.mod_date = DateTime.Now;
            if (api_Databoard.id>0)
            {
                boardDbHelper.Update(api_Databoard);
            }
            else
            {
                api_Databoard.mod_man = "";
                api_Databoard.cre_date = DateTime.Now;
                api_Databoard.cre_man = "";
                api_Databoard.boardno = Guid.NewGuid().ToString();
                boardDbHelper.Insert(api_Databoard);
            }
            var m = boardDbHelper.SaveChanges();
            if (m>0)
            {
                return Json(new
                {
                    success = true,
                    message = "保存成功",
                    data = m
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                message = "保存失败"
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 通过id返回看板定义对像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SelectDRBById(int id)
        {
            var drb = boardDbHelper.FindById(id);
            return Json(drb, JsonRequestBehavior.AllowGet);
        }
              /// <summary>
              /// 通过id删除看析定义对像
              /// </summary>
              /// <param name="id"></param>
              /// <returns></returns>
        public JsonResult DeleteDRB(int id)
        {
            var drb = boardDbHelper.FindById(id);
            boardDbHelper.Delete(drb);
            var m = boardDbHelper.SaveChanges();
            if (m>0)
            {
                return Json(new
                {
                    success = true,
                    data = m,
                    message = "删除成功"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                message = "删除失败"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 看板定义设计
        /// </summary>
        /// <returns></returns>
        public JsonResult DesignDRB()
        {


            return Json(null, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///复制看板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult boardcopy (string boardno)
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


                    return Json(new { access = true, result = board, msg ="复制看板成功" });
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
        #endregion
    }
}
