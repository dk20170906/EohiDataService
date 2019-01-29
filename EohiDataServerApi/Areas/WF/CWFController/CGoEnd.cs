using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFServerWeb
{
    public class CGoEnd
    {
        /// <summary>
        /// 将工作流流转到结束节点
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        public static string GoEnd(string InstanceID, string NodeID, string EndNodeID)
        {
            try
            {
                return CNodeManager.NodeTransfer(InstanceID, NodeID, EndNodeID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}