using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;

namespace EohiDataCenter
{
    public partial class From_ApiServer : Form
    {

        public From_ApiServer()
        {
            InitializeComponent();
        }

        private List<ApiItem> apiItemList = new List<ApiItem>();



        private void ReportList_Load(object sender, EventArgs e)
        {
            int iport = 9933;
            string port = Common.Util.LocalConfigXml.GetKey("remoting.xml", "remotingport", false);
            try
            {
                iport = Convert.ToInt32(port);
            }
            catch (Exception)
            {
            }

            this.textEdit_apiexec_port.Text = iport.ToString();

            GetList();

            
        }
        bool bApiServerIsRunning = false;
        private void btn_startApiDataServer_Click(object sender, EventArgs e)
        {
            StartRemotingServer();
        }

        private void StartRemotingServer()
        {

            if (bApiServerIsRunning)
            {
                //停止;
                EohiDataRemoteObject.ApiHelper.ApiRequsetSendedEvent -= ApiHelper_ApiRequsetSendedEvent;
                bApiServerIsRunning = false;
            }
            else
            {
                //启动;
                this.label_apiitemcount.Text = this.apiItemList.Count.ToString();
                /*
                int port = Convert.ToInt32(this.textEdit_apiexec_port.Text);
                System.Runtime.Remoting.Channels.Http.HttpChannel channel = new System.Runtime.Remoting.Channels.Http.HttpChannel(port);
                //TcpServerChannel channel = new TcpServerChannel(9932);
                ChannelServices.RegisterChannel(channel, false);
                */

                //注册响应方法;
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(EohiDataRemoteObject.ApiHelper), "apihelper", WellKnownObjectMode.SingleCall);
                EohiDataRemoteObject.ApiHelper.ApiRequsetSendedEvent += ApiHelper_ApiRequsetSendedEvent;
                //
                bApiServerIsRunning = true;
            }

            SetStartButton();
        }

        public void SetStartButton()
        {
            if (bApiServerIsRunning)
            {
                this.btn_startApiDataServer.Text = "停止";
            }
            else
            {
                this.btn_startApiDataServer.Text = "启动";
            }
        }


        void ApiHelper_ApiRequsetSendedEvent(EohiDataRemoteObject.ApiHelper apiHelper, Hashtable hashtable)
        {
            try
            {
                //MessageBox.Show(hashtable.Count.ToString());

                string method = hashtable["method"].ToString();

                EohiDataRemoteObject.ApiResult result = new EohiDataRemoteObject.ApiResult();

                ApiHost apihost = new ApiHost();
                apihost.apiResult = result;
                apihost.requestParas = hashtable;


                //执行查询；
                ApiItem apiItem = this.apiItemList.Find(x => x.apiname == method);
                if (apiItem == null)
                {
                    result.Code = 1;
                    result.ResultDataType = 2;
                    result.Msg = "未找到名为" + method + "的方法,该api可能不存在未处于在线服务状态";
                    result.ResultDataType = 4;
                }
                else
                {
                    //执行脚本;
                    IronPythonManager.ScriptExec(apihost, apiItem.apiscript);
                }


                //将查询结果返回;
                apiHelper._reslut = result;

                PrintLog("> "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +" "+ method + ",执行完成", true);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
               PrintLog("> Error:" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +" "+ ex.ToString(),false);
            }

        }

        public StringBuilder sb = new StringBuilder();

        //创建一个委托：赋值操作
        private delegate void PrintLog_Delegate(string value, bool access);
        public void PrintLog(string value, bool access)
        {
            if (this.memoEdit_Print.InvokeRequired)
            {
                this.memoEdit_Print.BeginInvoke(
                    new PrintLog_Delegate(PrintLog),
                    new Object[] { value, access });
                return;
            }
            try
            {
                if (sb.Length > 200)
                    sb.Clear();

                sb.Append(value);
                if(true)
                    this.memoEdit_Print.ForeColor = Color.Green;
                else
                    this.memoEdit_Print.ForeColor = Color.Red;
             
                
                memoEdit_Print.Text = sb.ToString();
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }


        private void GetList()
        {
            string strSql = @"select
            api_items_on.*,
            api_items.apipars,
            api_items.apinote,
            api_items.apiscript
            from api_items_on
            left join api_items on (api_items.apiname =api_items_on.apiname)
            order by api_items_on.apiname asc";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@keyword","%"+this.textEdit_apiexec_port.Text+"%")
            };
            //string strSql = @"select * from u3d_project  order by id asc";
            //SqlParameter[] pars = new SqlParameter[]
            //{
            //    new SqlParameter("@keyword","%"+this.textEdit_keyword.Text+"%")
            //};

            //DataTable dt = Common.DBHelper.SqlCmd.getDataTable(strSql, pars);
            //this.gridControl_report.DataSource = dt;

            //构建一个哈希表，把参数依次压入
            Hashtable parames = new Hashtable();
            parames.Add("@keyword", this.textEdit_apiexec_port.Text);
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
                    this.gridControl_report.DataSource = result.DataTable;


                    //
                    this.apiItemList.Clear();
                    //
                    for (int i = 0; i < result.DataTable.Rows.Count; i++)
                    {
                        ApiItem apiItem = new ApiItem();
                        apiItem.apiname = result.DataTable.Rows[i]["apiname"].ToString();
                        apiItem.apiscript = result.DataTable.Rows[i]["apiscript"].ToString();

                        this.apiItemList.Add(apiItem);
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void toolStripButton_New_Click(object sender, EventArgs e)
        {
            Form_Api_Dlg frm = new Form_Api_Dlg();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetList();
            }
        }

        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            if (this.gridView_report.FocusedRowHandle < 0)
            {
                MessageBox.Show("请选择需要编辑的数据！");
                return;
            }

            string id = this.gridView_report.GetFocusedRowCellValue("id").ToString();

            Form_Api_Dlg frm = new Form_Api_Dlg();
            frm.id = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                GetList();
            }
        }

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            if (this.gridView_report.FocusedRowHandle < 0)
            {
                MessageBox.Show("请选择需要删除的数据！");
                return;
            }

            if (MessageBox.Show("是否删除当前选择的数据[" + this.gridView_report.GetFocusedRowCellValue("apiname").ToString() + "]？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                return;

            try
            {
                string strSql = "delete from api_iems where id=@id";
                Hashtable parames = new Hashtable();
                parames.Add("@id", this.gridView_report.GetFocusedRowCellValue("id").ToString());
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                 typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }

                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("删除成功！");
                    this.gridView_report.DeleteRow(this.gridView_report.FocusedRowHandle);
                }
            }
            catch (Exception exp)
            {
                Common.Util.NocsMessageBox.Message(exp.Message);
            }
           
        }
        private void toolStripButton_Design_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_addapi_on_Click(object sender, EventArgs e)
        {
            From_ApiList_Selector frm = new From_ApiList_Selector();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //select_apiname 

                string apiname = frm.select_apiname;


                string strSql = "insert into api_items_on (apiname) values(@apiname)";


                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@apiname", apiname);
                try
                {
                    EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                       typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                    EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.ExecuteNonQuery(strSql, parames);
                    if (result.Code > 0)
                    {
                        MessageBox.Show(result.Msg);
                    }
                    else
                    {
                        //刷新;
                        GetList();
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void print(string msg)
        {
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.memoEdit_Print.Text = "";
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetList();
        }
        
    }
}
