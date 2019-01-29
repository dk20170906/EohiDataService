using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace EohiDataRemoteObject
{
    public class ApiHelper : MarshalByRefObject
    {
        public delegate void ApiRequestEventHandler(ApiHelper apiHelper, Hashtable hashtable);
        public static event ApiRequestEventHandler ApiRequsetSendedEvent;

        public ApiHelper()
        {
            Console.WriteLine("[ApiHelper]:Remoting Object 'ApiHelper' is activated.");
        }

        public ApiResult _reslut;
        public ApiResult getResult(Hashtable hashtable)
        {
            
            
            //ApiResult result = new ApiResult();
            /*
            //
            DataTable dt = new DataTable();
            dt.Columns.Add("a", typeof(string));

            DataRow dr = dt.NewRow();
            dr["a"] = "111";
            dt.Rows.Add(dr);

            result.DataTable = dt;


            result.ResultDataType = 4;
            */
            if (ApiRequsetSendedEvent != null)
            {
                ApiRequsetSendedEvent(this, hashtable);
            }
            if (_reslut == null)
            {
                _reslut = new ApiResult();
                _reslut.Msg = "无返回数据,请求已处理,但可能请求处理并未连接到API数据处理终端.";
            }

            return this._reslut;
        }
       
    }

}
