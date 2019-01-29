using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.Remoting;

namespace EohiDataCenter
{
    public partial class Form_ServiceController : DevExpress.XtraEditors.XtraForm
    {
        public Form_ServiceController()
        {
            InitializeComponent();
        }

        private void btn_sqlservice_start_Click(object sender, EventArgs e)
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
        }

        private void btn_startApiDataServer_Click(object sender, EventArgs e)
        {

        }
    }
}