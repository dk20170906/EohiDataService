namespace EohiNFFunctionPublic
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_funpub = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_sql = new System.Windows.Forms.TextBox();
            this.textBox_funid = new System.Windows.Forms.TextBox();
            this.textBox_funname = new System.Windows.Forms.TextBox();
            this.textBox_funtype = new System.Windows.Forms.TextBox();
            this.textBox_xml = new System.Windows.Forms.TextBox();
            this.button_loadfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "功能设计号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "功能名称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "类型:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "内容:";
            // 
            // button_funpub
            // 
            this.button_funpub.Location = new System.Drawing.Point(467, 110);
            this.button_funpub.Name = "button_funpub";
            this.button_funpub.Size = new System.Drawing.Size(75, 49);
            this.button_funpub.TabIndex = 3;
            this.button_funpub.Text = "功能发布";
            this.button_funpub.UseVisualStyleBackColor = true;
            this.button_funpub.Click += new System.EventHandler(this.button_funpub_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "目标服务器:";
            // 
            // textBox_sql
            // 
            this.textBox_sql.Location = new System.Drawing.Point(89, 17);
            this.textBox_sql.Name = "textBox_sql";
            this.textBox_sql.Size = new System.Drawing.Size(418, 21);
            this.textBox_sql.TabIndex = 4;
            this.textBox_sql.Text = "server=192.168.15.90,1433;uid=sa;pwd=Smk2016/;database=v6smkmes;";
            // 
            // textBox_funid
            // 
            this.textBox_funid.Location = new System.Drawing.Point(89, 48);
            this.textBox_funid.Name = "textBox_funid";
            this.textBox_funid.Size = new System.Drawing.Size(308, 21);
            this.textBox_funid.TabIndex = 4;
            // 
            // textBox_funname
            // 
            this.textBox_funname.Location = new System.Drawing.Point(89, 75);
            this.textBox_funname.Name = "textBox_funname";
            this.textBox_funname.Size = new System.Drawing.Size(308, 21);
            this.textBox_funname.TabIndex = 4;
            // 
            // textBox_funtype
            // 
            this.textBox_funtype.Enabled = false;
            this.textBox_funtype.Location = new System.Drawing.Point(89, 111);
            this.textBox_funtype.Name = "textBox_funtype";
            this.textBox_funtype.Size = new System.Drawing.Size(308, 21);
            this.textBox_funtype.TabIndex = 4;
            this.textBox_funtype.Text = "NFLayout";
            // 
            // textBox_xml
            // 
            this.textBox_xml.Location = new System.Drawing.Point(89, 174);
            this.textBox_xml.Multiline = true;
            this.textBox_xml.Name = "textBox_xml";
            this.textBox_xml.Size = new System.Drawing.Size(308, 236);
            this.textBox_xml.TabIndex = 4;
            // 
            // button_loadfile
            // 
            this.button_loadfile.Location = new System.Drawing.Point(89, 147);
            this.button_loadfile.Name = "button_loadfile";
            this.button_loadfile.Size = new System.Drawing.Size(75, 21);
            this.button_loadfile.TabIndex = 3;
            this.button_loadfile.Text = "加载文件";
            this.button_loadfile.UseVisualStyleBackColor = true;
            this.button_loadfile.Click += new System.EventHandler(this.button_loadfile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 417);
            this.Controls.Add(this.textBox_xml);
            this.Controls.Add(this.textBox_funtype);
            this.Controls.Add(this.textBox_funname);
            this.Controls.Add(this.textBox_funid);
            this.Controls.Add(this.textBox_sql);
            this.Controls.Add(this.button_loadfile);
            this.Controls.Add(this.button_funpub);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "加载文件";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_funpub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_sql;
        private System.Windows.Forms.TextBox textBox_funid;
        private System.Windows.Forms.TextBox textBox_funname;
        private System.Windows.Forms.TextBox textBox_funtype;
        private System.Windows.Forms.TextBox textBox_xml;
        private System.Windows.Forms.Button button_loadfile;
    }
}

