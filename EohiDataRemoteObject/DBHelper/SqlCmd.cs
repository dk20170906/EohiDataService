using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiDataRemoteObject
{
    public class SqlCmd
    {
        public static string GetConn()
        {
            return SqlConn.GetConnectionString();
        }
        public static bool TestConn()
        {

            try
            {
                SqlConnection sqlConn = null;
                sqlConn = new SqlConnection(GetConn());
                sqlConn.Open();
            }
            catch (SqlException exp)
            {
                return false;
            }
            return true;

        }

        public static string GetTransactionCode(string keyString, int length, bool bAdd)
        {
            string connString = GetConn();
            return GetTransactionCode(connString, keyString, length, bAdd);
        }
        public static string GetTransactionCode(string connString,string keyString, int length, bool bAdd)
        {
            //NocsTools.Db
            SqlConnection myConn = new SqlConnection(connString);
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
                return "-" + keyString + DateTime.Now.Ticks.ToString();
            }
            finally
            {
                MyCmd.Dispose();
                myConn.Close();
                myConn.Dispose();
            }

            return "get maskvalue error!";
        }
        /// <summary>
        /// 获取数据表;
        /// </summary>
        /// <returns></returns>
        public static DataTable getDataTable(string sql)
        {
            return SqlCmdExec.getDataTable(GetConn(), sql, null);
        }
        public static DataTable getDataTable( string sql, SqlParameter[] pars)
        {
            return SqlCmdExec.getDataTable(GetConn(), sql, pars);
        }



        public static int ExecuteNonQuery(string sql)
        {
            return SqlCmdExec.ExecuteNonQuery(GetConn(), sql, null);
        }
        public static int ExecuteNonQuery(string sql, SqlParameter[] pars)
        {
            return SqlCmdExec.ExecuteNonQuery(GetConn(), sql, pars);
        }


        public static bool BoolExecuteNonQuery(string sql)
        {
            return SqlCmdExec.BoolExecuteNonQuery(GetConn(), sql, null);
        }
        public static bool BoolExecuteNonQuery(string sql, SqlParameter[] pars)
        {
            return SqlCmdExec.BoolExecuteNonQuery(GetConn(), sql, pars);
        }


        public static bool DoTran( List<string> sqlComm)
        {
            return SqlCmdExec.DoTran(GetConn(), sqlComm);
           
        }
        public static bool DoTran( List<NFXmlSql> nfxmlsqlList)
        {
            return SqlCmdExec.DoTran(GetConn(), nfxmlsqlList);
        }
        public static bool DoTran( List<string> sqlComm, List<SqlParameter[]> sqlParsList)
        {
            return SqlCmdExec.DoTran(GetConn(), sqlComm, sqlParsList);
        }

       

    }
}
