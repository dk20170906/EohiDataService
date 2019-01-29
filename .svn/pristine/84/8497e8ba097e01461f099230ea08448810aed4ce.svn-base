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
    public partial class From_LinkList : Form
    {
        public string projectId = "";

        public From_LinkList()
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
            //string strSql = @"select * from api_links where (linkname like '%'+@keyword+'%') order by id asc";
            //SqlParameter[] pars = new SqlParameter[]
            //{
            //    new SqlParameter("@keyword","%"+this.textEdit_keyword.Text+"%")
            //};

            //DataTable dt = Common.DBHelper.SqlCmd.getDataTable(strSql, pars);
            //this.gridControl_report.DataSource = dt;

          
            try
            {
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                  typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }


                string strSql = @"select * from api_links where (linkname like '%'+@keyword+'%') order by id asc";
                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@keyword", this.textEdit_keyword.Text);
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

        private void toolStripButton_New_Click(object sender, EventArgs e)
        {
            Form_LinkDlg frm = new Form_LinkDlg();
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


            Form_LinkDlg frm = new Form_LinkDlg();
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

            if (MessageBox.Show("是否删除当前选择的数据[" + this.gridView_report.GetFocusedRowCellValue( "linkname").ToString() + "]？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                return;

            string strSql = "delete from api_links where id=@id";
            EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                 typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

            if (remotingSQLHelper == null)
            {
                MessageBox.Show("连接创建失败！");
                return;
            }

            //构建一个哈希表，把参数依次压入
            Hashtable parames = new Hashtable();
            parames.Add("@id", this.gridView_report.GetFocusedRowCellValue( "id").ToString());
            EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.ExecuteNonQuery(strSql, parames);
            if (result.Code > 0)
            {

                Common.Util.NocsMessageBox.Message(result.Msg);
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
