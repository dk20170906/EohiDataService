using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EohiDataServerApi
{
    /// <summary>
    /// MySQL类  MySqlClient连接
    /// </summary>
    public class MySQLHelper
    {

       
        /// <summary>
        /// 执行带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">增删改SQL语句或存储过程的字符串</param>
        /// <param name="paras">往存储过程或SQL中赋的参数集合</param>
        /// <returns>受影响的函数</returns>
        public static int ExecuteNonQuery(string mySqlConnString, string cmdText, MySqlParameter[] paras)
        {
            int rows = -1;
            MySqlConnection myConn = new MySqlConnection(mySqlConnString);
            try
            {
                myConn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(cmdText, myConn);
                if (paras != null)
                    sqlCmd.Parameters.AddRange(paras);
                sqlCmd.CommandType = CommandType.Text;

                rows = sqlCmd.ExecuteNonQuery();
            }
            catch (MySqlException exp)
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



        ///   <summary>   
        ///   执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行
        ///   如果为NULL 则返回 -1
        ///   </summary> 
        ///   <param name="sqlString">被执行的SQL语句</param>
        ///   <returns>返回值</returns>
        public static object ExecuteScalar(string connString, string sqlString, MySqlParameter[] pars)
        {
            MySqlConnection myConn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sqlString, myConn);
            try
            {
                

                myConn.Open();

                cmd.Parameters.AddRange(pars);

                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (MySqlException exp)
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

        public static DataTable ExecuteDataTable(String mysqlConnString, string cmdText, MySqlParameter[] paras)
        {
            DataTable dataTable = new DataTable();
            MySqlConnection sqlConn = new MySqlConnection(mysqlConnString);
           
            try
            {
                MySqlCommand sqlCmd = new MySqlCommand(cmdText, sqlConn);
                if (paras != null)
                    sqlCmd.Parameters.AddRange(paras);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCmd);
                dataAdapter.Fill(dataTable);
            }
            catch (MySqlException exp)
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
    }
}