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

        #region �������ݿ����Ӷ���
        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <returns>����һ�����ݿ������SqlConnection����</returns>
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

        #region ����SqlCommand����
        /// <summary>
        /// ����SqlCommand����      
        /// </summary>
        /// <param name="cmd">SqlCommand���� </param>
        /// <param name="cmdText">�����ı�</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdParms">��������</param>
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

        #region ִ����Ӧ��sql��䣬������Ӧ��DataSet����
        /// <summary>
        /// ִ����Ӧ��sql��䣬������Ӧ��DataSet����
        /// </summary>
        /// <param name="sqlstr">sql���</param>
        /// <returns>������Ӧ��DataSet����</returns>
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

        #region ִ����Ӧ��sql��䣬������Ӧ��DataSet����
        /// <summary>
        /// ִ����Ӧ��sql��䣬������Ӧ��DataSet����
        /// </summary>
        /// <param name="sqlstr">sql���</param>
        /// <param name="tableName">����</param>
        /// <returns>������Ӧ��DataSet����</returns>
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

        #region ִ�в�������sql��䣬������Ӱ�������
        /// <summary>
        /// ִ�в�������sql��䣬������Ӱ�������
        /// </summary>
        /// <param name="cmdstr">����ɾ����sql���</param>
        /// <returns>������Ӱ�������</returns>
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

        #region ִ�д�����sql����洢���̣�������Ӱ�������
        /// <summary>
        /// ִ�д�����sql����洢���̣�������Ӱ�������
        /// </summary>
        /// <param name="cmdText">��������sql���ʹ洢������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdParms">��������</param>
        /// <returns>������Ӱ�������</returns>
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

        #region ִ�в�������sql��䣬����һ��������Դ��ȡ���ݵ�SqlDataReader����
        /// <summary>
        /// ִ�в�������sql��䣬����һ��������Դ��ȡ���ݵ�SqlDataReader����
        /// </summary>
        /// <param name="cmdstr">��Ӧ��sql���</param>
        /// <returns>����һ��������Դ��ȡ���ݵ�SqlDataReader����</returns>
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

        #region ִ�д�������sql����洢���̣�����һ��������Դ��ȡ���ݵ�SqlDataReader����
        /// <summary>
        /// ִ�д�������sql����洢���̣�����һ��������Դ��ȡ���ݵ�SqlDataReader����
        /// </summary>
        /// <param name="cmdText">sql����洢������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdParms">��������</param>
        /// <returns>����һ��������Դ��ȡ���ݵ�SqlDataReader����</returns>
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

        #region ִ�в�������sql��䣬����һ��DataTable����
        /// <summary>
        /// ִ�в�������sql��䣬����һ��DataTable����
        /// </summary>
        /// <param name="cmdText">��Ӧ��sql���</param>
        /// <returns>����һ��DataTable����</returns>
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

        #region ִ�д�������sql����洢���̣�����һ��DataTable����
        /// <summary>
        /// ִ�д�������sql����洢���̣�����һ��DataTable����
        /// </summary>
        /// <param name="cmdText">sql����洢������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdParms">��������</param>
        /// <returns>����һ��DataTable����</returns>
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

        #region ִ�в�������sql���,���ؽ�����������е�ֵobject
        /// <summary>
        /// ִ�в�������sql���,���ؽ�����������е�ֵobject
        /// </summary>
        /// <param name="cmdstr">��Ӧ��sql���</param>
        /// <returns>���ؽ�����������е�ֵobject</returns>
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

        #region ִ�д�����sql����洢����,���ؽ�����������е�ֵobject
        /// <summary>
        /// ִ�д�����sql����洢����,���ؽ�����������е�ֵobject
        /// </summary>
        /// <param name="cmdText">sql����洢������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdParms">���ؽ�����������е�ֵobject</param>
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