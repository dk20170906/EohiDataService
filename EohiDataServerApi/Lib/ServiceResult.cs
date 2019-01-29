using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Lib
{
    public class ServiceResult
    {
        public int code { get; set; }      //返回代码 1成功 0失败
        public string msg { get; set; }    //错误信息
        public object data { get; set; }   //返回数据
        public int total { get; set; }
        public long servertime { get { return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}