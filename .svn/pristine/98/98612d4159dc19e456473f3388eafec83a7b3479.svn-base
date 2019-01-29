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
    public class QuartzNetService 
    {

        public QuartzNetService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Models.Model_QuartzNetItem> GetList()
        {
            try
            {
                string strSql = @"select  * from api_quartz order by quartzname asc ";

                //
                SqlParameter[] parames = new SqlParameter[]
                {
                    
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);
                if (dt != null)
                {

                    List<Models.Model_QuartzNetItem> list = Utils.DataTableEntityConverter.ConvertToEntityList<Models.Model_QuartzNetItem>(dt);
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
        public static Models.Model_QuartzNetItem GetById(int id)
        {
            try
            {
                string strSql = @"select  * from api_quartz where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Models.Model_QuartzNetItem list = Utils.DataTableEntityConverter.ConvertToEntity<Models.Model_QuartzNetItem>(dt.Rows[0]);
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




        public static void Add(Models.Model_QuartzNetItem item)
        {
            try
            {
                string strSql = @"insert into api_quartz (quartzname,quartznote,quartzstatus,crontrigger,jobtype,jobpars,mod_man,mod_date)
                    values (@quartzname,@quartznote,@quartzstatus,@crontrigger,@jobtype,@jobpars,'',getdate())";
                SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("@quartzname", item.Quartzname==null? "":item.Quartzname),
                        new SqlParameter("@quartznote",  item.Quartznote==null? "":item.Quartznote),
                        new SqlParameter("@quartzstatus", item.Quartzstatus==null? "":item.Quartzstatus), 
                        new SqlParameter("@crontrigger", item.Crontrigger==null? "":item.Crontrigger),
                        new SqlParameter("@jobtype",  item.Jobtype==null? "":item.Jobtype),
                        new SqlParameter("@jobpars",  item.Jobpars==null? "":item.Jobpars)
                };

                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static void Update(Models.Model_QuartzNetItem item)
        {
            try
            {
                string strSql = @"update  api_quartz set 
                   
                    quartzname=@quartzname,
                    quartznote=@quartznote,
                    quartzstatus=@quartzstatus,
                    crontrigger=@crontrigger,
                    jobtype=@jobtype,
                    jobpars=@jobpars,
                    mod_date=getdate(),
                    mod_man=''
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", item.Id),
                      new SqlParameter("@quartzname", item.Quartzname==null? "":item.Quartzname),
                        new SqlParameter("@quartznote",  item.Quartznote==null? "":item.Quartznote),
                        new SqlParameter("@quartzstatus", item.Quartzstatus==null? "":item.Quartzstatus), 
                        new SqlParameter("@crontrigger", item.Crontrigger==null? "":item.Crontrigger),
                        new SqlParameter("@jobtype",  item.Jobtype==null? "":item.Jobtype),
                        new SqlParameter("@jobpars",  item.Jobpars==null? "":item.Jobpars)
                    };
                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void UpdateStatus(Models.Model_QuartzNetItem item)
        {
            try
            {
                string strSql = @"update  api_quartz set   quartzstatus=@quartzstatus  where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", item.Id),
                        new SqlParameter("@quartzstatus", item.Quartzstatus==null? "":item.Quartzstatus)
                    };
                DBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public static void Delete(Models.Model_QuartzNetItem apiItem)
        {
            try
            {
                string strSql = @"delete  from api_quartz where id=@id";
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