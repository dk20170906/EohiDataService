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
    public partial class From_WebPageList : Form
    {

        public From_WebPageList()
        {
            InitializeComponent();
        }

        private void ReportList_Load(object sender, EventArgs e)
        {
            GetList();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            GetList();
        }
        private void GetList()
        {
          
            try
            {
               

                string strSql = @"select id,webappname,webappnote from api_webapp where (webappname like '%'+@keyword+'%') order by id asc";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@keyword",this.textEdit_keyword.Text.Trim())
                };

                ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
                if (result.code != 1)
                {
                    MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = Core.DeserializeDataTable(result.data.ToString());
                this.gridControl_report.DataSource = dt;

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
            Form_WebPageDlg frm = new Form_WebPageDlg();
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


            Form_WebPageDlg frm = new Form_WebPageDlg();
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

            if (MessageBox.Show("是否删除当前选择的数据[" + this.gridView_report.GetFocusedRowCellValue( "webappname").ToString() + "]？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                return;

            string strSql = "delete from api_webapp where id=@id";
            SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id",this.gridView_report.GetFocusedRowCellValue( "id").ToString())
                };

            ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);
            if (result.code != 1)
            {
                MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           

            GetList();
           
        }
        private void toolStripButton_Design_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
