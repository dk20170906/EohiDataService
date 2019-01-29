using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace EohiDataCenter
{
    public partial class RemotingSQLControl : UserControl
    {
        public RemotingSQLControl()
        {
            InitializeComponent();
        }

        private void RemotingSQLControl_Load(object sender, EventArgs e)
        {

           
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                EohiDataRemoteObject.SqlConn.Server = Common.DBHelper.SqlConn.Server;
                EohiDataRemoteObject.SqlConn.Port = Common.DBHelper.SqlConn.Port;
                EohiDataRemoteObject.SqlConn.Uid = Common.DBHelper.SqlConn.Uid;
                EohiDataRemoteObject.SqlConn.Pwd = Common.DBHelper.SqlConn.Pwd;
                EohiDataRemoteObject.SqlConn.Database = Common.DBHelper.SqlConn.Database;
                EohiDataRemoteObject.SqlConn.ReadConnConfig();
                //注册响应方法;--sql--服务
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(EohiDataRemoteObject.RemotingSQLHelper),
                    "remotingsqlhelper",
                    WellKnownObjectMode.SingleCall);


                this.label1.BackColor = Color.Green;
                this.label1.Text = "正在服务";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
