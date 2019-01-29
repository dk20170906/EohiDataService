using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFServerWeb
{
    public class CReturnToStart
    {
        /// <summary>
        /// 将工作流流转到开始节点
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="CurrentNodeID">当前节点ID</param>
        public static string ReturnToStart(string InstanceID, string CurrentNodeID, string StartNodeID)
        {
            try 
            { 
                //将当前所有节点的审批状态设置为完成状态
                string UpdateInstanceApprovalStatusResult = CApprovalManager.UpdateInstanceApprovalStatus(InstanceID, EApprovalStatus.Complete);
                if (UpdateInstanceApprovalStatusResult != WFGlobal.success)
                    return UpdateInstanceApprovalStatusResult;

                //流程流转到开始节点
                return CNodeManager.NodeTransfer(InstanceID, CurrentNodeID, StartNodeID);
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("返回到开始节点操作异常。实例ID："+InstanceID+"，当前节点ID："+CurrentNodeID+"，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }
    }
}