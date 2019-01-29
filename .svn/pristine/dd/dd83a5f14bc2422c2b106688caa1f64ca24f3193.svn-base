using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// DataDownload 的摘要说明
    /// </summary>
    public class DataDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string xmlStr = "";
            string actionname = context.Request["Action"];
            if(actionname == null || actionname.Length <=0)
            {
               //异常输出
               xmlStr += @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += AddField("result","false");
                xmlStr += AddField("message", "unknow Action! [" + actionname + "]");
                xmlStr += AddField("rows","0");
                xmlStr += "</summary>";
                xmlStr += "</data>";

                context.Response.Write(xmlStr);
                return;
            }

            try
            {

                switch (actionname)
                {
                    case "sqlselect":
                        xmlStr = SqlSelect(context);
                        break;
                    default: 
                        break;
                }
            }
            catch (Exception  exp)
            {
                //异常输出
                //context.Response.Write();
                //异常输出
                xmlStr += @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += AddField("result", "false");
                xmlStr += AddField("message",  exp.Message);
                xmlStr += AddField("rows", "0");
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

        private string AddField(string fieldname,string value)
        {
            return "<" + fieldname + "><![CDATA[" + value + "]]></" + fieldname + ">";
        }
        private string SqlSelect(HttpContext context)
        {
            string sqlcmd = context.Request["sqlcmd"];
            sqlcmd = Common.Base.DESEncrypt.Decrypt(sqlcmd); //解密；
            try
            {
                DataTable dt = DBHelper.DataTableDBExecuteSqlCommand(sqlcmd);
                if (dt != null)
                {
                    return Common.Base.XmlHelper.ConvertDataTableToXML(dt);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return "";
        }
    }
}