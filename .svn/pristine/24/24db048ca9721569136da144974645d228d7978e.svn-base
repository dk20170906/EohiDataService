using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Microsoft.Scripting.Hosting;
using System.Collections;
namespace WFServerWeb
{
    class CCommonFunc
    {
        public static string GetStartNodeID(string WFID)
        {
            return CDataHelper.GetData("select [node_id] from [a_flowchart_node] where [flowchart_id]='" + WFID + "' and [nodetype]='" + CNodeType.StartType + "'");
        }

        public static string GetWFIDByInstanceID(string InstanceID)
        {
            return CDataHelper.GetData("select [flowchart_id] from a_flowchart_instance where [instance_id]='" + InstanceID + "'");
        }

        public static string GetWFIDByWFName(string WFName)
        {
            return CDataHelper.GetData("select top 1 flowchart_id from a_flowchart where flowchart_name='" + WFName + "' order by flowchart_version desc");
        }

        public static string GetWFVersion(string WFName)
        {
            return CDataHelper.GetData("select top 1 isnull(flowchart_version,'') as flowchart_version from a_flowchart where flowchart_name='" + WFName + "' order by id desc");
        }

        public static string GetNewInstanceID()
        {
            return Guid.NewGuid().ToString();
        }

        public static ArrayList  GetNextNodeID(string WFID, string InstanceID, string CurrentNodeID)
        {
            ArrayList NextNodeArr = new ArrayList();
            string NodeType = GetNodeType(CurrentNodeID);

            if (NodeType == CNodeType.SwitchType)
            {
                NextNodeArr = GetNextNodesBySwitchNode(WFID, InstanceID, CurrentNodeID);
            }
            else
            {
                NextNodeArr = GetNextNodesByUnSwitchNode(WFID, InstanceID, CurrentNodeID);
            }
            return NextNodeArr;
        }

        public static ArrayList GetNextNodesBySwitchNode(string WFID, string InstanceID, string SwitchNodeID)
        {
            ArrayList NextNodeArr = new ArrayList();
            string[] SwitchNames = GetSwitchName(InstanceID, SwitchNodeID);
            for (int i = 0; i < SwitchNames.Length; i++)
            {
                string SwitchName = SwitchNames[i];
                PutDownSwitchNodeSelect(InstanceID, SwitchNodeID, SwitchName);
                string SwitchID = GetSwitchID(WFID, SwitchNodeID, SwitchName);
                string NextNodeID = GetNextNodeIDBySwitchID(SwitchID);

                CCommonFunc.PutDownFlowHistory(InstanceID, SwitchNodeID, NextNodeID);

                string NodeType = GetNodeType(NextNodeID);

                if (NodeType == CNodeType.SwitchType)
                {
                    ArrayList NextNodeArrTmp = GetNextNodesBySwitchNode(WFID, InstanceID, NextNodeID);
                    NextNodeArr.AddRange(NextNodeArrTmp);
                }
                else
                {
                    NextNodeArr.Add(NextNodeID);
                }
            }
            return NextNodeArr;
        }

        public static ArrayList GetNextNodesByUnSwitchNode(string WFID, string InstanceID, string NodeID)
        {
            ArrayList NextNodeArr = new ArrayList();
            string NextNodeID = GetNextNodeIDByCurrentNodeID(NodeID);

            string NodeType = GetNodeType(NextNodeID);
            CCommonFunc.PutDownFlowHistory(InstanceID, NodeID, NextNodeID);
            if (NodeType == CNodeType.SwitchType)
            {
                NextNodeArr = GetNextNodesBySwitchNode(WFID, InstanceID, NextNodeID);
            }
            else
            {
                NextNodeArr.Add(NextNodeID);
            }
            return NextNodeArr;
        }

        public static void PutDownSwitchNodeSelect(string InstanceID, string SwitchNodeID, string SwitchName)
        {
            CDataHelper.ExecuteNonQuery("insert into [a_flowchart_instance_node_receiver]([instance_id],[node_id],[approval_opinion],[operate_time],[user_id],[approval_status],node_status) values('" + InstanceID + "','" + SwitchNodeID + "','" + SwitchName + "','" + DateTime.Now.ToString() + "','SwitchNode','"+ApprovalStatus.Complete+"','"+NodeStatus.Complete.ToString()+"')");
        }

