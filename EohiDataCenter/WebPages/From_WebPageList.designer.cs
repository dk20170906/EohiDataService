namespace EohiDataCenter
{
    partial class From_WebPageList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(From_WebPageList));
            this.gridControl_report = new DevExpress.XtraGrid.GridControl();
            this.gridView_report = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_keyword = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_New = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Exit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_report)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_report)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_keyword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl_report
            // 
            this.gridControl_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_report.Location = new System.Drawing.Point(0, 80);
            this.gridControl_report.MainView = this.gridView_report;
            this.gridControl_report.Name = "gridControl_report";
            this.gridControl_report.Size = new System.Drawing.Size(953, 370);
            this.gridControl_report.TabIndex = 21;
            this.gridControl_report.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_report});
            // 
            // gridView_report
            // 
            this.gridView_report.Appearance.Row.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_report.Appearance.Row.Options.UseFont = true;
            this.gridView_report.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7});
            this.gridView_report.GridControl = this.gridControl_report;
            this.gridView_report.Name = "gridView_report";
            this.gridView_report.OptionsBehavior.Editable = false;
            this.gridView_report.OptionsView.ColumnAutoWidth = false;
            this.gridView_report.OptionsView.ShowFooter = true;
            this.gridView_report.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "编号";
            this.gridColumn1.FieldName = "id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 64;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "webappname";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "datasourcename", "合计:{0}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 201;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "备注";
            this.gridColumn7.FieldName = "webappnote";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 412;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.textEdit_keyword);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 55);
            this.panel1.TabIndex = 23;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(341, 5);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "查询";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // textEdit_keyword
            // 
            this.textEdit_keyword.Location = new System.Drawing.Point(107, 6);
            this.textEdit_keyword.Name = "textEdit_keyword";
            this.textEdit_keyword.Size = new System.Drawing.Size(217, 20);
            this.textEdit_keyword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "关键词搜索";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 18);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Refresh,
            this.toolStripSeparator1,
            this.toolStripButton_New,
            this.toolStripButton_Edit,
            this.toolStripButton_Delete,
            this.toolStripSeparator2,
            this.toolStripButton_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(953, 25);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Refresh
            // 
            this.toolStripButton_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Refresh.Image")));
            this.toolStripButton_Refresh.Name = "toolStripButton_Refresh";
            this.toolStripButton_Refresh.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Refresh.Text = "刷新";
            this.toolStripButton_Refresh.ToolTipText = "项目管理";
            this.toolStripButton_Refresh.Click += new System.EventHandler(this.toolStripButton_Refresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_New
            // 
            this.toolStripButton_New.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_New.Image")));
            this.toolStripButton_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_New.Name = "toolStripButton_New";
            this.toolStripButton_New.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_New.Text = "添加";
            this.toolStripButton_New.ToolTipText = "添加";
            this.toolStripButton_New.Click += new System.EventHandler(this.toolStripButton_New_Click);
            // 
            // toolStripButton_Edit
            // 
            this.toolStripButton_Edit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Edit.Image")));
            this.toolStripButton_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Edit.Name = "toolStripButton_Edit";
            this.toolStripButton_Edit.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Edit.Text = "修改";
            this.toolStripButton_Edit.Click += new System.EventHandler(this.toolStripButton_Edit_Click);
            // 
            // toolStripButton_Delete
            // 
            this.toolStripButton_Delete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Delete.Image")));
            this.toolStripButton_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Delete.Name = "toolStripButton_Delete";
            this.toolStripButton_Delete.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Delete.Text = "删除";
            this.toolStripButton_Delete.Click += new System.EventHandler(this.toolStripButton_Delete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Exit
            // 
            this.toolStripButton_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Exit.Image")));
            this.toolStripButton_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Exit.Name = "toolStripButton_Exit";
            this.toolStripButton_Exit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Exit.Text = "关闭";
            this.toolStripButton_Exit.Click += new System.EventHandler(this.toolStripButton_Exit_Click);
            // 
            // From_WebPageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 450);
            this.Controls.Add(this.gridControl_report);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "From_WebPageList";
            this.Text = "页面设置";
            this.Load += new System.EventHandler(this.ReportList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_report)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_report)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_keyword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl_report;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_report;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_search;
        private DevExpress.XtraEditors.TextEdit textEdit_keyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Refresh;
        private System.Windows.Forms.ToolStripButton toolStripButton_New;
        private System.Windows.Forms.ToolStripButton toolStripButton_Edit;
        private System.Windows.Forms.ToolStripButton toolStripButton_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Exit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}