﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
namespace WFServerWeb
{
    public static class CDataHelper
    {
        public static IDataManager idm = new CDataManagFactory().CreateDataManager(Global.DataTypeUse);
        public static int ExecuteNonQuery(string cmdText)
        {
            return idm.ExecuteNonQuery(cmdText); 
        }
        public static DataTable GetDataTable(string cmdText)
        {
            return idm.GetDataTable(cmdText);
        }
        public static string GetData(string cmdText)
        {
            string strData = null;
            DataTable dt = GetDataTable(cmdText);
            if(dt.Rows.Count>0)
            {
                strData = dt.Rows[0][0].ToString();
            }
            return strData;
        }
    }
}