using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// GetLicense 的摘要说明
    /// </summary>
    public class GetLicense : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            string directoryPath = context.Server.MapPath("~/LicenseFile/");
            string filepath = directoryPath + "/" + "License.lic";

            MyLicense license = MyLicenseHelper.Get(filepath);
            if (license.licenseno == "")
            {
                //item.system_licenseno = "未授权";
                //item.system_effdate_e = "未授权";
                //item.system_effdate_s = "未授权";
            }
            else
            {
                //item.system_licenseno = license.licenseno;
                //item.system_effdate_s = license.licensedatestart.ToString("yyyy-MM-dd");
                //item.system_effdate_e = license.licensedateend.ToString("yyyy-MM-dd");
            }

            string strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            strXml += "<License>";

            //
            strXml += "<hardwarecode><![CDATA[" + license.hardwarecode + "]]></hardwarecode>";
            strXml += "<licenseno><![CDATA[" + license.licenseno + "]]></licenseno>";
            strXml += "<licensedatestart><![CDATA[" + license.licensedatestart.ToString("yyyy-MM-dd") + "]]></licensedatestart>";
            strXml += "<licensedateend><![CDATA[" + license.licensedateend.ToString("yyyy-MM-dd") + "]]></licensedateend>";
            //
            strXml += "</License>";

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