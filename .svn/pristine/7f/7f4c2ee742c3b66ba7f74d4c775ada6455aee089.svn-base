using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EohiDataCenter
{
    public class IMMessage
    {
        public string fromuserid = "";
        public string touserid = "";
        public string msgtype = "个人";//个人,广播,连接
        public string msgtxt = "";
        public string msgdate = "";

        public override string ToString()
        {
            string msg = "{";
            msg += "\"fromuserid\":";
            msg += "\"" + fromuserid + "\"";
            msg += ",";

            msg += "\"touserid\":";
            msg += "\"" + touserid + "\"";
            msg += ",";


            msg += "\"msgtype\":";
            msg += "\"" + msgtype + "\"";
            msg += ",";


            msg += "\"msgtxt\":";
            msg += "\"" + msgtxt + "\"";
            msg += ",";

            msg += "\"msgdate\":";
            msg += "\"" + msgdate + "\"";
            msg += "}";

            return msg;
            //return base.ToString();
        }
    }
}
