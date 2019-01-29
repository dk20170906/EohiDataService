using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{
    public class MyLicense
    {
        public string hardwarecode = "";//读取硬件设备信息;
        public string licenseno="";
        public DateTime licensedatestart = DateTime.MinValue;
        public DateTime licensedateend = DateTime.MinValue;
    }
}