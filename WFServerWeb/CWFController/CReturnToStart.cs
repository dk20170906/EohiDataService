using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFServerWeb
{
    public class CReturnToStart
    {
        public static void ReturnToStart(string InstanceID,string CurrentNodeID)
        {
            CCommonFunc.SetNodeStatusComplete(InstanceID, CurrentNodeID);
            string WFID = CCommonFunc.GetWFIDByInstanceID(InstanceID);
            string StartNodeID = CCommonFunc.GetStartNodeID(WFID);
            CCommonFunc.DeleteCurrentNode(InstanceID);
            CCommonFunc.InsertCurrentNode(InstanceID, StartNodeID);
            CCommonFunc.PutDownFlowHistory(InstanceID, CurrentNodeID, StartNodeID);
            CCommonFunc.SetNodeApprovalStatus(InstanceID, CurrentNodeID, ApprovalStatus.Complete);
            CPutDownReceiver.PutDownReceiver(InstanceID,StartNodeID);
        }
    }
}