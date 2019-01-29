using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web;

namespace EohiDataServerApi
{

    public class XmlToList
    {
        public XmlToList() { }

        /// <summary>
        /// T-SQL语句
        /// </summary>
        public string SQL
        {
            get;
            set;
        }
        /// <summary>
        /// T-SQL语句对应的参数结果
        /// </summary>
        public SqlParameter[] PARS
        {
            get;
            set;
        }
        /// <summary>
        /// 返回结果的表名
        /// </summary>
        public String TABLENAME { get; set; }
    }

    public class XmlSQLHelper
    {
        public XmlSQLHelper()
        {
        }
        public static string XmlAddFiled(string fieldname, string value)
        {
            return "<" + fieldname + "><![CDATA[" + value + "]]></" + fieldname + ">";
        }
        /*
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
                List<XmlToList> list = ConvertXmlToSqlCommand(doc);
                List<string> sqlList = new List<string>();
                List<SqlParameter[]> parsList = new List<SqlParameter[]>();
                for (int i = 0; i < list.Count; i++)
                {
                    sqlList.Add(list[i].SQL);
                    parsList.Add(list[i].PARS);
                }
                Db.DBHelper.DoTran(sqlList, parsList, out msg);
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
        */

        /// <summary>
        /// 获取POST提交的内容
        /// UTF-8编码
        /// </summary>
        /// <returns></returns>
        public static String getPostContent(){
            byte[] bt = new byte[HttpContext.Current.Request.InputStream.Length];
            HttpContext.Current.Request.InputStream.Read(bt, 0, bt.Length);
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        
        public static List<XmlToList> ConvertXmlToSqlCommand(String xml){
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return ConvertXmlToSqlCommand(doc);
        }

        public static List<XmlToList> ConvertXmlToSqlCommand(XmlDocument xml)
        {
            List<XmlToList> list = new List<XmlToList>();
            try
            {
                XmlNodeList xmlList = xml.SelectSingleNode("data").ChildNodes;
                foreach (XmlNode node in xmlList)
                {
                    if (node.Name == "assistcmd")
                    {
                        //此XML的处理指令
                    }
                    else if (node.Name == "item")
                    {
                        XmlToList model = new XmlToList();

                        XmlNodeList xtable = node.SelectNodes("table");//表

                        XmlNodeList cmd = node.SelectNodes("cmd");//动作
                        if (xtable.Count == 1 && cmd.Count == 1)
                        {
                            if (cmd[0].InnerText.ToUpper() == "INSERT")
                            {
                                XmlNodeList xfield = node.SelectNodes("fields");//字段
                                //&& xfield.Count == 1
                                model = XmlToInseret(xtable, xfield);
                            }
                            else if (cmd[0].InnerText.ToUpper() == "UPDATE")
                            {
                                XmlNodeList xfield = node.SelectNodes("fields");//字段
                                XmlNodeList xwhere = node.SelectNodes("conditions");
                                model = XmlToUpdate(xtable, xfield, xwhere);
                            }
                            else if (cmd[0].InnerText.ToUpper() == "DELETE")
                            {
                                XmlNodeList xwhere = node.SelectNodes("conditions");
                                model = XmlToDelete(xtable, xwhere);
                            }
                            else if (cmd[0].InnerText.ToUpper() == "EXEC")
                            {

                                XmlNodeList xsqlparameters = node.SelectNodes("sqlparameters");
                                model = XmlToExec(xtable, xsqlparameters);
                            }
                        }
                        XmlNodeList tablename = node.SelectNodes("tablename");//返回结果的表名
                        if (tablename.Count > 0)
                            model.TABLENAME = cmd[0].InnerText;
                        else model.TABLENAME = "";

                        list.Add(model);
                    }
                    else
                    {
                        //意外数据
                    }
                }
            }
            catch (Exception ex)
            {
                //错误时
            }
            return list;
        }
        //转换为插入语句
        private static XmlToList XmlToInseret(XmlNodeList table, XmlNodeList fields)
        {
            XmlToList model = new XmlToList();
            List<SqlParameter> pars = new List<SqlParameter>();
            model.SQL = "INSERT INTO " + table.Item(0).InnerText + " ({0}) VALUES({1});";
            string field = string.Empty;
            string result = string.Empty;

            foreach (XmlNode xnode in fields.Item(0).ChildNodes)
            {
                string type = "string";

                if (xnode.Attributes.Count > 0)
                {
                    type = xnode.Attributes["type"].Value;
                    if (type.Length <= 0 || type == "undefined")
                        type = "string";
                }
                type = type.ToLower();


                //时间
                if (type == "date" || type == "datetime")
                {
                    if (xnode.InnerText == "null")
                    {
                        field += xnode.Name + ",";
                        result += "@" + xnode.Name + ",";

                        pars.Add(new SqlParameter("@" + xnode.Name, DBNull.Value));
                    }
                    else if (xnode.InnerText.ToLower().IndexOf("getdate()") >= 0)
                    {
                        //pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                        field += xnode.Name + ",";
                        result += "" + xnode.InnerText + ",";
                    }
                    else
                    {
                        field += xnode.Name + ",";
                        result += "@" + xnode.Name + ",";
                        pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                    }
                }
                else
                {
                    field += xnode.Name + ",";
                    result += "@" + xnode.Name + ",";

                    pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                }
            }
            field = field.Remove(field.Length - 1, 1);
            result = result.Remove(result.Length - 1, 1);

            model.SQL = string.Format(model.SQL, field, result);
            model.PARS = ConvertSqlParamsListToSqlParameters(pars);
            return model;
        }
        //转换为修改语句
        private static XmlToList XmlToUpdate(XmlNodeList table, XmlNodeList fields, XmlNodeList where)
        {
            XmlToList model = new XmlToList();
            List<SqlParameter> pars = new List<SqlParameter>();
            model.SQL = "UPDATE " + table.Item(0).InnerText + " SET {0} WHERE {1}";

            string field = string.Empty;
            string result = string.Empty;

            foreach (XmlNode xnode in fields.Item(0).ChildNodes)
            {

                string type = "string";
                if (xnode.Attributes.Count > 0)
                {
                    type = xnode.Attributes["type"].Value;
                    if (type.Length <= 0 || type == "undefined")
                        type = "string";
                }
                type = type.ToLower();

                if (type == "date" || type == "datetime")
                {
                    if (xnode.InnerText == "null")
                    {
                        field += xnode.Name + "=" + "@" + xnode.Name + ",";
                        pars.Add(new SqlParameter("@" + xnode.Name, DBNull.Value));
                    }
                    else if (xnode.InnerText.ToLower().IndexOf("getdate()") >= 0)
                    {
                        //pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                        field += xnode.Name + "=" + xnode.InnerText + ",";
                    }
                    else
                    {
                        field += xnode.Name + "=" + "@" + xnode.Name + ",";
                        pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                    }
                }
                else
                {
                    field += xnode.Name + "=" + "@" + xnode.Name + ",";
                    pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                }
                //pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
            }

            if (where.Count == 1)
            {
                foreach (XmlNode xnode in where.Item(0).ChildNodes)
                {
                    if (!string.IsNullOrEmpty(result))
                        result += " AND ";
                    result += xnode.Name + "=@C_" + xnode.Name;
                    pars.Add(new SqlParameter("@C_" + xnode.Name, xnode.InnerText));
                }
            }
            else
            {
                result = " 1=1 ";
            }
            if (!string.IsNullOrEmpty(field))
                field = field.Remove(field.Length - 1, 1);
            else
                throw new Exception("修改时找不到需要修改的字段.");

            model.PARS = ConvertSqlParamsListToSqlParameters(pars);
            model.SQL = string.Format(model.SQL, field, result);


            return model;
        }
        //转换为修改语句
        private static XmlToList XmlToDelete(XmlNodeList table, XmlNodeList where)
        {
            XmlToList model = new XmlToList();
            List<SqlParameter> pars = new List<SqlParameter>();
            model.SQL = "DELETE FROM  " + table.Item(0).InnerText + " WHERE {0}";

            string result = string.Empty;

            if (where.Count == 1)
            {
                foreach (XmlNode xnode in where.Item(0).ChildNodes)
                {
                    if (!string.IsNullOrEmpty(result))
                        result += " AND ";
                    result += xnode.Name + "=@C_" + xnode.Name;
                    pars.Add(new SqlParameter("@C_" + xnode.Name, xnode.InnerText));
                }
            }
            else
            {
                result = " 1=1 ";
            }

            model.PARS = ConvertSqlParamsListToSqlParameters(pars);
            model.SQL = string.Format(model.SQL, result);

            return model;
        }
        //转换为执行语句
        private static XmlToList XmlToExec(XmlNodeList table, XmlNodeList sqlparameters)
        {
            XmlToList model = new XmlToList();
            List<SqlParameter> pars = new List<SqlParameter>();
            model.SQL = table.Item(0).InnerText;

            string result = string.Empty;

            foreach (XmlNode xnode in sqlparameters.Item(0).ChildNodes)
            {
                string type = "string";

                if (xnode.Attributes.Count > 0)
                {
                    type = xnode.Attributes["type"].Value;
                    if (type.Length <= 0 || type == "undefined")
                        type = "string";
                }



                result += "@" + xnode.Name + ",";


                if (type == "date" || type == "datetime")
                {
                    if (xnode.InnerText == "null")
                        pars.Add(new SqlParameter("@" + xnode.Name, DBNull.Value));
                    else
                        pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                }
                else
                {
                    pars.Add(new SqlParameter("@" + xnode.Name, xnode.InnerText));
                }
            }
            if (result.Length > 0)
                result = result.Remove(result.Length - 1, 1);

            if (pars.Count > 0)
            {
                model.SQL = model.SQL + " {0}";

                model.SQL = string.Format(model.SQL, result);

            }


            model.PARS = ConvertSqlParamsListToSqlParameters(pars);


            return model;
        }

        //转换参数形式
        public static SqlParameter[] ConvertSqlParamsListToSqlParameters(List<SqlParameter> parList)
        {

            SqlParameter[] pars = new SqlParameter[parList.Count];

            for (int i = 0; i < parList.Count; i++)
            {
                pars[i] = parList[i];
            }

            //清除
            parList.Clear();

            return pars;
        }

        public static XmlToList ConvertXmlToQueryCondition(XmlDocument xml)
        {
            XmlToList model = new XmlToList();
            model.SQL = "";
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();

                #region 解析xml查询条件


                XmlNodeList xmlList = xml.SelectSingleNode("querycondition").ChildNodes;
                foreach (XmlNode node in xmlList)
                {
                    if (node.Name == "item")
                    {
                        //XmlToList model = new XmlToList();                    
                        XmlNodeList xfield = node.SelectNodes("field");//字段
                        XmlNodeList xvalue1 = node.SelectNodes("field_value_start");//
                        XmlNodeList xvalue2 = node.SelectNodes("field_value_end");//动作
                        if (xfield.Count == 1)
                        {
                            if (xvalue1.Count == 1 || xvalue2.Count == 1)
                            {

                                if (xvalue1.Count == 1)
                                {
                                    if (xvalue1.Item(0).Attributes.Count > 0)
                                    {
                                        string calculationoperator = xvalue1.Item(0).Attributes["calculationoperator"].Value;
                                        if (calculationoperator == "包含") calculationoperator = "like";
                                        else if (calculationoperator == "等于") calculationoperator = "=";
                                        else if (calculationoperator == "大于等于") calculationoperator = ">=";
                                        else if (calculationoperator == "小于等于") calculationoperator = "<=";
                                        else calculationoperator = "=";

                                        if (calculationoperator == "like")
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " like '%' +@" + xfield.Item(0).InnerText + "_start +'%' ";


                                            pars.Add(new SqlParameter("@" + xfield.Item(0).InnerText + "_start", xvalue1.Item(0).InnerText));
                                        }
                                        else
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + "  " + calculationoperator + " @" + xfield.Item(0).InnerText + "_start ";

                                            pars.Add(new SqlParameter("@" + xfield.Item(0).InnerText + "_start", xvalue1.Item(0).InnerText));
                                        }
                                    }
                                }
                                if (xvalue2.Count == 1)
                                {
                                    if (xvalue2.Item(0).Attributes.Count > 0)
                                    {
                                        string calculationoperator = xvalue2.Item(0).Attributes["calculationoperator"].Value;

                                        if (calculationoperator == "包含") calculationoperator = "like";
                                        else if (calculationoperator == "等于") calculationoperator = "=";
                                        else if (calculationoperator == "大于等于") calculationoperator = ">=";
                                        else if (calculationoperator == "小于等于") calculationoperator = "<=";
                                        else calculationoperator = "=";
                                        if (calculationoperator == "like")
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " like '%' +@" + xfield.Item(0).InnerText + "_end +'%' ";

                                            pars.Add(new SqlParameter("@" + xfield.Item(0).InnerText + "_end", xvalue2.Item(0).InnerText));
                                        }
                                        else
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " " + calculationoperator + " @" + xfield.Item(0).InnerText + "_end ";

                                            pars.Add(new SqlParameter("@" + xfield.Item(0).InnerText + "_end", xvalue2.Item(0).InnerText));
                                        }
                                    }
                                }
                            }

                        }

