using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WFServerWeb;

namespace EohiDataServerApi.Areas.WF.Controllers
{
    public class WFServiceController : Controller
    {
        private WSWF wSWF = new WSWF();
        // GET: /WF/WFService/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 服务状态
        /// </summary>
        /// <returns></returns>
        public JsonResult GetServiceStatus()
        {
            if (WFGlobal.ServerStarted)
            {
                return Json(new
                {
                    statuCode = 0,
                    success = true,
                    msg = "WF服务正在运行"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    statuCode = 0,
                    success =false,
                    msg = "WF服务已停止运行"
                }, JsonRequestBehavior.AllowGet);
            }
          
        }
        public JsonResult WFServiceStart()
        {

            wSWF.StartServer();
            return Json(new
            {
                statuCode = 0,
                success = true,
                msg = "WF服务启动成功"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WFServiceStop()
        {
            wSWF.StopServer();
            return Json(new
            {
                statuCode = 0,
                success = true,
                msg = "WF服务已停止"
            }, JsonRequestBehavior.AllowGet);
        }
        //
        /// <summary>
        /// 接收流程流转命令
        /// </summary>
        /// <param name="InstanceID">流程实例的ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="ApprovalOpinion">申批意见，同意，不同意，驳回上级，驳回至发起人</param>
        /// <param name="OpeManID">申批人ID</param>
        /// <param name="ApprovalReason">申批备注</param>
        public string CWFTransmit(string InstanceID, string NodeID, EApprovalOpinion ApprovalOpinion, string OpeManID, string ApprovalReason)
        {
            return wSWF.WFTransmit(InstanceID, NodeID, ApprovalOpinion, OpeManID, ApprovalReason);
        }
        /// <summary>
        /// 接收发起流程命令
        /// </summary>
        /// <param name="WFName">流程名称</param>
        /// <param name="WFDataID">数据表相对应数据ID</param>
        /// <param name="OpeManID">发起人ID</param>
        public string CWFStart(string WFName, string WFDataID, string OpeManID)
        {
            return wSWF.WFStart(WFName, WFDataID, OpeManID);
        }

        /// <summary>
        /// 结束工作流实例
        /// </summary>
        /// <param name="InstanceID">流程实例的ID</param>
        public string CTerminateInstance(string InstanceID)
        {
            return wSWF.TerminateInstance(InstanceID);
        }

      // public JsonResult 

    }
}
