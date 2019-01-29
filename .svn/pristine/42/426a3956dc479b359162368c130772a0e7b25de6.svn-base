using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiNFFunctionPublic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button_funpub_Click(object sender, EventArgs e)
        {
            //获取连接信息;
            string connstring = this.textBox_sql.Text;

            if (connstring.Trim().Length <= 0)
            {
                MessageBox.Show("请输入服务器连接地址(SQL)");
                return;
            }
            if (this.textBox_funid.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请输入功能编号");
                return;
            }
            if (this.textBox_funname.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请输入功能名称");
                return;
            }
            if (this.textBox_xml.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择功能设计文件(.xml)");
                return;
            }

            //解析sql;
            //获取数据;
            string sql = " exec pr_app_funRelease @fun_designid,@fun_designname,@fun_designtype,@fun_designxml,@cre_man";

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@fun_designid",this.textBox_funid.Text),
                new SqlParameter("@fun_designname",this.textBox_funname.Text),
                new SqlParameter("@fun_designtype",this.textBox_funtype.Text),
                new SqlParameter("@fun_designxml",this.textBox_xml.Text),
                new SqlParameter("@cre_man",""),
            };

            ExecuteNonQuery(connstring, sql, pars);

            //执行;
            MessageBox.Show("功能发布完成！");
        }

        private void button_loadfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "XML文件|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                if (File.Exists(fName))
                {
                    TextReader txtReader = new StreamReader(fName);
                    string layoutString = txtReader.ReadToEnd();
                    this.textBox_xml.Text = layoutString;
                }
            }
        
            
          
        }

        private  SqlConnection getSqlConnection(string connString)
        {
            SqlConnection sqlConn = null;
            try
            {
                sqlConn = new SqlConnection(connString);
                sqlConn.Open();
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            return sqlConn;
        }
            /// <summary>
        /// 执行语句，返回受影响的行数 ，select 语句不返回受影响行数
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public  int ExecuteNonQuery(string connString, string sql, SqlParameter[] pars)
        {
            int rows = -1;
            SqlConnection myConn = getSqlConnection(connString);// new SqlConnection(connString); 
            if (myConn == null)
                return -1;

            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, myConn);
                if (pars != null)
                    sqlCmd.Parameters.AddRange(pars);
                sqlCmd.CommandType = CommandType.Text;

                rows = sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
                myConn.Close();
                myConn.Dispose();
            }
            return rows;
        }


        
    }
}
