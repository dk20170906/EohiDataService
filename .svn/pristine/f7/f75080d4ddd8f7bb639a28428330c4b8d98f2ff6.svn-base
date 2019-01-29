using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiDataServerLicenseTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = DateTime.Now.AddYears(1);

        }
        private void btn_new_licenseno_Click(object sender, EventArgs e)
        {
            this.textBox_no.Text = Guid.NewGuid().ToString().ToUpper();
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            //判断授权是否正确;
            string hardcode = this.textBox_hardno.Text.Trim().ToUpper();
            //hardcode = EohiDataServer.Util.MD5.Get(hardcode).ToUpper();

            string licenseno = this.textBox_no.Text.Trim().ToUpper();

            DateTime licensedatestart = this.dateTimePicker1.Value;
            DateTime licensedateend = this.dateTimePicker2.Value;

            //硬件码一致
            string checkkeytmp = "";// = Computer.GetDiskID();
            //判断加密是否正确
            //使用硬件码+许可号+授权开始日期+授权截止日期 md5
            //1. 硬件码+许可号 md5
            checkkeytmp =MD5.Get(hardcode + licenseno);
            // + 授权开始日期
            checkkeytmp = MD5.Get(checkkeytmp + licensedatestart.ToString("yyyy-MM-dd"));
            // +授权截止日期
            checkkeytmp = MD5.Get(checkkeytmp + licensedateend.ToString("yyyy-MM-dd"));



            string localFilePath = "";
            string dircPath = "";
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "许可证文件（*.lic）|*.lic";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                FileStream fs1 = new FileStream(localFilePath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                //sw.WriteLine(this.textBox3.Text.Trim() + "+" + this.textBox4.Text);//开始写入值
                sw.WriteLine("hardwarecode=" + this.textBox_hardno.Text.Trim().ToUpper());//开始写入值
                sw.WriteLine("licenseno=" + this.textBox_no.Text.Trim().ToUpper());//开始写入值
                sw.WriteLine("datestart=" + licensedatestart.ToString("yyyy-MM-dd"));//开始写入值
                sw.WriteLine("dateend=" + licensedateend.ToString("yyyy-MM-dd"));//开始写入值
                sw.WriteLine("checkkey=" + checkkeytmp);//开始写入值
                sw.Close();
                fs1.Close();


              string dir=  Path.GetDirectoryName(localFilePath); 
              

                if (MessageBox.Show("是否打开文件所在文件夹？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
                {
                    //打开文件夹
                    try
                    {
                        System.Diagnostics.Process.Start("Explorer.exe", dir);
                    }
                    catch
                    {
                       // MessageBox.Show(fileName + "无法打开！");
                    }

                }
            }
           
           
        }
    }
}
