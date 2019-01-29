using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WFServerWeb
{
    public class CWFManager
    {
        /// <summary>
        /// 获取工作流名称对应的最后一版工作流的ID号
        /// </summary>
        /// <param name="WFName"></param>
        /// <returns></returns>
        public static string GetLastVersionWFID(string WFName)
        {
            try
            {
                return CDataHelper.GetData("select top 1 flowchart_id from " + CTableName.FlowChart + " where flowchart_name='" + WFName + "' order by flowchart_version desc");
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流" + WFName + "的最后一个版本号操作异常，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 获取最后一个版本号
        /// </summary>
        /// <param name="WFName"></param>
        /// <returns></returns>
        public static string GetLastVersion(string WFName)
        {
            try
            {
                return CDataHelper.GetData("select top 1 isnull(flowchart_version,'') as flowchart_version from " + CTableName.FlowChart + " where flowchart_name='" + WFName + "' order by id desc");
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取工作流" + WFName + "的最后一个版本号操作异常，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public static string GetWFIDByNodeID(string NodeID) 
        {
            try
            {
                return CDataHelper.GetData("select flowchart_id from " + CTableName.FlowChartNode + " where node_id='" + NodeID + "'"); 
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("获取节点所属工作流ID操作异常，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
    }
}