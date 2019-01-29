using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace WFServerWeb
{
    public class CSQLSERVERHelper : IDataManager
    {
        public  SqlCommand cmd = null;
        public  SqlConnection conn = null;
        public  string connstr = ""; //ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        #region 建立数据库连接对象
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns>返回一个数据库的连接SqlConnection对象</returns>
        public  SqlConnection Init()
        {
            try
            {
                conn = new SqlConnection(connstr);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return conn;
        }
        #endregion

        #region 设置SqlCommand对象
        /// <summary>
        /// 设置SqlCommand对象      
        /// </summary>
        /// <param name="cmd">SqlCommand对象 </param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        private  void SetCommand(SqlCommand cmd, string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            //if (cmdParms != null)
            //{
            //    cmd.Parameters.AddRange(cmdParms);
            //}
            foreach (SqlParameter par in cmdParms)
            {
                cmd.Parameters.Add(par);
            }
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <returns>返回相应的DataSet对象</returns>
        public  DataSet GetDataSet(string sqlstr)
        {
            DataSet set = new DataSet();
            try
            {
                Init();
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(set);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回相应的DataSet对象</returns>
        public  DataSet GetDataSet(string sqlstr, string tableName)
        {
            DataSet set = new DataSet();
            try
            {
                Init();
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(set, tableName);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行不带参数sql语句，返回所影响的行数
        /// <summary>
        /// 执行不带参数sql语句，返回所影响的行数
        /// </summary>
        /// <param name="cmdstr">增，删，改sql语句</param>
        /// <returns>返回所影响的行数</returns>
        public  int ExecuteNonQuery(string cmdText)
        {
            int count;
            try
            {
                Init();
                cmd = new SqlCommand(cmdText, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行带参数sql语句或存储过程，返回所影响的行数
        /// <summary>
        /// 执行带参数sql语句或存储过程，返回所影响的行数
        /// </summary>
        /// <param name="cmdText">带参数的sql语句和存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回所影响的行数</returns>
        public  int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            int count;
            try
            {
                Init();
                cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个从数据源读取数据的SqlDataReader对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个从数据源读取数据的SqlDataReader对象
        /// </summary>
        /// <param name="cmdstr">相应的sql语句</param>
        /// <returns>返回一个从数据源读取数据的SqlDataReader对象</returns>
        public  SqlDataReader ExecuteReader(string cmdText)
        {
            SqlDataReader reader;
            try
            {
                Init();
                cmd = new SqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的SqlDataReader对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的SqlDataReader对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个从数据源读取数据的SqlDataReader对象</returns>
        public  SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlDataReader reader;
            try
            {
                Init();
                cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个DataTable对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">相应的sql语句</param>
        /// <returns>返回一个DataTable对象</returns>
        public  DataTable GetDataTable(string cmdText)
        {
            SqlDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                Init(); 
                cmd = new SqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个DataTable对象</returns>
        public  DataTable GetDataTable(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                Init();
                cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行不带参数sql语句,返回结果集首行首列的值object
        /// <summary>
        /// 执行不带参数sql语句,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdstr">相应的sql语句</param>
        /// <returns>返回结果集首行首列的值object</returns>
        public  object ExecuteScalar(string cmdText)
        {
            object obj;
            try
            {
                Init();
                cmd = new SqlCommand(cmdText, conn);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion

        #region 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// <summary>
        /// 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">返回结果集首行首列的值object</param>
        /// <returns></returns>
        public  object ExecuteScalar(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            object obj;
            try
            {
                Init();
                cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion
    }
}