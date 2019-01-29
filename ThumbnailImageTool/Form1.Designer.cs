namespace ThumbnailImageTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_dirc = new System.Windows.Forms.TextBox();
            this.button_change_dir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_logs = new System.Windows.Forms.TextBox();
            this.button_start_watch = new System.Windows.Forms.Button();
            this.button_init_image = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件夹";
            // 
            // textBox_dirc
            // 
            this.textBox_dirc.Location = new System.Drawing.Point(66, 20);
            this.textBox_dirc.Name = "textBox_dirc";
            this.textBox_dirc.ReadOnly = true;
            this.textBox_dirc.Size = new System.Drawing.Size(326, 21);
            this.textBox_dirc.TabIndex = 1;
            this.textBox_dirc.Text = "C:\\upload";
            // 
            // button_change_dir
            // 
            this.button_change_dir.Location = new System.Drawing.Point(399, 19);
            this.button_change_dir.Name = "button_change_dir";
            this.button_change_dir.Size = new System.Drawing.Size(68, 23);
            this.button_change_dir.TabIndex = 2;
            this.button_change_dir.Text = "更改目录";
            this.button_change_dir.UseVisualStyleBackColor = true;
            this.button_change_dir.Click += new System.EventHandler(this.button_change_dir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "缩率图生成日志:";
            // 
            // textBox_logs
            // 
            this.textBox_logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_logs.Location = new System.Drawing.Point(0, 81);
            this.textBox_logs.Multiline = true;
            this.textBox_logs.Name = "textBox_logs";
            this.textBox_logs.ReadOnly = true;
            this.textBox_logs.Size = new System.Drawing.Size(800, 353);
            this.textBox_logs.TabIndex = 3;
            // 
            // button_start_watch
            // 
            this.button_start_watch.Location = new System.Drawing.Point(482, 19);
            this.button_start_watch.Name = "button_start_watch";
            this.button_start_watch.Size = new System.Drawing.Size(115, 23);
            this.button_start_watch.TabIndex = 2;
            this.button_start_watch.Text = "启动文件夹监控";
            this.button_start_watch.UseVisualStyleBackColor = true;
            this.button_start_watch.Click += new System.EventHandler(this.button_start_watch_Click);
            // 
            // button_init_image
            // 
            this.button_init_image.Location = new System.Drawing.Point(643, 19);
            this.button_init_image.Name = "button_init_image";
            this.button_init_image.Size = new System.Drawing.Size(115, 23);
            this.button_init_image.TabIndex = 2;
            this.button_init_image.Text = "初始化文件夹缩略图";
            this.button_init_image.UseVisualStyleBackColor = true;
            this.button_init_image.Click += new System.EventHandler(this.button_init_image_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_dirc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_init_image);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_start_watch);
            this.panel1.Controls.Add(this.button_change_dir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 81);
            this.panel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 434);
            this.Controls.Add(this.textBox_logs);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "缩略图工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_dirc;
        private System.Windows.Forms.Button button_change_dir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_logs;
        private System.Windows.Forms.Button button_start_watch;
        private System.Windows.Forms.Button button_init_image;
        private System.Windows.Forms.Panel panel1;
    }
}

