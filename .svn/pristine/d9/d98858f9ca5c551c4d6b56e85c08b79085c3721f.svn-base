using Common.Files.FileSystem;
using Common.Util;
using Common.WebDataHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EohiDataCenter
{
    public partial class Form_Project_Docs_UpLoad : Form
    {
        public string md5 = "";
        public string key = "";
        public string Uploadfilename = "";

        public Form_Project_Docs_UpLoad()
        {
            InitializeComponent();
            
        }

        private void Form_Project_Docs_UpLoad_Load(object sender, EventArgs e)
        {
            label_filename.Text = Uploadfilename;
            IFileUpload upload = new IFileUpload(LoginInfo.GetLoginId());
            upload.EventFileUploadProgress += web_UploadProgressChanged;

            try
            {
                upload.Upload(Uploadfilename, md5, key);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void button_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        bool btimesadd = false;
        void  web_UploadProgressChanged(System.Net.UploadProgressChangedEventArgs progress)
        {
            progressBarControl1.EditValue = progress.ProgressPercentage;
            if (progress.ProgressPercentage >= 100)
            {
                if (!btimesadd)
                {
                    this.DialogResult = DialogResult.OK;
                    btimesadd = true;
                    this.Close();
                }
            }
                

        }

    }
}
