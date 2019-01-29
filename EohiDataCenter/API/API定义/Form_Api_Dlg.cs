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
    public partial class Form_Api_Dlg : Form
    {

        public Form_Api_Dlg()
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

                string strSql = @"select * from  api_items where id=@id";
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id",id.ToString())
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
                    this.textEdit_apiname.Text = dt.Rows[0]["apiname"].ToString();
                    this.memoEdit_apipars.Text = dt.Rows[0]["apipars"].ToString();
                    this.memoEdit_apinote.Text = dt.Rows[0]["apinote"].ToString();
                    this.memoEdit_script.Text = dt.Rows[0]["apiscript"].ToString();
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


            if (this.textEdit_apiname.Text.Trim().Length <= 0)
            {
                Common.Util.NocsMessageBox.Message("请输入api名称！");
                return;
            }

            if (this.id.Length > 0)
            {
                string strSql = @"update  api_items set 
                   
                    apiname=@apiname,
                    apipars=@apipars,
                    apinote=@apinote,
                    apiscript=@apiscript,
                    mod_date=getdate(),
                    mod_man=''
                   where id=@id";

                SqlParameter[] pars = new SqlParameter[]
                    { 
                        new SqlParameter("@id", this.id),
                        new SqlParameter("@apiname", this.textEdit_apiname.Text.Trim()),
                        new SqlParameter("@apipars", this.memoEdit_apipars.Text.Trim()),
                        new SqlParameter("@apinote", this.memoEdit_apinote.Text.Trim()),
                        new SqlParameter("@apiscript", this.memoEdit_script.Text)
                    };

                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("修改成功！");
                }
            }
            else
            {
                string strSql = @"insert into api_items (apiname,apipars,apinote,apiscript,mod_man,mod_date)
                    values (@apiname,@apipars,@apinote,@apiscript,'',getdate())";
                SqlParameter[] pars = new SqlParameter[] {
                  new SqlParameter("@apiname", this.textEdit_apiname.Text.Trim()),
                        new SqlParameter("@apipars", this.memoEdit_apipars.Text.Trim()),
                        new SqlParameter("@apinote", this.memoEdit_apinote.Text.Trim()),
                        new SqlParameter("@apiscript", this.memoEdit_script.Text)
                };
                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Common.Util.NocsMessageBox.Message("添加成功！");
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

        private void btn_editscript_Click(object sender, EventArgs e)
        {
            LayoutScriptEditForm frm = new LayoutScriptEditForm();
            frm.scriptTxt = this.memoEdit_script.Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.memoEdit_script.Text = frm.scriptTxt;
            }
        }

    }
}
