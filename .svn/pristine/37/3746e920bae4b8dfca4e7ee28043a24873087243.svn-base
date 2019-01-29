using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace EohiDataServerApi
{
    public class XmlSQLExec
    {
        public XmlSQLExec() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static string XmlSave(string xml)
        {
            string msg = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                List<XmlToList> list = XmlSQLHelper.ConvertXmlToSqlCommand(doc);
                List<string> sqlList = new List<string>();
                List<SqlParameter[]> parsList = new List<SqlParameter[]>();
                for (int i = 0; i < list.Count; i++)
                {
                    sqlList.Add(list[i].SQL);
                    parsList.Add(list[i].PARS);
                }
                DBHelper.DoTran(sqlList, parsList, out msg);
                doc.RemoveAll();
                list.Clear();
                sqlList.Clear();
                parsList.Clear();
                if (msg.Trim() == "")
                    msg = "xmlsave_access";

                return msg;
            }
            catch (Exception exp)
            {
                msg = "xmlsave_err!" + exp.Message;
                return msg;
            }


            //return "";
        }



    }
}