using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace EohiDataServerApi

{
  // 数据库接口类
	public class DBHelper
	{
		
    
		//构造函数
		public DBHelper()
		{
            
        }

        public static string ReReadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        
        }

        #region ExecuteScalar 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行

        public static object ExecuteScalar(string sqlString, SqlParameter[] pars)
        {
            return ExecuteScalar(ReReadConnectionString(), sqlString, pars);
        }
        ///   <summary>   
        ///   执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行
        ///   如果为NULL 则返回 -1
        ///   </summary> 
        ///   <param name="sqlString">被执行的SQL语句</param>
        ///   <returns>返回值</returns>
        public static object ExecuteScalar(string connString,string sqlString, SqlParameter[] pars)
        {
            SqlConnection myConn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sqlString, myConn);
            try
            {
                myConn.Open();

                cmd.Parameters.AddRange(pars);

                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (SqlException exp)
            {
                //MessageBox.Show("数据操作失败！\r\n\r\n描述：" + exp.Message.ToString()
                //    , "数据库执行提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return -1;
                throw exp;
            }
            finally
            {
                cmd.Dispose();

                myConn.Close();
                myConn.Dispose();
            }
        }
        #endregion

        #region ExecuteNonQuery 执行SQL语句，返回执行语句所影响的行数
        /// <summary>
        /// 执行SQL语句，返回执行语句所影响的行数
        /// </summary>
        /// <param name="StrSql">被执行的SQL语句</param>
        /// <param name="pars">参数</param>
        /// <returns>返回首影响的行数</returns>
        public static int ExecuteNonQuery(string StrSql, SqlParameter[] pars)
        {
            return ExecuteNonQuery(ReReadConnectionString(),StrSql,pars);
        }
        /// <summary>
        /// 执行SQL语句，返回执行语句所影响的行数
        /// </summary>
        /// <param name="StrSql">被执行的SQL语句</param>
        /// <param name="pars">参数</param>
        /// <returns>返回首影响的行数</returns>
        public static int ExecuteNonQuery(string connString,string StrSql, SqlParameter[] pars)
        {
                int rows = -1;
                SqlConnection myConn = new SqlConnection(connString);
                try
                {
                    myConn.Open();

                    SqlCommand com = new SqlCommand(StrSql, myConn);
                    com.Parameters.AddRange(pars);
                    rows = com.ExecuteNonQuery();
                }
                catch (SqlException exp)
                {
                    //MessageBox.Show("数据操作失败！\r\n\r\n描述：" + exp.Message.ToString()
                    //    , "数据库执行提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //return -1;

                    throw exp;
                }
                finally
                {
                    myConn.Close();
                    myConn.Dispose();
                }
                return rows;
            
        }

        #endregion

   
        #region SqlDataAdapter 返回执行SQL查询语句返回的数据表
        public static DataTable getDataTable(string sqlString, SqlParameter[] pars)
        {
            return getDataTable(ReReadConnectionString(), sqlString, pars);
        }

        public static DataTable getDataTable(string connString,string sqlString, SqlParameter[] pars)
        {

            DataTable tb = new DataTable();

            SqlConnection myConn = new SqlConnection(connString);
            SqlCommand MyCmd = new SqlCommand(sqlString, myConn);
            try
            {
                MyCmd.Parameters.AddRange(pars);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(MyCmd);
                dataAdapter.Fill(tb);
            }
            catch (SqlException exp)
            {
                //    , "数据库执行提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return tb;
                throw exp;
            }
            finally
            {
                myConn.Close();
                myConn.Dispose();
            }
            return tb;
        }


        #endregion


        #region 执行存储过程


        /// <summary>
        /// 执行存储过程。不带返回信息
        /// </summary>
        /// <param name="StoredProcedure_name">被执行的存储过程名称</param>
        /// <param name="commandParamters">存储过程参数集</param>
        /// <returns>执行成功返回一个DataSet，否则返回null</returns>
        public static DataSet DataSetDBStoredProcedure(string StoredProcedure_name, params SqlParameter[] pars)
        {
            
                DataSet dt = new DataSet();

                SqlConnection myConn = new SqlConnection(ReReadConnectionString());
                SqlCommand MyCmd = new SqlCommand(StoredProcedure_name, myConn);
                MyCmd.CommandType = CommandType.StoredProcedure;
                try
                {

                    myConn.Open();

                    if (pars != null)
                    {
                        foreach (SqlParameter parm in pars)
                        {
                            MyCmd.Parameters.Add(parm);
                        }
                    }

                    //填充数据源
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = MyCmd;
                    dataAdapter.Fill(dt);

                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.Message);
                    //return null;
                    throw exp;
                }
                finally
                {
                    MyCmd.Dispose();
                    myConn.Close();
                    myConn.Dispose();
                }
                return dt;
            
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="StoredProcedure_name">被执行的存储过程名称</param>
        /// <param name="pars">存储过程参数集</param>
        /// <returns>执行成功返回true 否则 false</returns>
        public static bool BoolDBStoredProcedure(string StoredProcedure_name, SqlParameter[] pars)
        {

            SqlConnection myConn = new SqlConnection(ReReadConnectionString());
                SqlCommand MyCmd = new SqlCommand(StoredProcedure_name, myConn);
                MyCmd.CommandType = CommandType.StoredProcedure;

                //        
                try
                {
                    myConn.Open();

                    MyCmd.Parameters.AddRange(pars);

                    MyCmd.ExecuteNonQuery();
                }
                catch (SqlException exp)
                {
                    //MessageBox.Show(exp.Message.ToString(), "数据库操作失败：存储过程错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return false;
                    throw exp;
                }
                finally
                {
                    MyCmd.Dispose();
                    myConn.Close();
                    myConn.Dispose();
                }

                return true;
            

        }

        public static SqlParameter[] SqlParametersDBStoredProcedure(string StoredProcedure_name, params SqlParameter[] pars)
        {

            SqlConnection myConn = new SqlConnection(ReReadConnectionString());
            SqlCommand MyCmd = new SqlCommand(StoredProcedure_name, myConn);
            MyCmd.CommandType = CommandType.StoredProcedure;

            //        
            try
            {
                myConn.Open();
                MyCmd.Parameters.AddRange(pars);



                MyCmd.ExecuteNonQuery();


            }
            catch (SqlException exp)
            {
                //MessageBox.Show(exp.Message.ToString(), "数据库操作失败：存储过程错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return pars;
                throw exp;
            }
            finally
            {
                MyCmd.Dispose();
                myConn.Close();
                myConn.Dispose();
            }

            return pars;
            

        }

      

        #endregion

        #region 执行事务


        /// <summary>
        /// 多SQL语句操作--事务处理
        /// </summary>
        /// <param name="sqlComm">SQL语句数组</param>
        /// <returns>事务，返回String事务结果</returns>
        public static bool DoTran(string[] sqlComm)
        {

            SqlConnection myConn = new SqlConnection(ReReadConnectionString());

                SqlTransaction myTran;
                myTran = myConn.BeginTransaction();

                SqlCommand command = new SqlCommand("", myConn, myTran);
                try
                {
                    foreach (string comm in sqlComm)
                    {
                        command.CommandText = comm;
                        command.ExecuteNonQuery();
                    }
                    myTran.Commit();
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.Message.ToString(), "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("系统信息：" + e.Message);
                    //return false;
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

        public static bool DoTran(List<string> sqlComm)
        {

            SqlConnection myConn = new SqlConnection(ReReadConnectionString());
                try
                {
                    myConn.Open();
                    SqlTransaction myTran;
                    myTran = myConn.BeginTransaction();

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
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.Message.ToString(), "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("系统信息：" + e.Message);
                    //return false;
                    throw exp;
                }
                finally
                {


                    myConn.Close();
                }
                return true;
            
        }

        public static bool DoTran(List<string> sqlComm, List<SqlParameter[]> sqlParsList)
        {
            
                string strSql = string.Empty;

                SqlConnection myConn = new SqlConnection(ReReadConnectionString());

                try
                {
                    myConn.Open();
                    SqlTransaction myTran = myConn.BeginTransaction();
                    SqlCommand command = new SqlCommand("", myConn, myTran);


                    for (int i = 0; i < sqlComm.Count; i++)
                    {
                        command.Parameters.Clear();

                        command.CommandText = sqlComm[i];
                        strSql = sqlComm[i];
                        command.Parameters.AddRange(sqlParsList[i]);
                        command.ExecuteNonQuery();
                    }
                    myTran.Commit();


                    command.Dispose();
                    myTran.Dispose();
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.Message.ToString() + "\r\n" + strSql, "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("系统信息：" + e.Message);
                    //return false;
                    throw exp;
                }
                finally
                {
                    myConn.Close();
                }
                return true;
            
        }

        public static bool DoTran(List<string> sqlComm, List<SqlParameter[]> sqlParsList, out string msg)
        {
            
                string strSql = string.Empty;

                SqlConnection myConn = new SqlConnection(ReReadConnectionString());
                try
                {
                    myConn.Open();
                    SqlTransaction myTran = myConn.BeginTransaction();
                    SqlCommand command = new SqlCommand("", myConn, myTran);


                    for (int i = 0; i < sqlComm.Count; i++)
                    {
                        command.Parameters.Clear();

                        command.CommandText = sqlComm[i];
                        strSql = sqlComm[i];
                        command.Parameters.AddRange(sqlParsList[i]);
                        command.ExecuteNonQuery();
                    }
                    myTran.Commit();


                    command.Dispose();
                    myTran.Dispose();
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.Message.ToString() + "\r\n" + strSql, "事务处理失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DBHelperErrorLogs.ErrorDeal(exp);
                    //throw new ApplicationException("系统信息：" + e.Message);
                    msg = exp.Message;

                    return false;
                }
                finally
                {
                    myConn.Close();
                }


                msg = "";
                return true;
        }
        #endregion

	}
}
