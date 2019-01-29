using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// FileCommon 的摘要说明
    /// </summary>
    public class FileCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action.ToLower())
                {
                    case "upload": Upload(); break;
                    case "delete": Delete(); break;
                    case "updete": break;
                    case "select": Select(); break;
                    case "ckecked": Checked(); break;
                    case "create": Create(); break;
                    case "down": DownFile(); break;
                    case "uploadfelod": UploadFelod(); break;
                    case "downurlfile": DownUrlFile(); break;
                }
            }
        }

        private void DownUrlFile() {
            String fileName = HttpContext.Current.Request["fileName"];
            string path = HttpContext.Current.Server.MapPath(fileName);
            if(File.Exists(path))
                HttpContext.Current.Response.WriteFile(path);
        }

        /// <summary>
        /// 上传文件到指定位置
        /// url参数:action=uploadfelod&felod=report&fileName=report000001.biz
        /// </summary>
        private void UploadFelod() {
            String felod = HttpContext.Current.Request["felod"];
            String fileName = HttpContext.Current.Request["fileName"];
            string path = HttpContext.Current.Server.MapPath("~/" + felod + "/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                //检查文件是否存在，存在删除；
                if (File.Exists(path + fileName))
                {
                    File.Delete(path + fileName);
                }
                HttpContext.Current.Request.Files[0].SaveAs(path + fileName);
            }
            HttpContext.Current.Response.Write(1);
        }

        /*
            下载文件
            url参数:action=down&md5=xxxxxx
         */
        private void DownFile()
        {
            string md5 = HttpContext.Current.Request["md5"];
            string sql = "select filepath from a_file_md5 where md5='" + md5 + "'";
            string path = DBHelper.DBExecuteString(sql);
            if (!string.IsNullOrEmpty(path))
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/" + path));
            else
                HttpContext.Current.Response.Write(0);
        }

        /*
            获取文件列表
            url参数:action=select         
         */
        private void Select()
        {
            string sql = @"select a_file_list.*,a_file_md5.filepath from a_file_list
left join a_file_md5 on(a_file_md5.md5 =a_file_list.md5) ";
            string keyval = HttpContext.Current.Request["keyval"];
            if (keyval != null)
            {
                //
                sql += " where keyval =@keyval ";
            }
            else
            {
                keyval = "";
            }

            sql += " order by uptime asc";

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@keyval",keyval)
            };
            DataSet ds = DBHelper.DataSetDBExecuteSqlCommand(sql, pars);
            
            ds.DataSetName = "data";
            ds.Tables[0].TableName = "item";
            string xml = ds.GetXml();
            HttpContext.Current.Response.Write(xml);
        }

        /*
            创建目录
            url参数:action=create&name=xxxxxx&pid=(int)
         */
        private void Create()
        {
            string pid = HttpContext.Current.Request["pid"];
            string name = HttpContext.Current.Request["name"];
            if (string.IsNullOrEmpty(pid)) pid = "-1";
            string sql = "insert into [a_file_catalog]";
            sql += "(pid,[catalogname])";
            sql += "values(" + pid + ",'" + name + "')";

            if (DBHelper.BoolDBExecuteNonQuery(sql))
                HttpContext.Current.Response.Write(1);
            else HttpContext.Current.Response.Write(0);
        }

        /*
            检查文件是否存在
            url参数:action=checked&md5=xxxxxx         
         */
        private void Checked()
        {
            string md5 = HttpContext.Current.Request["md5"];
            string sql = "select count(*) from a_file_md5 where md5='" + md5 + "'";
            if (DBHelper.DBExecuteScalar(sql) > 0)
            {
                HttpContext.Current.Response.Write(1);
            }
            else HttpContext.Current.Response.Write(0);

        }
        /// <summary>
        /// 删除文件;
        /// </summary>
        private void Delete()
        {
            try
            {
                string md5 = HttpContext.Current.Request["md5"];
                string keyval = HttpContext.Current.Request["keyval"];
                string sql = "delete from a_file_list where md5=@md5 and keyval=@keyval";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@md5",md5),
                    new SqlParameter("@keyval",keyval)
                };
                if (DBHelper.DBExecuteNonQuery(sql, pars) > 0)
                {
                    HttpContext.Current.Response.Write(1);
                }
                else 
                    HttpContext.Current.Response.Write(0);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Write(0); 
            }

        }

        /*
            上传文件
            url参数:action=upload&md5=xxxxxx&user=xxxx&key=xxxxxx
         */
        private void Upload()
        {
            string path = HttpContext.Current.Server.MapPath("~/uploads/" + DateTime.Now.ToString("yyyy") + "/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string ext = System.IO.Path.GetExtension(HttpContext.Current.Request.Files[0].FileName);
            string filename = HttpContext.Current.Request["md5"] + ext;
            string key = HttpContext.Current.Request["key"];
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path + filename);
                //生成缩略图;
                if (ext.ToLower() == ".jpg"
                       || ext.ToLower() == ".bmp"
                       || ext.ToLower() == ".png"
                       || ext.ToLower() == ".gif"
                      )
                {
                    //生成缩略图;
                   
                }

                //检查是否已经存在
                if (DBHelper.DBExecuteScalar("select count(*) from a_file_md5 where md5='" + HttpContext.Current.Request["md5"] + "'") == 0)
                {
                    string sql = "insert into a_file_md5 (md5,filepath)values('" + HttpContext.Current.Request["md5"] + "','uploads\\" + DateTime.Now.ToString("yyyy") + "\\" + filename + "')";
                    DBHelper.DBExecuteNonQuery(sql);
                }
                string insql = "insert into a_file_list (md5,filename,uptime,upuser,keyval)values('" + HttpContext.Current.Request["md5"] + "','" + HttpContext.Current.Request.Files[0].FileName + "',getdate(),'" + HttpContext.Current.Request["user"] + "','" + key + "')";
                DBHelper.DBExecuteNonQuery(insql);

                HttpContext.Current.Response.Write(1);
            }
            else HttpContext.Current.Response.Write(0);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}