using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common.Base
{
   public class XmlHelper
    {
       public XmlHelper()
       {
       }

       public static string AddField(string fieldname, string value)
       {
           return "<" + fieldname + "><![CDATA[" + value + "]]></" + fieldname + ">";
       }
       public static string AddField(string fieldname, string fileddbtype, string value)
       {
           return "<" + fieldname + " type='datetime'" + "><![CDATA[" + value + "]]></" + fieldname + ">";
       }
       
       /// <summary>
       /// 将xml转换成DataSet
       /// </summary>
       /// <param name="xmlText"></param>
       /// <returns></returns>
       public static DataSet ConvertXMLFileToDataSet(string xmlText)
       {
           StringReader stream = null;
           XmlTextReader reader = null;
           try
           {
               //XmlDocument xmld = new XmlDocument();
               //xmld.Load(xmlFile);

               DataSet xmlDS = new DataSet();
               //stream = new StringReader(xmld.InnerXml);
               stream = new StringReader(xmlText);
               reader = new XmlTextReader(stream);
               xmlDS.ReadXml(reader);
               reader.Close();
               stream.Close();

               stream.Dispose();
               

               return xmlDS;
           }
           catch (System.Exception ex)
           {
               reader.Close();
               throw ex;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static string ConvertDataTableToXML(DataTable dt)
       {
           DataSet ds = new DataSet();
           ds.DataSetName = "data";
           dt.TableName = "item";
           ds.Tables.Add(dt);

          string xml = ds.GetXml();

           //释放资源
           ds.Clear();
          
           return xml;
       }
    }
}
