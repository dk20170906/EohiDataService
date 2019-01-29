using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// DataUpload 的摘要说明
    /// </summary>
    public class DataUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            string xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
            string actionname = context.Request["Action"];
            if (actionname == null || actionname.Length <= 0)
            {
                //异常输出

                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += Common.Base.XmlHelper.AddField("result", "false");
                xmlStr += Common.Base.XmlHelper.AddField("message", "unknow Action! [" + actionname + "]");
                xmlStr += Common.Base.XmlHelper.AddField("rows", "0");
                xmlStr += "</summary>";
                xmlStr += "</data>";

                context.Response.Write(xmlStr);
                context.Response.End();
                return;
            }

            try
            {

                switch (actionname.ToLower())
                {
                    case "xmldatasave":
                        xmlStr += XmlDataSave( context);
                        break;
                    case "sqlexec":
                        xmlStr += SqlExec(context);
                        break;
                    default: 
                        break;
                }
            }
            catch (Exception exp)
            {
                //异常输出
                //context.Response.Write();
                //异常输出
                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += Common.Base.XmlHelper.AddField("result", "false");
                xmlStr += Common.Base.XmlHelper.AddField("message", exp.Message);
                xmlStr += Common.Base.XmlHelper.AddField("rows", "0");
                xmlStr += "</summary>";
                xmlStr += "</data>";
            }

            context.Response.Write(xmlStr);
            context.Response.End();
          
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string XmlDataSave(HttpContext context)
        {
            string xml = context.Request["xml"];
            return XmlSQLExec.XmlSave(xml);
        }
        private string SqlExec(HttpContext context)
        {
            string sqlcmd = context.Request["sqlcmd"];

            //sqlcmd = Common.Base.DESEncrypt.Decrypt(sqlcmd); //解密；
            int rows = 0;
            try
            {
              rows=  DBHelper.DBExecuteNonQuery(sqlcmd);


              string xmlStr = "";
              xmlStr += "<data>";
              xmlStr += "<summary>";
              xmlStr += Common.Base.XmlHelper.AddField("result", "true");
              xmlStr += Common.Base.XmlHelper.AddField("message", "");
              xmlStr += Common.Base.XmlHelper.AddField("rows", rows.ToString());
              xmlStr += "</summary>";
              xmlStr += "</data>";

              return xmlStr;
            }
            catch (Exception exp)
            {
                string xmlStr = "";
                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += Common.Base.XmlHelper.AddField("result", "false");
                xmlStr += Common.Base.XmlHelper.AddField("message", exp.Message);
                xmlStr += Common.Base.XmlHelper.AddField("rows", "0");
                xmlStr += "</summary>";
                xmlStr += "</data>";

                return xmlStr;
            }
            
        }
    }
}