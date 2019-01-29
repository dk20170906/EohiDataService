using EohiDataServerApi.CommonUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFServerWeb;

namespace EohiDataServerApi.Areas.WF.Controllers
{
    public class WorkflowTrackingController : Controller
    {
        WSWF wSWF = new WSWF();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="instance_status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult GetInstanceActiveDt(string user_name=null,string instance_status=null,int page=1,int rows=20)
        {
            try
            {
              DataTable dataTable=  wSWF.GetInstanceAllDataTable();
            
                if (dataTable!=null&&dataTable.Rows.Count>0)
                {
                    var list = ToolUnit.ModelConvertHelper<WFT>.ConvertToModel(dataTable).ToList();
                    if (user_name!=null&&user_name.Length>0&&user_name!="")
                    {
                        list = list.FindAll(u => u.user_name == user_name);
                    }
                    if (instance_status!=null&&instance_status.Length>0&&instance_status!="")
                    {
                        list = list.FindAll(u => u.instance_status == instance_status);
                    }
                    list= list.Skip((page - 1) * rows).Take(rows).ToList();
                    return Json(new
                    {
                        success = true,
                        msg = "请求成功",
                        total = list.Count,
                        rows = list
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        success = true,
                        msg = "请求成功，暂无数据"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    msg = ex.Message
                }, JsonRequestBehavior.AllowGet);
                throw;
            }

           
        }

        /// <summary>
        /// 终止流程
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <returns></returns>
        public JsonResult TerminateInstance(string InstanceID)
        {
            try
            {
                wSWF.TerminateInstance(InstanceID);
                return Json(new
                {
                    success = true,
                    msg = "终止成功"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    msg = "终止成功"+ex.Message
                }, JsonRequestBehavior.AllowGet);
                throw;
            }

        }



        class WFT
        {
            public string flowchart_name { get; set; }
            public string flowchart_id { get; set; }
            public string flowchart_version { get; set; }
            public string instance_id { get; set; }
            public DateTime cre_date { get; set; }
            public string user_name { get; set; }
            public string cre_man { get; set; }
            public string instance_status { get; set; }
            public string instance_message { get; set; }
        }


    }
}
