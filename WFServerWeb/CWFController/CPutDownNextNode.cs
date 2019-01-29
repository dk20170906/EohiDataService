using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
namespace WFServerWeb
{
    public static class CPutDownNextNode
    {
        public static void PutDownNextNode(string WFID, string InstanceID, string CurrentNodeID)
        {
            ArrayList NextNodeArr = CCommonFunc.GetNextNodeID(WFID, InstanceID, CurrentNodeID);
            if (NextNodeArr.Count == 0)
            {
                CCommonFunc.SetInstanceStatus(InstanceID, InstanceStatus.Error,"找不到节点："+CurrentNodeID+" 的下一个节点。");
                return;
            }

            CCommonFunc.DeleteCurrentNode(InstanceID, CurrentNodeID);

            foreach(string NextNodeID in NextNodeArr)
            {
                CCommonFunc.InsertCurrentNode(InstanceID, NextNodeID);
                //判断流程是否结束
                string nodetype = CCommonFunc.GetNodeType(NextNodeID);
                if (nodetype == CNodeType.EndType)
                {
                    string cmdUpteInstanceStatus = "update a_flowchart_instance set [instance_status]='" + InstanceStatus.Complete.ToString() + "' where [instance_id]='" + InstanceID + "'";
                    CDataHelper.ExecuteNonQuery(cmdUpteInstanceStatus);
                }
                else
                {
                    CPutDownReceiver.PutDownReceiver(InstanceID, NextNodeID);
                }
            }
        }
    }
}
