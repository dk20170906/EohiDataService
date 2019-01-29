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



namespace EohiDataCenter
{
    public partial class LayoutScriptEditForm : Form
    {
        public string funId = "";
        public int id = 0;
        public int copyid = 0;

        //public LayoutScriptList layoutScriptList;

        //public RuntimeHost runtimeHost;

        public LayoutScriptEditForm()
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
            //if (id > 0)
            //{
            //    read(id);
            //}
            //if (copyid > 0)
            //{
            //    read(copyid);
            //}
            //LoadMethodList("");

            this.textEditorControl.Text = scriptTxt;
            
        }

        private void LoadMethodList(string search)
        {
            //string strSql = @"select * from   erpsys_define_scrpit_method order by sortnum asc";
            //if (search.Length > 0)
            //    strSql = @"select * from   erpsys_define_scrpit_method where 1=1 and ( method like '%'+@search+'%' or [desc] like '%'+@search+'%') order by sortnum asc";

            //SqlParameter[] pars = new SqlParameter[]
            //{
            //    new SqlParameter("@search",search)
            //};
            //DataTable dt = Common.DBHelper.SqlSysCmd.getDataTable(strSql, pars);

            //if (dt == null || dt.Rows.Count <= 0)
            //{
            //    MessageBox.Show("预置脚本方法列表获取失败！");
            //    return;
            //}

            //this.gridControl1.DataSource = dt;
        }

      

        public string scriptTxt = "";
        private void btn_Save_Click(object sender, EventArgs e)
        {
          
            scriptTxt = this.textEditorControl.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hashTable = new Hashtable();
                string[] paras = this.textEdit_pars.Text.Trim().Split('&');
                for (int a = 0; a < paras.Length; a++)
                {
                    string[] parasv = paras[a].Split('=');
                    if (parasv.Length > 1)
                    {
                        hashTable.Add(parasv[0], parasv[1]);

                    }
                }


                EohiDataRemoteObject.ApiResult apiResult = new EohiDataRemoteObject.ApiResult();



                ApiHost apihost = new ApiHost();
                apihost.apiResult = apiResult;
                apihost.requestParas = hashTable;


                //执行脚本;
                
                var actual =IronPythonManager.ScriptExec(apihost, this.textEditorControl.Text);

               

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                this.memoEdit_Print.ForeColor = Color.Red;
                memoEdit_Print.Text = "> Error:" + ex.ToString();
            }

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
           
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle < 0)
            {
                this.memoEdit_methodesc.Text = "";
                return;
            }

            this.memoEdit_methodesc.Text = this.gridView1.GetFocusedRowCellValue("desc").ToString();
        }

        private void simpleButton_scriptSample_Click(object sender, EventArgs e)
        {
            /*
            LayoutScriptSample frm = new LayoutScriptSample();
            frm.ShowDialog();

            this.BringToFront();
            this.Activate();
            */
        }

        private void textEdit_sysfunname_EditValueChanged(object sender, EventArgs e)
        {
            LoadMethodList(this.textEdit_sysfunname.Text.Trim());
        }
    }
}
