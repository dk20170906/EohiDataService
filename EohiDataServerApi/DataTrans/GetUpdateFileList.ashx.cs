using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// GetUpdateFileList 的摘要说明
    /// </summary>
    public class GetUpdateFileList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //string fileContent = string.Empty;
            //System.IO.StreamReader sr = new System.IO.StreamReader(context.Server.MapPath("update/update.xml"));
            //fileContent = sr.ReadToEnd();
            //sr.Close();


            string strsql= @"SELECT [id]
                  ,[filename]
                  ,[versionno]
                  ,[filesize]
                  ,[savedir]
                  ,[uptime]
                  ,[fileurl]
              FROM a_system_updatefile
            ";

            DataTable dt = DBHelper.DataTableDBExecuteSqlCommand(strsql);

            string strXml="<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            strXml+="<files>";
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strXml += "<file>";
                    //
                    strXml += "<filename><![CDATA[" + dt.Rows[i]["filename"].ToString() + "]]></filename>";
                    strXml += "<version><![CDATA[" + dt.Rows[i]["versionno"].ToString() + "]]></version>";
                    strXml += "<size><![CDATA[" + dt.Rows[i]["filesize"].ToString() + "]]></size>";
                    strXml += "<savepath><![CDATA[" + dt.Rows[i]["savedir"].ToString() + "]]></savepath>";
                    strXml += "<fileurl><![CDATA[" + dt.Rows[i]["fileurl"].ToString() + "]]></fileurl>";
                    strXml += "<uptime><![CDATA[" + dt.Rows[i]["uptime"].ToString() + "]]></uptime>";
                    //
                    strXml += "</file>";
                }
            }
            strXml += "</files>";

            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(strXml);
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