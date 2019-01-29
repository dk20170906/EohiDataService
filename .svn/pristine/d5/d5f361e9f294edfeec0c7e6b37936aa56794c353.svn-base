using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace EohiDataCenter
{
    public partial class RemotingAPIControl : UserControl
    {
        public RemotingAPIControl()
        {
            InitializeComponent();
        }
        bool bApiServerIsRunning = false;
        private List<ApiItem> apiItemList = new List<ApiItem>();



        private void RemotingAPIControl_Load(object sender, EventArgs e)
        {
        }
        private void btn_start_Click(object sender, EventArgs e)
        {

            if (bApiServerIsRunning)
            {
                //停止;
                EohiDataRemoteObject.ApiHelper.ApiRequsetSendedEvent -= ApiHelper_ApiRequsetSendedEvent;
                bApiServerIsRunning = false;
            }
            else
            {
                GetList();


                int iport = 9933;
                string port = Common.Util.LocalConfigXml.GetKey("remoting.xml", "remotingport", false);
                try
                {
                    iport = Convert.ToInt32(port);
                }
                catch (Exception)
                {
                }
                //启用remoting服务;
                System.Runtime.Remoting.Channels.Http.HttpChannel channel = new System.Runtime.Remoting.Channels.Http.HttpChannel(iport);
                ChannelServices.RegisterChannel(channel, false);


                //启动;
                this.label_apiitemcount.Text = this.apiItemList.Count.ToString();

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
                this.btn_start.Text = "停止";


                this.label_status.BackColor = Color.Green;
                this.label_status.Text = "启动";
            }
            else
            {
                this.btn_start.Text = "启动";

                this.label_status.BackColor = Color.Red;
                this.label_status.Text = "停止";
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

                PrintLog("> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + method + ",执行完成", true);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                PrintLog("> Error:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + ex.ToString(), false);
            }

        }

        public StringBuilder sb = new StringBuilder();

        //创建一个委托：赋值操作
        private delegate void PrintLog_Delegate(string value, bool access);
        public void PrintLog(string value, bool access)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                    new PrintLog_Delegate(PrintLog),
                    new Object[] { value, access });
                return;
            }
            try
            {
                if (sb.Length > 200)
                    sb.Clear();

                sb.Append(value);
             
            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetList();
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


    }
}
