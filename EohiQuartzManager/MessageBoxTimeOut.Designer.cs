namespace EohiQuartzManager
{
    partial class MessageBoxTimeOut
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
            this.labmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labmsg
            // 
            this.labmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labmsg.Location = new System.Drawing.Point(0, 0);
            this.labmsg.Name = "labmsg";
            this.labmsg.Size = new System.Drawing.Size(354, 141);
            this.labmsg.TabIndex = 0;
            this.labmsg.Text = "提示信息";
            this.labmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageBoxTimeOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 141);
            this.Controls.Add(this.labmsg);
            this.Name = "MessageBoxTimeOut";
            this.Text = "MessageBoxTimeOut";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labmsg;
    }
}