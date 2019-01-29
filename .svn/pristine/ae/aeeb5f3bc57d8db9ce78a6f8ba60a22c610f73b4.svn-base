using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace IMServer.DBHelper
{
    public partial class Form_SqlConfig : Form
    {

        private System.Threading.Thread MyThread_conn = null ;

        public string Server_DataName = string.Empty;
        public string Server_ServerName = string.Empty;
        public int Server_UserRemote = 0;
        private string str_path = string.Empty;
        private bool bConnAccess = false;
        private string connectionString = string.Empty;       //内容数据库连接字符串

        /// <summary>
        /// 服务器名
        /// </summary>
        public  string Server = string.Empty;             //服务器名称
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


        private bool bTestData = false;

        public Form_SqlConfig()
        {
            InitializeComponent();
        }

        private void Form_DataServerConfig_Load(object sender, EventArgs e)
        {
            //初始化2个数据库连接参数
            SqlConn.ReadConnConfig();
            //
            this.textBox_Server.Text = SqlConn.Server;
            this.textBox_Port.Text = SqlConn.Port;
            this.textBox_Uid.Text = SqlConn.Uid;
            this.textBox_Psw.Text = SqlConn.Pwd;
            this.textBox_DataBae.Text = SqlConn.Database;

            string usedconn = Util.LocalConfigXml.GetKey(SqlConn.connUsedConfigFileName, "Data");

            if (usedconn == "1")
                bTestData = true;
           
            this.checkBox_data.Checked = bTestData;
        }

        private void textBox_SearverName_TextChanged(object sender, EventArgs e)
        {
            //当任何属性发生改变时，需要重新执行测试操作
            bConnAccess = false;
            this.button_Ok.Enabled = false;
        }
        private void button_Test_Click(object sender, EventArgs e)
        {
            this.textBox_Msg.Text = "";
            bConnAccess = false;
            //测试服务器连接是否成功
            Test();
        }

        private void Test()
        {
            this.textBox_Msg.Text = "";

            if (this.checkBox_data.Checked)
            {
                if (this.textBox_Server.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("服务器信息不能为空!", "连接提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.textBox_DataBae.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("数据数据库信息不能为空!", "连接提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


             bTestData = this.checkBox_data.Checked;

             if (bTestData == false )
             {
                 MessageBox.Show("至少启用一种连接!", "连接提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

            Server = this.textBox_Server.Text;
            Port = this.textBox_Port.Text;
            Uid = this.textBox_Uid.Text;
            Pwd = this.textBox_Psw.Text;
            Database = this.textBox_DataBae.Text;

         

            connectionString = "server=" + Server + "," + Port + ";uid=" + Uid + ";pwd=" + Pwd + ";database=" + Database;




            MyThread_conn = new System.Threading.Thread(
               new System.Threading.ThreadStart(Conn_Test));

            MyThread_conn.IsBackground = true;
            MyThread_conn.Start();//线程开始。。。
        }


        private void Conn_Test()
        {
            updateEnableInfo(false);

            if (bTestData)
            {
                updateConnTestInfo("服务器：" + Server);
                updateConnTestInfo("数据数据库：" + Database);

                // updateConnTestInfo("测试连接……[" + Server+"]"); 

                SqlConnection myConn = new SqlConnection(connectionString);
                try
                {
                    myConn.Open();
                }
                catch (SqlException)
                {
                    updateConnTestInfo("数据服务器连接测试……失败！请检查配置");
                    updateEnableInfo(true);
                    MyThread_conn.Abort();//关闭线程
                }
                finally
                {
                    myConn.Close();
                    myConn.Dispose();
                }
                updateConnTestInfo("数据数连接成功！" + Database);
                updateConnTestInfo("-----------------------------------");

            }
            else
            {
                updateConnTestInfo("数据服务器连接未启用！");
            }

            

            bConnAccess = true;

            if (bConnAccess)
            {
                #region 将新的连接信息保存到配置文件
                SaveConfigXml();
                #endregion          
            }
            updateConnTestInfo("写入配置……成功");
            updateEnableInfo(true);


            MyThread_conn.Abort();//关闭线程

        }
        //托管
        private delegate void updateConnTestDelegate(string connMsg);
        public void updateConnTestInfo(string connMsg)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(
                    new updateConnTestDelegate(updateConnTestInfo),
                    new object[] { (string)connMsg });

                return;
            }
            if (this.textBox_Msg.Text != "")
                this.textBox_Msg.Text += "\r\n";
                 
            this.textBox_Msg.Text += connMsg;         
        }

        private delegate void updateEnableDelegate(bool Enable);
        public void updateEnableInfo(bool Enable)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(
                    new updateEnableDelegate(updateEnableInfo),
                    new object[] { (bool)Enable });

                return;
            }
            this.button_Ok.Enabled = Enable;
            this.button_Test.Enabled = Enable;
        }

        private void Form_DataServerConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyThread_conn != null)
            {
                if (MyThread_conn.IsAlive)
                    MyThread_conn.Abort();//终止线程
            }
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {

            if (!bConnAccess)
            {
                MessageBox.Show("未通过连接测试!", "操作提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.None;
                return;
            }

            //重新读取配置信息
            SqlConn.ReadConnConfig();
            //SqlSysConn.ReadConnConfig();
        }


        private bool SaveConfigXml()
        {
            try
            {
                Util.LocalConfigXml.SetKey(SqlConn.connUsedConfigFileName, "Data", bTestData == true ? "1" : "0");



                Util.LocalConfigXml.SetKey(SqlConn.connConfigFileName, "Server", Server);
                Util.LocalConfigXml.SetKey(SqlConn.connConfigFileName, "Port", Port);
                Util.LocalConfigXml.SetKey(SqlConn.connConfigFileName, "Uid", Uid);
                Util.LocalConfigXml.SetKey(SqlConn.connConfigFileName, "Pwd", Pwd);
                Util.LocalConfigXml.SetKey(SqlConn.connConfigFileName, "Database", Database);

                return true;
            }
            catch (Exception exp)
            {
                // Response.Write(ex.Message);
                throw new ArgumentNullException("XmlWrite", "配置信息保存失败!" + exp.Message);
            }

        }

        private void checkBox_data_CheckedChanged(object sender, EventArgs e)
        {
            this.groupControl_data.Enabled = this.checkBox_data.Checked;
        }

    }
}