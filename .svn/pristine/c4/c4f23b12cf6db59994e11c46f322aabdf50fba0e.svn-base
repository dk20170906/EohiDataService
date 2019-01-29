using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{
    /// <summary>
    /// FileUpLoadLayuiEditor 的摘要说明
    /// </summary>
    public class FileUpLoadLayuiEditor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //合法性校验;
            bool isSavedSuccessfully = true;
            int count = 0;
            string msg = "";


            //string folder = "";
            string folder = HttpContext.Current.Request["folder"];
            if (folder == null)
                folder = "";


            //JArray jArray = new JArray();
            JObject joResult_Img = null;// new JObject();
            try
            {
                string day = DateTime.Now.ToString("yyyy-MM-dd");
                string directoryPath = context.Server.MapPath("~/Upload/" + day);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                //判断是否有文件提交'
                if (context.Request.Files != null && context.Request.Files.Count > 0)
                {

                    for (var i = 0; i < context.Request.Files.Count; i++)
                    {
                        HttpPostedFile file = context.Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {

                            string fileName = file.FileName;
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNewName = Guid.NewGuid().ToString() + fileExtension;
                            string filePath = Path.Combine(directoryPath, fileNewName);


                            //检查文件是否存在，如果已经存在则删除；
                            file.SaveAs(filePath);


                            System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);

                            string fileurl = "/Upload/" + day + "/" + fileNewName;
                            ////上传成功;
                            //JObject json = new JObject();
                            //json.Add("filename", fileName);
                            //json.Add("filesavename", fileNewName);
                            //json.Add("size", file.ContentLength);
                            //json.Add("shorturl", fileurl);
                            //json.Add("url", fileurl);
                            //json.Add("urlsmall", fileurl);
                            //json.Add("version", info.FileVersion);
                            //jArray.Add(json);
                            joResult_Img = new JObject();
                            joResult_Img.Add("filename", fileName);
                            joResult_Img.Add("src", fileurl);
                          
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
            }
            /*
            return Json(new
            {
                Result = isSavedSuccessfully,
                Count = count,
                Message = msg
            });
            */

            //{
            //  "code": 0 //0表示成功，其它失败
            //  ,"msg": "" //提示信息 //一般上传失败后返回
            //  ,"data": {
            //    "src": "图片路径"
            //    ,"title": "图片名称" //可选
            //  }
            //}

            JObject joResult = new JObject();
            if (isSavedSuccessfully)
                joResult.Add("code", "0");
            else
                joResult.Add("code", "-1");
            joResult.Add("msg", msg);
            joResult.Add("data", joResult_Img);

            context.Response.ContentType = "json/plain";
            context.Response.Write(joResult.ToString());
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