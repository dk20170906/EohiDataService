using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Threading;
using System.Data;
using System.Collections;
using Newtonsoft.Json;
namespace WFServerWeb
{
    /// <summary>
    /// WSWF 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。
    // [System.Web.Script.Services.ScriptService]
    public class WSWF : System.Web.Services.WebService
    {
        /// <summary>
        /// 发起流程命令处理
        /// </summary>
        /// <param name="WFName">流程名称</param>
        /// <param name="WFDataID">数据表相对应数据ID</param>
        /// <param name="OpeManID">发起人ID</param>        
        private string WFStartDeal(string WFName, string WFDataID, string OpeManID)
        {
            try
            {
                string WFID = CWFManager.GetLastVersionWFID(WFName);

                if (WFID == null || WFID=="")
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("工作流" + WFName + "不存在。");
                    return WFGlobal.ErrInfo;
                }

                string StartNodeID = CNodeManager.GetStartNodeID(WFID);
                if (StartNodeID == null || StartNodeID == "")
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("工作流" + WFID + "没有起始节点。");
                    return WFGlobal.ErrInfo;
                }

                string NewInstanceID = Guid.NewGuid().ToString();

                //记录工作流实例
                string PutDownInstanceResult=CInstanceManager.PutDownNewInstance(WFName, WFDataID, OpeManID, WFID, NewInstanceID);
                if (PutDownInstanceResult != WFGlobal.success)
                    return PutDownInstanceResult;

                //将工作流实例的审批人设定为发起人
                string PutDownApproverResult = CApprovalManager.PutDownApprover(NewInstanceID, StartNodeID, OpeManID, 1,"");
                if(PutDownApproverResult!= WFGlobal.success)
                    return PutDownApproverResult;

