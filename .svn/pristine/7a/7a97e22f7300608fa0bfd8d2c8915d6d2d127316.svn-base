using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{
    /// <summary>
    /// LicenseFile 的摘要说明
    /// </summary>
    public class FileUploadLicense : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            //合法性校验;
            bool isSavedSuccessfully = true;
            int count = 0;
            string msg = "";


            //判断是否为有效的许可文件;
            //格式
            //文件校验;

            //string folder = "";
            string folder = HttpContext.Current.Request["folder"];
            if (folder == null)
                folder = "";
            MyLicense myLicense = new MyLicense();
            JArray jArray = new JArray();
            try
            {
                string directoryPath = context.Server.MapPath("~/LicenseFile/");
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


                            //判断文件类型;
                            string fileName = file.FileName;
                            string fileExtension = Path.GetExtension(fileName);

                            if (fileExtension.ToLower() != ".lic")
                            {
                               
                                isSavedSuccessfully = false;
                                msg = "请上传正确的.lic格式文件。";
                                break;
                            }

                            string fileNewName_tmp = "License_tmp" + fileExtension;
                            string filePath_tmp = Path.Combine(directoryPath, fileNewName_tmp);

                            //检查文件是否存在，如果已经存在则删除；
                            if (System.IO.File.Exists(filePath_tmp))
                            {
                                //删除原有文件;
                                System.IO.File.Delete(filePath_tmp);
                            }
                            
                            //保存文件;
                            file.SaveAs(filePath_tmp);

                            //从文件中读取授权信息;
                            myLicense = MyLicenseHelper.Get(filePath_tmp);
                            if (myLicense.licenseno == "")
                            {
                                //删除;
                                System.IO.File.Delete(filePath_tmp);

                                //
                                isSavedSuccessfully = false;
                                msg = "授权信息错误。";
                                break;
                            }
                            else
                            {
                                isSavedSuccessfully = true;
                                msg = "";
                            }

                            string fileNewName = "License" + fileExtension;
                            string filePath = Path.Combine(directoryPath, fileNewName);
                            if (System.IO.File.Exists(filePath))
                            {
                                //删除原有授权文件;
                                System.IO.File.Delete(filePath);
                            }

                            if (System.IO.File.Exists(filePath_tmp))
                            {
                                //将临时文件-改名为正式文件
                                System.IO.File.Move(filePath_tmp, filePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
            }

            JObject joResult = new JObject();
            if (isSavedSuccessfully)
            {
                joResult.Add("access", "true");
                joResult.Add("msg", "");
            }
            else
            {
                joResult.Add("access", "false");
                joResult.Add("msg", msg);
            }

            if (myLicense.licenseno == "")
            {
                joResult.Add("licenseno", "未授权");
                joResult.Add("licensedatestart", "未授权");
                joResult.Add("licensedateend", "未授权");
            }
            else
            {
                joResult.Add("licenseno", myLicense.licenseno.ToString());
                joResult.Add("licensedatestart", myLicense.licensedatestart.ToString("yyyy-MM-dd"));
                joResult.Add("licensedateend", myLicense.licensedateend.ToString("yyyy-MM-dd"));
            }

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