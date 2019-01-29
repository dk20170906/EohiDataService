using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiData.Lib
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            lblTime.Text = "按 ESC键 关闭等待。";
            base.OnLoad(e);
        }

        private int _kzq = 0;
        /// <summary>
        /// 如果此值不等于0 那么不会自动关闭
        /// </summary>
        public int __Kzq { get { return _kzq; } set { _kzq = value; } }

        public String SetText
        {
            get { return lblShow.Text; }
            set { lblShow.Text = value; }
        }

        private void FrmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                this.Hide();
            }
        }
    }
}