                        //list.Add(model);



                    }
                    else
                    {
                        //意外数据
                    }

                }
                #endregion

                model.PARS = ConvertSqlParamsListToSqlParameters(pars);
            }
            catch (Exception ex)
            {
                //错误时
            }

            return model;
        }

        public static XmlToList ConvertXmlToQueryCondition_toString(XmlDocument xml)
        {
            XmlToList model = new XmlToList();
            model.SQL = "";
            try
            {


                #region 解析xml查询条件


                XmlNodeList xmlList = xml.SelectSingleNode("querycondition").ChildNodes;
                foreach (XmlNode node in xmlList)
                {
                    if (node.Name == "item")
                    {
                        //XmlToList model = new XmlToList();                    
                        XmlNodeList xfield = node.SelectNodes("field");//字段
                        XmlNodeList xvalue1 = node.SelectNodes("field_value_start");//
                        XmlNodeList xvalue2 = node.SelectNodes("field_value_end");//动作
                        if (xfield.Count == 1)
                        {
                            if (xvalue1.Count == 1 || xvalue2.Count == 1)
                            {

                                if (xvalue1.Count == 1)
                                {
                                    if (xvalue1.Item(0).Attributes.Count > 0)
                                    {
                                        string calculationoperator = xvalue1.Item(0).Attributes["calculationoperator"].Value;
                                        if (calculationoperator == "包含") calculationoperator = "like";
                                        else if (calculationoperator == "等于") calculationoperator = "=";
                                        else if (calculationoperator == "大于等于") calculationoperator = ">=";
                                        else if (calculationoperator == "小于等于") calculationoperator = "<=";
                                        else calculationoperator = "=";

                                        if (calculationoperator == "like")
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " like '%" + xvalue1.Item(0).InnerText.Replace("'", "''") + "%' ";


                                        }
                                        else
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + "  " + calculationoperator + " '" + xvalue1.Item(0).InnerText.Replace("'", "''") + "' ";


                                        }
                                    }
                                }
                                if (xvalue2.Count == 1)
                                {
                                    if (xvalue2.Item(0).Attributes.Count > 0)
                                    {
                                        string calculationoperator = xvalue2.Item(0).Attributes["calculationoperator"].Value;

                                        if (calculationoperator == "包含") calculationoperator = "like";
                                        else if (calculationoperator == "等于") calculationoperator = "=";
                                        else if (calculationoperator == "大于等于") calculationoperator = ">=";
                                        else if (calculationoperator == "小于等于") calculationoperator = "<=";
                                        else calculationoperator = "=";
                                        if (calculationoperator == "like")
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " like '%" + xvalue2.Item(0).InnerText.Replace("'", "''") + "%' ";


                                        }
                                        else
                                        {
                                            if (model.SQL.Length > 0)
                                                model.SQL += " and ";
                                            model.SQL += " " + xfield.Item(0).InnerText + " " + calculationoperator + " '" + xvalue2.Item(0).InnerText.Replace("'", "''") + "' ";


                                        }
                                    }
                                }
                            }

                        }

                        //list.Add(model);


                    }
                    else
                    {
                        //意外数据
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {
                //错误时
            }

            return model;
        }

        /// <summary>
        /// 解析查询条件。
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static XmlToList ConvertXmlToQueryCondition_FullForamt(XmlDocument xml)
        {
            XmlToList model = new XmlToList();
            model.SQL = "";
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();

                #region 解析xml查询条件

                int conditonrowIndex = 0;

                XmlNodeList xmlList = xml.SelectSingleNode("data").ChildNodes;
                foreach (XmlNode nodeRoot in xmlList)
                {
                    if (nodeRoot.Name == "querycondition")
                    {


                        xmlList = nodeRoot.SelectNodes("item");// xml.SelectSingleNode("querycondition").ChildNodes;
                        foreach (XmlNode node in xmlList)
                        {
                            //if (node.Name == "item")
                            {
                                //XmlToList model = new XmlToList();                    
                                XmlNodeList xfieldname = node.SelectNodes("fieldname");//查询条件字段
                                XmlNodeList xfieldtype = node.SelectNodes("fieldtype");//查询条件字段
                                XmlNodeList xlbrackets = node.SelectNodes("lbrackets");//左括号
                                XmlNodeList xrbrackets = node.SelectNodes("rbrackets");//右括号
                                XmlNodeList xjoin = node.SelectNodes("join");//连接符
                                XmlNodeList xcompare = node.SelectNodes("compare");//比较符
                                XmlNodeList xvalue = node.SelectNodes("value");//值

                                if (xfieldname.Count == 1)//判断节点信息是否正确；判断依据，有且仅有一个字段节点信息
                                {
                                    //if (xvalue1.Count == 1 || xvalue2.Count == 1)

                                    // if (model.SQL.Length > 0)
                                    // ( and xxx  = xxx )
                                    if (xvalue.Item(0).InnerText.Length > 0)
                                    {
                                        if (xcompare.Item(0).InnerText.ToLower() == "like")
                                            model.SQL += " " + xjoin.Item(0).InnerText + " " + xlbrackets.Item(0).InnerText + " " + xfieldname.Item(0).InnerText + " " + xcompare.Item(0).InnerText + " '%'+@" + xfieldname.Item(0).InnerText + "_" + conditonrowIndex.ToString() + "+'%' " + xrbrackets.Item(0).InnerText;
                                        else
                                            model.SQL += " " + xjoin.Item(0).InnerText + " " + xlbrackets.Item(0).InnerText + " " + xfieldname.Item(0).InnerText + " " + xcompare.Item(0).InnerText + "     @" + xfieldname.Item(0).InnerText + "_" + conditonrowIndex.ToString() + " " + xrbrackets.Item(0).InnerText;

                                        pars.Add(new SqlParameter("@" + xfieldname.Item(0).InnerText + "_" + conditonrowIndex.ToString(), xvalue.Item(0).InnerText));


                                        conditonrowIndex += 1;
                                    }
                                }

                                //list.Add(model);
                            }

                        }
                    }

                }
                #endregion

                model.PARS = ConvertSqlParamsListToSqlParameters(pars);
            }
            catch (Exception ex)
            {
                //错误时
            }

            return model;
        }
    }
}
