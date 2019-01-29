using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace EohiDataServerApi
{
    [DisallowConcurrentExecution]     //禁止并发
    public class HttpRequestJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public string httpurl = "http://www.baidu.com";
        public void Execute(IJobExecutionContext context)
        {
            /*
            var reportDirectory = string.Format("~/reports/{0}/", DateTime.Now.ToString("yyyy-MM"));
            reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            var dailyReportFullPath = string.Format("{0}report_{1}.log", reportDirectory, DateTime.Now.Day);
            var logContent = string.Format("{0}==>>{1}{2}", DateTime.Now, "create new log.", Environment.NewLine);
            File.AppendAllText(dailyReportFullPath, logContent);
            */

            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "创建任务连接请求");
            log.Info(start);

            System.Net.HttpWebRequest myHttpWebRequest = null;
            System.Net.HttpWebResponse myHttpWebResponse = null;
            JobDataMap dataMap = null;
            string httpUrlex = null;
            try
            {
                dataMap = context.JobDetail.JobDataMap;
                string httpurl = dataMap.GetString("httpurl");
                httpUrlex = httpurl;
                myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpurl);
               // int timeOut= Convert.ToInt32(ConfigurationManager.AppSettings["QuarztHttpWebTimeOut"]);
              //  myHttpWebRequest.ReadWriteTimeout = timeOut;
              //  myHttpWebRequest.Timeout = timeOut;
                myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
                HttpStatusCode statusCode= myHttpWebResponse.StatusCode;
                string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob " + httpUrlex+"返回状态："+(int)statusCode+":"+ statusCode.ToString());
                log.Info(startm);
                #region 解析返回的内容
                //System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  
                // if (receiveStream!=null)
                // {
                //     var memoryStream = new MemoryStream();
                //     //将基础流写入内存流
                //     const int bufferLength = 1024*100;
                //     byte[] buffer = new byte[bufferLength];
                //     int actual = receiveStream.Read(buffer, 0, bufferLength);
                //     //if (actual > 0)
                //     //{
                //     //    memoryStream.Write(buffer, 0, actual);
                //     //}

                //     if (actual > 0)
                //     {
                //         string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute:" + httpurl + " HttpRequestJobreceiveStream+ Execute:" + actual + System.Text.Encoding.Default.GetString(buffer));
                //         log.Info(startm);
                //     }
                //     else
                //     {
                //         string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute:" + httpurl + " 请求成功，返回的stream长度为<=0");
                //         log.Info(startm);
                //     }

                // }
                // else
                // {
                //     string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute:" + httpurl+"请求失败，返回结果为null");
                //     log.Info(startm);
                // } 
                #endregion
            }
            catch (Exception exp)
            {
                string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute:" + exp.Message+":"+ httpUrlex);
                log.Info(startm);
                //throw;
            }
            finally
            {
                //任务完成或失败都要释资源
                if (dataMap!=null)
                {
                    dataMap.Clear();
                    dataMap.Clone();
                    string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute: dataMap.Clear();");
                    log.Info(startm);
                }
                if (myHttpWebRequest != null)
                {
                    myHttpWebRequest.Abort();
                    string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute: myHttpWebRequest.Abort();");
                    log.Info(startm);
                }
                if (myHttpWebResponse != null)
                {
                    myHttpWebResponse.Close();
                    string startm = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), " HttpRequestJob+ Execute: myHttpWebResponse.Close();");
                    log.Info(startm);
                }
            }
        }
    }
}