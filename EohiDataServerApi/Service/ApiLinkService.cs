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
    public class ApiLinkService 
    {

        public ApiLinkService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Models.Model_ApiLink> GetList()
        {
            try
            {
                string strSql = @"select  * from api_links order by id asc ";

                //
                SqlParameter[] parames = new SqlParameter[]
                {
                    
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);
                if (dt != null && dt.Rows.Count > 0)
                {

                    List<Models.Model_ApiLink> list = Utils.DataTableEntityConverter.ConvertToEntityList<Models.Model_ApiLink>(dt);
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
        public static Models.Model_ApiLink GetById(int id)
        {
            try
            {
                string strSql = @"select  * from api_links where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Models.Model_ApiLink list = Utils.DataTableEntityConverter.ConvertToEntity<Models.Model_ApiLink>(dt.Rows[0]);
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




        public static void Add(Models.Model_ApiLink link)
        {
            try
            {
                string strSql = @"insert into api_links (linkname,linktype,linkstring,mod_man,mod_date)
                    values (@linkname,@linktype,@linkstring,'',getdate())";

                SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("@linkname", link.Linkname),
                        new SqlParameter("@linktype",  link.Linktype),
                        new SqlParameter("@linkstring", link.Linkstring),
                };


                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static void Update(Models.Model_ApiLink link)
        {
            try
            {
                string strSql = @"update  api_links set 
                    linkname=@linkname,
                    linktype=@linktype,
                    linkstring=@linkstring
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", link.Id),
                        new SqlParameter("@linkname",  link.Linkname),
                        new SqlParameter("@linktype", link.Linktype),
                        new SqlParameter("@linkstring",  link.Linkstring)
                    };
                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void Delete(Models.Model_ApiLink link)
        {
            try
            {
                string strSql = @"delete  from api_links where id=@id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",link.Id),
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