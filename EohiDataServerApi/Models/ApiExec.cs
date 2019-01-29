using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{
    public class ApiExec
    {

        public static ApiResult Done(Hashtable hashtable)
        {

            //查询api信息;
            ApiResult result = new ApiResult();

            ApiHost apihost = new ApiHost();
            apihost.apiResult = result;

            try
            {
                string method = hashtable["method"].ToString();

                apihost.apiResult = result;
                apihost.requestParas = hashtable;

                //执行查询；
                ApiItem apiItem = GetApiItem(method);
                if (apiItem == null)
                {
                    result.Code = 1;
                    result.ResultDataType = 2;
                    result.msg = "未找到名为" + method + "的方法,该api可能不存在未处于在线服务状态";
                    result.ResultDataType = 4;
                }
                else
                {
                    //执行脚本;
                    IronPythonManager.ScriptExec(apihost, apiItem.apiscript);
                }

                //将查询结果返回;

                //PrintLog("> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + method + ",执行完成", true);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //PrintLog("> Error:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + ex.ToString(), false);

                apihost.apiResult.Code = 1;
                apihost.apiResult.Msg = ex.Message;
            }

            return apihost.apiResult;
        }

        private static ApiItem GetApiItem(string apiname)
        {
            try
            {
                apiname = apiname.Trim();
                string strSql = @"  select *  from api_items where apiname =@apiname";

                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@apiname",apiname)
                };

                DataTable dt = DBHelper.getDataTable(strSql, parames);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ApiItem apiItem = new ApiItem();
                    apiItem.apiname = dt.Rows[0]["apiname"].ToString();
                    apiItem.apiscript = dt.Rows[0]["apiscript"].ToString();

                    return apiItem;
                }

            }
            catch (Exception exp)
            {

            }
            return null;

        }
    }
}