        public static string GetUpNodeID(string InstanceID, string CurrentNodeID)
        {
            string WFID = CCommonFunc.GetWFIDByInstanceID(InstanceID);
            string UpNodeID = null;
            UpNodeID = GetUpNodeIDByCurrentNodeID(CurrentNodeID);
            if (UpNodeID == null)
            {
                return null;
            }
            if (IsSwitchNode(UpNodeID))
            {
                UpNodeID=GetNodeIDBySwitchID(UpNodeID);
            }
            string NodeType = GetNodeType(UpNodeID);
            while (NodeType == CNodeType.SwitchType)
            {
                UpNodeID = GetUpNodeIDByCurrentNodeID(UpNodeID);
                if (IsSwitchNode(UpNodeID))
                {
                    UpNodeID = GetNodeIDBySwitchID(UpNodeID);
                }
                NodeType = GetNodeType(UpNodeID);
            }

            return UpNodeID;
        }

        public static bool IsSwitchNode(string NodeID)
        {
            bool SwitchNode = false;
            string strNodeFoundNum = CDataHelper.GetData("select count(*) from [a_flowchart_node_switchitem] where [switchid]='" + NodeID + "'");
            int NodeFoundNum = Convert.ToInt32(strNodeFoundNum);
            if (NodeFoundNum > 0)
            {
                SwitchNode = true;
            }
            return SwitchNode;
        }

        public static string GetStartMan(string InstanceID)
        {
            return CDataHelper.GetData("select cre_man from [a_flowchart_instance] where instance_id='" + InstanceID + "'");
        }
        public static void PutDownNewInstance(string WFName, string WFDataID, string OpeManID, string WFID, string NewInstanceID)
        {
            string WFVersion = CCommonFunc.GetWFVersion(WFName);
            if (WFVersion == null)
            {
                return;
            }

            string DateTimeNow = DateTime.Now.ToString();
            string cmdPutDownInstance = "insert into a_flowchart_instance(flowchart_id,flowchart_version,instance_id,cre_date,cre_man,instance_status) values('" + WFID + "','" + WFVersion + "','" + NewInstanceID + "','" + DateTimeNow + "','" + OpeManID + "','" + InstanceStatus.Active.ToString() + "')";
            CDataHelper.ExecuteNonQuery(cmdPutDownInstance);

            string cmdPutDownDataSource = "insert into a_flowchart_instance_pars(instance_id,keyvalue) values('" + NewInstanceID + "','" + WFDataID + "')";
            CDataHelper.ExecuteNonQuery(cmdPutDownDataSource);
        }

        public static void SetNodeApprovalStatus(string InstanceID, string NodeID, ApprovalStatus Status)
        {
            CDataHelper.ExecuteNonQuery("update [a_flowchart_instance_node_receiver] set [approval_status]='" + Status.ToString() + "' where instance_id='" + InstanceID + "' and node_id='" + NodeID + "'");
        }
        public static void PutDownStartMan(string InstanceID, string NodeID, string UserID)
        {
            CDataHelper.ExecuteNonQuery("insert into [a_flowchart_instance_node_receiver](instance_id,node_id,user_id,approval_opinion,operate_time,approval_status,node_status) values('" + InstanceID + "','" + NodeID + "','" + UserID + "','发起','" + DateTime.Now.ToString() + "','"+ApprovalStatus.Complete.ToString()+"','"+NodeStatus.Active.ToString()+"')");
        }

        public static string GetNodeIDBySwitchID(string SwitchID)
        {
            string NodeID = null;
            NodeID = CDataHelper.GetData("select node_id from [a_flowchart_node_switchitem] where [switchid]='" + SwitchID + "'");
            return NodeID;
        }

        public static void ClearNodeOpinion(string InstanceID,string NodeID)
        {
            CDataHelper.ExecuteNonQuery("update [a_flowchart_instance_node_receiver] set approval_opinion=null where instance_id='" + InstanceID + "' and node_id='" + NodeID + "'");
        }

        public static void ClearNodeReceiver(string InstanceID, string NodeID)
        {
            CDataHelper.ExecuteNonQuery("delete from [a_flowchart_instance_node_receiver] where instance_id='" + InstanceID + "' and node_id='" + NodeID + "'");
        }
        
        public static void SetInstanceStatus(string InstanceID, InstanceStatus Status,string Info)
        {
            CDataHelper.ExecuteNonQuery("update a_flowchart_instance set [instance_status]='" + Status.ToString() + "',[instance_message]='"+Info+"' where instance_id='" + InstanceID + "'");
        }

        public static void DeleteCurrentNode(string InstanceID, string CurrentNodeID)
        {
            CDataHelper.ExecuteNonQuery("delete from a_flowchart_currentnode where instance_id='" + InstanceID + "' and current_node='" + CurrentNodeID + "'");
        }

        public static void DeleteCurrentNode(string InstanceID)
        {
            CDataHelper.ExecuteNonQuery("delete from a_flowchart_currentnode where instance_id='" + InstanceID + "'");
        }

