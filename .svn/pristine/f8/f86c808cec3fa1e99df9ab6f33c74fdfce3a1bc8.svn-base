using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiQuartzManager
{
    public partial class FrmTaskAdd : Form
    {

        public int Id { get; set; }
        public FrmTaskAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string txtnamestr = txtname.Text.Trim();
            string txtrulestr = txtrule.Text;
            string txtjobparstr = txtjobparams.Text;
            string txtjobtypestr = txtjobtype.Text;
            string txttaskdesstr = txttaskdes.Text;
            string txtstatustr = txtstatu.Text;
            string mod_man ="dddkkk";

     
                 //插入
            string sqlstr = "INSERT INTO api_quartz (quartzname,quartznote,quartzstatus,crontrigger,jobtype,jobpars,mod_man,mod_date) VALUES (@quartzname,@quartznote,@quartzstatus,@crontrigger,@jobtype,@jobpars,@mod_man,@mod_date); ";

            //修改
            if (Id > 0)
            {                                                                                                                                                                                                                                                                                                                                                                                                    
                sqlstr = "UPDATE api_quartz SET quartzname=@quartzname,quartznote=@quartznote,quartzstatus=@quartzstatus,crontrigger=@crontrigger,jobtype=@jobtype,jobpars=@jobpars,mod_man=@mod_man,mod_date=@mod_date  WHERE id = @id;";
            }
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                              new SqlParameter("@id",Id),
                         new SqlParameter("@quartzname",txtnamestr),
                              new SqlParameter("@quartznote",txttaskdesstr),
                                   new SqlParameter("@quartzstatus",txtstatustr),
                                        new SqlParameter("@crontrigger",txtrulestr),
                                             new SqlParameter("@jobtype",txtjobtypestr),
                                                  new SqlParameter("@jobpars",txtjobparstr),
                                             new SqlParameter("@mod_man",mod_man),
                                                                      new SqlParameter("@mod_date",DateTime.Now),

            };

            int mint = Common.DBHelper.SqlCmd.ExecuteNonQuery(sqlstr, sqlParameters);
           
            if (mint>0)
            {
                MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                messageBoxTimeOut.ShowMessageBoxTimeout("保存成功", "提示作息", MessageBoxButtons.OK,3000);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void FrmTaskAdd_Load(object sender, EventArgs e)
        {
            if (Id>0)
            {
                string sqlstr = "select * from api_quartz where id=@id;";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@id",Id)
                };
            DataTable dt = Common.DBHelper.SqlCmd.getDataTable(sqlstr, sqlParameters);
                if (dt!=null)
                {
                    txtname.Text = dt.Rows[0]["quartzname"].ToString();
                   txtrule.Text = dt.Rows[0]["crontrigger"].ToString();
                    txtjobparams.Text = dt.Rows[0]["jobpars"].ToString();
                    txtjobtype.Text = dt.Rows[0]["jobtype"].ToString();
                    txttaskdes.Text = dt.Rows[0]["quartznote"].ToString();
                    txtstatu.Text = dt.Rows[0]["quartzstatus"].ToString();
                }
                else
                {
                    MessageBoxTimeOut messageBoxTimeOut = new MessageBoxTimeOut();
                    messageBoxTimeOut.ShowMessageBoxTimeout("数据丢失,请重试！！！");
                }
            }
        }

        private void butcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
