using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// WebSQL 的摘要说明
    /// </summary>
    public class WebSQL : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string xmlStr = @"";
            string actionname = context.Request["Action"];
            if (actionname == null || actionname.Length <= 0)
            {
                //异常输出
                xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
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
                    case "query":
                        xmlStr = SqlSelect(context);
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
                xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
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


        private string SqlSelect(HttpContext context)
        {
            string sqlcmd = context.Request["sqlcmd"];
            sqlcmd = Common.Base.DESEncrypt.Decrypt(sqlcmd); //解密；
            string sqlcmdpars = context.Request["sqlcmdpars"];
            if (sqlcmdpars != null)
                sqlcmdpars = Common.Base.DESEncrypt.Decrypt(sqlcmdpars); //解密；

            try
            {
                
                DataTable dt = null;
                if (sqlcmdpars != null)
                {
                    SqlParameter[] pars = Common.Base.SqlParameterConvert.ConvertDbParameterXmlToSqlParameterArray(sqlcmdpars);
                    dt = DBHelper.DataTableDBExecuteSqlCommand(sqlcmd, pars);
                }
                else
                {
                    dt = DBHelper.DataTableDBExecuteSqlCommand(sqlcmd);
                }

             
                DataSet ds = new DataSet("data");
                dt.TableName = "item";
                ds.Tables.Add(dt);

                string XmlSchema = ds.GetXmlSchema();
                string XmlData = ds.GetXml();

                return XmlSchema + "<xmlsplit></xmlsplit>" + XmlData ;

            }
            catch (Exception exp)
            {
                //异常输出
                //context.Response.Write();
                //异常输出
                string xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
                xmlStr += "<data>";
                xmlStr += "<summary>";
                xmlStr += Common.Base.XmlHelper.AddField("result", "false");
                if (exp.InnerException != null)
                    xmlStr += Common.Base.XmlHelper.AddField("message", exp.InnerException.Message);
                else
                    xmlStr += Common.Base.XmlHelper.AddField("message", exp.Message);
                xmlStr += Common.Base.XmlHelper.AddField("rows", "0");
                xmlStr += "</summary>";
                xmlStr += "</data>";

                return xmlStr;
                //throw;
            }

            return "";
        }

    }
}