using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace EohiDataServerApi
{
    public class XmlToSqlCmd_v2
    {
        public XmlToSqlCmd_v2() { }
        /// <summary>
        /// 获取POST提交的内容
        /// UTF-8编码
        /// </summary>
        /// <returns></returns>
        public static String getPostContent()
        {
            byte[] bt = new byte[HttpContext.Current.Request.InputStream.Length];
            HttpContext.Current.Request.InputStream.Read(bt, 0, bt.Length);
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        public static List<XmlToList> ConvertXmlToSqlCommand(String xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return ConvertXmlToSqlCommand(doc);
        }

        private static List<XmlToList> ConvertXmlToSqlCommand(XmlDocument xml)
        {
            List<XmlToList> list = new List<XmlToList>();
            try
            {
                XmlNodeList xmlList = xml.SelectSingleNode("data").ChildNodes;
                foreach (XmlNode node in xmlList)
                {
                    if (node.Name == "item")
                    {
                        XmlToList xtl = new XmlToList();
                        xtl.SQL = node.SelectNodes("cmd")[0].InnerText;
                        xtl.PARS = XmlToParameter(node.SelectNodes("cmdpars"));
                        if (node.SelectNodes("table").Count > 0)
                            xtl.TABLENAME = node.SelectNodes("table")[0].InnerText;
                        list.Add(xtl);
                    }
                
                }
            }
            catch (Exception ex) { }
            return list;
        }

        private static SqlParameter[] XmlToParameter(XmlNodeList xnl) {
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (XmlNode xnode in xnl.Item(0).ChildNodes)
                list.Add(new SqlParameter(xnode.Attributes["name"].Value, xnode.InnerText));
            return list.ToArray();
        }
    }
}