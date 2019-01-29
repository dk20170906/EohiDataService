using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
namespace WFServerWeb
{
    public class CNodeManager
    {
        /// <summary>
        /// 获取审批人最后一次审批意见
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="ApproverID"></param>
        /// <returns></returns>
        public static string GetNodeApprovalOpinion(string InstanceID, string NodeID,string ApproverID)
        {
            try
            {
                return CDataHelper.GetData("select top 1 approval_opinion from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and user_id='" + ApproverID + "' order by approval_num desc");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取节点审批意见操作异常。实例ID：，节点ID：，审批人：，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取分支节点的下一级节点ID
        /// </summary>
        /// <param name="WFID"></param>
        /// <param name="InstanceID"></param>
        /// <param name="SwitchNodeID">分支节点ID</param>
        /// <returns></returns>
        public static string GoNextNodesFromSwitchNode(string InstanceID, string SwitchNodeID)
        {
            try
            {
                string[] SwitchNames = CNodeManager.GetSwitchName(InstanceID, SwitchNodeID);
                for (int i = 0; i < SwitchNames.Length; i++)
                {
                    string SwitchID = CNodeManager.GetSwitchIDBySwitchName(SwitchNodeID, SwitchNames[i]);
                    //根据switchid获取下一级节点ID
                    string NextNodeID = CNodeManager.GetNextNodeIDBySwitchID(SwitchID);
                    string NodeTransferResult = NodeTransfer(InstanceID, SwitchNodeID, NextNodeID);
                    if (NodeTransferResult != WFGlobal.success)
                        return NodeTransferResult;
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("流程流转操作异常，工作流实例ID：" + InstanceID + "，当前节点ID：" + SwitchNodeID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }


        /// <summary>
        /// 运行流程节点处理脚本
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="ProcessNodeID"></param>
        private static void RunProcessScript(string InstanceID, string ProcessNodeID)
        {
            try
            {
                string ProcessScript = CNodeManager.GetNodeScript(ProcessNodeID, EScriptType.ProcessScript);
                if (ProcessScript != null && ProcessScript !="")
                    CScriptOpe.ScriptExec(ProcessScript, InstanceID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 节点转移，从当前节点进入下一节点
        /// </summary>
        /// <param name="WFID"></param>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <param name="NextNodeID"></param>
        public static string NodeTransfer(string InstanceID, string NodeID, string NextNodeID)
        {
            try
            {
                if (CNodeManager.IsNodeThroughEnable(InstanceID, NextNodeID))
                {
                    //记录流转历史
                    if (CDataHelper.ExecuteNonQuery("insert into " + CTableName.FlowChartHistory + "(instance_id,node_id,next_node_id) values('" + InstanceID + "','" + NodeID + "','" + NextNodeID + "')") < 0)
                    {
                        WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录工作流实例流转节点失败，工作流实例ID：" + InstanceID + "，开始节点ID：" + NodeID + "，结束节点ID：" + NextNodeID);
                        return WFGlobal.ErrInfo;
                    }

                    string NodeType = CNodeManager.GetNodeType(NextNodeID);
                    if (NodeType == CNodeType.SwitchType)
                    {
                        return GoNextNodesFromSwitchNode(InstanceID, NextNodeID);
                    }
                    else if (NodeType == CNodeType.ProcessType)
                    {
                        RunProcessScript(InstanceID, NextNodeID);
                        return CNodeManager.GoNextNodesFromOtherNode(InstanceID, NextNodeID);
                    }
                    else if (NodeType == CNodeType.ApproveType || NodeType == CNodeType.StartType)
                    {
                        return CNodeManager.PutDownNodeApprover(InstanceID, NextNodeID);
                    }
                    else if (NodeType == CNodeType.EndType)
                    {
                        return CInstanceManager.SetInstanceComplete(InstanceID);
                    }
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("节点流转异常，工作流实例ID：" + InstanceID + "，开始节点ID：" + NodeID + "，结束节点ID：" + NextNodeID);
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 通过SwitchID获取下一个节点ID
        /// </summary>
        /// <param name="SwitchID"></param>
        /// <returns></returns>
        public static string GetNextNodeIDBySwitchID(string SwitchID)
        {
            try
            {
                return CDataHelper.GetData("select next_node_id from " + CTableName.FlowChartLine + " where [sourcepoint]='" + SwitchID + "-out'"); ;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过SwitchID获取下一个节点ID操作异常。SwitchID："+SwitchID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取非分支节点的下一级节点ID
        /// </summary>
        /// <param name="WFID"></param>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static string GoNextNodesFromOtherNode(string InstanceID, string NodeID)
        {
            try
            {
                string NextNodeID = CDataHelper.GetData("select next_node_id from " + CTableName.FlowChartLine + " where node_id='" + NodeID + "'");
                if (NextNodeID == null || NextNodeID == "")
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("找不到节点" + NodeID + "的下一个节点。");
                    return WFGlobal.ErrInfo;
                }
                return NodeTransfer(InstanceID, NodeID, NextNodeID);
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("获取节点的下一个节点操作失败。工作流实例ID：" + InstanceID + "，节点ID：" + NodeID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 通过工作流实例ID和分支节点ID，获取当前分支节点所选的分支的名称
        /// </summary>
        /// <param name="InstanceID">工作流实例ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <returns></returns>
        public static string[] GetSwitchName(string InstanceID, string NodeID)
        {
            try
            {
                string[] SwitchNames = null;
                string SwitchScript = CDataHelper.GetData("select switchscript from " + CTableName.FlowChartNode + " where [node_id]='" + NodeID + "'");
                if (SwitchScript == null || SwitchScript == "")
                    return null;
                else
                {
                    object SwitchNameObj = CScriptOpe.ScriptExec(SwitchScript, InstanceID);
                    if (SwitchNameObj == null || SwitchNameObj == "")
                        return null;
                    else
                    {
                        SwitchNames = SwitchNameDecode(SwitchNameObj);
                        return SwitchNames;
                    }
                }
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取节点流转分支操作异常。实例ID："+InstanceID +"，节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 从返回值中分解得到具体的SwitchName
        /// </summary>
        /// <param name="SwitchNameObj"></param>
        /// <returns></returns>
        public static string[] SwitchNameDecode(object SwitchNameObj)
        {
            try
            {
                string[] SwitchNames = null;
                SwitchNames = ((string)(SwitchNameObj)).Split(';');
                return SwitchNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过Switch名称获取SwitchID
        /// </summary>
        /// <param name="SwitchNodeID"></param>
        /// <param name="SwitchName"></param>
        /// <returns></returns>
        public static string GetSwitchIDBySwitchName(string SwitchNodeID, string SwitchName) 
        {
            try
            {
                return CDataHelper.GetData("select [switchid] from " + CTableName.FlowChartSwitchitem + " where [node_id]='" + SwitchNodeID + "' and [switchname]='" + SwitchName + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过SwitchName获取SwitchID操作异常。节点ID："+SwitchNodeID+"，SwitchName："+SwitchName+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
        /// <summary>
        /// 获取节点类型
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static string GetNodeType(string NodeID)
        {
            try
            {
                return CDataHelper.GetData("select [nodetype] from " + CTableName.FlowChartNode + " where [node_id]='" + NodeID + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取节点类型操作异常。节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public static string GetNodeScript(string NodeID, EScriptType ScriptType)
        {
            try
            {
                return CDataHelper.GetData("select " + ScriptType.ToString() + " from " + CTableName.FlowChartNode + " where  node_id='" + NodeID + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取节点脚本操作异常。节点ID："+NodeID+"，脚本类型："+ScriptType.ToString()+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取各个审批意见对应的处理脚本
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="ApprovalOpinion"></param>
        /// <returns></returns>
        public static string GetApprovalScript(string NodeID, EApprovalOpinion ApprovalOpinion)
        {
            try
            {
                return CDataHelper.GetData("select [approvescript] from " + CTableName.FlowChartNodeApproveItem + " where node_id='" + NodeID + "' and [approvename]='" + ApprovalOpinion.ToString() + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断是否可以向下个节点流转
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static bool IsNodeThroughEnable(string InstanceID, string NextNodeID)
        {
            try
            {
                string FlowScript = GetNodeScript(NextNodeID, EScriptType.FlowScript);
                string ScriptResult = null;
                if (FlowScript != "")
                {
                    ScriptResult = CScriptOpe.ScriptExec(FlowScript, InstanceID).ToString();
                }

                if (FlowScript == "" || ScriptResult == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("判断是否可以向下流转操作异常。实例ID："+InstanceID+"，下一个节点ID："+NextNodeID+"，异常信息" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 判断该节点的所有审批人员是否都已完成审批
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static bool IsNodeApprovalComplete(string InstanceID, string NodeID)
        {
            try
            {
                string ReceiverCount = CDataHelper.GetData("select count(*) from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "'");
                string CompleteCount = CDataHelper.GetData("select count(*) from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and approval_status='" + EApprovalStatus.Complete.ToString() + "'");
                if (ReceiverCount == CompleteCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("判断节点审批状态是否已结束操作异常。实例ID："+InstanceID+"，节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 根据不同的审批意见获取不同的下一级节点
        /// </summary>
        /// <param name="NodeID">当前节点ID</param>
        /// <param name="Apo"></param>
        /// <returns></returns>
        public static string GetNextNodeIDByCurrentNodeID(string NodeID, EApprovalOpinion Apo)
        {
            try
            {
                string WFID = CWFManager.GetWFIDByNodeID(NodeID);
                if (Apo == EApprovalOpinion.同意)
                {
                    return CDataHelper.GetData("select next_node_id from " + CTableName.FlowChartLine + " where node_id='" + NodeID + "'");
                }
                else if (Apo == EApprovalOpinion.不同意)
                {
                    return CDataHelper.GetData("select [node_id] from " + CTableName.FlowChartNode + " where [flowchart_id]='" + WFID + "' and [nodetype]='" + CNodeType.EndType + "'");
                }
                else if (Apo == EApprovalOpinion.驳回上级)
                {
                    return GetUpApprovalNodeID(NodeID);
                }
                else
                {
                    return GetStartNodeID(WFID);
                }
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("通过节点ID获取下一个节点ID操作异常。节点ID：" + NodeID + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取上一级审批节点
        /// </summary>
        /// <param name="NodeID">当前节点ID</param>
        /// <returns></returns>
        public static string GetUpApprovalNodeID(string NodeID)
        {
            try
            {
                string UpNodeID = null;
                while (true)
                {
                    UpNodeID = GetUpNodeID(NodeID);
                    string NodeType = CNodeManager.GetNodeType(UpNodeID);
                    if (NodeType != CNodeType.SwitchType && NodeType != CNodeType.ProcessType)
                    {
                        break;
                    }
                    NodeID = UpNodeID;
                }
                return UpNodeID;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("通过节点ID获取上一个审批节点ID操作异常。节点ID：" + NodeID + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
        
        /// <summary>
        /// 获取上一级节点
        /// </summary>
        /// <param name="NodeID">当前节点ID</param>
        /// <returns></returns>
        public static string GetUpNodeID(string NodeID)
        {
            try
            {
                return CDataHelper.GetData("select node_id from " + CTableName.FlowChartLine + " where next_node_id='" + NodeID + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过节点ID获取上一个节点ID操作异常。节点ID：" + NodeID + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <param name="WFID"></param>
        /// <returns></returns>
        public static string GetStartNodeID(string WFID)
        {
            try
            {
                return CDataHelper.GetData("select [node_id] from " + CTableName.FlowChartNode + " where [flowchart_id]='" + WFID + "' and [nodetype]='" + CNodeType.StartType + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流的开始节点操作异常。工作流ID："+WFID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取活动未完成审批的活动节点
        /// </summary>
        /// <returns></returns>
        public static string GetActiveNode(string InstanceID)
        {
            try
            {
                string ActiveNodeStr = "";
                DataTable dtActiveNode = CDataHelper.GetDataTable("select distinct node_id from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and approval_status='" + EApprovalStatus.Active.ToString() + "'");
                if (dtActiveNode != null)
                {
                    for (int i = 0; i < dtActiveNode.Rows.Count; i++)
                    {
                        ActiveNodeStr += dtActiveNode.Rows[i][0].ToString() + ";";
                    }
                    if (ActiveNodeStr.Length > 0)
                    {
                        ActiveNodeStr = ActiveNodeStr.Substring(0, ActiveNodeStr.Length - 1);
                    }
                }
                return ActiveNodeStr;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取活动节点操作异常。实例ID："+InstanceID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 记录节点的审批人
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        public static string PutDownNodeApprover(string InstanceID, string NodeID)
        {
            try
            {
                string nodetype = CNodeManager.GetNodeType(NodeID);
                if (nodetype == null)
                {
                    WFGlobal.ErrInfo=CLog.PutDownErrInfo("获取节点" + NodeID + "的节点类型失败，工作流实例ID："+InstanceID);
                    CInstanceManager.SetInstanceError(InstanceID, WFGlobal.ErrInfo);
                    return WFGlobal.ErrInfo;
                }

                int ApprovalNum = CApprovalManager.GetLastApprovalNum(InstanceID, NodeID);
                //判断是否开始流程
                if (nodetype == CNodeType.StartType)
                {
                    string StartManID = CInstanceManager.GetInstanceStartManID(InstanceID);
                    if (StartManID == null)
                    {
                        WFGlobal.ErrInfo=CLog.PutDownErrInfo("获取流程实例的发起人失败，工作流实例ID：" + InstanceID);
                        CInstanceManager.SetInstanceError(InstanceID, WFGlobal.ErrInfo);
                        return WFGlobal.ErrInfo;
                    }

                    string ApprovalNote = CApprovalManager.GetApprovalNote(InstanceID, NodeID);

                    string PutDownApproverResult = CApprovalManager.PutDownApprover(InstanceID, NodeID, StartManID, ApprovalNum + 1, ApprovalNote);
                    if (PutDownApproverResult != WFGlobal.success)
                        return PutDownApproverResult;
                }
                //判断流程是否结束
                else if (nodetype != CNodeType.EndType)
                {
                    DataTable dtReceiver = CApprovalManager.GetApprover(InstanceID, NodeID);
                    string ApprovalNote = CApprovalManager.GetApprovalNote(InstanceID, NodeID);
                    if (dtReceiver != null)
                    {
                        if (dtReceiver.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtReceiver.Rows.Count; i++)
                            {
                                string ReceiverID = dtReceiver.Rows[i][WFGlobal.UserID].ToString();
                                string PutDownApproverResult = CApprovalManager.PutDownApprover(InstanceID, NodeID, ReceiverID, ApprovalNum + 1, ApprovalNote);
                                if (PutDownApproverResult != WFGlobal.success)
                                    return PutDownApproverResult;
                            }
                        }
                    }
                }
                return WFGlobal.success;
            }
            catch (Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录节点审批人操作异常。工作流实例ID：" + InstanceID + "，节点ID：" + NodeID + "，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }
    }
}