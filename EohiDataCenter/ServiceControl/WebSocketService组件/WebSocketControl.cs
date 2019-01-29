using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SuperWebSocket;
using System.Net;
using System.Collections;

namespace EohiDataCenter
{
    public partial class WebSocketControl : UserControl
    {
        public WebSocketControl()
        {
            InitializeComponent();
        }
        private bool bTCPServerIsRunning = false;
        private Thread threadTestWebSocketStatus = null;
        private WebSocketServer wsServer = null;


        private void WebSocketControl_Load(object sender, EventArgs e)
        {

            GetAddressIP();


            //this.gridControl_client.DataSource = dtSession;


            this.button_serverStop.Enabled = false;
            //this.button_send.Enabled = false;

            //启动服务状态保持线程;
            //try
            //{
            //    //重新创建线程
            //    threadTestWebSocketStatus = new Thread(TestWebSocketStatus);
            //    threadTestWebSocketStatus.IsBackground = true;
            //    threadTestWebSocketStatus.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
            //启动服务状态保持线程;
            try
            {
                GetList();
                if (threadTestWebSocketStatus != null && threadTestWebSocketStatus.IsAlive)
                {
                    MessageBox.Show("线程已启动");
                    return;
                }
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

        private string pythonScriptTxt = "";
        private string pythonLoopScriptTxt = "";
        private void GetList()
        {
            string strSql = @"select * from api_websocket";

            //构建一个哈希表，把参数依次压入
            Hashtable parames = new Hashtable();
            try
            {
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                   typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    for (int i = 0; i < result.DataTable.Rows.Count; i++)
                    {
                        pythonScriptTxt = result.DataTable.Rows[i]["scripttxt"].ToString();
                        pythonLoopScriptTxt = result.DataTable.Rows[i]["loopscripttxt"].ToString();
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            for (int i = 0; i < sessionList.Count; i++)
            {
                sessionList[i].loopScripTxt = this.pythonLoopScriptTxt;
            }
        }

        /// <summary>
        /// 让websocket保持状态;
        /// </summary>
        private void TestWebSocketStatus()
        {
            while (true)
            {
                //如果已经启动;
                if (bTCPServerIsRunning)
                {
                    //
                    if (wsServer != null)
                    {
                        if (wsServer.State == SuperSocket.SocketBase.ServerState.Running)
                        {
                            //wsServer.Stop();
                            if (OnAddLogEvent != null)
                            {
                                OnAddLogEvent("状态", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",服务正常……");
                            }
                        }
                        else
                        {
                            if (OnAddLogEvent != null)
                            {
                                OnAddLogEvent("状态", "WebScoket服务已停止,端口" + this.textBox_prot.Text);
                                OnAddLogEvent("状态", "正在重启WebSocket服务……");
                            }

                            wsServer.Stop();
                            wsServer = null;

                            Thread.Sleep(1000 * 1);//wait to connect
                            if (OnAddLogEvent != null)
                                OnAddLogEvent("状态", "3……");
                            Thread.Sleep(1000 * 1);//wait to connect
                            if (OnAddLogEvent != null)
                                OnAddLogEvent("状态", "2……");
                            if (OnAddLogEvent != null)
                                Thread.Sleep(1000 * 1);//wait to connect
                            OnAddLogEvent("状态", "1……");

                            //启动websocket
                            StartWebScoketServer();
                        }

                    }
                    
                }
                else
                {
                    //启动websocket
                    StartWebScoketServer();
                }
                //
                Thread.Sleep(1000 * 5);//wait to connect
            }
        }


        private void StartWebScoketServer()
        {
            try
            {
                wsServer = new WebSocketServer();

                if (!wsServer.Setup(this.textBox_ip.Text, Convert.ToInt32(this.textBox_prot.Text)))
                {
                    //设置IP 与 端口失败  通常是IP 和端口范围不对引起的 IPV4 IPV6
                }

                if (!wsServer.Start())
                {
                    //开启服务失败 基本上是端口被占用或者被 某杀毒软件拦截造成的
                    //MessageBox.Show("未知原因导致无法启动WebSocket服务");
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
                    CloseClient(session, reason);
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


                if (OnAddLogEvent != null)
                {
                    OnAddLogEvent("状态", "---------------------");
                    OnAddLogEvent("状态", "WebScoket服务已启动,端口" + this.textBox_prot.Text);
                }

                bTCPServerIsRunning = true;
                SetStatus(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //连接断开;
        private delegate void SetStatus_Delegate(bool baccess);
        public void SetStatus(bool baccess)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new SetStatus_Delegate(SetStatus),
                    new Object[] { baccess });
                return;
            }
            try
            {
                if (baccess)
                {
                    this.button_serverStart.Enabled = false;
                    this.button_serverStop.Enabled = true;



                    this.label_status.BackColor = Color.Green;
                    this.label_status.Text = "启动";

                    this.button_serverStart.Text = "停止";

                }
                else
                {
                    this.button_serverStart.Enabled =true;
                    this.button_serverStop.Enabled = false;


                    this.label_status.BackColor = Color.Red;
                    this.label_status.Text = "停止";


                    this.button_serverStart.Text = "启动";
                }

               
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }
        }

        public List<MyWebSocketSession> sessionList = new List<MyWebSocketSession>();
        private delegate void AddClient_Delegate(WebSocketSession session);
        /// <summary>
        /// 有新的连接;
        /// </summary>
        /// <param name="session"></param>
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
                    //循环处理脚本;
                    newSession.loopScripTxt = this.pythonLoopScriptTxt;

                    //DealSession(newSession);
                }
                //IList<T> list = new List<T>();
                //DataGridView.DataSource = list;//DataGridView的行不能添加删除
                //DataGridView.DataSource = new BindingList<T>(list);//DataGridView的行可以添加删除（只有允许添加行、删除行）
                //this.dataGridView1.DataSource = new BindingList<MyWebSocketSession>(sessionList);


                string str = "";
                str += session.SessionID + "-连接," + session.SocketSession.Client.RemoteEndPoint.ToString();

                //DataRow dr = dtSession.NewRow();
                //dr["sessionid"] = session.SessionID;
                //dr["ipinfo"] = session.SocketSession.Client.RemoteEndPoint.ToString();
                //dr["userid"] = "";
                //dtSession.Rows.Add(dr);


                //增加连接记录;
                if (OnAddLogEvent != null)
                {
                    OnAddLogEvent("连接", str);
                }
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

                //this.dataGridView1.DataSource = new BindingList<MyWebSocketSession>(sessionList);

                str += session.SessionID + "-离开," + reason.ToString();
                //增加连接记录;
                 OnAddLogEvent("连接", str);

                
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
                //AddClientConnLog(message);
                if (OnAddLogEvent != null)
                {
                    OnAddLogEvent("连接", message);
                }
                //收到消息;
                //解析数据;
                DealMsg( session,message);

                

            }
            catch (Exception exp)
            {
                //捕捉错误;
                if (OnAddLogEvent != null)
                {
                    OnAddLogEvent("连接", "消息解释失败"+exp.Message);
                }
            }
        }

        private void DealMsg(WebSocketSession session, string msg)
        {
            try
            {
                if (OnAddLogEvent != null)
                {
                    OnAddLogEvent("连接", msg);
                }
                //MessageBox.Show(hashtable.Count.ToString());
                MyWebSocketSession ReqSessoin = sessionList.Find(delegate(MyWebSocketSession temp)
                {
                    return temp.sessionid == session.SessionID;
                });
                if (ReqSessoin != null)
                {
                    ReqSessoin.LastMsg = msg;
                    //执行脚本;
                    WebSocketIronPythonManager.ScriptExec(ReqSessoin, this.pythonScriptTxt);
                }

            }
            catch (Exception ex)
            {

            }
        }


        //
        public delegate void AddLogEventHandler(string type,string  msg);
        public  event AddLogEventHandler OnAddLogEvent;

        private void button_serverStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (wsServer != null)
                {
                    if (wsServer.State == SuperSocket.SocketBase.ServerState.Running)
                    {
                        wsServer.Stop();
                    }
                    bTCPServerIsRunning = false;
                    if (OnAddLogEvent != null)
                    {
                        OnAddLogEvent("状态", "WebScoket服务已停止,端口" + this.textBox_prot.Text);
                        OnAddLogEvent("状态", "*******************");
                    }
                    wsServer = null;

                    SetStatus(false);

                    //this.button_serverStart.Enabled = true;
                    //this.button_serverStop.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnviewlogs_Click(object sender, EventArgs e)
        {
            FrmWebSocket frm = new FrmWebSocket();
            frm.webSocketControl = this;
            frm.Show();
        }

        private void btn_reloadscript_Click(object sender, EventArgs e)
        {
            GetList();
        }

        
    }
}
