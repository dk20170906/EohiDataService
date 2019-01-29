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
    public partial class Form_LinkDlg : Form
    {

        public Form_LinkDlg()
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
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                  typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }


                string strSql = @"select * from  api_links where id=@id";
                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@id", this.id.ToString());
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    MessageBox.Show(result.Msg);
                }
                else
                {
                    //this.gridControl_report.DataSource = result.DataTable;

                    DataTable dt = result.DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.textEdit_id.Text = dt.Rows[0]["id"].ToString();
                        this.textEdit_linktype.Text = dt.Rows[0]["linktype"].ToString();
                        this.textEdit_linkname.Text = dt.Rows[0]["linkname"].ToString();
                        this.memoEdit_linkstring.Text = dt.Rows[0]["linkstring"].ToString();
                    }
                    else
                    {
                        Common.Util.NocsMessageBox.Message("信息获取失败！");
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.textEdit_linkname.Text.Trim().Length <= 0)
            {
                Common.Util.NocsMessageBox.Message("请输入模型名称！");
                return;
            }



            if (this.id.Length > 0)
            {
                string strSql = @"update  api_links set 
                    linkname=@linkname,
                    linktype=@linktype,
                    linkstring=@linkstring
                   where id=@id";

                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                 typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }

                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@id", this.id);
                parames.Add("@linkname", this.textEdit_linkname.Text.Trim());
                parames.Add("@linktype", this.textEdit_linktype.Text.Trim());
                parames.Add("@linkstring", this.memoEdit_linkstring.Text.Trim());
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.ExecuteNonQuery(strSql, parames);

                if (result.Code > 0)
                {
                    Common.Util.NocsMessageBox.Message(result.Msg);
                    return;
                }
                else
                {
                }
            }
            else
            {

                string strSql = @"insert into api_links (linkname,linktype,linkstring)
                    values (@linkname,@linktype,@linkstring)";
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                if (remotingSQLHelper == null)
                {
                    MessageBox.Show("连接创建失败！");
                    return;
                }

                //构建一个哈希表，把参数依次压入
                Hashtable parames = new Hashtable();
                parames.Add("@linkname", this.textEdit_linkname.Text.Trim());
                parames.Add("@linktype", this.textEdit_linktype.Text.Trim());
                parames.Add("@linkstring", this.memoEdit_linkstring.Text.Trim());
                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.ExecuteNonQuery(strSql, parames);
                if (result.Code > 0)
                {
                    Common.Util.NocsMessageBox.Message(result.Msg);
                    return;
                }
                else
                {
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
