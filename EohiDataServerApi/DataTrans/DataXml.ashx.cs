using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// DataXml 的摘要说明
    /// </summary>
    public class DataXml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String action = context.Request["action"];
            if (String.IsNullOrEmpty(action)) {
                context.Response.Write("通讯正常,请提供参数.");
                return;
            }

            switch (action.ToUpper()) {
                default: context.Response.Write("通讯正常,请提供参数.");
                    break;
                case "XMLSQL-1.0": XmlToSqlExec(); break;
                case "XMLSQL-2.0": XmlToSqlExec2(); break;
                case "EXECUTESCALAR": ExecuteScalar(); break;
                case "EXECUTENONQUERY": ExecuteNonQuery(); break;
            }
        }

        private void XmlToSqlExec2() {
            String xml = XmlToSqlCmd_v2.getPostContent();
            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(xml);
            DataSet ds = new DataSet("data");
            foreach (XmlToList xtl in list)
            {
                try
                {
                    DataTable table = DBHelper.DataTableDBExecuteSqlCommand(xtl.SQL, xtl.PARS);
                    if (!String.IsNullOrEmpty(xtl.TABLENAME)) table.TableName = xtl.TABLENAME;
                    ds.Tables.Add(table);
                }
                catch (SqlException exp)
                {
                    //exp.Message;
                    HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                    HttpContext.Current.Response.ContentType = "text/xml";
                    
                    HttpContext.Current.Response.Write("<sqlerr><![CDATA[" + exp.Message + "]]></sqlerr>");
                    
                    return;
                    //throw;
                }
            }

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/xml";

            string XmlSchema = ds.GetXmlSchema();
            string Xml = ds.GetXml();

            HttpContext.Current.Response.Write(XmlSchema + "<xmlsplit></xmlsplit>" + Xml);
        }

        /// <summary>
        ///  执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        private void ExecuteScalar()
        {
            object obj = null;
            String xml = XmlToSqlCmd_v2.getPostContent();
            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(xml);

            foreach (XmlToList xtl in list)
            {
                try
                {
                    obj = DBHelper.DBExecuteScalar(xtl.SQL, xtl.PARS);
                }
                catch (SqlException exp)
                {
                    //exp.Message;
                    HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                    HttpContext.Current.Response.ContentType = "text/xml";
                    HttpContext.Current.Response.Write("<sqlerr><![CDATA[" + exp.Message + "]]></sqlerr>");
                    return;
                    //throw;
                }
            }

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/xml";
            if (obj != null)
                HttpContext.Current.Response.Write("<sqlresult><![CDATA[" +obj.ToString()+ "]]></sqlresult>");
        }
 
        /// <summary>
        ///  执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        private void ExecuteNonQuery()
        {
            int rows = -1;
            String xml = XmlToSqlCmd_v2.getPostContent();
            List<XmlToList> list = XmlToSqlCmd_v2.ConvertXmlToSqlCommand(xml);

            foreach (XmlToList xtl in list)
            {
                try
                {
                    rows = DBHelper.DBExecuteNonQuery(xtl.SQL, xtl.PARS);
                }
                catch (SqlException exp)
                {
                    //exp.Message;
                    HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                    HttpContext.Current.Response.ContentType = "text/xml";

                    HttpContext.Current.Response.Write("<sqlerr><![CDATA[" + exp.Message + "]]></sqlerr>");

                    return;
                    //throw;
                }
            }

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/xml";

            HttpContext.Current.Response.Write("<sqlresult><![CDATA[" + rows.ToString() + "]]></sqlresult>");
        }

        private void XmlToSqlExec() {

            String xml = XmlSQLHelper.getPostContent();

            List<XmlToList> list = XmlSQLHelper.ConvertXmlToSqlCommand(xml);


            DataSet ds = new DataSet("data");
            foreach (XmlToList xtl in list) {
                 DataTable table = DBHelper.DataTableDBExecuteSqlCommand(xtl.SQL, xtl.PARS);
                 if (!String.IsNullOrEmpty(xtl.TABLENAME)) table.TableName = xtl.TABLENAME;
                 ds.Tables.Add(table);
            }

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.Write(ds.GetXml());
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