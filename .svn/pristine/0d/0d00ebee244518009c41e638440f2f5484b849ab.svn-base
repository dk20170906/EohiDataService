using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFServerWeb
{
    public class CReturnUP
    {
        /// <summary>
        /// 将工作流流转到上一级节点
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="CurrentNodeID"></param>
        public static string ReturnUp(string InstanceID, string CurrentNodeID, string UpNodeID)
        {
            try
            {
                return CNodeManager.NodeTransfer(InstanceID, CurrentNodeID, UpNodeID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}