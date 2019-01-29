using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
namespace WFServerWeb
{
    public class CApprovalManager
    {
        public static string UpdateInstanceApprovalStatus(string InstanceID,EApprovalStatus ApprStu)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartReceiver + " set [approval_status]='" + ApprStu.ToString() + "' where instance_id='" + InstanceID + "' and [approval_status]='" + EApprovalStatus.Active.ToString() + "'") < 0)
                {
                    WFGlobal.ErrInfo=CLog.PutDownErrInfo("更新实例审批状态操作失败，实例ID：" + InstanceID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch (Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新实例审批状态操作异常，实例ID：" + InstanceID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 更新审批意见
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="ApprovalOpinion"></param>
        /// <param name="OpeManID"></param>
        /// <param name="ApprovalReason"></param>
        public static string UpdateApprovalOpinion(string InstanceID, string NodeID, EApprovalOpinion ApprovalOpinion, string OpeManID, string ApprovalReason)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartReceiver + " set [approval_opinion]='" + ApprovalOpinion + "',operate_time='" + DateTime.Now.ToString() + "',approval_status='" + EApprovalStatus.Complete.ToString() + "',approval_reason='" + ApprovalReason + "' where [instance_id]='" + InstanceID + "' and [node_id]='" + NodeID + "' and [user_id]='" + OpeManID + "' and [approval_status]='" + EApprovalStatus.Active.ToString() + "'") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录实例" + InstanceID + "审批意见异常。审批节点：" + NodeID + "，审批人：" + OpeManID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录实例" + InstanceID + "审批意见异常。审批节点：" + NodeID + "，审批人：" + OpeManID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 更新节点所有审批人的审批状态
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="AppStu">审批状态</param>
        public static string UpdateNodeApprovalStatus(string InstanceID,string NodeID,EApprovalStatus AppStu)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartReceiver + " set [approval_status]='" + AppStu.ToString() + "' where instance_id='" + InstanceID + "' and node_id='" + NodeID + "'") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新节点审批状态操作失败。实例ID：" + InstanceID + "，节点ID：" + NodeID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch (Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新节点审批状态操作异常。实例ID：" + InstanceID + "，节点ID：" + NodeID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 更新审批人的审批状态
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="AppStu">审批状态</param>
        public static string UpdateApprovalStatus(string InstanceID, string NodeID,string ApproverID, EApprovalStatus AppStu)
        {
            try
            {
                if(CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartReceiver + " set [approval_status]='" + AppStu.ToString() + "' where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and user_id='" + ApproverID + "'")<0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新审批状态操作失败。实例ID："+InstanceID+"，节点ID："+NodeID+"，审批人ID："+ApproverID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新审批状态操作失败。实例ID：" + InstanceID + "，节点ID：" + NodeID + "，审批人ID：" + ApproverID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 记录节点的审批人
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="OpeManID"></param>
        /// <param name="ApprovalNum">表示该节点第几次审批</param>
        public static string PutDownApprover(string InstanceID, string NodeID, string OpeManID, int ApprovalNum, string ApprovalNote)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("insert into " + CTableName.FlowChartReceiver + "(instance_id,node_id,user_id,operate_time,approval_status,approval_num,approval_note) values('" + InstanceID + "','" + NodeID + "','" + OpeManID + "','" + DateTime.Now.ToString() + "','" + EApprovalStatus.Active.ToString() + "','" + ApprovalNum.ToString() + "','" + ApprovalNote + "')") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录审批人操作失败。工作流实例ID：" + InstanceID + "，节点ID：" + NodeID + "，操作人ID：" + OpeManID + "，操作次数：" + ApprovalNum.ToString());
                    return WFGlobal.ErrInfo;
                }

                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录审批人操作异常。工作流实例ID：" + InstanceID + "，节点ID：" + NodeID + "，操作人ID：" + OpeManID + "，操作次数：" + ApprovalNum.ToString()+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 获取该节点最后一次审批是第几次审批
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static int GetLastApprovalNum(string InstanceID, string NodeID)
        {
            try
            {
                string strApprovalNum = CDataHelper.GetData("select top 1 approval_num from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' order by approval_num desc");
                int ApprovalNum = 0;
                if (strApprovalNum != null && strApprovalNum != "")
                {
                    ApprovalNum = Convert.ToInt32(strApprovalNum);
                }
                return ApprovalNum;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取最后的审批次数操作异常。实例ID："+InstanceID+"，节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static DataTable GetApprover(string InstanceID, string NodeID)
        {
            try
            {
                string ApproverScript = CDataHelper.GetData("select [operatorscript] from " + CTableName.FlowChartNode + " where [node_id]='" + NodeID + "'");
                DataTable dtApprover = (DataTable)CScriptOpe.ScriptExec(ApproverScript, InstanceID);
                return dtApprover;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取审批人操作异常。实例ID：，节点ID：，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取审批提示信息
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static string GetApprovalNote(string InstanceID, string NodeID)
        {
            try
            {
                string strApprovalNote = null;
                string ApprovalNoteScript = CDataHelper.GetData("select [approvalnotescript] from " + CTableName.FlowChartNode + " where [node_id]='" + NodeID + "'");
                if(ApprovalNoteScript!=null&&ApprovalNoteScript!="")
                    strApprovalNote = (string)CScriptOpe.ScriptExec(ApprovalNoteScript, InstanceID);
                return strApprovalNote;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取审批提示操作异常。实例ID：，节点ID：，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
    }
}