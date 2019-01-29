namespace EohiDataCenter
{
    partial class FormRemotingConf
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
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.lb_splitline = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit_apipars = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_apipars.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(290, 519);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(91, 31);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lb_splitline
            // 
            this.lb_splitline.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_splitline.LineColor = System.Drawing.Color.DarkGray;
            this.lb_splitline.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.lb_splitline.LineVisible = true;
            this.lb_splitline.Location = new System.Drawing.Point(-11, 503);
            this.lb_splitline.Name = "lb_splitline";
            this.lb_splitline.Size = new System.Drawing.Size(900, 10);
            this.lb_splitline.TabIndex = 8;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(188, 519);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(91, 31);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // memoEdit_apipars
            // 
            this.memoEdit_apipars.Location = new System.Drawing.Point(135, 28);
            this.memoEdit_apipars.Name = "memoEdit_apipars";
            this.memoEdit_apipars.Size = new System.Drawing.Size(497, 82);
            this.memoEdit_apipars.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(19, 30);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(110, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "RemotingSQL地址：";
            // 
            // FormRemotingConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 560);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lb_splitline);
            this.Controls.Add(this.memoEdit_apipars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRemotingConf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remoting设置";
            this.Load += new System.EventHandler(this.Form_Project_Dlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_apipars.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.LabelControl lb_splitline;
        private DevExpress.XtraEditors.MemoEdit memoEdit_apipars;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}