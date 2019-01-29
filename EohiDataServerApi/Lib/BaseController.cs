using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace EohiDataServerApi.Lib
{
    public class BaseController : Controller
    {
        public HttpRequest GetRequest()
        {
            System.Web.HttpRequest _req;
            if (System.Web.HttpContext.Current != null)//当前请求
                _req = System.Web.HttpContext.Current.Request;
            else
                _req = null;
            return _req;
        }

        public string GetIP(HttpRequest _req)
        {
            string ip = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(_req.ServerVariables["HTTP_VIA"]))
                    ip = Convert.ToString(_req.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            }
            catch { }
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(_req.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }

        public virtual ActionResult Visit(Func<AccountToken, object> action)
        {
            return VisitCore(action);
        }

        protected virtual ActionResult VisitCore(Func<AccountToken, object> action)
        {
            var json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                //验证token
                HttpRequest req = GetRequest();
                string token = req["loginToken"] ?? "";
                int data = 1;// CheckTool.CheckToken(token.Trim());
                if (data == 1)
                {
                    json.Data = action.Invoke(new AccountToken()
                    {
                        appid = "0",
                        id = 0,
                        sign = "",
                        timestamp = DateTime.Now,
                        token = token,
                        userID = req["loginID"] ?? "",
                        userAccount = req["loginAccount"] ?? "",
                        userName = req["loginName"] ?? ""
                    });
                }
                else
                {
                    json.Data = new ServiceResult() { code = 0, msg = "token令牌无效,请重新登陆!" };
                }
            }
            catch (Exception ex)
            {
                json.Data = new ServiceResult() { code = 0, msg = ex.Message };                
            }
            return json;
        }

        public string SerializeDataTableXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            pDt.TableName = "tableName";
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }
        public DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }
        /// <summary>
        /// 控制表编码从10000开始
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">需要控制到字段</param>
        /// <returns></returns>
        public string GetIdCode(string tableName, string fieldName) {
            string sql = "SELECT ISNULL(MAX(CONVERT(INT, " + fieldName + ")),10000)+1 FROM " + tableName;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.GetConnSting, CommandType.Text, sql);
            return obj.ToString();
        }
        /// <summary>
        /// 控制表编码从10000开始 但是允许附加字符串在前面
        /// 同一张表里面允许多种不同的头单号  比如 XJ10001  HN10001  ZJ10001 等格式
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">需要控制到字段</param>
        /// <param name="hader">编码的头  比如:XXL  那么会生成 XXL10000</param>
        /// <returns></returns>
        public string GetGCode(string tableName, string fieldName, string hader) {
            string sql = "SELECT ISNULL(MAX(CONVERT(INT, REPLACE(" + fieldName + ",'" + hader + "',''))),10000)+1 FROM " + tableName + " WHERE " + fieldName + " LIKE '" + hader + "%'";
            object obj = SqlHelper.ExecuteScalar(SqlHelper.GetConnSting, CommandType.Text, sql);
            return hader + obj;
        }
    }
}