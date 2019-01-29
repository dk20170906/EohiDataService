using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EohiDataServerApi
{
    /// <summary>
    /// DBService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.eohi700.com/webservice/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DBService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataSet getDataSet(string nfCmdXmlString)
        {

            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(nfCmdXmlString);
            DataSet ds = new DataSet("data");
            foreach (XmlToList xtl in list)
            {
                DataTable table = DBHelper.DataTableDBExecuteSqlCommand(xtl.SQL, xtl.PARS);
                if (!String.IsNullOrEmpty(xtl.TABLENAME)) 
                    table.TableName = xtl.TABLENAME;
                ds.Tables.Add(table);
            }

            return ds;
        }



        [WebMethod]
        public int ExecuteNonQuery(string nfCmdXmlString)
        {
            int rows = -1;

            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(nfCmdXmlString);
            DataSet ds = new DataSet("data");
            foreach (XmlToList xtl in list)
            {
                rows = DBHelper.ExecuteNonQuery(xtl.SQL, xtl.PARS);
            }

            return rows;
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="nfCmdXmlString"></param>
        /// <returns></returns>
        [WebMethod]
        public object ExecuteScalar(string nfCmdXmlString)
        {
            object obj = null;

            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(nfCmdXmlString);
            DataSet ds = new DataSet("data");
            foreach (XmlToList xtl in list)
            {

                try
                {
                    obj = DBHelper.DBExecuteScalar(xtl.SQL, xtl.PARS);
                }
                catch (SqlException exp)
                {
                    obj = exp.Message;
                }
            }

            return obj;
        }
        /// <summary>
        /// 获取掩码 codetype 默认 0
        /// </summary>
        /// <param name="keyString">掩码前缀</param>
        /// <param name="length">序号长度</param>
        /// <param name="bAdd">是否标记为已使用</param>
        /// <returns></returns>
        [WebMethod]
        public  string GetTransactionCode(string keyString, int length, bool bAdd)
        {
            //NocsTools.Db
            SqlConnection myConn = new SqlConnection(DBHelper.ReReadConnectionString());
            SqlCommand MyCmd = new SqlCommand("pr_app_getTransactionCode", myConn);
            MyCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] pars = new SqlParameter[5];

            //        
            try
            {

                SqlParameter param1 = new SqlParameter("@codetype", SqlDbType.Int);
                param1.Value = 0;
                pars[0] = param1;

                param1 = new SqlParameter("@codekey", SqlDbType.VarChar, 50);
                param1.Value = keyString;
                pars[1] = param1;

                param1 = new SqlParameter("@codelen", SqlDbType.Int);
                param1.Value = length;
                pars[2] = param1;

                param1 = new SqlParameter("@badd", SqlDbType.Bit);
                param1.Value = bAdd;
                pars[3] = param1;


                param1 = new SqlParameter("@newcode", SqlDbType.VarChar, 30);
                param1.Direction = ParameterDirection.Output;
                pars[4] = param1;

                myConn.Open();
                MyCmd.Parameters.AddRange(pars);
                MyCmd.ExecuteNonQuery();

                return pars[4].Value.ToString();

            }
            catch (SqlException exp)
            {
                //MessageBox.Show(exp.Message.ToString(), "数据库操作失败：存储过程错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return pars;
                //return exp.Message;

                //系统掩码生成，转为本地生成;
                return "ERR-" + keyString +"-"+ DateTime.Now.Ticks.ToString();
            }
            finally
            {
                MyCmd.Dispose();
                myConn.Close();
                myConn.Dispose();
            }
        }


        /// <returns></returns>
        [WebMethod]
        public bool DoTrain(string nfCmdXmlString)
        {
            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(nfCmdXmlString);
            DataSet ds = new DataSet("data");

            List<string> cmdList = new List<string>();
            List<SqlParameter[]> parsList = new List<SqlParameter[]>();

            foreach (XmlToList xtl in list)
            {
                cmdList.Add(xtl.SQL);
                parsList.Add(xtl.PARS);
            }
            
            //执行事务;
            return DBHelper.DoTran(cmdList, parsList);
        }
    }
}
