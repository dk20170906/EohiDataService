using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.FTP.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /FTP/Image/

        public ActionResult Index()
        {
            return View();
        }
        public FileResult View(string username, string pwd, string filepath)
        {

            //string ftpPath = "ftp://192.168.1.10/Pictures/201684/2016.jpg";
            string ftpPath = filepath;
            FtpWebRequest reqFTP;

            // 根据uri创建FtpWebRequest对象   
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath));

            // 指定执行什么命令  
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

            // 指定数据传输类型  
            //reqFTP.UseBinary = true;
            //reqFTP.UsePassive = false;

            //设置文件传输类型
            reqFTP.UseBinary = true;
            reqFTP.KeepAlive = false;
            reqFTP.UsePassive = true;
            //设置登录FTP帐号和密码
            reqFTP.Credentials = new NetworkCredential(username, pwd);


            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            // 把下载的文件写入流
            Stream ftpStream = response.GetResponseStream();

            /*
            long cl = response.ContentLength;

            // 缓冲大小设置为2kb  
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            //ftpStream.
            ftpStream.Read(buffer, 0, bufferSize);

            //关闭两个流和ftp连接
            //ftpStream.Close();
            //outputStream.Close();
            //response.Close();
            return File(buffer, "image/jpg");
            */




            // 缓冲大小设置为2kb  
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            MemoryStream mStream = new MemoryStream();
            //每次读文件流的2kb
            readCount = ftpStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                //把内容从文件流写入   
                //outputStream.Write(buffer, 0, readCount);
                mStream.Write(buffer, 0, readCount);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
            }
            //关闭两个流和ftp连接
            ftpStream.Close();
            mStream.Close();
            response.Close();
            return File(mStream.ToArray(), "image/jpg");

        }

    }
}
