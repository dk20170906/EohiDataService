using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections;
using EohiDataCenter.Lib.NetWork;



namespace EohiDataCenter
{
    public partial class WebSocketScriptEditForm : Form
    {
        public int id = 0;

        public WebSocketScriptEditForm()
        {
            InitializeComponent();
        }
        private TextEditorControl ActiveEditor
        {
            get
            {
                return this.textEditorControl;
            }
        }
        private void LayoutScriptEditForm_Load(object sender, EventArgs e)
        {
            ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            ActiveEditor.Encoding = System.Text.Encoding.Default;


            //
            this.textEditorControl_loop.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            this.textEditorControl_loop.Encoding = System.Text.Encoding.Default; 
            //if (id > 0)
            //{
            //    read(id);
            //}
            //if (copyid > 0)
            //{
            //    read(copyid);
            //}
            //LoadMethodList("");

            ReadInfo();
            
        }

        private void ReadInfo()
        {
            try
            {
                string strSql = @"select * from  api_websocket ";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id",this.id.ToString())
                };

                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable dt = Core.DeserializeDataTable(result.data.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    this.textEditorControl.Text = dt.Rows[0]["scripttxt"].ToString();

                    this.textEditorControl_loop.Text = dt.Rows[0]["loopscripttxt"].ToString();
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("信息获取失败！");
                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        public string scriptTxt = "";
        private void btn_Save_Click(object sender, EventArgs e)
        {
          


            if (this.id > 0)
            {
                string strSql = @"update  api_websocket set 
                   
                    scripttxt=@scripttxt,
                    loopscripttxt=@loopscripttxt
                 
                   where id=@id";
              
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                 typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }

                Hashtable parames = new Hashtable();
                parames.Add("@id", this.id);
                parames.Add("@scripttxt", this.textEditorControl.Text);
                parames.Add("@loopscripttxt", this.textEditorControl_loop.Text);
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("修改成功！");
                }
            }
            else
            {
                string strSql = @"insert into api_websocket (scripttxt,loopscripttxt)
                    values (@scripttxt,@loopscripttxt)";
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }

                Hashtable parames = new Hashtable();
                parames.Add("@scripttxt", this.textEditorControl.Text);
                parames.Add("@loopscripttxt", this.textEditorControl_loop.Text);
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("添加成功！");
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
           
        }

      
        private void simpleButton_scriptSample_Click(object sender, EventArgs e)
        {
            /*
            LayoutScriptSample frm = new LayoutScriptSample();
            frm.ShowDialog();

            this.BringToFront();
            this.Activate();
            */
            
                //执行脚本;
           //WebSocketIronPythonManager.ScriptExec(ReqSessoin, this.pythonScriptTxt);

        }

    }
}
