using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
namespace WFServerWeb
{
    public class CFuncForPython
    {
        public enum GetUserType
        {
            ByDepartment,
            ByRole,
            BySql,
            ByID
        }

        public DataTable getdepartmentuser(string strDepartmenNameList)
        {
            try
            {
                string strDepartments = GetItemArray(strDepartmenNameList);
                string strSql = @"select user_id,user_name,'" + GetUserType.ByDepartment + "' as GetUserType from  view_workflow_department_users where department_name in (" + strDepartments + ")";
                DataTable dtUser = CDataHelper.GetDataTable(strSql);
                return dtUser;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("通过部门获取人员信息操作异常。部门名称列表："+strDepartmenNameList+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public DataTable getroleuser(string strRoleNameList)
        {
            try
            {
                string strRoles = GetItemArray(strRoleNameList);
                string strSql = @"select user_id,user_name,'" + GetUserType.ByRole + "' as GetUserType from  view_workflow_role_users where role_name in (" + strRoles + ")";
                DataTable dtUser = CDataHelper.GetDataTable(strSql);
                return dtUser;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过角色获取人员信息操作异常。角色名称列表：" + strRoleNameList + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public DataTable getDataTable(string strSql)
        {
            try
            {
                return CDataHelper.GetDataTable(strSql);
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过SQL语句获取人员信息操作异常。SQL语句：" + strSql + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public string GetData(string strSql)
        {
            try
            {
                string data = null;
                DataTable dt = CDataHelper.GetDataTable(strSql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过SQL语句获取数据操作异常。SQL语句：" + strSql + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public DataTable getuser(string strUserList)
        {
            try
            {
                string strUsers = GetItemArray(strUserList);
                string strSql = "select user_id,user_name,'" + GetUserType.ByID + "' as GetUserType from view_workflow_users where user_id in (" + strUsers + ")";

                DataTable dtUser = CDataHelper.GetDataTable(strSql);
                return dtUser;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("通过人员ID获取人员信息操作异常。人员ID列表：" + strUserList + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        public int ConvertToInt32(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("object对象转换为Int32类型操作异常。object对象：" + obj + "，异常信息：" + ex.Message.ToString());
            }

            return 0;
        }

        public string ConvertToString(object obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("object对象转换为String类型操作异常。object对象：" + obj + "，异常信息：" + ex.Message.ToString());
            }

            return "";
        }

        private string GetItemArray(string strList)
        {
            try
            {
                string[] Items = strList.Split(',');
                string strItems = "";
                for (int i = 0; i < Items.Length; i++)
                {
                    strItems += "'" + Items[i] + "',";
                }
                strItems = strItems.Substring(0, strItems.Length - 1);
                return strItems;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetNodeApprovalOpinion(string InstanceID, string NodeID, string ApproverID)
        {
            try
            {
                return CNodeManager.GetNodeApprovalOpinion(InstanceID, NodeID, ApproverID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool IsNodeAllAgree(string InstanceID, string NodeID)
        {
            try
            {
                int ApprovalNum = CApprovalManager.GetLastApprovalNum(InstanceID, NodeID);
                if (ApprovalNum == 0)
                {
                    return false;
                }

                string strUnCompleteNum = CDataHelper.GetData("select count(*) from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and approval_num='" + ApprovalNum + "' and approval_status<>'" + EApprovalStatus.Complete.ToString() + "'");
                if (strUnCompleteNum != "0")
                {
                    return false;
                }

                string strUnAgreeNum = CDataHelper.GetData("select count(*) from " + CTableName.FlowChartReceiver + " where instance_id='" + InstanceID + "' and node_id='" + NodeID + "' and approval_num='" + ApprovalNum + "' and approval_opinion<>'" + EApprovalStatus.Complete.ToString() + "' and approval_opinion<>NULL and approval_opinion<>''");
                if (strUnAgreeNum != "0")
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("判断节点审批人是否都同意操作异常。实例ID："+InstanceID+"，节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 判断该节点的所有审批人员是否都已完成审批
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public bool IsNodeApprovalComplete(string InstanceID, string NodeID)
        {
            try
            {
                return CNodeManager.IsNodeApprovalComplete(InstanceID, NodeID);
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("判断节点是否审批结束操作异常。实例ID："+InstanceID+"，节点ID："+NodeID+"，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
    }
}