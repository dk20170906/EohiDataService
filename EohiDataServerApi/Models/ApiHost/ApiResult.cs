using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiDataServerApi
{
    [Serializable]
    public class ApiResult
    {
       
        private int resultDataType = 0;
        /// <summary>
        /// 0 txt
        /// 1 json
        /// 2 xml
        /// 3 other
        /// 4 datatable
        /// </summary>
        public int ResultDataType
        {
            get { return this.resultDataType; }
            set { this.resultDataType = value; }
        }


        private int code = 0;
        /// <summary>
        /// >0 标示包含错误信息;
        /// </summary>
        public int Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string access
        {
            get
            {
                if (code == 0)
                    return "true";
                else

                    return "false";
            }
        }
    

        private DataTable datatable;
        public DataTable DataTable
        {
            get { return this.datatable; }
            set { this.datatable = value; }
        }

        private string _msg = "";
        public string msg
        {
            get { return this._msg; }
            set { this._msg = value; }
        }

        public string Msg
        {
            get { return this._msg; }
            set { this._msg = value; }
        }
        

        private int total = 0;
        public int Total
        {
            get { return this.total; }
            set { this.total = value; }
        }


        public object Data
        {
            get;
            set;
        }

    }
}
