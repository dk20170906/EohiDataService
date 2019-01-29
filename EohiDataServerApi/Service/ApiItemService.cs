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
    public class ApiItemService 
    {

        public ApiItemService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Models.Model_ApiItem> GetList()
        {
            try
            {
                string strSql = @"select  * from api_items order by apiname asc ";
                //
                SqlParameter[] parames = new SqlParameter[]
                {
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Models.Model_ApiItem> list = Utils.DataTableEntityConverter.ConvertToEntityList<Models.Model_ApiItem>(dt);
                    return list;
                }
                else
                    return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static Models.Model_ApiItem GetById(int id)
        {
            try
            {
                string strSql = @"select  * from api_items where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Models.Model_ApiItem list = Utils.DataTableEntityConverter.ConvertToEntity<Models.Model_ApiItem>(dt.Rows[0]);
                    return list;
                }
                else
                    return null;

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }




        public static void Add(Models.Model_ApiItem apiItem)
        {
            try
            {
                string strSql = @"insert into api_items (apiname,apistatus,apipars,apinote,apiscript,mod_man,mod_date)
                    values (@apiname,@apistatus,@apipars,@apinote,@apiscript,'',getdate())";
                SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("@apiname", apiItem.Apiname),
                        new SqlParameter("@apipars",  apiItem.Apipars),
                        new SqlParameter("@apinote", apiItem.Apinote),
                        new SqlParameter("@apiscript",  apiItem.Apiscript),
                        new SqlParameter("@apistatus",  apiItem.Apistatus)
                };


                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static void Update(Models.Model_ApiItem apiItem)
        {
            try
            {
                string strSql = @"update  api_items set 
                   
                    apiname=@apiname,
                    apistatus=@apistatus,
                    apipars=@apipars,
                    apinote=@apinote,
                    apiscript=@apiscript,
                    mod_date=getdate(),
                    mod_man=''
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", apiItem.Id),
                        new SqlParameter("@apiname", apiItem.Apiname),
                        new SqlParameter("@apipars",  apiItem.Apipars),
                        new SqlParameter("@apinote", apiItem.Apinote),
                        new SqlParameter("@apiscript",  apiItem.Apiscript),
                        new SqlParameter("@apistatus",  apiItem.Apistatus)
                    };
                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void Delete(Models.Model_ApiItem apiItem)
        {
            try
            {
                string strSql = @"delete  from api_items where id=@id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",apiItem.Id),
                };
                DBHelper.ExecuteNonQuery(strSql, parames);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}