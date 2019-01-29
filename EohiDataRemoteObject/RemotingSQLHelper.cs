using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiDataRemoteObject
{
    public class RemotingSQLHelper : MarshalByRefObject
    {
        public RemotingSQLHelper()
        {
            //Console.WriteLine("[DBHelper]:Remoting Object 'DBHelper' is activated.");

            //string name = System.Web.Configuration.WebConfigurationManager.AppSettings["connstring"];
        }
        public string getConnString()
        {
            return "[ConnString]:" + SqlConn.connectionString;
        }
        public bool TestConn()
        {
            return SqlCmd.TestConn();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public RemotingSQLResult getDataTable(string sql)
        {
            RemotingSQLResult reslut = new RemotingSQLResult();

            try
            {
              DataTable dt =SqlCmd.getDataTable(sql);

              reslut.DataTable = dt;
            }
            catch (Exception exp)
            {
                reslut.Code = 1;
                reslut.Msg = exp.Message;
            }

            return reslut;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public RemotingSQLResult getDataTable(string sql, Hashtable parsHashtable)
        {
            RemotingSQLResult reslut = new RemotingSQLResult();
            try
            {
                int i = parsHashtable.Count;
                //以下构造存储过程参数
                SqlParameter[] cmdParms = new SqlParameter[i];
                int j = 0;
                foreach (DictionaryEntry de in parsHashtable)
                {
                    cmdParms[j] = new SqlParameter(de.Key.ToString(), de.Value);
                    j++;
                }


                DataTable dt = SqlCmd.getDataTable(sql, cmdParms);

                reslut.DataTable = dt;
            }
            catch (Exception exp)
            {

                reslut.Code = 1;
                reslut.Msg = exp.Message;
            }

            return reslut;
        }

        /// <summary>
        /// 执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public RemotingSQLResult ExecuteNonQuery(string sql, Hashtable parsHashtable)
        {
            RemotingSQLResult reslut = new RemotingSQLResult();

            try
            {

                int i = parsHashtable.Count;
                //以下构造存储过程参数
                SqlParameter[] cmdParms = new SqlParameter[i];
                int j = 0;
                foreach (DictionaryEntry de in parsHashtable)
                {
                    cmdParms[j] = new SqlParameter(de.Key.ToString(), de.Value);
                    j++;
                }
                int r= SqlCmd.ExecuteNonQuery(sql, cmdParms);
                //reslut.ResultDataType = 1;
                reslut.Total = r;
            }
            catch (Exception exp)
            {
                reslut.Code = 1;
                reslut.Msg = exp.Message;
            }

            return reslut;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public bool BoolExecuteNonQuery(string sql, Hashtable parsHashtable)
        {
            int i = parsHashtable.Count;
            //以下构造存储过程参数
            SqlParameter[] cmdParms = new SqlParameter[i];
            int j = 0;
            foreach (DictionaryEntry de in parsHashtable)
            {
                cmdParms[j] = new SqlParameter(de.Key.ToString(), de.Value);
                j++;
            }
            return SqlCmd.BoolExecuteNonQuery(sql, cmdParms);
        }


        public bool DoTran(List<string> sqlComm)
        {
            return SqlCmd.DoTran(sqlComm);
        }

        //public bool DoTran(List<string> sqlComm, List<SerSqlParameter[]> sersqlParsArrayList)
        //{
        //    List<SqlParameter[]> sqlParsArrayList = new List<SqlParameter[]>();

        //    foreach (SerSqlParameter[] serparaArray in sersqlParsArrayList)
        //    {
        //        sqlParsArrayList.Add(SerSqlParameterHelper.ConverToSqlParameterArray(serparaArray));
        //    }

        //    return SqlCmd.DoTran(sqlComm, sqlParsArrayList);
        //}

        ////public  bool DoTran( List<NFXmlSql> nfxmlsqlList)
        ////{
        ////    return SqlCmd.DoTran(nfxmlsqlList);
        ////}
       
    }

}
