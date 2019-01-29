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
    public partial class From_ApiList_Selector : Form
    {

        public From_ApiList_Selector()
        {
            InitializeComponent();
        }

        private void ReportList_Load(object sender, EventArgs e)
        {

            GetList();
        }


        private void GetList()
        {
            string strSql = @"select * from api_items where (apiname like '%'+@keyword+'%') order by apiname asc";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@keyword","%"+this.textEdit_keyword.Text+"%")
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
            parames.Add("@keyword", this.textEdit_keyword.Text);
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

      
       

        private void toolStripButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string select_apiname = "";

        private void btn_select_Click(object sender, EventArgs e)
        {
            if (this.gridView_report.FocusedRowHandle < 0)
            {
                MessageBox.Show("请选择数据！");
                return;
            }
            //
            select_apiname = this.gridView_report.GetFocusedRowCellValue("apiname").ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
