using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFServerWeb
{
    public class CReturnUP
    {
        public static void ReturnUp(string InstanceID,string CurrentNodeID)
        {
            CCommonFunc.SetNodeApprovalStatus(InstanceID, CurrentNodeID, ApprovalStatus.Complete);
            CCommonFunc.SetNodeStatusComplete(InstanceID, CurrentNodeID);
            string UpNodeID = CCommonFunc.GetUpNodeID(InstanceID, CurrentNodeID);
            CCommonFunc.DeleteCurrentNode(InstanceID, CurrentNodeID);
            CCommonFunc.InsertCurrentNode(InstanceID, UpNodeID);
            CCommonFunc.PutDownFlowHistory(InstanceID, CurrentNodeID, UpNodeID);
            CPutDownReceiver.PutDownReceiver(InstanceID,UpNodeID);
        }
    }
}