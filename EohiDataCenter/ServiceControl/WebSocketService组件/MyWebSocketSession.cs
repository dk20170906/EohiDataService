using Newtonsoft.Json.Linq;
using SuperWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EohiDataCenter
{
    public class MyWebSocketSession
    {
        private WebSocketSession session = null;
        private Thread loopThread = null;

        public string loopScripTxt = "";
        public Hashtable hashPars = new Hashtable();
        public MyWebSocketSession(WebSocketSession websession)
        {
            this.session = websession;

            //重新创建线程
            loopThread = new Thread(loopMsg);
            loopThread.IsBackground = true;
            loopThread.Start();
        }

        public int getRadom(int min, int max)
        {
            try
            {

                Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
                return rd.Next(min, max);

            }
            catch (Exception exp)
            {
                throw;
            }
            return 0;
        }
        public void SetTmp(string key,string v)
        {
            if (hashPars.ContainsKey(key))
                hashPars[key] = v;
            else
                hashPars.Add(key, v);
        }
        public string GetTmp(string key)
        {
            if (hashPars.ContainsKey(key))
                return hashPars[key].ToString();

            return "";

           
        }


        public int MSecond
        {
            get { return msecond; }
        }


        private int msecond = 1000;
        private void loopMsg()
        {
            while (this.session.Connected)
            {
                //
                if (msecond > 5 * 1000)
                    msecond = 1000;

                //执行脚本;
                WebSocketIronPythonManager.ScriptExec(this, this.loopScripTxt);


                //等待10毫秒;
                Thread.Sleep(1000);
                msecond += 1000;
            }
        }


        /// <summary>
        /// 是否连接;
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (session != null)
                    return session.Connected;

                return false;
            }
        }
        public string userid
        {
            get;
            set;
        }

        public string sessionid
        {
            get
            {
                if (session != null)
                    return session.SessionID;

                else
                    return "";
            }
        }

        public string ipinfo
        {
            get
            {
                if (session != null)
                    return session.SocketSession.Client.RemoteEndPoint.ToString();
                else
                    return "";
            }
        }


        public JObject jo;//= JObject.Parse(valueJson);


       


        private string _lastmsg="";
        public string LastMsg
        {
            get { return _lastmsg; }
            set
            {
                _lastmsg = value;
                try
                {
                    if (_lastmsg.Length <= 0)
                    {
                        jo = null;
                        return;
                    }
                    //数据集合值写入后，自动创建 json 读取对象;
                    jo = JObject.Parse(value);

                    //string vvv = "[\"4\",\"5\"]";
                    //JArray jr = JArray.Parse(vvv);
                }
                catch (Exception ex)
                {
                    jo =null;
                    throw ex;
                }
            }
        }


        public string GetEquList(string equname)
        {
            JArray jr = JArray.Parse(equname);

            return "";
        }

        public string GetParaValue(string path)
        {
            if (jo == null)
                return "";

            string v = "";
            try
            {
                bool has = jo.Properties().Any(p => p.Name == path);//或是这样
                if (has)
                {
                    v = jo[path].ToString();
                }
            }
            catch (Exception)
            {

            }
            return v;
        }

        /// <summary>
        /// 发送广播;
        /// </summary>
        public void SendBoard()
        {
        }

        public void ShowMsg(string v)
        {
            MessageBox.Show(v);
        }
             
        /*
        public void Send(IMMessage imMessage)
        {
            if (session != null)
                 session.Send(imMessage.ToString());
        }

        public void Send(U3dMessage u3dMessage)
        {
            if (session != null)
                session.Send(u3dMessage.ToString());
        }
        */

        public void SendMsg(string str)
        {
            try
            {
                if (session != null)
                    session.Send(str);
            }
            catch (Exception)
            {
                
                //throw;
            }
           
        }

        public ParameterManager NewPars()
        {
            ParameterManager pm = new ParameterManager();
            return pm;
        }


        public void MsgBox(string msg)
        {
            MessageBox.Show(msg);
        }
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

        public string ConvertToString(object obj)
        {
            try
            {
                string value = Convert.ToString(obj);
                if (String.IsNullOrEmpty(value))
                    return "";
                return value;
            }
            catch (Exception exp)
            {
            }

            return "";
        }

       
        #endregion


        #region 数据库操作;

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
                var abc = EohiDataCenter.Helper.SqlCmdExec.ExecuteNonQuery(
               connstring,
               sqltxt,
               sqlPars.ToArray());
                return abc;
            }
            catch (SqlException exp)
            {
                Common.Util.NocsMessageBox.Message(exp.Message);
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

                object obj = EohiDataCenter.Helper.SqlCmdExec.ExecuteScalar(
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

        public DataTable SQLGetDataTable(string connstring, string sqltxt, ParameterManager pars)
        {
            try
            {
                List<SqlParameter> sqlPars = new List<SqlParameter>();
                foreach (KeyValuePair<string, Parameter> kv in pars._dic)
                {
                    sqlPars.Add(new SqlParameter("@" + kv.Value.parname, kv.Value.value.ToString()));
                }

                return EohiDataCenter.Helper.SqlCmdExec.getDataTable(
               connstring,
               sqltxt,
               sqlPars.ToArray());
            }
            catch (SqlException exp)
            {
                Common.Util.NocsMessageBox.Message(exp.Message);
                //throw;
            }
            return null;
        }



        #endregion
    }
}
