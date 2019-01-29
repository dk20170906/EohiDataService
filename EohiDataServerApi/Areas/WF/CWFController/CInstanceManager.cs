using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Newtonsoft.Json;
namespace WFServerWeb
{
    public class CInstanceManager
    {
        /// <summary>
        /// 将工作流实例设置为错误状态
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="ErrorMessage">错误状态信息</param>
        public static string SetInstanceError(string InstanceID, string ErrorMessage)
        {
            try
            {
                //设置审批状态为无效
                string UpdateInstanceApprovalStatusResult= CApprovalManager.UpdateInstanceApprovalStatus(InstanceID, EApprovalStatus.Invalid);
                if (UpdateInstanceApprovalStatusResult != WFGlobal.success)
                    return UpdateInstanceApprovalStatusResult;

                //设置实例状态为错误
                string UpdateInstanceStatusResult=UpdateInstanceStatus(InstanceID, EInstanceStatus.Error);
                if (UpdateInstanceStatusResult != WFGlobal.success)
                    return UpdateInstanceStatusResult;

                //记录出错信息
                return UpdateInstanceMessage(InstanceID, ErrorMessage);
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("设置工作流实例的错误状态操作异常，工作流实例ID：" + InstanceID + "，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        public static string UpdateInstanceStatus(string InstanceID, EInstanceStatus InsStu)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartInstance + " set [instance_status]='" + InsStu.ToString() + "' where instance_id='" + InstanceID + "'") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新实例状态操作失败。实例ID：" + InstanceID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新实例状态操作异常。实例ID：" + InstanceID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        public static string UpdateInstanceMessage(string InstanceID, string ErrorMessage)
        {
            try
            {
                if (CDataHelper.ExecuteNonQuery("update " + CTableName.FlowChartInstance + " set [instance_message]='" + ErrorMessage + "' where instance_id='" + InstanceID + "'") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新工作流实例信息操作失败。实例ID：" + InstanceID);
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch(Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("更新工作流实例信息操作失异常。实例ID：" + InstanceID+"，异常信息："+ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 将工作流实例的状态设置为Complete
        /// </summary>
        /// <param name="InstanceID"></param>
        public static string SetInstanceComplete(string InstanceID)
        {
            try
            {
                string UpdateInstanceApprovalStatusResult = CApprovalManager.UpdateInstanceApprovalStatus(InstanceID, EApprovalStatus.Complete);
                if (UpdateInstanceApprovalStatusResult != WFGlobal.success)
                    return UpdateInstanceApprovalStatusResult;
                return UpdateInstanceStatus(InstanceID, EInstanceStatus.Complete);
            }
            catch (Exception ex)
            {
                WFGlobal.ErrInfo = CLog.PutDownErrInfo("设置实例为结束状态的操作异常，实例ID：" + InstanceID + "，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }

        /// <summary>
        /// 获取工作流实例的发起人ID
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <returns></returns>
        public static string GetInstanceStartManID(string InstanceID) 
        {
            try
            {
                return CDataHelper.GetData("select cre_man from " + CTableName.FlowChartInstance + " where instance_id='" + InstanceID + "'");
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流实例发起人ID操作异常。实例ID："+InstanceID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取流程实例的流转路径
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <returns></returns>
        public static string GetInstanceFlowHistory(string InstanceID)
        {
            try
            {
                string strHistory = null;
                DataTable dtHistory = CDataHelper.GetDataTable("select node_id,next_node_id from " + CTableName.FlowChartHistory + " where instance_id='" + InstanceID + "' order by ID asc");
                if (dtHistory != null)
                {
                    if (dtHistory.Rows.Count > 0)
                    {
                        struWFJoin[] FlowHistory = new struWFJoin[dtHistory.Rows.Count];
                        for (int i = 0; i < dtHistory.Rows.Count; i++)
                        {
                            string NodeID = dtHistory.Rows[i]["node_id"].ToString();
                            string NextNodeID = dtHistory.Rows[i]["next_node_id"].ToString();
                            string SourcePoint = null;
                            string TargetPoint = null;
                            string Line = null;
                            DataTable dtLine = CDataHelper.GetDataTable("select top 1 sourcepoint,targetpoint,line_id from " + CTableName.FlowChartLine + " where node_id='" + NodeID + "' and next_node_id='" + NextNodeID + "'");
                            if (dtLine != null)
                            {
                                if (dtLine.Rows.Count > 0)
                                {
                                    SourcePoint = dtLine.Rows[0]["sourcepoint"].ToString();
                                    TargetPoint = dtLine.Rows[0]["targetpoint"].ToString();
                                    Line = dtLine.Rows[0]["line_id"].ToString();
                                }
                            }
                            FlowHistory[i] = new struWFJoin(NodeID, NextNodeID, SourcePoint, TargetPoint, Line);
                        }
                        strHistory = JsonConvert.SerializeObject(FlowHistory);
                    }
                }
                return strHistory;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流实例流转历史操作异常。实例ID：，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static string GetInstance(string strsql)
        {
            try
            {
                string strInstance = null;

                DataTable dtInstance = CDataHelper.GetDataTable(strsql);
                if (dtInstance != null)
                {
                    if (dtInstance.Rows.Count > 0)
                    {
                        struInstance[] struInst = new struInstance[dtInstance.Rows.Count];
                        for (int i = 0; i < dtInstance.Rows.Count; i++)
                        {
                            string FlowName = dtInstance.Rows[i]["flowchart_name"].ToString();
                            string FlowID = dtInstance.Rows[i]["flowchart_id"].ToString();
                            string FlowVersion = dtInstance.Rows[i]["flowchart_version"].ToString();
                            string InstanceID = dtInstance.Rows[i]["instance_id"].ToString();
                            string CreateDate = dtInstance.Rows[i]["cre_date"].ToString();
                            string CreateManName = dtInstance.Rows[i]["user_name"].ToString();
                            string CreateManID = dtInstance.Rows[i]["cre_man"].ToString();
                            string EInstanceStatus = dtInstance.Rows[i]["instance_status"].ToString();
                            string InstanceMessage = dtInstance.Rows[i]["instance_message"].ToString();
                            struInst[i] = new struInstance(FlowName, FlowID, FlowVersion, InstanceID, CreateDate, CreateManName, CreateManID, EInstanceStatus, InstanceMessage);
                        }
                        strInstance = JsonConvert.SerializeObject(struInst);
                    }
                }
                return strInstance;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流实例操作异常。SQL语句："+strsql+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 记录新发起的工作流实例
        /// </summary>
        /// <param name="WFName">工作流名称</param>
        /// <param name="WFDataID">工作流实例对应的数据的ID</param>
        /// <param name="OpeManID">操作人</param>
        /// <param name="WFID">工作流ID</param>
        /// <param name="NewInstanceID">新工作流实例ID</param>
        public static string PutDownNewInstance(string WFName, string WFDataID, string OpeManID, string WFID, string NewInstanceID)
        {
            try
            {
                string WFVersion = CWFManager.GetLastVersion(WFName);
                if (WFVersion == null || WFVersion == "")
                {
                    CInstanceManager.SetInstanceError(NewInstanceID, "缺少工作流版本号");
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("工作流实例" + NewInstanceID + "缺少工作流版本号");
                    return WFGlobal.ErrInfo;
                }

                if (CDataHelper.ExecuteNonQuery("insert into " + CTableName.FlowChartInstance + "(flowchart_id,flowchart_version,instance_id,cre_date,cre_man,instance_status) values('" + WFID + "','" + WFVersion + "','" + NewInstanceID + "','" + DateTime.Now.ToString() + "','" + OpeManID + "','" + EInstanceStatus.Active.ToString() + "')") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录工作流实例信息失败。");
                    return WFGlobal.ErrInfo;
                }

                if (CDataHelper.ExecuteNonQuery("insert into " + CTableName.FlowChartPars + "(instance_id,keyvalue) values('" + NewInstanceID + "','" + WFDataID + "')") < 0)
                {
                    WFGlobal.ErrInfo = CLog.PutDownErrInfo("记录工作流数据信息失败。");
                    return WFGlobal.ErrInfo;
                }
                return WFGlobal.success;
            }
            catch (Exception ex)
            {
                WFGlobal.ErrInfo= CLog.PutDownErrInfo("记录工作流实例操作异常。工作流名称："+WFName+"，数据ID："+WFDataID+"，发起人ID："+OpeManID+"，工作流ID："+WFID+"，异常信息：" + ex.Message.ToString());
                return WFGlobal.ErrInfo;
            }
        }
    }
}