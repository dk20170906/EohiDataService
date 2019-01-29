using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common.Base
{
    public class SqlParameterConvert
    {
        /// <summary>
        /// 将pars转换成xml文本，用于在网络上传输；
        /// </summary>
        /// <param name="pars"></param>
        public static string ConvertSqlParameterArrayToDbParameterXml(SqlParameter[] pars)
        {
            string dbparsxml="";
            dbparsxml += "<data>";
           


            for (int i = 0; i < pars.Length; i ++ )
            {
                dbparsxml += "<parsitem>";
                dbparsxml += XmlHelper.AddField("parname", pars[i].ParameterName);
                dbparsxml += XmlHelper.AddField("pardbtype", pars[i].SqlDbType.ToString());
                if (pars[i].Value == null)
                {
                    dbparsxml += XmlHelper.AddField("parvalue","null");
                    dbparsxml += XmlHelper.AddField("parvalueisnull", "1");
                }
                else if (pars[i].Value == System.DBNull.Value)
                {
                    dbparsxml += XmlHelper.AddField("parvalue", "db.null");
                    dbparsxml += XmlHelper.AddField("parvalueisnull", "1");
                }
                else
                {
                    dbparsxml += XmlHelper.AddField("parvalue", pars[i].Value.ToString());
                    dbparsxml += XmlHelper.AddField("parvalueisnull", "0");
                }
                dbparsxml += XmlHelper.AddField("pardirection",((int)pars[i].Direction).ToString());
                dbparsxml += XmlHelper.AddField("parsize",((int)pars[i].Size).ToString() );
                dbparsxml += "</parsitem>";
            }
            dbparsxml += "</data>";

            return dbparsxml;
        }

        /// <summary>
        /// 解析网络传输的参数。
        /// </summary>
        /// <param name="dbparsxml"></param>
        /// <returns></returns>
        public static SqlParameter[] ConvertDbParameterXmlToSqlParameterArray(string dbparsxml)
        {
            //获取item个数；
            DataSet ds = XmlHelper.ConvertXMLFileToDataSet(dbparsxml);
            if (ds == null || ds.Tables.Contains("parsitem") == false)
                return new SqlParameter[]{};
            

            int count = ds.Tables["parsitem"].Rows.Count;
            if(count<=0)
                return new SqlParameter[] { };


            SqlParameter[] pars = new SqlParameter[count];
            try
            {
                for (int i = 0; i < count; i++)
                {
                    // 0 参数名称
                    // 1 参数类型
                    // 2 参数值
                    // 3 参数输入输出方向
                    // 4 参数大小
                    // 5 参数值是否为空
                    SqlParameter par = new SqlParameter();
                    par.ParameterName = ds.Tables["parsitem"].Rows[i]["parname"].ToString();
                    par.SqlDbType = FindSqlDbType(ds.Tables["parsitem"].Rows[i]["pardbtype"].ToString());

                    par.Direction = (ParameterDirection)Convert.ToInt32(ds.Tables["parsitem"].Rows[i]["pardirection"].ToString());
                    par.Size = Convert.ToInt32(ds.Tables["parsitem"].Rows[i]["parsize"].ToString());

                    //判断传递过来的是否为NULL
                    if (Convert.ToInt32(ds.Tables["parsitem"].Rows[i]["parvalueisnull"].ToString()) == 1)
                    {
                        if (ds.Tables["parsitem"].Rows[i]["parvalue"].ToString() == "null")
                        {
                            par.Value = null;

                        }
                        else
                        {
                            par.Value = System.DBNull.Value;
                        }
                    }
                    else
                        par.SqlValue = ds.Tables["parsitem"].Rows[i]["parvalue"].ToString();
                    //
                    pars[i] = par;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return pars;
        }


        private static SqlDbType FindSqlDbType(string strType)
        {

            //BigInt  Int64. 64 位的有符号整数。  
            //Binary  Byte 类型的 Array。 二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。  
            //  Boolean. 无符号数值，可以是 0、1 或 Nothing。  
            //  String. 非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。  
            //  DateTime. 日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。  
            //  Decimal. 固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。  
            //  Double. -1.79E +308 到 1.79E +308 范围内的浮点数。  
            //  Byte 类型的 Array。 二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。  
            //  Int32. 32 位带符号整数。  
            //  Decimal. 货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。  
            //  String. Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。  
            //  String. Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。  
            //  String. Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。 如果字符串大于 4,000 个字符，隐式转换会失败。 在使用比 4,000 个字符更长的字符串时，请显式设置对象。 挂起更改进行替换的数据源中实体的所有值，更新的实体 （HTTP PUT） 中的值，而不是只更新更改的值 （HTTP 合并)，这默认行为的情况。NVarCharnvarchar(max)  
            //  Single. -3.40E +38 到 3.40E +38 范围内的浮点数。  
            //  Guid. 全局唯一标识符（或 GUID）。  
            //  DateTime. 日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。  
            //  Int16. 16 位的带符号整数。  
            //  Decimal. 货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。  
            //  String. 非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。  
            //  Byte 类型的 Array。 自动生成的二进制数字，它们保证在数据库中是唯一的。 timestamp 通常用作为表行添加版本戳的机制。 存储大小为 8 字节。  
            //  Byte. 8 位无符号整数。  
            //  Byte 类型的 Array。 二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。 如果字节数组大于 8,000 个字节，隐式转换会失败。 在使用比 8,000 个字节大的字节数组时，请显式设置对象。  
            //  String. 非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。 使用 的生成 uri 添加查询选项VarCharvarchar(max)  
            //  Object. 特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。  
            //  XML 值。 使用 GetValue 方法或 Value 属性获取字符串形式的 XML，或通过调用 CreateReader 方法获取 XmlReader 形式的 XML。  
            //  SQL Server 2005 用户定义的类型 (UDT)。  
            //  指定表值参数中包含的构造数据的特殊数据类型。  
            //  日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。  
            //  基于 24 小时制的时间数据。 时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。 对应于 SQL Server time 值。  
            //  日期和时间数据。 日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。 时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。  
            //  显示时区的日期和时间数据。 日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。 时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。 时区值范围从 -14:00 到 +14:00。 



            SqlDbType dbtype = SqlDbType.NText;

            switch (strType)
            {
                case "BigInt":
                    dbtype = SqlDbType.Binary;
                    break;
                case "Binary":
                    dbtype = SqlDbType.Binary;
                    break;
                case "Bit":
                    dbtype = SqlDbType.Bit;
                    break;
                case "Char":
                    dbtype = SqlDbType.Char;
                    break;
                case "DateTime":
                    dbtype = SqlDbType.DateTime;
                    break;
                case "Decimal":
                    dbtype = SqlDbType.Decimal;
                    break;
                case "Float":
                    dbtype = SqlDbType.Float;
                    break;
                case "Image":
                    dbtype = SqlDbType.Image;
                    break;
                case "Int":
                    dbtype = SqlDbType.Int;
                    break;
                case "Money":
                    dbtype = SqlDbType.Money;
                    break;
                case "NChar":
                    dbtype = SqlDbType.NChar;
                    break;
                case "NText":
                    dbtype = SqlDbType.NText;
                    break;
                case "NVarChar":
                    dbtype = SqlDbType.NVarChar;
                    break;
                case "Real":
                    dbtype = SqlDbType.Real;
                    break;
                case "UniqueIdentifier":
                    dbtype = SqlDbType.UniqueIdentifier;
                    break;

                case "SmallDateTime":
                    dbtype = SqlDbType.SmallDateTime;
                    break;
                case "SmallInt":
                    dbtype = SqlDbType.SmallInt;
                    break;
                case "SmallMoney":
                    dbtype = SqlDbType.SmallMoney;
                    break;
                case "Text":
                    dbtype = SqlDbType.Text;
                    break;
                case "Timestamp":
                    dbtype = SqlDbType.Timestamp;
                    break;
                case "TinyInt":
                    dbtype = SqlDbType.TinyInt;
                    break;
                case "VarBinary":
                    dbtype = SqlDbType.VarBinary;
                    break;

                case "VarChar":
                    dbtype = SqlDbType.VarChar;
                    break;
                case "Variant":
                    dbtype = SqlDbType.Variant;
                    break;
                case "Xml":
                    dbtype = SqlDbType.Xml;
                    break;
                case "Udt":
                    dbtype = SqlDbType.Udt;
                    break;
                case "Structured":
                    dbtype = SqlDbType.Structured;
                    break;
                case "Date":
                    dbtype = SqlDbType.Date;
                    break;
                case "Time":
                    dbtype = SqlDbType.Time;
                    break;
                case "DateTime2":
                    dbtype = SqlDbType.DateTime2;
                    break;
                case "DateTimeOffset":
                    dbtype = SqlDbType.DateTimeOffset;
                    break;

                default:
                    dbtype = SqlDbType.NText;
                    break;
            }

            return dbtype;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parsList"></param>
        /// <returns></returns>
        public static SqlParameter[] ConvertParameterListToSqlParameterArray(List<SqlParameter> parsList)
        {

            SqlParameter[] pars = new SqlParameter[parsList.Count];
            try
            {
                for (int i = 0; i < parsList.Count; i++)
                {
                    pars[i] = parsList[i];
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return pars;
        }
    }
}
