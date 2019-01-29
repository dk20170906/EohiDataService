namespace EohiQuartzManager
{
    partial class FrmTaskAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.butok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtrule = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtstatu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttaskdes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtjobparams = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtjobtype = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butcancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butok
            // 
            this.butok.Location = new System.Drawing.Point(311, 342);
            this.butok.Name = "butok";
            this.butok.Size = new System.Drawing.Size(75, 23);
            this.butok.TabIndex = 0;
            this.butok.Text = "确定";
            this.butok.UseVisualStyleBackColor = true;
            this.butok.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "调度名称";
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(148, 44);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(334, 21);
            this.txtname.TabIndex = 2;
            this.txtname.Text = "新建";
            // 
            // txtrule
            // 
            this.txtrule.Location = new System.Drawing.Point(148, 92);
            this.txtrule.Name = "txtrule";
            this.txtrule.Size = new System.Drawing.Size(334, 21);
            this.txtrule.TabIndex = 4;
            this.txtrule.Text = "/2 * * ? * *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "调度规则";
            // 
            // txtstatu
            // 
            this.txtstatu.Location = new System.Drawing.Point(148, 144);
            this.txtstatu.Name = "txtstatu";
            this.txtstatu.Size = new System.Drawing.Size(334, 21);
            this.txtstatu.TabIndex = 6;
            this.txtstatu.Text = "停止";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "任务状态";
            // 
            // txttaskdes
            // 
            this.txttaskdes.Location = new System.Drawing.Point(148, 194);
            this.txttaskdes.Name = "txttaskdes";
            this.txttaskdes.Size = new System.Drawing.Size(334, 21);
            this.txttaskdes.TabIndex = 8;
            this.txttaskdes.Text = "每5秒各钟执行一次";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "任务描述";
            // 
            // txtjobparams
            // 
            this.txtjobparams.Location = new System.Drawing.Point(148, 290);
            this.txtjobparams.Name = "txtjobparams";
            this.txtjobparams.Size = new System.Drawing.Size(334, 21);
            this.txtjobparams.TabIndex = 12;
            this.txtjobparams.Text = "http://";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Job参数";
            // 
            // txtjobtype
            // 
            this.txtjobtype.Location = new System.Drawing.Point(148, 240);
            this.txtjobtype.Name = "txtjobtype";
            this.txtjobtype.Size = new System.Drawing.Size(334, 21);
            this.txtjobtype.TabIndex = 10;
            this.txtjobtype.Text = "Http";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Job类型";
            // 
            // butcancel
            // 
            this.butcancel.Location = new System.Drawing.Point(419, 342);
            this.butcancel.Name = "butcancel";
            this.butcancel.Size = new System.Drawing.Size(75, 23);
            this.butcancel.TabIndex = 13;
            this.butcancel.Text = "取消";
            this.butcancel.UseVisualStyleBackColor = true;
            this.butcancel.Click += new System.EventHandler(this.butcancel_Click);
            // 
            // FrmTaskAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 398);
            this.Controls.Add(this.butcancel);
            this.Controls.Add(this.txtjobparams);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtjobtype);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txttaskdes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtstatu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtrule);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butok);
            this.Name = "FrmTaskAdd";
            this.Text = "任务添加";
            this.Load += new System.EventHandler(this.FrmTaskAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtrule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtstatu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttaskdes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtjobparams;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtjobtype;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butcancel;
    }
}