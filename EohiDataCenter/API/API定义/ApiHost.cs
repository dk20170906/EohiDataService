using DevExpress.XtraBars.Docking2010.Customization;
using Midapex.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace EohiDataCenter
{
    public class ApiHost
    {
        public string _funId = "";

        public ApiHost()
        {
        }

        public void printout(string msg)
        {
            Common.Base.NFConsoleHelper.printout(msg);
            //NFConsoleHelper.printout(msg);
        }

        public Hashtable requestParas;

        public EohiDataRemoteObject.ApiResult apiResult;

        //脚本集合
        public Dictionary<string, string> _scriptDictionary = new Dictionary<string, string>();

        //页面参数;
        public ParameterManager _pageParamterManager = new ParameterManager();
        //定时器
        private Dictionary<string, Timer> _timerDictionary = new Dictionary<string, Timer>();

        //多线程
        private Dictionary<string, System.Threading.Thread> _threadDictionary = new Dictionary<string, System.Threading.Thread>();
        private Dictionary<string, bool> _threadStopFlagDictionary = new Dictionary<string, bool>();

        //串口;
        private Dictionary<string, System.IO.Ports.SerialPort> _comDictionary = new Dictionary<string, System.IO.Ports.SerialPort>();
        //串口脚本;
        private Dictionary<string, string> _comScriptsDictionary = new Dictionary<string, string>();
        //串口收到的数据;
        private Dictionary<string, StringBuilder> _comValuesDictionary = new Dictionary<string, StringBuilder>();


        #region 数据转换
        public int ConvertToInt(object obj)
        {
            return ConvertToInt(obj, 0);
        }
        public int ConvertToInt(object obj, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public double ConvertToDouble(object obj)
        {
            return ConvertToDouble(obj, 0);
        }
        public double ConvertToDouble(object obj, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public DateTime ConvertToDateTime(string v)
        {
            try
            {
                return Convert.ToDateTime(v.ToString());
            }
            catch (Exception)
            {
                return new DateTime(1901, 1, 1);
            }
        }

        public string ConvertToString(object obj)
        {
            return Convert.ToString(obj);
        }

        /// <summary>  
        /// 获得字符串中开始和结束字符串之间的值  
        /// </summary>  
        /// <param name="portname">字符串</param>  
        /// <param name="s">开始</param>  
        /// <param name="e">结束</param>  
        /// <returns></returns>   
        public string MatchString(string str, string s, string e)
        {
            //string str= ReadComValue(portname);
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }

        public Color ConvertToColor(int argb)
        {
            return Color.FromArgb(argb);
        }
        public Color ConvertToColor(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }
        public Color ConvertToColorFromHex(string sColor)
        {
            return System.Drawing.ColorTranslator.FromHtml(sColor);
        }
        #endregion

        /// <summary>
        /// 获取来自api请求的参数
        /// </summary>
        /// <param name="paraname"></param>
        /// <returns></returns>
        public string GetRequestPara(string paraname)
        {
            if (this.requestParas == null || this.requestParas.Count <= 0)
                return "";

            if (this.requestParas.ContainsKey(paraname))
                return this.requestParas[paraname].ToString();

            return "";
        }


        #region 页面参数
        public ParameterManager NewPars()
        {
            ParameterManager pm = new ParameterManager();
            return pm;
        }

        public string GetParameterValue(string key)
        {
            if (this._pageParamterManager != null)
            {
                if (this._pageParamterManager._dic.ContainsKey(key))
                    return _pageParamterManager._dic[key].value.ToString();
            }
            return "";
        }

        public void SetParameterValue(string key, object value)
        {
            if (this._pageParamterManager == null)
                this._pageParamterManager = new ParameterManager();


            if (this._pageParamterManager._dic.ContainsKey(key))
            {
                Parameter p = _pageParamterManager._dic[key];
                //= value.ToString();
                p.value = value;
            }
            else
            {
                //
                this._pageParamterManager.Add(key, value);
            }
        }
        #endregion


        #region 脚本执行
        //执行脚本;
        public object ExecScript(string scriptnames)
        {
            if (scriptnames.Length <= 0)
                return null;


            string[] eventList = scriptnames.Split(';');

            for (int i = 0; i < eventList.Count(); i++)
            {
                try
                {

                    string scrpitName = eventList[i];
                    if (scrpitName.Length <= 0)
                        continue;
                    string scrpitTxt = "";
                    if (this._scriptDictionary.ContainsKey(scrpitName))
                        scrpitTxt = this._scriptDictionary[scrpitName];
                    if (scrpitTxt.Length <= 0)
                        continue;

                    var actual = IronPythonManager.ScriptExec(this, scrpitTxt);
                    if (actual == null)
                    {
                        //memoEdit_Print.Text = "> 无返回结果";
                    }
                    else
                    {
                        if (eventList.Count() <= 1)
                            return actual;
                    }
                }
                catch (Exception ex)
                {
                    Common.Util.NocsMessageBox.Error(ex.Message);
                    //MessageBox.Show(ex.Message);
                    //this.memoEdit_Print.ForeColor = Color.Red;
                    //memoEdit_Print.Text = "> Error:" + ex.ToString();
                }
            }

            return null;
        }
        #endregion

        #region 数据库操作;

        public DataTable SQLGetDataTable(string connstring,string sqltxt, ParameterManager pars)
        {
            try
            {

                List<SqlParameter> sqlPars = new List<SqlParameter>();
                foreach (KeyValuePair<string, Parameter> kv in pars._dic)
                {
                    sqlPars.Add(new SqlParameter("@" + kv.Value.parname, kv.Value.value.ToString()));
                }

                return Helper.SqlCmdExec.getDataTable(
               connstring,
               sqltxt,
               sqlPars.ToArray());
            }
            catch (SqlException exp)
            {
                //Common.Util.NocsMessageBox.Message(exp.Message);
                //throw;
            }

            return null;
        }


        /// <summary>
        /// 执行语句，返回受影响的行数；
        /// select 行数不返回;
        /// </summary>
        /// <param name="sqltxt"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int SQLExecuteNonQuery(string connstring, string sqltxt, ParameterManager pars)
        {
            try
            {

                List<SqlParameter> sqlPars = new List<SqlParameter>();
                foreach (KeyValuePair<string, Parameter> kv in pars._dic)
                {
                    sqlPars.Add(new SqlParameter("@" + kv.Value.parname, kv.Value.value.ToString()));
                }

                return Helper.SqlCmdExec.ExecuteNonQuery(
              connstring,
               sqltxt,
               sqlPars.ToArray());
            }
            catch (SqlException exp)
            {
                //Common.Util.NocsMessageBox.Message(exp.Message);
                //throw;
            }

            return -1;
        }

        /// <summary>
        /// 返回第一行第一列的值，如果无返回值，则返回-1;
        /// </summary>
        /// <param name="sqltxt"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object SQLExecuteScalar(string connstring, string sqltxt, ParameterManager pars)
        {
            try
            {

                List<SqlParameter> sqlPars = new List<SqlParameter>();
                foreach (KeyValuePair<string, Parameter> kv in pars._dic)
                {
                    sqlPars.Add(new SqlParameter("@" + kv.Value.parname, kv.Value.value.ToString()));
                }

                object obj = Helper.SqlCmdExec.ExecuteScalar(
               connstring,
               sqltxt,
               sqlPars.ToArray());

                return obj;
            }
            catch (SqlException exp)
            {
                Common.Util.NocsMessageBox.Message(exp.Message);
                //throw;
            }
            return null;
        }


        #endregion

        #region 系统参数
        public string GetLoginId()
        {
            return Common.Util.LoginInfo.GetLoginId();
        }
        public string GetSysLoginId()
        {
            return Common.Util.LoginInfo.GetLoginId();
        }
        //终端登录人id
        public string GetWorkSiteLoginId()
        {
            return Common.Util.WorkSiteInfo._loginId;//
        }
        //终端登录人姓名
        public string GetWorkSiteLoginName()
        {
            return Common.Util.WorkSiteInfo._loginName;
        }
        //终端关联的工位id
        public string GetWorkSitePostionId()
        {
            return Common.Util.WorkSiteInfo._postionId;//
        }
        //终端管理的工位名称
        public string GetWorkSitePostionName()
        {
            return Common.Util.WorkSiteInfo._postionName;//
        }

        //
        public string GetIPAddress()
        {
            return Common.Base.HostInfo.GetIpAddress();
        }
        public string GetMACAddress()
        {
            return Common.Base.HostInfo.GetMacAddress();
        }


        #endregion



        #region 多线程;

        public void CreateThread(string threadname, int seconds, string pythonscripts)
        {

            if (_threadDictionary.Keys.Contains(threadname))
                throw new Exception("线程[" + threadname + "]已经存在！禁止重复创建。");

            ParameterManager pm = new ParameterManager();
            pm.Add("threadname", threadname);
            pm.Add("pythonscripts", pythonscripts);
            pm.Add("seconds", seconds);

            //System.Threading.Thread thread = new Thread(new ParameterizedThreadStart(EnableButton));
            //thread.Start(null);

            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ThreadRunning));
            thread.IsBackground = true;
            thread.Name = threadname;
            thread.Start(pm);//线程开始。。。

            //
            _threadDictionary.Add(threadname, thread);
            _threadStopFlagDictionary.Add(threadname, true);
        }

        public void StopThread(string threadname)
        {

            if (!_threadDictionary.Keys.Contains(threadname))
                throw new Exception("无法找到线程[" + threadname + "]。");

            try
            {
                //更改线程标志;
                _threadStopFlagDictionary[threadname] = false;
                System.Threading.Thread thread = _threadDictionary[threadname];
                //线程;
                //bThreadStop = true;//
                thread.Join(1000);
                thread.Abort();
                thread = null;

                //
                _threadDictionary.Remove(threadname);
                _threadStopFlagDictionary.Remove(threadname);
            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void ThreadRunning(object pm)
        {
            if (!(pm is ParameterManager))
                return;



            try
            {
                ParameterManager parameterManager = (ParameterManager)pm;
                int seconds = (int)parameterManager.Get("seconds");
                string threadname = parameterManager.Get("threadname").ToString();

                if (seconds == -1)
                {
                    //只执行一次;
                    //执行脚本;
                    this.ExecScript(parameterManager.Get("pythonscripts").ToString());
                }
                else
                {
                    //获取
                    while (_threadStopFlagDictionary[threadname])
                    {
                        //如果数据库网络连接中断;不执行;
                        //if (Common.DBHelper.SqlConn.bConnIsAceess)
                        {
                            //执行脚本;
                            this.ExecScript(parameterManager.Get("pythonscripts").ToString());
                        }
                        //间隔;
                        System.Threading.Thread.Sleep(seconds * 1000);
                    }

                }

            }
            catch (Exception exp)
            {
                //AddLogValue("错误：" + exp.Message);
            }
        }

        #endregion


        public string GetDataLinkString(string linkname)
        {
            try
            {
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                  typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    return "";
                }


                string strSql = @"select top 1  * from api_links where linkname =@linkname ";
                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@linkname", linkname);
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    return "";
                }
                else
                {
                    DataTable dt = result.DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0]["linkstring"].ToString();
                    }
                    else

                        return "";
                }

            }
            catch (Exception exp)
            {
                return "";
            }
            return "";
        }

    }
}
