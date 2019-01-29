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
    public class UpdateFileService 
    {

        public UpdateFileService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        public static List<Models.UpdateFile> GetList()
        {
            try
            {
                string strSql = @"select  * from a_system_updatefile order by uptime desc ";

                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@linkname", "")
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);
                if (dt != null && dt.Rows.Count > 0)
                {

                    List<Models.UpdateFile> list = Utils.DataTableEntityConverter.ConvertToEntityList<Models.UpdateFile>(dt);
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
        public static Models.UpdateFile GetById(int id)
        {
            try
            {
                string strSql = @"select  * from a_system_updatefile where id=@id";
                //构建一个哈希表，把参数依次压入
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Models.UpdateFile list = Utils.DataTableEntityConverter.ConvertToEntity<Models.UpdateFile>(dt.Rows[0]);
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



       
        public static void  Add(Models.UpdateFile uploadFile)
        {
            try
            {
                string strSql = @"insert into   a_system_updatefile (filename,filesize,versionno,savedir,uptime)
                values(@filename,@filesize,@versionno,@savedir,getdate())";
                
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@filename",uploadFile.Filename),
                    new SqlParameter("@filesize",uploadFile.Filesize),
                    new SqlParameter("@versionno", uploadFile.Versionno),
                    new SqlParameter("@savedir", uploadFile.Savedir),
                };
                DBHelper.ExecuteNonQuery(strSql, parames);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static void Update(Models.UpdateFile uploadFile)
        {
            try
            {
                string strSql = @"update   a_system_updatefile
                set
                    filename=@filename,
                    filesize=@filesize,
                    versionno=@versionno,
                    savedir=@savedir,
                    uptime=getdate()
                where id=@id";


                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",uploadFile.Id),
                    new SqlParameter("@filename",uploadFile.Filename),
                    new SqlParameter("@filesize",uploadFile.Filesize),
                    new SqlParameter("@versionno", uploadFile.Versionno),
                    new SqlParameter("@savedir", uploadFile.Savedir),
                };
                DBHelper.ExecuteNonQuery(strSql, parames);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void Delete(Models.UpdateFile uploadFile)
        {
            try
            {
                string strSql = @"delete  from a_system_updatefile where id=@id";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@id",uploadFile.Id),
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