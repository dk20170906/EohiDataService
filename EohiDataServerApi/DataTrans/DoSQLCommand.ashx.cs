using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// DoSQLCommand 的摘要说明
    /// </summary>
    public class DoSQLCommand : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
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
                    case "sqlselect":
                        xmlStr = SqlSelect(context);
                        break;
                    case "sqlexec":
                        xmlStr = SqlExec(context);
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

        private string SqlExec(HttpContext context)
        {
           string sqlcmd = context.Request["sqlcmd"];
		   string sqlcmdpars = context.Request["sqlcmdpars"];

           if (sqlcmd == null)
           {
               string xmlStr = "";
               xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
               xmlStr += "<data>";
               xmlStr += "<summary>";
               xmlStr += Common.Base.XmlHelper.AddField("result", "false");
               xmlStr += Common.Base.XmlHelper.AddField("message", "为找到指定的参数 [sqlcmd]");
               xmlStr += Common.Base.XmlHelper.AddField("rows", "-1");
               xmlStr += "</summary>";
               xmlStr += "</data>";

               return xmlStr;
           }
           else
           {
               sqlcmd = Common.Base.DESEncrypt.Decrypt(sqlcmd); //解密；
           }


           if (sqlcmdpars != null)
               sqlcmdpars = Common.Base.DESEncrypt.Decrypt(sqlcmdpars); //解密；

           
            int rows = 0;
            try
            {
                

                if (sqlcmdpars != null)
                {
                    SqlParameter[] pars = Common.Base.SqlParameterConvert.ConvertDbParameterXmlToSqlParameterArray(sqlcmdpars);
                    rows = DBHelper.DBExecuteNonQuery(sqlcmd,pars);
                }
                else
                {
                    rows = DBHelper.DBExecuteNonQuery(sqlcmd);
                }


                string xmlStr = "";
                xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
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
            {//异常输出
                string xmlStr = "";
                xmlStr = @"<?xml version=" + "\"1.0\" encoding=\"UTF-8\"?>";
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
                    dt = DBHelper.DataTableDBExecuteSqlCommand(sqlcmd,pars);
                }
                else
                {
                    dt = DBHelper.DataTableDBExecuteSqlCommand(sqlcmd);
                }
             
                if (dt != null)
                {
                    return Common.Base.XmlHelper.ConvertDataTableToXML(dt);
                }

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