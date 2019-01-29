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

namespace EohiDataCenter
{
    public partial class FrmWebSocket : Form
    {
        public FrmWebSocket()
        {
            InitializeComponent();
        }

        public WebSocketControl webSocketControl = null;


        private void WebSocketControl_Load(object sender, EventArgs e)
        {
           
            if (this.webSocketControl != null)
            {
                this.gridControl_client.DataSource = webSocketControl.sessionList;

                webSocketControl.OnAddLogEvent += WebSocketControl_OnAddLogEvent;
            }

        }
        private void FrmWebSocket_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.webSocketControl != null)
            {

                webSocketControl.OnAddLogEvent -= WebSocketControl_OnAddLogEvent;
            }
        }
        void WebSocketControl_OnAddLogEvent(string type, string msg)
        {
            //throw new NotImplementedException();
            if (type == "状态")
            {
                SetTCPValue(msg);
            }
            if (type == "连接")
            {
                AddClientConnLog(msg);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
