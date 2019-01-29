using Common.DBHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace EohiQuartzManager
{

    public partial class FormMain : Form
    {
        private int m = 0;
        public FormMain()
        {

            InitializeComponent();
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            FrmTaskAdd frmTaskAdd = new FrmTaskAdd();
            frmTaskAdd.ShowDialog(this);
            RefreshDateGridView(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvtasklist.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                DataGridViewButtonCell btnCell = dgvtasklist.CurrentCell as DataGridViewButtonCell;
                if (btnCell != null && btnCell.Value.ToString() == "查看")
                {
                    MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                    messageBoxTimeOut.ShowMessageBoxTimeout("无相关操作");
                }
                else if (btnCell != null && btnCell.Value.ToString() == "编辑")
                {
                    int mint = btnCell.RowIndex;
                    if (mint > 0)
                    {
                        int id = (int)dgvtasklist.Rows[mint].Cells["id"].Value;
                        FrmTaskAdd frmTaskAdd = new FrmTaskAdd();
                        frmTaskAdd.Id = id;
                        frmTaskAdd.ShowDialog(this);
                    }
                    RefreshDateGridView(sender, e);
                }
                else if (btnCell != null && btnCell.Value.ToString() == "删除")
                {
                    int mint = btnCell.RowIndex;
                    if (mint > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("消息内容", "返回值 确定1 取消2", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                        int id = (int)dgvtasklist.Rows[mint].Cells["id"].Value;
                        string sqlstr = "delete from api_quartz where id=@id;";
                        SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter("@id",id)
                        };
                        int sqlmint = Common.DBHelper.SqlCmd.ExecuteNonQuery(sqlstr, sqlParameters);
                        if (sqlmint > 0)
                        {

                            MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                            messageBoxTimeOut.ShowMessageBoxTimeout("删除成功！！！");
                        }
                        else
                        {
                            MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                            messageBoxTimeOut.ShowMessageBoxTimeout("删除失败，请重试！！！");
                        }
                    }
                    RefreshDateGridView(sender, e);
                }
                else if (btnCell != null)
                {
                    //获取当前被点击的单元格 
                    DataGridViewButtonCell vCell = (DataGridViewButtonCell)dgvtasklist.CurrentCell;
                    string sqlstr = "UPDATE api_quartz SET quartzstatus=@quartzstatus  WHERE id = @id;";
                    string butval = "启动";
                    if (vCell.Value.ToString() == butval)
                    {
                        butval = "停止";
                    }
                    int id = (int)dgvtasklist.Rows[e.RowIndex].Cells["id"].Value;
                    SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter("@id",id),
                            new SqlParameter("@quartzstatus",butval)
                        };
                    int mint = Common.DBHelper.SqlCmd.ExecuteNonQuery(sqlstr, sqlParameters);
                    RefreshDateGridView(sender, e);
                }
                else
                {

                }
            }

        }

        private void butljsz_Click(object sender, EventArgs e)
        {
            Form_SqlConfig form_SqlConfig = new Form_SqlConfig();
            form_SqlConfig.ShowDialog();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshDateGridView(object sender, EventArgs e)
        {
            string sqlstrfordgvlist = "select id,quartzname,crontrigger,quartzstatus,  quartznote,jobtype,jobpars,mod_date from api_quartz;";
            DataTable dt = Common.DBHelper.SqlCmd.getDataTable(sqlstrfordgvlist);
            dgvtasklist.DataSource = dt;
        }
        /// <summary>
        ///添加查看删除列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddSelectEditDelBut(object sender, EventArgs e)
        {
            DataGridViewButtonColumn seebut = new DataGridViewButtonColumn();
            seebut.Text = "查看";//添加的这列的显示文字，即每行最后一列显示的文字。
            seebut.Name = "buttonEdit";
            seebut.HeaderText = "查看";//列的标题
            seebut.UseColumnTextForButtonValue = true;//上面设置的dlink.Text文字在列中显示
            seebut.Width = 66;
            DataGridViewButtonColumn editbut = new DataGridViewButtonColumn();
            editbut.Text = "编辑";//添加的这列的显示文字，即每行最后一列显示的文字。
            editbut.Name = "buttonEdit";
            editbut.HeaderText = "编辑";//列的标题

            editbut.UseColumnTextForButtonValue = true;//上面设置的dlink.Text文字在列中显示
            editbut.Width = 66;
            DataGridViewButtonColumn delbut = new DataGridViewButtonColumn();
            delbut.Text = "删除";//添加的这列的显示文字，即每行最后一列显示的文字。
            delbut.Name = "buttonEdit";
            delbut.HeaderText = "删除";//列的标题
            delbut.UseColumnTextForButtonValue = true;//上面设置的dlink.Text文字在列中显示
            delbut.Width = 66;
            int index = dgvtasklist.Columns.Count;
            dgvtasklist.Columns.Insert(index, delbut);
            dgvtasklist.Columns.Insert(index, editbut);
            dgvtasklist.Columns.Insert(index, seebut);
            //dgvtasklist.Columns.AddRange(new DataGridViewColumn[] { seebut, editbut, delbut });






        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            RefreshDateGridView(sender, e);
            AddSelectEditDelBut(sender, e);
            //
            //启动默认启动服务
            StartWinServices();
        }


        private void TopOrBottom(object sender, EventArgs e)
        {
            foreach (var item in splitContainer1.Panel1.Controls)
            {
                if (item is Button)
                {
                    Button button = item as Button;
                    if (button.Name == "butlookstatus")
                    {
                        button.Click += new EventHandler(BebbTop);
                    }
                    else
                    {
                        button.Click += new EventHandler(BebbBottom);
                    }
                }
            }
        }
        private void BebbTop(object sender, EventArgs e)
        {
            //  webBaxd.BringToFront();
            string CrystalQuartzPanelUrl = ConfigurationManager.AppSettings["CrystalQuartzPanelUrl"].ToString();
            //Service1 service1 = new Service1();
            //service1.ServiceShowTaskHandler(CrystalQuartzPanelUrl);
            //  webBaxd.Navigate("~/ CrystalQuartzPanel.axd");
        }
        private void BebbBottom(object sender, EventArgs e)
        {
            //    webBaxd.SendToBack();
        }
        public bool IsServiceExsit()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName == "EohiQuartzService")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        private void StartWinServices()
        {
            try
            {
                if (IsServiceExsit() == false)
                {
                    InsertBat();
                }
                ServiceController sc = new ServiceController("EohiQuartzService");
                if (sc != null && (sc.Status == ServiceControllerStatus.Paused || sc.Status == ServiceControllerStatus.Stopped || sc.Status == ServiceControllerStatus.StopPending))
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    while (true)
                    {
                        m++;
                         StartWinServices();
                        if (m >= 3 && sc.Status != ServiceControllerStatus.Running)
                        {
                            MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                            messageBoxTimeOut.ShowMessageBoxTimeout("Quarzt服务启动失败，可点击启动按键启动！！！");
                            butqdfw.Enabled = true;
                            buttzfw.Enabled = false;
                            break;
                        }
                    }
                }
                // StratStatus();
                butqdfw.Enabled = false;
                buttzfw.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout(ex.Message);
                butqdfw.Enabled = true;
                buttzfw.Enabled = false;
            }

        }

        private Process proc = null;
        /// <summary>
        /// 安装服务
        /// </summary>

        private void InsertBat()
        {
            try
            {
                #region 此代码会提示未加载程序集。。。。。。
                //string serviceFilePath = Environment.CurrentDirectory + "\\Install.bat";
                //using (AssemblyInstaller installer = new AssemblyInstaller())
                //{
                //    installer.UseNewContext = true;
                //    installer.Path = serviceFilePath;
                //    IDictionary savedState = new Hashtable();
                //    installer.Install(savedState);
                //    installer.Commit(savedState);
                //} 
                #endregion
                #region 测试安装
                string str = System.Windows.Forms.Application.StartupPath + "\\Install.bat";

                string strDirPath = System.IO.Path.GetDirectoryName(str);
                string strFilePath = System.IO.Path.GetFileName(str);

                string targetDir = string.Format(strDirPath);//this is where mybatch.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = strFilePath;

                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();

                //proc.WaitForExit();
                #endregion
                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout("执行成功！！！");
                butqdfw.Enabled = false;
                buttzfw.Enabled = true;

            }
            catch (Exception ex)
            {

                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout(ex.Message);
                butqdfw.Enabled = false;
                buttzfw.Enabled = true;
                throw;
            }
        }



        /// <summary>
        /// 服务启动，true启动
        /// </summary>
        /// <returns></returns>
        //    private bool StratStatus()
        //{
        //    Service1 service1 = new Service1();
        //    if (service1.ServiceRunIsTrue == false)
        //    {
        //        while (true)
        //        {
        //            m++;
        //            service1.ServiceStratHandler(null);
        //            if (service1.ServiceRunIsTrue)
        //            {
        //                butqdfw.Enabled = false;
        //                buttzfw.Enabled = true;
        //                return true;
        //            }
        //            else
        //            {
        //                if (m >= 3)
        //                {
        //                    MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
        //                    messageBoxTimeOut.ShowMessageBoxTimeout("Quarzt服务启动失败，可点击启动按键启动！！！");
        //                    butqdfw.Enabled = true;
        //                    buttzfw.Enabled = false;
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        butqdfw.Enabled = false;
        //        buttzfw.Enabled = true;
        //        return true;
        //    }
        //    return false;
        //}

        private void butqdfw_Click(object sender, EventArgs e)
        {
            StartWinServices();
            //  StratStatus();
        }

        private void buttzfw_Click(object sender, EventArgs e)
        {
            StopWebSerices();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        private void StopWebSerices()
        {
            try
            {
                butqdfw.Enabled = false;
                buttzfw.Enabled = false;
                ServiceController sc = new ServiceController("EohiQuartzService");
                if (sc != null && sc.Status != ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                    butqdfw.Enabled = true;
                    buttzfw.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout(ex.Message);
                butqdfw.Enabled = false;
                buttzfw.Enabled = true;
            }
        }

        private void butcqfw_Click(object sender, EventArgs e)
        {
            try
            {
                butcqfw.Enabled = false;
                butqdfw.Enabled = false;
                buttzfw.Enabled = false;
                StopWebSerices();
                StartWinServices();
                ServiceController sc = new ServiceController("EohiQuartzService");
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    butcqfw.Enabled = true;
                    butqdfw.Enabled = false;
                    buttzfw.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout(ex.Message);
                butcqfw.Enabled = true;
                butqdfw.Enabled = false;
                buttzfw.Enabled = true;
            }

            //StratStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CrystalQuartzPanelUrl = ConfigurationManager.AppSettings["CrystalQuartzPanelUrl"].ToString();
         System.Diagnostics.Process.Start(CrystalQuartzPanelUrl);
        }

        private void butrefresh_Click(object sender, EventArgs e)
        {
            RefreshDateGridView(sender, e);
        }
    }
}