        public static void InsertCurrentNode(string InstanceID, string CurrentNodeID)
        {
            CDataHelper.ExecuteNonQuery("insert into a_flowchart_currentnode(instance_id,current_node) values('" + InstanceID + "','" + CurrentNodeID + "')");
        }

        public static void UpdateCurrentNode(string InstanceID, string CurrentNodeID, string LastNodeID)
        {
            CDataHelper.ExecuteNonQuery("update a_flowchart_currentnode set current_node='" + CurrentNodeID + "' where instance_id='" + InstanceID + "' and current_node='" + LastNodeID + "'");
        }

        public static DataTable GetReceiver(string InstanceID, string NodeID)
        {
            string WFID=GetWFIDByInstanceID(InstanceID);
            string ReceiverScript = CDataHelper.GetData("select [operatorscript] from [a_flowchart_node] where [flowchart_id]='" + WFID + "' and [node_id]='" + NodeID + "'");
            DataTable dtReceiver = (DataTable)ScriptExec(ReceiverScript, InstanceID);
            return dtReceiver;
        }

        public static string GetNextNodeIDByCurrentNodeID(string CurrentNodeID)
        {
            return CDataHelper.GetData("select next_node_id from a_flowchart_line where node_id='" + CurrentNodeID + "'");
        }

        public static string GetNextNodeIDBySwitchID(string SwitchID)
        {
            return CDataHelper.GetData("select next_node_id from a_flowchart_line where [sourcepoint]='" + SwitchID + "-out'");
        }

        public static string GetUpNodeIDByCurrentNodeID(string CurrentNodeID)
        {
            return CDataHelper.GetData("select node_id from a_flowchart_line where next_node_id='" + CurrentNodeID + "'");
        }

        public static string GetSwitchID(string WFID, string NodeID, string SwitchName)
        {
            return CDataHelper.GetData("select [switchid] from [a_flowchart_node_switchitem] where [flowchart_id]='" + WFID + "' and [node_id]='" + NodeID + "' and [switchname]='" + SwitchName + "'");
        }

        public static string[] GetSwitchName(string InstanceID, string NodeID)
        {
            string[] SwitchNames = null;
            string WFID=GetWFIDByInstanceID(InstanceID);
            string SwitchScript = CDataHelper.GetData("select switchscript from [a_flowchart_node] where [flowchart_id]='" + WFID + "' and [node_id]='"+NodeID+"'");
            object SwitchNameObj = ScriptExec(SwitchScript, InstanceID);
            SwitchNames = SwitchNameDecode(SwitchNameObj);
            return SwitchNames;
        }

        public static string[] SwitchNameDecode(object SwitchNameObj)
        {
            string[] SwitchNames = null;
            SwitchNames = ((string)(SwitchNameObj)).Split(';');
            return SwitchNames;
        }

        public static string GetNodeType(string NodeID)
        {
            return CDataHelper.GetData("select [nodetype] from [a_flowchart_node] where [node_id]='" + NodeID + "'");
        }

        private static ScriptEngine engine = null;
        public static ScriptEngine CreateScriptEngine()
        {
            if (engine == null)
            {
                Console.Write("创建 ScriptEngine ");
                engine = IronPython.Hosting.Python.CreateEngine();
            }
            return engine;
        }

        public static object ScriptExec(string scrpitTxt, string InstanceID)
        {
            try
            {
                //创建一个IpyRunTime，需要2-3秒时间。建议进入全局时加载，此为演示
                var engine = CreateScriptEngine();// IronPython.Hosting.Python.CreateEngine();
                var code = engine.CreateScriptSourceFromString(scrpitTxt);

                //设置参数;
                ScriptScope scope = engine.CreateScope();
                CFuncForPython cffp = new CFuncForPython();
                scope.SetVariable("wfhost", cffp);
                scope.SetVariable("InstanceID", InstanceID);

                var actual = code.Execute<object>(scope);

                return actual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetNodeStatusComplete(string InstanceID,string NodeID)
        {
            CDataHelper.ExecuteNonQuery("update [a_flowchart_instance_node_receiver] set node_status='" + NodeStatus.Complete.ToString() + "' where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and node_status='" + NodeStatus.Active.ToString() + "'");
        }

        public static void PutDownFlowHistory(string InstanceID, string NodeID, string NextNodeID)
        {
            CDataHelper.ExecuteNonQuery("insert into a_flowchart_flow_history(instance_id,node_id,next_node_id) values('"+InstanceID+"','"+NodeID+"','"+NextNodeID+"')");
        }
    }
}
