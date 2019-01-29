using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiDataRemoteObject
{
    public class SqlCmdExec
    {

        private static SqlConnection getSqlConnection(string connString)
        {
            SqlConnection sqlConn = null;
            try
            {
                sqlConn = new SqlConnection(connString);
                sqlConn.Open();
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            return sqlConn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static DataSet getDataSet(string connString, string sql, SqlParameter[] pars)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConn = getSqlConnection(connString); //new SqlConnection(connString);
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, sqlConn);
                if (pars != null)
                    sqlCmd.Parameters.AddRange(pars);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                dataAdapter.Fill(dataSet);
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

            return dataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static DataTable getDataTable(string connString, string sql, SqlParameter[] pars)
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConn = getSqlConnection(connString);// new SqlConnection(connString);
            if (sqlConn == null)
                return null;
            try
            {
                /*
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException connexp)
                {
                    //连接异常;

                    return null;
                    //throw;
                }
                */

                SqlCommand sqlCmd = new SqlCommand(sql, sqlConn);
                if (pars != null)
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

        /// <summary>
        /// 执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connString, string sql, SqlParameter[] pars)
        {
            int rows = -1;
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return -1;

            try
            {
                //myConn.Open();

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


        /// <summary>
        /// 执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connString, string sql, SqlParameter[] pars)
        {
            object obj = -1;
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return -1;

            try
            {
                //myConn.Open();

                SqlCommand sqlCmd = new SqlCommand(sql, myConn);
                if (pars != null)
                    sqlCmd.Parameters.AddRange(pars);
                sqlCmd.CommandType = CommandType.Text;

                obj = sqlCmd.ExecuteScalar();
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
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
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



        /// <summary>
        /// 多SQL语句操作--事务处理
        /// </summary>
        /// <param name="sqlComm">SQL语句数组</param>
        /// <returns>事务，返回String事务结果</returns>
        public static bool DoTran(string connString, string[] sqlComm)
        {
            //SqlConnection myConn = new SqlConnection(connString);
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return false;

            try
            {
                //myConn.Open();

                SqlTransaction myTran = myConn.BeginTransaction();
                SqlCommand command = new SqlCommand("", myConn, myTran);

                foreach (string comm in sqlComm)
                {
                    command.CommandText = comm;
                    command.ExecuteNonQuery();
                }
                myTran.Commit();


                command.Dispose();
                myTran.Dispose();
            }
            catch (SqlException exp)
            {
                //MessageBox.Show(exp.Message.ToString(), "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw exp;
            }
            finally
            {

                myConn.Close();
            }

            return true;
        }

        public static bool DoTran(string connString, List<string> sqlComm)
        {

            //SqlConnection myConn = new SqlConnection(connString);
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return false;

            try
            {
                //myConn.Open();
                SqlTransaction myTran = myConn.BeginTransaction();
                SqlCommand command = new SqlCommand("", myConn, myTran);
                foreach (string comm in sqlComm)
                {
                    command.CommandText = comm;
                    command.ExecuteNonQuery();
                }
                myTran.Commit();


                command.Dispose();
                myTran.Dispose();
            }
            catch (SqlException exp)
            {
                // MessageBox.Show(exp.Message.ToString(), "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw exp;
            }
            finally
            {
                myConn.Close();
            }
            return true;
        }

        public static bool DoTran(string connString, List<string> sqlComm, List<SqlParameter[]> sqlParsList)
        {
            //string strSql = string.Empty;
            //SqlConnection myConn = new SqlConnection(connString);
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return false;


            SqlTransaction myTran = myConn.BeginTransaction();
            SqlCommand command = new SqlCommand("", myConn, myTran);
            try
            {
                //myConn.Open();

                for (int i = 0; i < sqlComm.Count; i++)
                {
                    command.Parameters.Clear();
                    command.CommandText = sqlComm[i];
                    if(sqlParsList[i]!=null)
                        command.Parameters.AddRange(sqlParsList[i]);
                    command.ExecuteNonQuery();
                }
                myTran.Commit();

            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
                command.Dispose();
                myTran.Dispose();
                myConn.Close();
            }
            return true;
        }

        public static bool DoTran(string connString, List<NFXmlSql> nfxmlsqlList)
        {
            //string strSql = string.Empty;
            //SqlConnection myConn = new SqlConnection(connString);
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return false;

            try
            {
                //myConn.Open();
                SqlTransaction myTran = myConn.BeginTransaction();
                SqlCommand command = new SqlCommand("", myConn, myTran);

                for (int i = 0; i < nfxmlsqlList.Count; i++)
                {
                    command.Parameters.Clear();
                    command.CommandText = nfxmlsqlList[i].cmd;
                    if (nfxmlsqlList[i].parsList != null && nfxmlsqlList[i].parsList.Count >0 )
                        command.Parameters.AddRange(nfxmlsqlList[i].parsList.ToArray());
                    command.ExecuteNonQuery();
                }
                myTran.Commit();


                //
                command.Dispose();
                myTran.Dispose();

            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
               
                myConn.Close();
            }
            return true;
        }
    }
}
