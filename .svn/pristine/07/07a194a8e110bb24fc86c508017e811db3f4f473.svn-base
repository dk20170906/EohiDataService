using EohiDataCenter.Lib.NetWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiDataCenter
{
    public partial class Form_WebPageDlg : Form
    {

        public Form_WebPageDlg()
        {
            InitializeComponent();
        }

        public string id = "";

        private void Form_Project_Dlg_Load(object sender, EventArgs e)
        {
            if (id.Length > 0)
                ReadInfo();
            else
            {
                this.textEdit_id.Text = "";
            }
          
        }
        private void ReadInfo()
        {
            try
            {

                string strSql = @"select * from  api_webapp where id=@id";

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
                    this.textEdit_id.Text = dt.Rows[0]["id"].ToString();
                    this.textEdit_webappnote.Text = dt.Rows[0]["webappnote"].ToString();
                    this.textEdit_webappname.Text = dt.Rows[0]["webappname"].ToString();
                    this.memoEdit_wenapphtml.Text = dt.Rows[0]["webapphtml"].ToString();
                    this.memoEdit_script.Text = dt.Rows[0]["webappscript"].ToString();
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.textEdit_webappname.Text.Trim().Length <= 0)
            {
                Common.Util.NocsMessageBox.Message("请输入模型名称！");
                return;
            }



            if (this.id.Length > 0)
            {
                string strSql = @"update  api_webapp set 
                    webappname=@webappname,
                    webappnote=@webappnote,
                    webapphtml=@webapphtml,
                    webappscript=@webappscript
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id",id.ToString()),
                    new SqlParameter("@webappname",this.textEdit_webappname.Text.Trim()),
                    new SqlParameter("@webappnote",this.textEdit_webappnote.Text.Trim()),
                    new SqlParameter("@webapphtml",this.memoEdit_wenapphtml.Text.Trim()),
                    new SqlParameter("@webappscript",this.memoEdit_script.Text.Trim())
                };

                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {

                string strSql = @"insert into api_webapp (webappname,webappnote,webapphtml,webappscript)
                    values (@webappname,@webappnote,@webapphtml,@webappscript)";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@webappname",this.textEdit_webappname.Text.Trim()),
                    new SqlParameter("@webappnote",this.textEdit_webappnote.Text.Trim()),
                    new SqlParameter("@webapphtml",this.memoEdit_wenapphtml.Text.Trim()),
                    new SqlParameter("@webappscript",this.memoEdit_script.Text.Trim())
                };


                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

 
    }
}
