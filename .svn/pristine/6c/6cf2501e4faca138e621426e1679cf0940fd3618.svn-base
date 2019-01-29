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
    public class WebAppService 
    {

        public WebAppService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Models.Model_WebApp> GetList()
        {
            try
            {
                string strSql = @"select  * from api_webapp order by id asc ";

                //
                SqlParameter[] parames = new SqlParameter[]
                {
                    
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);
                if (dt != null && dt.Rows.Count > 0)
                {

                    List<Models.Model_WebApp> list = Utils.DataTableEntityConverter.ConvertToEntityList<Models.Model_WebApp>(dt);
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
        public static Models.Model_WebApp GetById(int id)
        {
            try
            {
                string strSql = @"select  * from api_webapp where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Models.Model_WebApp list = Utils.DataTableEntityConverter.ConvertToEntity<Models.Model_WebApp>(dt.Rows[0]);
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




        public static void Add(Models.Model_WebApp webapp)
        {
            try
            {
                string strSql = @"insert into api_webapp (webappname,webappnote,webapphtml,webappscript,mod_man,mod_date)
                    values (@webappname,@webappnote,@webapphtml,@webappscript,'',getdate())";

                SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("@webappname", webapp.Webappname),
                        new SqlParameter("@webappnote",  webapp.Webappnote),
                        new SqlParameter("@webapphtml", webapp.Webapphtml),
                        new SqlParameter("@webappscript",  webapp.Webappscript)
                };


                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static void Update(Models.Model_WebApp webapp)
        {
            try
            {
                string strSql = @"update  api_webapp set 
                    webappname=@webappname,
                    webappnote=@webappnote,
                    webapphtml=@webapphtml,
                    webappscript=@webappscript,
                    mod_date=getdate()
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", webapp.Id),
                        new SqlParameter("@webappname",  webapp.Webappname),
                        new SqlParameter("@webappnote", webapp.Webappnote),
                        new SqlParameter("@webapphtml",  webapp.Webapphtml),
                        new SqlParameter("@webappscript",  webapp.Webappscript)
                    };
                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void Delete(Models.Model_WebApp webapp)
        {
            try
            {
                string strSql = @"delete  from api_webapp where id=@id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",webapp.Id),
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