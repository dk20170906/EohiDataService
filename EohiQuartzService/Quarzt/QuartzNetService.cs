using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiQuartzService.Quarzt
{
    public class QuartzNetService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public QuartzNetService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Model_QuartzNetItem> GetList()
        {
            try
            {
                string strSql = @"select  * from api_quartz order by quartzname asc ";

                //
                SqlParameter[] parames = new SqlParameter[]
                {

                };
                DataTable dt = QuartzDBHelper.getDataTable(strSql, parames);
                if (dt != null)
                {

                    List<Model_QuartzNetItem> list = DataTableEntityConverter.ConvertToEntityList<Model_QuartzNetItem>(dt);
                    return list;
                }
                else
                    return null;

            }
            catch (Exception exp)
            {  
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ GetList:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static Model_QuartzNetItem GetById(int id)
        {
            try
            {
                string strSql = @"select  * from api_quartz where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = QuartzDBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Model_QuartzNetItem list = DataTableEntityConverter.ConvertToEntity<Model_QuartzNetItem>(dt.Rows[0]);
                    return list;
                }
                else
                    return null;

            }
            catch (Exception exp)
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ GetById:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }




        public static void Add(Model_QuartzNetItem item)
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

                QuartzDBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ Add:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }


        public static void Update(Model_QuartzNetItem item)
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
                QuartzDBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ Update:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }

        public static void UpdateStatus(Model_QuartzNetItem item)
        {
            try
            {
                string strSql = @"update  api_quartz set   quartzstatus=@quartzstatus  where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    {
                        new SqlParameter("@id", item.Id),
                        new SqlParameter("@quartzstatus", item.Quartzstatus==null? "":item.Quartzstatus)
                    };
                QuartzDBHelper.ExecuteNonQuery(strSql, pars);

            }
            catch (Exception exp)
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ UpdateStatus:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }
        public static void Delete(Model_QuartzNetItem apiItem)
        {
            try
            {
                string strSql = @"delete  from api_quartz where id=@id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",apiItem.Id),
                };
                QuartzDBHelper.ExecuteNonQuery(strSql, parames);

            }
            catch (Exception exp)
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " QuartzNetService+ Delete:" + exp.Message);
                log.Info(start);
                throw exp;
            }
        }
    }
}
