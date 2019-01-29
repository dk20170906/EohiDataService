
using Newtonsoft.Json.Linq;
using SuperWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace IMServer
{
    public partial class FormMain_WebSocket : Form
    {
        public FormMain_WebSocket()
        {
            InitializeComponent();
        }
        private bool bTCPServerIsRunning = false;
        private Thread threadTestWebSocketStatus = null;


        private DataTable dtSession = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAddressIP();

            /*
            dtSession = new DataTable();
            dtSession.Columns.Add("sessionid", typeof(string));
            dtSession.Columns.Add("ipinfo", typeof(string));
            dtSession.Columns.Add("userid", typeof(string));

            this.dataGridView1.DataSource = dtSession;
            */

            this.dataGridView1.DataSource = sessionList;


            this.button_serverStop.Enabled = false;
            this.button_send.Enabled = false;

            //启动服务状态保持线程;
            try
            {
                //重新创建线程
                threadTestWebSocketStatus = new Thread(TestWebSocketStatus);
                threadTestWebSocketStatus.IsBackground = true;
                threadTestWebSocketStatus.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TestWebSocketStatus()
        {
            while (true)
            {

                if (bTCPServerIsRunning)
                {
                    //
                    if (wsServer != null)
                    {
                        if (wsServer.State == SuperSocket.SocketBase.ServerState.Running)
                        {
                            //wsServer.Stop();
                            SetTCPValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+",服务正常……");
                        }
                        else
                        {

                            SetTCPValue("WebScoket服务已停止,端口" + this.textBox_prot.Text);
                            SetTCPValue("正在重启WebSocket服务……");
                            wsServer.Stop();
                            wsServer = null;

                            Thread.Sleep(1000 * 1);//wait to connect
                            SetTCPValue("3……");
                            Thread.Sleep(1000 * 1);//wait to connect
                            SetTCPValue("2……");
                            Thread.Sleep(1000 * 1);//wait to connect
                            SetTCPValue("1……");

                            //启动websocket
                            StartWebScoketServer();
                        }

                    }
                }

                Thread.Sleep(1000* 15);//wait to connect
            }
        }
        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        void GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            this.textBox_ip.Text = AddressIP;
        }

        private void button_serverStart_Click(object sender, EventArgs e)
        {
            StartWebScoketServer();

            StartRemotingServer();
        }

        private void StartRemotingServer()
        {
            System.Runtime.Remoting.Channels.Http.HttpChannel channel = new System.Runtime.Remoting.Channels.Http.HttpChannel(9932);
            //TcpServerChannel channel = new TcpServerChannel(9932);
            ChannelServices.RegisterChannel(channel, false);

            //注册响应方法;
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(EohiDataRemoteObject.ApiHelper), "apihelper", WellKnownObjectMode.SingleCall);

            EohiDataRemoteObject.ApiHelper.ApiRequsetSendedEvent += ApiHelper_ApiRequsetSendedEvent;
            //FaxBusiness.FaxSendedEvent += new FaxEventHandler(OnFaxSended);
        }

        void ApiHelper_ApiRequsetSendedEvent(EohiDataRemoteObject.ApiHelper apiHelper, Hashtable hashtable)
        {
            MessageBox.Show(hashtable.Count.ToString());

            //执行查询；
            EohiDataRemoteObject.ApiResult result = new EohiDataRemoteObject.ApiResult();

            //
            DataTable dt = new DataTable();
            dt.Columns.Add("a", typeof(string));

            DataRow dr = dt.NewRow();
            dr["a"] = "在程序段执行";
            dt.Rows.Add(dr);

            result.DataTable = dt;

            result.ResultDataType = 4;

            //将查询结果返回;
            apiHelper._reslut = result;
        }

        private WebSocketServer wsServer = null;
        private void StartWebScoketServer()
        {
            try
            {
                wsServer = new WebSocketServer();

                if (!wsServer.Setup(this.textBox_ip.Text,  Convert.ToInt32(this.textBox_prot.Text)))
                {
                    //设置IP 与 端口失败  通常是IP 和端口范围不对引起的 IPV4 IPV6
                }

                if (!wsServer.Start())
                {
                    //开启服务失败 基本上是端口被占用或者被 某杀毒软件拦截造成的
                    MessageBox.Show("未知原因导致无法启动WebSocket服务");
                    return;
                }

                wsServer.NewSessionConnected += (session) =>
                {
                    //有新的连接
                    AddClient(session);
                };
                wsServer.SessionClosed += (session, reason) =>
                {
                    //有断开的连接
                    CloseClient(session,reason);
                };
                wsServer.NewMessageReceived += (session, message) =>
                {
                    //接收到新的文本消息
                    NewMessageReceive(session, message);
                };
                wsServer.NewDataReceived += (session, bytes) =>
                {
                    //接收到新的二进制消息
                };

               

                SetTCPValue("---------------------");
                SetTCPValue("WebScoket服务已启动,端口" + this.textBox_prot.Text);

                bTCPServerIsRunning = true;

                this.button_serverStart.Enabled = false;
                this.button_serverStop.Enabled = true;
                this.button_send.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void button_serverStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (wsServer != null)
                {
                    if (wsServer.State== SuperSocket.SocketBase.ServerState.Running)
                    {
                        wsServer.Stop();
                    }
                    bTCPServerIsRunning = false;
                    SetTCPValue("WebScoket服务已停止,端口" + this.textBox_prot.Text);
                    SetTCPValue("*******************");
                    wsServer = null;

                   

                    this.button_serverStart.Enabled = true;
                    this.button_serverStop.Enabled = false;
                    this.button_send.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public List<MyWebSocketSession> sessionList = new List<MyWebSocketSession>();

        private delegate void AddClient_Delegate(WebSocketSession session);
        public void AddClient(WebSocketSession session)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new AddClient_Delegate(AddClient),
                    new Object[] { session });
                return;
            }
            try
            {
                //判断连接是否已经存在;
                bool bsend = false;
                MyWebSocketSession oldSessoin = sessionList.Find(delegate(MyWebSocketSession temp)
                {
                    return temp.sessionid == session.SessionID;
                });
                if (oldSessoin == null)
                {
                    //加入;
                    MyWebSocketSession newSession = new MyWebSocketSession(session);
                    newSession.userid = "";
                    sessionList.Add(newSession);
                }
                //IList<T> list = new List<T>();
                //DataGridView.DataSource = list;//DataGridView的行不能添加删除
                //DataGridView.DataSource = new BindingList<T>(list);//DataGridView的行可以添加删除（只有允许添加行、删除行）
                this.dataGridView1.DataSource = new BindingList<MyWebSocketSession>(sessionList);


                string str = "";
                str += session.SessionID + "-连接," + session.SocketSession.Client.RemoteEndPoint.ToString();

                //DataRow dr = dtSession.NewRow();
                //dr["sessionid"] = session.SessionID;
                //dr["ipinfo"] = session.SocketSession.Client.RemoteEndPoint.ToString();
                //dr["userid"] = "";
                //dtSession.Rows.Add(dr);
                
                
                //增加连接记录;
                AddClientConnLog(str);
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }
        //连接断开;
        private delegate void CloseClient_Delegate(WebSocketSession session, SuperSocket.SocketBase.CloseReason reason);
        public void CloseClient(WebSocketSession session, SuperSocket.SocketBase.CloseReason reason)
        {
             if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new CloseClient_Delegate(CloseClient),
                    new Object[] { session, reason });
                return;
            }
             try
             {
                 string str = "";
                 string sessionid = session.SessionID;

                 MyWebSocketSession oldSessoin = sessionList.Find(delegate(MyWebSocketSession temp)
                 {
                     return temp.sessionid == session.SessionID;
                 });
                 if (oldSessoin != null)
                 {
                     sessionList.Remove(oldSessoin);
                 }

                 this.dataGridView1.DataSource = new BindingList<MyWebSocketSession>(sessionList);

                 str += session.SessionID + "-离开," + reason.ToString();
                 //增加连接记录;
                 AddClientConnLog(str);
             }
             catch (Exception exp)
             {
                 //捕捉错误;
                 //this.CatchException(exp);
             }
        }

        //接收到消息
        private delegate void NewMessageReceive_Delegate(WebSocketSession session, string message);
        public void NewMessageReceive(WebSocketSession session, string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new NewMessageReceive_Delegate(NewMessageReceive),
                    new Object[] { session, message });
                return;
            }
            try
            {
                //增加连接记录;
                AddClientConnLog(message);

                //收到消息;
                //解析数据;
                IMMessage imMessage = JsonHelper.DeserializeJsonToObject<IMMessage>(message);
                if (imMessage.msgtype == "连接")
                {
                    //更新连接对应的用户名称;
                    List<MyWebSocketSession> findSessoinList = sessionList.FindAll(delegate(MyWebSocketSession temp)
                    {
                        return temp.sessionid == session.SessionID;
                    });
                    if (findSessoinList != null & findSessoinList.Count > 0)
                    {
                        for (int i = 0; i < findSessoinList.Count; i++)
                        {
                            findSessoinList[i].userid = imMessage.msgtxt;
                        }
                    }
                    //
                    this.dataGridView1.DataSource = new BindingList<MyWebSocketSession>(sessionList);
                }
                else
                {
                    //转发消息;
                    bool bsend = false;
                    List<MyWebSocketSession> acceptSessoinList = sessionList.FindAll(delegate(MyWebSocketSession temp)
                    {
                        return temp.userid == imMessage.touserid;
                    });
                    if (acceptSessoinList != null & acceptSessoinList.Count > 0)
                    {
                        for (int i = 0; i < acceptSessoinList.Count; i++)
                        {
                            acceptSessoinList[i].Send(imMessage);
                            bsend = true;
                        }
                    }

                    //保存至数据库;
                    SaveToDB(imMessage, false);

                }

            }
            catch (Exception exp)
            {
                //捕捉错误;
                AddClientConnLog("消息解释失败。" + exp.Message);
            }
        }

        private delegate void SetTCPValue_Delegate(string value);
        public void SetTCPValue(string value)
        {

            if (this.textBox_tcp.InvokeRequired)
            {
                this.textBox_tcp.BeginInvoke(
                    new SetTCPValue_Delegate(SetTCPValue),
                    new Object[] { value });
                return;
            }
            try
            {
                if (this.textBox_tcp.Lines.Length > 100)
                {
                    this.textBox_tcp.Text = "------------";
                    this.textBox_tcp.AppendText("\r\n");
                    this.textBox_tcp.AppendText("WebSocket服务已启动");
                }
                //
                this.textBox_tcp.AppendText("\r\n");
                this.textBox_tcp.AppendText(value);
                //
                this.textBox_tcp.SelectionStart = this.textBox_tcp.Text.Length;
                this.textBox_tcp.ScrollToCaret();
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }

        //显示连接、离开记录；
        private delegate void AddClientConnLog_Delegate(string value);
        public void AddClientConnLog(string value)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new AddClientConnLog_Delegate(AddClientConnLog),
                    new Object[] { value });
                return;
            }
            try
            {
                string str = "";

                if (this.textBox_conn_logs.Lines.Length > 100)
                {
                    this.textBox_conn_logs.Text = "------------";
                    this.textBox_conn_logs.AppendText("\r\n");
                    this.textBox_conn_logs.AppendText("WebSocket服务已启动");
                }
              
                //
                this.textBox_conn_logs.AppendText("\r\n");
                this.textBox_conn_logs.AppendText(value);
                //
                this.textBox_conn_logs.SelectionStart = this.textBox_conn_logs.Text.Length;
                this.textBox_conn_logs.ScrollToCaret();
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }

        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton_Conf_Click(object sender, EventArgs e)
        {
            DBHelper.SqlConn.ConnectionSetting();
        }

        private bool SaveToDB(IMMessage imMessage, bool bSend)
        {
            try
            {
                string strSql = @"insert into erpsys_chatmessage(userid,partnerid,message,Isread,remark,send_date)
                     values (@userid,@partnerid,@message,@Isread,@remark,getdate())";
                SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("@userid",imMessage.fromuserid),
                        new SqlParameter("@partnerid",imMessage.touserid),
                        new SqlParameter("@message",imMessage.msgtxt),
                        new SqlParameter("@Isread","0"),
                        new SqlParameter("@remark",""),
                        };

                DBHelper.SqlCmd.ExecuteNonQuery(strSql, pars);
                return true;
            }
            catch (Exception exp)
            {
                //throw;
            }

            return false;
        }
    }
}
