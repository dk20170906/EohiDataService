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
    public partial class FormRemotingConf : Form
    {

        public FormRemotingConf()
        {
            InitializeComponent();
        }


        private void Form_Project_Dlg_Load(object sender, EventArgs e)
        {
            ReadInfo();

        }
        private void ReadInfo()
        {
            try
            {
                this.memoEdit_apipars.Text = Common.Util.LocalConfigXml.GetKey("RemotingConf.xml", "RemotingSqlAddress",false);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }



        private void btn_Save_Click(object sender, EventArgs e)
        {
            //测试;
            string RemotingSqlAddress = this.memoEdit_apipars.Text;
            if (ConnCheck(RemotingSqlAddress))
            {
                Common.Util.LocalConfigXml.SetKey("RemotingConf.xml", "RemotingSqlAddress", this.memoEdit_apipars.Text.Trim(),false);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {

            }

        }


        /// <summary>
        /// 连接检查
        /// </summary>
        /// <returns></returns>
        private static bool ConnCheck(string RemotingSqlAddress)
        {
            try
            {
                string strSql = @"select getdate()";

                Hashtable parames = new Hashtable();
                EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                   typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingSqlAddress);

                EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                if (result.Code > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void btn_editscript_Click(object sender, EventArgs e)
        {
        }

    }
}
