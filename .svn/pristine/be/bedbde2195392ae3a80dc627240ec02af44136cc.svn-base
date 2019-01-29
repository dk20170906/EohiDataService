using EohiDataServerApi.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace EohiDataServerApi.Areas.DB.Controllers
{
    public class SQLController : BaseController
    {
        [ValidateInput(false)]
        public ActionResult GetDataTable()
        {
            return this.Visit((token) =>
            {
                DataTable table=new DataTable();
                try
                {
                    //解析数据;
                    /*
                    <?xml version="1.0" encoding="utf-8">
                    <items>
                        <item >
                            <cmd></cmd>
                            <parameters>
                                <parameter>
                                    <pname></pname>
                                    <pvalue></pvalue>
                                </parameter>
                            </parameters>
                        </item>
                        <item >
                            <cmd></cmd>
                            <pars>
                                <parameters>
                                    <pname></pname>
                                    <pvalue></pvalue>
                                </parameters>
                            </pars>
                        </item>
                    </items>
                    */



                    string xmldata = Request.Params["xmldata"];
                    if (xmldata == null)
                    {
                        return new ServiceResult() { code = 0, data =null, msg = "参数错误，需要参数[xmldata]", total = 0 };
                    }
                    xmldata =DESEncrypt.Decrypt(xmldata);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmldata);
                    //查找<items>
                    XmlNode root = xmlDoc.SelectSingleNode("items");
                    //获取到所有<items>的子节点
                    XmlNodeList nodeList = root.ChildNodes;
                    //遍历所有子节点
                    foreach (XmlNode xn in nodeList)
                    {
                        string sql = "";
                        List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                        //解析
                        sql = xn.SelectSingleNode("cmd").InnerText;
                        sql=DESEncrypt.Decrypt(sql);
                        //sql = Lib.EncryptUtils.DecryptString(sql, "NFERP"); //cmd;

                        //
                        XmlNode parametersNode = xn.SelectSingleNode("parameters");
                        XmlNodeList parametersList = parametersNode.ChildNodes;
                        foreach (XmlNode xnparameter in parametersList)
                        {
                            string pname = xnparameter.SelectSingleNode("pname").InnerText; ;// xnparameter["pname"].Value.ToString();
                            string pvalue = xnparameter.SelectSingleNode("pvalue").InnerText; //xnparameter["pvalue"].Value.ToString();

                            //增加参数;
                            sqlParameterList.Add(new SqlParameter(pname, pvalue));
                        }

                        //执行,
                        table = DBHelper.getDataTable(sql, sqlParameterList.ToArray());
                    }

                    return new ServiceResult() { code = 1, data = SerializeDataTableXml(table), msg = null, total = table.Rows.Count };
                }
                catch (Exception ex)
                {
                    return new ServiceResult() { code = 0, data = null, msg = ex.Message, total = 0 };
                }
            });
        }
    }
}
