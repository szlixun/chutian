using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CT.ERP.Client.Util;
using DevComponents.DotNetBar;

namespace CT.ERP.Client
{
    public partial class frmMain : Office2007Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void imgDelivery_Click(object sender, EventArgs e)
        {
            if (!Global.LoginUser.dodelivery)
            {
                MessageBox.Show("你没有权限操作此模块");
                return;
            }
            frmMainDelivery frm = new frmMainDelivery();
            frm.ShowDialog();
        }

        private void imgTracking_Click(object sender, EventArgs e)
        {
            if (!Global.LoginUser.dotracking)
            {
                MessageBox.Show("你没有权限操作此模块");
                return;
            }

            FrmMainTracking frm = new FrmMainTracking();
            frm.ShowDialog();
        }

        private void imgUserManage_Click(object sender, EventArgs e)
        {
            if (!Global.LoginUser.dousermanage)
            {
                MessageBox.Show("你没有权限操作此模块");
                return;
            }

            frmUserManager frm = new frmUserManager();
            frm.ShowDialog();
        }

        private void imgChangePass_Click(object sender, EventArgs e)
        {
            frmChangePass frm = new frmChangePass();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!Global.LoginUser.dodelivery)
            {
                imgDelivery.Enabled = false;
            }

            if (!Global.LoginUser.dotracking)
            {
                imgTracking.Enabled = false;
            }

            if (!Global.LoginUser.dousermanage)
            {
                imgUserManage.Enabled = false;
            }
        }

    }
}
