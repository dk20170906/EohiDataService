namespace EohiDataServerLicenseTool
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
            this.textBox_hardno = new System.Windows.Forms.TextBox();
            this.textBox_no = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btn_new_licenseno = new System.Windows.Forms.Button();
            this.button_create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "硬件码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "许可号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "授权开始日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "授权截止日期";
            // 
            // textBox_hardno
            // 
            this.textBox_hardno.Location = new System.Drawing.Point(105, 24);
            this.textBox_hardno.Name = "textBox_hardno";
            this.textBox_hardno.Size = new System.Drawing.Size(263, 21);
            this.textBox_hardno.TabIndex = 1;
            // 
            // textBox_no
            // 
            this.textBox_no.Location = new System.Drawing.Point(105, 55);
            this.textBox_no.Name = "textBox_no";
            this.textBox_no.Size = new System.Drawing.Size(263, 21);
            this.textBox_no.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(105, 94);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(263, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(105, 121);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(263, 21);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // btn_new_licenseno
            // 
            this.btn_new_licenseno.Location = new System.Drawing.Point(374, 53);
            this.btn_new_licenseno.Name = "btn_new_licenseno";
            this.btn_new_licenseno.Size = new System.Drawing.Size(91, 23);
            this.btn_new_licenseno.TabIndex = 3;
            this.btn_new_licenseno.Text = "创建许可号";
            this.btn_new_licenseno.UseVisualStyleBackColor = true;
            this.btn_new_licenseno.Click += new System.EventHandler(this.btn_new_licenseno_Click);
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(105, 180);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(164, 37);
            this.button_create.TabIndex = 3;
            this.button_create.Text = "生成许可文件";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 251);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.btn_new_licenseno);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox_no);
            this.Controls.Add(this.textBox_hardno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "许可证生成器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_hardno;
        private System.Windows.Forms.TextBox textBox_no;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btn_new_licenseno;
        private System.Windows.Forms.Button button_create;
    }
}

