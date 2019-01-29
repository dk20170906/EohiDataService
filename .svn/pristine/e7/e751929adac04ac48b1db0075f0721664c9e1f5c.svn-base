using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EohiDataCenter
{
    public partial class Form_Main : DevExpress.XtraEditors.XtraForm
    {
        public Form_Main()
        {
            InitializeComponent();
        }

       
        private void Form_Main_Load(object sender, EventArgs e)
        {

        }
        #region 菜单
        private void toolStripMenuItem_conn_Click(object sender, EventArgs e)
        {
            if (RemotingConfig.SetConf())
            {
                RemotingConfig.ReadConf();
            }
        }

        private void toolStripMenuItem_exit_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_about_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// 检查页面是否已经存在，如已经存在，则激活。
        /// </summary>
        /// <param name="pagename"></param>
        private bool ActivePage(string pagename)
        {
            bool bActive = false;
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == pagename)
                {
                    this.MdiChildren[i].Activate();
                    bActive = true;
                    break;
                }
            }

            return bActive;
        }


        private void navBarItem_project_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (ActivePage("项目定义"))
            //    return;

            //From_ProjectList frm = new From_ProjectList();
            //frm.Text = "项目定义";
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void navBarItem_scene_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("场景定义"))
                return;

         
        }

        private void navBarItem_scene_design_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("场景设计"))
                return;

        }

        private void navBarItem_model_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
        }

        private void navBarItem_modeltoproject_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("模型分配"))
                return;


        }

        private void navBarItem_links_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("连接定义"))
                return;

            From_LinkList frm = new From_LinkList();
            frm.Text = "连接定义";
            frm.MdiParent = this;
            frm.Show();
        }
        //api定义
        private void navBarItem_api_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("API 定义"))
                return;

            From_ApiList frm = new From_ApiList();
            frm.Text = "API 定义";
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItem_apiopen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("API 服务"))
                return;

            From_ApiServer frm = new From_ApiServer();
            frm.Text = "API 服务";
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItem_webapp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("WebApp定义"))
                return;

            From_WebPageList frm = new From_WebPageList();
            frm.Text = "WebApp定义";
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItem_websocketscript_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ActivePage("WebSocket处理脚本"))
                return;

            WebSocketScriptEditForm frm = new WebSocketScriptEditForm();
            frm.Text = "WebSocket处理脚本";
            frm.MdiParent = this;
            frm.Show();

        }
       
    }
}
