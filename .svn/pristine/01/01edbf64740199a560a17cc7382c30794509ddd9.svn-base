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
    public partial class From_ApiList : Form
    {
        public string projectId = "";

        public From_ApiList()
        {
            InitializeComponent();
        }

        private void ReportList_Load(object sender, EventArgs e)
        {

            GetList();
        }


        private void GetList()
        {

            //List<ClientClass.ParmField> list = new List<ClientClass.ParmField>();
            //list.Add(new ClientClass.StringField("sql", "select * from api_items where (apiname like '%'+@keyword+'%') order by apiname asc"));
            string strSql = @"select * from api_items where (apiname like '%'+@keyword+'%') order by apiname asc";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@keyword","%"+this.textEdit_keyword.Text+"%")
            };

            ClientClass.ApiResult result = Core.RequestXMLSQL_GetDataTable(strSql, pars);

             //ClientClass.ApiResult result = Core.RequestApi("DB/SQL/GetDataTable", list);
             if (result.code != 1)
             {
                 MessageBox.Show(result.msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
            DataTable dt = Core.DeserializeDataTable(result.data.ToString());
            this.gridControl_report.DataSource = dt;

            
            //string strSql = @"select * from u3d_project  order by id asc";
            //SqlParameter[] pars = new SqlParameter[]
            //{
            //    new SqlParameter("@keyword","%"+this.textEdit_keyword.Text+"%")
            //};

            //DataTable dt = Common.DBHelper.SqlCmd.getDataTable(strSql, pars);
            //this.gridControl_report.DataSource = dt;

            //构建一个哈希表，把参数依次压入
          
            try
            {
                //EohiDataRemoteObject.RemotingSQLHelper
                //EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                //   typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                //EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                //if (result.Code > 0)
                //{
                //    MessageBox.Show(result.Msg);
                //}
                //else
                //{
                    
                //}

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

            if (MessageBox.Show("是否删除当前选择的数据[" + this.gridView_report.GetFocusedRowCellValue( "apiname").ToString() + "]？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
    }
}
