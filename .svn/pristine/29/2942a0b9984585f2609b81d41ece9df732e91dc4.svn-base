using System;
using System.Net;
using System.IO;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Text;
using System.Xml;

namespace EohiDataServerApi
{

    public class WebHttpHelper
    {
        public static string HttpPost(string url, ParameterManager pm)
        {
            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //参数;
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in pm._dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Value.parname, item.Value.value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            string retString = "";
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                retString = reader.ReadToEnd();
            }

            return retString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strPost"></param>
        /// <returns></returns>
        public static string HttpPostXml(string url, string strPost)
        {
            string result = "";
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            //objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "text/xml";//提交xml 
            try
            {
                //声明一个XMLDoc文档对象，LOAD（）xml字符串
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strPost);
                //把XML发送出去
                //Response.Write(doc.InnerXml);
                //Response.End();

                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(doc.InnerXml);

                
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}