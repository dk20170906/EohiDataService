namespace EohiDataCenter
{
    partial class Form_LinkDlg
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
            this.textEdit_id = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_linkname = new DevExpress.XtraEditors.TextEdit();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.lb_splitline = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit_linkstring = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_linktype = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_linkname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_linkstring.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_linktype.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit_id
            // 
            this.textEdit_id.Location = new System.Drawing.Point(106, 23);
            this.textEdit_id.Name = "textEdit_id";
            this.textEdit_id.Properties.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this.textEdit_id.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit_id.Properties.ReadOnly = true;
            this.textEdit_id.Size = new System.Drawing.Size(267, 20);
            this.textEdit_id.TabIndex = 5;
            // 
            // textEdit_linkname
            // 
            this.textEdit_linkname.Location = new System.Drawing.Point(106, 58);
            this.textEdit_linkname.Name = "textEdit_linkname";
            this.textEdit_linkname.Size = new System.Drawing.Size(269, 20);
            this.textEdit_linkname.TabIndex = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(295, 369);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(91, 31);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lb_splitline
            // 
            this.lb_splitline.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_splitline.LineColor = System.Drawing.Color.DarkGray;
            this.lb_splitline.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.lb_splitline.LineVisible = true;
            this.lb_splitline.Location = new System.Drawing.Point(18, 353);
            this.lb_splitline.Name = "lb_splitline";
            this.lb_splitline.Size = new System.Drawing.Size(481, 10);
            this.lb_splitline.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(34, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "连接类型：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(34, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "连接名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "连接编号：";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(193, 369);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(91, 31);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // memoEdit_linkstring
            // 
            this.memoEdit_linkstring.Location = new System.Drawing.Point(106, 129);
            this.memoEdit_linkstring.Name = "memoEdit_linkstring";
            this.memoEdit_linkstring.Size = new System.Drawing.Size(397, 146);
            this.memoEdit_linkstring.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(22, 130);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "连接字符串：";
            // 
            // textEdit_linktype
            // 
            this.textEdit_linktype.EditValue = "SQL Server";
            this.textEdit_linktype.Location = new System.Drawing.Point(106, 93);
            this.textEdit_linktype.Name = "textEdit_linktype";
            this.textEdit_linktype.Properties.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this.textEdit_linktype.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit_linktype.Properties.ReadOnly = true;
            this.textEdit_linktype.Size = new System.Drawing.Size(267, 20);
            this.textEdit_linktype.TabIndex = 5;
            // 
            // Form_LinkDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 431);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lb_splitline);
            this.Controls.Add(this.memoEdit_linkstring);
            this.Controls.Add(this.textEdit_linkname);
            this.Controls.Add(this.textEdit_linktype);
            this.Controls.Add(this.textEdit_id);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_LinkDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模型";
            this.Load += new System.EventHandler(this.Form_Project_Dlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_linkname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_linkstring.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_linktype.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit_id;
        private DevExpress.XtraEditors.TextEdit textEdit_linkname;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.LabelControl lb_splitline;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit_linkstring;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit textEdit_linktype;
    }
}