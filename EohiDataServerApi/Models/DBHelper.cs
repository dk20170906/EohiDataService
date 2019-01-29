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
  // ���ݿ�ӿ���
	public class DBHelper
	{
		
    
		//���캯��
		public DBHelper()
		{
            
        }

        public static string ReReadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        
        }

        #region ExecuteScalar ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����������л���

        public static object ExecuteScalar(string sqlString, SqlParameter[] pars)
        {
            return ExecuteScalar(ReReadConnectionString(), sqlString, pars);
        }
        ///   <summary>   
        ///   ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����������л���
        ///   ���ΪNULL �򷵻� -1
        ///   </summary> 
        ///   <param name="sqlString">��ִ�е�SQL���</param>
        ///   <returns>����ֵ</returns>
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
                //MessageBox.Show("���ݲ���ʧ�ܣ�\r\n\r\n������" + exp.Message.ToString()
                //    , "���ݿ�ִ����ʾ:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region ExecuteNonQuery ִ��SQL��䣬����ִ�������Ӱ�������
        /// <summary>
        /// ִ��SQL��䣬����ִ�������Ӱ�������
        /// </summary>
        /// <param name="StrSql">��ִ�е�SQL���</param>
        /// <param name="pars">����</param>
        /// <returns>������Ӱ�������</returns>
        public static int ExecuteNonQuery(string StrSql, SqlParameter[] pars)
        {
            return ExecuteNonQuery(ReReadConnectionString(),StrSql,pars);
        }
        /// <summary>
        /// ִ��SQL��䣬����ִ�������Ӱ�������
        /// </summary>
        /// <param name="StrSql">��ִ�е�SQL���</param>
        /// <param name="pars">����</param>
        /// <returns>������Ӱ�������</returns>
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
                    //MessageBox.Show("���ݲ���ʧ�ܣ�\r\n\r\n������" + exp.Message.ToString()
                    //    , "���ݿ�ִ����ʾ:", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

   
        #region SqlDataAdapter ����ִ��SQL��ѯ��䷵�ص����ݱ�
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
                //    , "���ݿ�ִ����ʾ:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        #region ִ�д洢����


        /// <summary>
        /// ִ�д洢���̡�����������Ϣ
        /// </summary>
        /// <param name="StoredProcedure_name">��ִ�еĴ洢��������</param>
        /// <param name="commandParamters">�洢���̲�����</param>
        /// <returns>ִ�гɹ�����һ��DataSet�����򷵻�null</returns>
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

                    //�������Դ
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
        /// ִ�д洢����
        /// </summary>
        /// <param name="StoredProcedure_name">��ִ�еĴ洢��������</param>
        /// <param name="pars">�洢���̲�����</param>
        /// <returns>ִ�гɹ�����true ���� false</returns>
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
                    //MessageBox.Show(exp.Message.ToString(), "���ݿ����ʧ�ܣ��洢���̴���", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //MessageBox.Show(exp.Message.ToString(), "���ݿ����ʧ�ܣ��洢���̴���", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region ִ������


        /// <summary>
        /// ��SQL������--������
        /// </summary>
        /// <param name="sqlComm">SQL�������</param>
        /// <returns>���񣬷���String������</returns>
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
                    //MessageBox.Show(exp.Message.ToString(), "������ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("ϵͳ��Ϣ��" + e.Message);
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
                    //MessageBox.Show(exp.Message.ToString(), "������ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("ϵͳ��Ϣ��" + e.Message);
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
                    //MessageBox.Show(exp.Message.ToString() + "\r\n" + strSql, "������ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ////throw new ApplicationException("ϵͳ��Ϣ��" + e.Message);
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
                    //MessageBox.Show(exp.Message.ToString() + "\r\n" + strSql, "������ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DBHelperErrorLogs.ErrorDeal(exp);
                    //throw new ApplicationException("ϵͳ��Ϣ��" + e.Message);
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
