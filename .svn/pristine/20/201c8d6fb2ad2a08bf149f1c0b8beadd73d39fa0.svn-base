using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace EohiDataServerApi
{
    public class TransactionCode
    {
        public TransactionCode()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }
        public static string ReReadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyString">事务号头字符串</param>
        /// <param name="length">事务号尾数部分长度 最大10，最小1</param>
        /// <param name="bAdd">是否把获取的编号标记在数据库中。true标记,false 不标记</param>
        /// <returns></returns>
        public static string GetTransactionCode(string keyString, int length, bool bAdd)
        {
            //NocsTools.Db
            SqlConnection myConn = new SqlConnection(ReReadConnectionString());
            SqlCommand MyCmd = new SqlCommand("pr_app_getTransactionCode", myConn);
            MyCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] pars = new SqlParameter[5];

            //        
            try
            {

                SqlParameter param1 = new SqlParameter("@codetype", SqlDbType.Int);
                param1.Value = 0;
                pars[0] = param1;

                param1 = new SqlParameter("@codekey", SqlDbType.VarChar,50);
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
                return exp.Message;
            }
            finally
            {
                MyCmd.Dispose();
                myConn.Close();
                myConn.Dispose();
            }

            return "get maskvalue error!";
        }


        
   
    }
}
