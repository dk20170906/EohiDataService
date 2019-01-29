using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.DataTrans
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string xmlStr = @"access";
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
    }
}