                //流程流转
                return WFTransmitDeal(NewInstanceID, StartNodeID, EApprovalOpinion.同意, OpeManID, "");
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("发起工作流操作异常。工作流名称：" + WFName + "，发起人ID：" + OpeManID + "，数据ID" + WFDataID + "，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 流程流转命令处理
        /// </summary>
        /// <param name="InstanceID">流程实例的ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="ApprovalOpinion">申批意见，同意，不同意，驳回上级，驳回至发起人</param>
        /// <param name="OpeManID">申批人ID</param>
        /// <param name="ApprovalReason">申批备注</param>
        private string WFTransmitDeal(string InstanceID, string NodeID, EApprovalOpinion ApprovalOpinion, string OpeManID, string ApprovalReason)
        {
            try 
            { 
                //获取并执行审批脚本
                string script = CNodeManager.GetApprovalScript(NodeID, ApprovalOpinion);
                if (script != null && script != "")
                {
                    CScriptOpe.ScriptExec(script, InstanceID);
                }

                //记录审批意见和状态
                string UpdateApprovalOpinionResult = CApprovalManager.UpdateApprovalOpinion(InstanceID, NodeID, ApprovalOpinion, OpeManID, ApprovalReason);
                if (UpdateApprovalOpinionResult !=WFGlobal.success)
                    return UpdateApprovalOpinionResult;

                //判断是否可以流转到下一级
                string NextNodeID = CNodeManager.GetNextNodeIDByCurrentNodeID(NodeID, ApprovalOpinion);
                if (CNodeManager.IsNodeThroughEnable(InstanceID, NextNodeID))
                {
                    //记录该节点所有审批人的审批状态
                    string UpdateNodeApprovalStatusResult=CApprovalManager.UpdateNodeApprovalStatus(InstanceID, NodeID, EApprovalStatus.Complete);
                    if (UpdateNodeApprovalStatusResult != WFGlobal.success)
                        return UpdateNodeApprovalStatusResult;

                    if (ApprovalOpinion == EApprovalOpinion.驳回上级)
                    {
                        return CReturnUP.ReturnUp(InstanceID, NodeID, NextNodeID);
                    }
                    else if (ApprovalOpinion == EApprovalOpinion.驳回至发起人)
                    {
                        return CReturnToStart.ReturnToStart(InstanceID, NodeID, NextNodeID);
                    }
                    else if (ApprovalOpinion == EApprovalOpinion.不同意)
                    {
                        return CGoEnd.GoEnd(InstanceID, NodeID, NextNodeID);
                    }
                    else
                    {
                        return CNodeManager.GoNextNodesFromOtherNode(InstanceID, NodeID);
                    }
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("流程流转异常：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 接收发起流程命令
        /// </summary>
        /// <param name="WFName">流程名称</param>
        /// <param name="WFDataID">数据表相对应数据ID</param>
        /// <param name="OpeManID">发起人ID</param>
        [WebMethod]
        public string WFStart(string WFName, string WFDataID, string OpeManID)
        {
            return WFStartDeal(WFName, WFDataID, OpeManID);
        }

        /// <summary>
        /// 接收流程流转命令
        /// </summary>
        /// <param name="InstanceID">流程实例的ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="ApprovalOpinion">申批意见，同意，不同意，驳回上级，驳回至发起人</param>
        /// <param name="OpeManID">申批人ID</param>
        /// <param name="ApprovalReason">申批备注</param>
        [WebMethod]
        public string WFTransmit(string InstanceID, string NodeID, EApprovalOpinion ApprovalOpinion, string OpeManID, string ApprovalReason)
        {
            return WFTransmitDeal(InstanceID, NodeID, ApprovalOpinion, OpeManID, ApprovalReason);
        }

        /// <summary>
        /// 启动工作流服务
        /// </summary>
        [WebMethod]
        public void StartServer()
        {
            if (WFGlobal.ServerStarted == false)
            {
                WFGlobal.ServerStarted = true;
            }
        }

        /// <summary>
        /// 工作流服务命令处理
        /// </summary>
        private void WFServer()
        {
            while (WFGlobal.ServerStarted)
            {
                while (WFGlobal.StartWFCmdArr.Count > 0)
                {
                    struCmdStartWF stcsw;
                    lock (WFGlobal.StartWFCmdArr)
                        stcsw = (struCmdStartWF)WFGlobal.StartWFCmdArr.Dequeue();
                    WFStartDeal(stcsw.WFName, stcsw.WFDataID, stcsw.OpeManID);
                    Thread.Sleep(10);
                }

                while (WFGlobal.WFTransmitCmdArr.Count > 0)
                {
                    struCmdWFTransmit stwft;
                    lock (WFGlobal.WFTransmitCmdArr)
                        stwft= (struCmdWFTransmit)WFGlobal.WFTransmitCmdArr.Dequeue();
                    WFTransmitDeal(stwft.InstanceID, stwft.NodeID, stwft.ApprovalOpinion, stwft.OpeManID, stwft.ApprovalReason);
                    Thread.Sleep(10);
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 结束工作流服务
        /// </summary>
        [WebMethod]
        public void StopServer()
        {
            if(WFGlobal.ServerStarted==true)
                WFGlobal.ServerStarted = false;
        }

        /// <summary>
        /// 结束工作流实例
        /// </summary>
        /// <param name="InstanceID"></param>
        [WebMethod]
        public string TerminateInstance(string InstanceID)
        {
            try
            {
                if (WFGlobal.ServerStarted == true)
                {
                    return CInstanceManager.SetInstanceComplete(InstanceID);
                }
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("停止工作流实例操作失败，工作流服务未启动。工作流实例ID：" + InstanceID);
                return WFGlobal.ErrInfo;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("停止工作流实例操作失败，工作流服务未启动。工作流实例ID：" + InstanceID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 获取活动流程实例
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetInstanceActive()
        {
            try
            {
                return CInstanceManager.GetInstance("SELECT f.flowchart_name,i.[flowchart_id],i.[flowchart_version],i.[instance_id],i.[cre_date] ,u.user_name,i.[cre_man],i.[instance_status],i.[instance_message] FROM [kailifon].[dbo].[a_flowchart_instance] as i left join " + CTableName.FlowChart + " as f on(f.flowchart_id=i.flowchart_id) left join a_user as u on (i.cre_man=u.user_id) where i.[instance_status]='" + EInstanceStatus.Active.ToString() + "'");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有流程实例
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetInstanceAll()
        {
            try
            {
                return CInstanceManager.GetInstance("SELECT f.flowchart_name,i.[flowchart_id],i.[flowchart_version],i.[instance_id],i.[cre_date] ,u.user_name,i.[cre_man],i.[instance_status],i.[instance_message] FROM [kailifon].[dbo].[a_flowchart_instance] as i left join " + CTableName.FlowChart + " as f on(f.flowchart_id=i.flowchart_id) left join a_user as u on (i.cre_man=u.user_id) ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取活动流程实例的dt数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetInstanceAllDataTable()
        {
            try
            {
                string strsql = "SELECT f.flowchart_name,i.[flowchart_id],i.[flowchart_version],i.[instance_id],i.[cre_date] ,u.user_name,i.[cre_man],i.[instance_status],i.[instance_message] FROM [kailifon].[dbo].[a_flowchart_instance] as i left join " + CTableName.FlowChart + " as f on(f.flowchart_id=i.flowchart_id) left join a_user as u on (i.cre_man=u.user_id) ";
                return CDataHelper.GetDataTable(strsql);
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流实例操作异常。异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取流程实例的流转路径
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetInstanceFlowHistory(string InstanceID)
        {
            try
            {
                return CInstanceManager.GetInstanceFlowHistory(InstanceID);
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流实例流转历史操作异常。实例ID：" + InstanceID + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
        
        void WriteLog(string str)
        {
            CLog.WriteLog(str);
        }
    }

    /// <summary>
    /// 表示流程流转信息的结构体
    /// </summary>
    public struct struWFJoin
    {
        public string StartNode;    //开始节点ID
        public string EndNode;  //结束节点ID
        public string SourcePoint;  //开始节点输出端点ID
        public string TargetPoint;  //结束节点输入端点ID
        public string Line; //两节点之间的连接线ID
        public struWFJoin(string StartNode, string EndNode, string SourcePoint, string TargetPoint, string Line)
        {
            this.StartNode = StartNode;
            this.EndNode = EndNode;
            this.SourcePoint = SourcePoint;
            this.TargetPoint = TargetPoint;
            this.Line = Line;
        }
    }

    /// <summary>
    /// 表示工作流实例信息的结构体
    /// </summary>
    public struct struInstance
    {
        public string FlowName; //工作流名称
        public string FlowID;   //工作流ID
        public string FlowVersion;  //工作流版本号
        public string InstanceID;   //工作流实例ID
        public string CreateDate;   //工作流实例发起日期
        public string CreateManName;    //工作流实例发起人名称
        public string CreateManID;  //工作流实例发起人ID
        public string EInstanceStatus;  //工作流实例状态
        public string InstanceMessage;  //工作流实例有关描述信息
        public struInstance(string FlowName, string FlowID, string FlowVersion, string InstanceID, string CreateDate, string CreateManName, string CreateManID, string EInstanceStatus, string InstanceMessage)
        {
            this.FlowName = FlowName;
            this.FlowID = FlowID;
            this.FlowVersion = FlowVersion;
            this.InstanceID = InstanceID;
            this.CreateDate = CreateDate;
            this.CreateManName = CreateManName;
            this.CreateManID = CreateManID;
            this.EInstanceStatus = EInstanceStatus;
            this.InstanceMessage = InstanceMessage;
        }
    }
}
