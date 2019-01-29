using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{
    public class AdminAccountService 
    {

        public AdminAccountService()
        {
            //获取 sqlmapper；
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static Models.AdminAccount GetbyUserId(string user_id)
        {
            try
            {
                string strSql = @"select top 1 * from a_user_admin where user_id=@user_id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@user_id", user_id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);

                //this.gridControl_report.DataSource = result.DataTable;
                if (dt!=null && dt.Rows.Count > 0)
                {
                    Models.AdminAccount entity = Utils.DataTableEntityConverter.ConvertToEntity<Models.AdminAccount>(dt.Rows[0]);
                    return entity;
                }
                else
                    return null;

            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        public static Models.AdminAccount UpdatePwd(string user_id,string password)
        {
            try
            {
                string strSql = @"update a_user_admin set user_password=@password where user_id=@user_id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@user_id", user_id) ,
                    new         SqlParameter("@password",password)
                };


         int mint = DBHelper.DBExecuteNonQuery(strSql, parames);

                //this.gridControl_report.DataSource = result.DataTable;
                if (mint > 0)
                {
                    return GetbyUserId(user_id);
                }
                else
                    return null;

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}