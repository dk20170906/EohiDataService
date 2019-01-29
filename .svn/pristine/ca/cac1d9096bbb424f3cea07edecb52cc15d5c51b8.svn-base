using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// FileCommon 的摘要说明
    /// </summary>
    public class FileFunction : IHttpHandler
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
                    case "down": DownFile(); break;
                }
            }
        }

        /*
            下载文件
            url参数:action=down&filename=xxxxxx
         */
        private void DownFile()
        {
            string filename = HttpContext.Current.Request["filename"];
            //string sql = "select filepath from a_file_md5 where md5='" + md5 + "'";
            string path = HttpContext.Current.Server.MapPath("~/function/") + filename;
            if (!string.IsNullOrEmpty(path))
                HttpContext.Current.Response.WriteFile(path);
            else
                HttpContext.Current.Response.Write(0);
        }

        /*
            上传文件
            url参数:action=upload&user=xxxx&key=xxxxxx
         */
        private void Upload()
        {
            string path = HttpContext.Current.Server.MapPath("~/function/" );
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
           
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                //string ext = System.IO.Path.GetExtension(HttpContext.Current.Request.Files[0].FileName);
                //string filename = HttpContext.Current.Request["md5"] + ext;
                //string key = HttpContext.Current.Request["key"];

                string filename = HttpContext.Current.Request.Files[0].FileName;

                HttpContext.Current.Request.Files[0].SaveAs(path + filename);
                

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