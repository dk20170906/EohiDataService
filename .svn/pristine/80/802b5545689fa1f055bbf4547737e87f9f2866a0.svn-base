using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// TransactionCode1 的摘要说明
    /// </summary>
    public class TransactionCode1 : IHttpHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int length = 0;
            bool bAdd = false;

            string key = context.Request["key"];
            string strlength = context.Request["length"];
            string strbAdd = context.Request["add"];


            string code = "get_maskvalue_error";
            try
            {
                if (key == null)
                    key = "NO_KEY";

                if (strlength != null)
                {
                    length = Convert.ToInt32(strlength);
                }

                if (strbAdd != null)
                {
                    if (strbAdd.ToLower().Equals("true"))
                        bAdd = true;
                }
            }
            catch (Exception)
            {
            }
             code = GetTransactionCode(key, length, bAdd);

            context.Response.Write(code);
            context.Response.End();
           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string GetTransactionCode(string keyString, int length, bool bAdd)
        {
            //NocsTools.Db
            SqlConnection myConn = new SqlConnection(DBHelper.ReReadConnectionString());
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
                return "-"+keyString + DateTime.Now.Ticks.ToString();
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