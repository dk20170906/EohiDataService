using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EohiDataCenter.Lib.NetWork
{
    public class ClientClass
    {
        #region 客户端请求子类
        public abstract class ParmField
        {
            protected string _key;
            protected string _value;
            public string Key
            {
                get { return _key; }
            }
            public string Value
            {
                get { return _value; }
            }
        }
        public class StringField : ParmField
        {
            public StringField(string key, string value)
            {
                _key = key;
                _value = value;
            }
        }
        public class FileField : ParmField
        {
            public FileField(string key, string value)
            {
                _key = key;
                _value = value;
            }
        }
        public class ApiResult
        {
            public bool result { get; set; }
            /// <summary>
            /// 返回代码 1成功 0失败
            /// </summary>
            public int code { get; set; }      //返回代码 1成功 0失败
            public string msg { get; set; }    //错误信息
            public JToken data { get; set; }   //返回数据
            public long total { get; set; }
            public long servertime { get; set; }
            public int statusCode { get; set; }
            public string responseContent { get; set; }
            public Stream responseStream { get; set; }
            public string responseContentType { get; set; }
            public JObject responseObject { get; set; }
        }
        #endregion 客户端请求子类
    }
}
