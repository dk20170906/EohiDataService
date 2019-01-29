using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IMServer.DBHelper
{
    public class SqlCmdExec
    {
       
        public static DataTable getDataTable(string connString, string sql, SqlParameter[] pars)
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConn = new SqlConnection(connString); 
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, sqlConn);
                if(pars!=null)
                    sqlCmd.Parameters.AddRange(pars);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                dataAdapter.Fill(dataTable);
            }
            catch (SqlException exp)
            {
                //MessageBox.Show("数据操作失败！\r\n\r\n描述：" + exp.Message.ToString()
                //    , "数据库执行提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw exp;
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }

            return dataTable;
        }


        public static int ExecuteNonQuery(string connString, string sql, SqlParameter[] pars)
        {
            int rows = -1;
            SqlConnection myConn = new SqlConnection(connString); 
            try
            {
                myConn.Open();

                SqlCommand sqlCmd = new SqlCommand(sql, myConn);
                if (pars != null)
                    sqlCmd.Parameters.AddRange(pars);
                sqlCmd.CommandType = CommandType.Text;

                rows = sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
                myConn.Close();
                myConn.Dispose();
            }
            return rows;
        }

        public static bool BoolExecuteNonQuery(string connString, string sql, SqlParameter[] pars)
        {
            try
            {
                int rows = ExecuteNonQuery(connString, sql, pars);
            }
            catch (SqlException exp)
            {
                return false;
            }
            return true;
        }
    }
}
