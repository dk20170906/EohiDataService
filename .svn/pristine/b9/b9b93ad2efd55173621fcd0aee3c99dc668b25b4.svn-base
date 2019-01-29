using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace EohiDataServerApi.Areas.Api.Controllers
{
    public class DoController : Controller
    {
        //public string apihelperAddress = "http://localhost:9933/apihelper";
        //
        // GET: /Api/Home/
        //
        // GET: /Data/
        // xxxx/api/data?method=getuserdate&par1=&par2&token=&returntype=json
        public string GetRomotingAddress()
        {
            return ConfigurationManager.AppSettings["remotingapiaddress"];
            //return "http://localhost:9933/apihelper";
        }

        public ActionResult Get()
        {
            string method = Request.QueryString["method"];
            //是否将结果包含在结构体内，默认为包含，withstruct=y withstruct=n
            string withstruct = Request.QueryString["withstruct"];//y/n
            string returntype = Request.QueryString["returntype"];

            if (withstruct == null || withstruct == "")
            {
                withstruct = "y";
                //return Json("", JsonRequestBehavior.AllowGet);
            }

            if (method == null || method == "")
            {

                JDataResult jdataResult = new JDataResult();
                jdataResult.resultcode = 200;
                jdataResult.apicode = 2;
                jdataResult.reason = "参数错误，请提供参数[method]";
                return Json(jdataResult, JsonRequestBehavior.AllowGet);

                //return Json("", JsonRequestBehavior.AllowGet);
            }

            if (returntype == null)
                returntype = "json";

            //获取所有参数;
            Hashtable parsHashtable = new Hashtable();
            for (int i = 0; i < Request.Params.Keys.Count; i++)
            {
                string key = Request.Params.GetKey(i);
                string value = Request.Params[key];
                parsHashtable.Add(key, value);
            }

            if (returntype.ToLower() == "xml")
            {
                return XmlData(parsHashtable);
            }
            else
            {
                return JData(parsHashtable, withstruct);
            }

        }
        public string AjaxJson()
        {
            string method = Request.QueryString["method"];
            //是否将结果包含在结构体内，默认为包含，withstruct=y withstruct=n
            string withstruct = Request.QueryString["withstruct"];//y/n
            string returntype = Request.QueryString["returntype"];

            if (withstruct == null || withstruct == "")
            {
                withstruct = "y";
                //return Json("", JsonRequestBehavior.AllowGet);
            }
            var result = "";
            if (method == null || method == "")
            {
                JDataResult jdataResult = new JDataResult();
                jdataResult.resultcode = 200;
                jdataResult.apicode = 2;
                jdataResult.reason = "参数错误，请提供参数[method]";
                return "{'err':'参数错误，请提供参数[method]'}";
            }

            if (returntype == null)
                returntype = "json";

            //获取所有参数;
            Hashtable parsHashtable = new Hashtable();
            for (int i = 0; i < Request.Params.Keys.Count; i++)
            {
                string key = Request.Params.GetKey(i);
                string value = Request.Params[key];
                parsHashtable.Add(key, value);
            }

            ApiResult apiresult = ApiExec.Done(parsHashtable);
            
            switch (apiresult.ResultDataType)
            {
                case 0: //txt
                    result = apiresult.Data.ToString();
                    break;
                case 1://json
                    result = apiresult.Data.ToString();
                    break;
                default:
                    result = "{'err':'无效的数据返回类型，请使用json类型'}";
                    break;
            }

            return result;
        }

        public ActionResult AjaxJsonp()
        {
            string method = Request.QueryString["method"];
            //是否将结果包含在结构体内，默认为包含，withstruct=y withstruct=n
            string withstruct = Request.QueryString["withstruct"];//y/n
            string returntype = Request.QueryString["returntype"];
            string callback = (string)Request.QueryString["callback"];

            string result = "";

            if (withstruct == null || withstruct == "")
            {
                withstruct = "y";
                //return Json("", JsonRequestBehavior.AllowGet);
            }
            JDataResult jdataResult = new JDataResult();
            if (method == null || method == "")
            {
                jdataResult.resultcode = 200;
                jdataResult.apicode = 2;
                jdataResult.reason = "参数错误，请提供参数[method]";
                //result = "{'err':'参数错误，请提供参数[method]'}";
                return new JsonpResult<object>(jdataResult, callback);
            }

            if (returntype == null)
                returntype = "json";

            //获取所有参数;
            Hashtable parsHashtable = new Hashtable();
            for (int i = 0; i < Request.Params.Keys.Count; i++)
            {
                string key = Request.Params.GetKey(i);
                string value = Request.Params[key];
                parsHashtable.Add(key, value);
            }

            ApiResult apiresult = ApiExec.Done(parsHashtable);
            
            //
            if (apiresult.Code > 0)
            {
                //程序异常;
                return new JsonpResult<object>(apiresult, callback);
            }

            jdataResult = ConvertToJDataResult(apiresult);
            if (withstruct.ToLower() == "y")
            {
                return new JsonpResult<object>(jdataResult, callback);
                //return Json(jdataResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //return Json(jdataResult.result, JsonRequestBehavior.AllowGet);
                return new JsonpResult<object>(jdataResult.result, callback);
            }
           
            //return new JsonpResult<object>(new { result = result }, callback);
            
        }
       

        public JsonResult JData(Hashtable hashtable, string withstruct)
        {
            Response.ContentType = "applicaton/json";
            try
            {
                ApiResult apiresult = ApiExec.Done(hashtable);
                if (apiresult.Code > 0)
                {
                    JDataResult jdataResult = new JDataResult();
                    jdataResult.resultcode = 200;
                    jdataResult.apicode = apiresult.Code;
                    jdataResult.reason = apiresult.msg;
                    return Json(jdataResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    JDataResult jdataResult = ConvertToJDataResult(apiresult);
                    if (withstruct.ToLower() == "y")
                    {
                        return Json(jdataResult, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(jdataResult.result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception exp)
            {
                JDataResult jdataResult = new JDataResult();
                jdataResult.resultcode = 200;
                jdataResult.apicode = 2;
                jdataResult.reason ="API数据处理子件通讯异常！"+ exp.Message;
                return Json(jdataResult, JsonRequestBehavior.AllowGet);
                //return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult XmlData(Hashtable hashtable)
        {
            try
            {
                ApiResult apiresult = ApiExec.Done(hashtable);
                if (apiresult.Code > 0)
                {
                    return Json(apiresult.msg, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (apiresult.ResultDataType == 4)
                    {
                        DataTable dt = apiresult.DataTable;

                        // 序列化为XML格式显示  
                        XmlResult xResult = new XmlResult(dt, dt.GetType());
                        return xResult;
                    } 
                    else if (apiresult.ResultDataType == 2)
                    {
                        //DataTable dt = apiresult.DataTable;
                        // 序列化为XML格式显示  
                        //XmlResult xResult = new XmlResult(dt, dt.GetType());
                        return new  XmlResult(apiresult.Data.ToString(),typeof(string));
                    }
                    else
                    {
                        //return Json(SerializeDataTableXml(dt), JsonRequestBehavior.AllowGet);
                        return new XmlResult("无法转换成xml输出", typeof(string));
                    }
                }

            }
            catch (Exception exp)
            {
                return new XmlResult(exp.Message, typeof(string));
                //return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }

        }



        public JDataResult ConvertToJDataResult(ApiResult apiresult)
        {
            JDataResult jdataResult = new JDataResult();
            jdataResult.resultcode = 200;
            jdataResult.apicode = apiresult.Code;
            jdataResult.reason = "";
            try
            {
                //数组，
                if (apiresult.ResultDataType == 4)
                {
                    DataTable dt = apiresult.DataTable;
                    ArrayList arrayList = new ArrayList();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>(); //实例化一个参数集合
                        foreach (DataColumn dataColumn in dt.Columns)
                        {
                            //dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName]);
                            dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                        }
                        arrayList.Add(dictionary); //ArrayList集合中添加键值
                    }
                    jdataResult.result = arrayList;
                }
                //0 txt 转换json对象
                else if (apiresult.ResultDataType == 0)
                {
                    var jobject = JObject.Parse(apiresult.Data.ToString());
                    Dictionary<string, object> dictionary = new Dictionary<string, object>(); //实例化一个参数集合
                    JToken record = jobject;
                    foreach (JProperty jp in record)
                    {
                        dictionary.Add(jp.Name, jp.Value.ToString());
                    }
                    jdataResult.result = dictionary;
                }
                //1 
                else if (apiresult.ResultDataType == 1)
                {
                    if (apiresult.Data is JArray)
                    {
                        jdataResult.result = apiresult.Data.ToString();
                    }
                    else
                    {
                        var jobject = (JObject)apiresult.Data;
                        Dictionary<string, object> dictionary = new Dictionary<string, object>(); //实例化一个参数集合
                        JToken record = jobject;
                        foreach (JProperty jp in record)
                        {
                            dictionary.Add(jp.Name, jp.Value.ToString());
                        }
                        jdataResult.result = dictionary;
                    }
                }
                //2 xml
                else if (apiresult.ResultDataType == 2)
                {
                    jdataResult.result = "无法将xml类型转换成json";
                }
                //3 other 
                else if (apiresult.ResultDataType == 2)
                {
                    jdataResult.result = "无法将other类型转换成json";
                }
                else
                {
                    jdataResult.result = "未知的值类型";
                }
            }
            catch (Exception exp)
            {
                jdataResult.reason = exp.Message;
            }
            return jdataResult;
        }


        public string DataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }
        public string SerializeDataTableXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xs = new XmlWriterSettings();
            xs.Encoding = System.Text.Encoding.UTF8;

            XmlWriter writer = XmlWriter.Create(sb, xs);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            pDt.TableName = "tableName";
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }



        public string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.UTF8);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();

                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

    }
}
