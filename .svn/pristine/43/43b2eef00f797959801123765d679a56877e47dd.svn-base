using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IMServer.DBHelper
{
    public partial class Form_ConnTesting : Form
    {
        /// <summary>
        /// 服务器名
        /// </summary>
        public string Server = string.Empty;             //服务器名称
        /// <summary>
        /// 服务器登录端口
        /// </summary>
        public string Port = string.Empty;            //端口
        /// <summary>
        /// 服务器登录账户
        /// </summary>
        public string Uid = string.Empty;                 //数据库登用用户
        /// <summary>
        /// 服务器登录密码
        /// </summary>
        public string Pwd = string.Empty;                   //数据库登录密码
        /// <summary>
        /// 数据数据库名
        /// </summary>
        public string Database = string.Empty;               //内容数据库

        private System.Threading.Thread MyThread_conn = null;
        public bool bConnAccess = false;
        private string connString = "";

        public Form_ConnTesting()
        {
            InitializeComponent();
        }

        private void Form_ConnTesting_Load(object sender, EventArgs e)
        {
            connString = "server=" + Server + "," + Port + ";uid=" + Uid + ";pwd=" + Pwd + ";database=" + Database;

            bConnAccess = false;
            //测试服务器连接是否成功
            Test();
        }
        private void Test()
        {
            this.label_msg.Text = "";

            MyThread_conn = new System.Threading.Thread(new System.Threading.ThreadStart(Conn_Test));
            MyThread_conn.IsBackground = true;
            MyThread_conn.Start();//线程开始。。。
        }


        private void Conn_Test()
        {

            bConnAccess = false;

            SqlConnection myConn = new SqlConnection(connString);
            try
            {
                myConn.Open();
            }
            catch (SqlException exp)
            {
                updateConnInfo("服务器连接……失败！请检查配置.");
                Thread.Sleep(2000);//线程休眠3000毫秒=3秒
                updateConnState(false);
                MyThread_conn.Abort();//关闭线程
            }
            finally
            {
                myConn.Close();
                myConn.Dispose();
            }


            updateConnInfo("服务器连接……成功！！");
            Thread.Sleep(1000);//线程休眠3000毫秒=3秒
            bConnAccess = true;
            updateConnState(true);

            MyThread_conn.Abort();//关闭线程

        }
        //托管 -
        private delegate void updateConnStateDelegate(bool access);
        public void updateConnState(bool access)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(
                    new updateConnStateDelegate(updateConnState),
                    new object[] { (bool)access });

                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        //托管
        private delegate void updateConnInfoDelegate(string connMsg);
        public void updateConnInfo(string connMsg)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(
                    new updateConnInfoDelegate(updateConnInfo),
                    new object[] { (string)connMsg });

                return;
            }
            this.label_msg.Text = connMsg;
        }

    }
}
