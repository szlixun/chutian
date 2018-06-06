using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CT.ERP.Client.BLL;
using CT.ERP.Client.Util;
using DevComponents.DotNetBar;

namespace CT.ERP.Client
{
    public partial class frmChangePass : Office2007Form
    {
        public frmChangePass()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string oldpass = txtOldPass.Text.Trim();
            string newpass = txtNewPass.Text.Trim();
            string newpass1 = txtNewPass1.Text.Trim();

            if (oldpass.Length == 0)
            {
                txtOldPass.Focus();
                MessageBox.Show("请输入旧密码");
                return;
            }

            if (newpass.Length == 0)
            {
                txtNewPass.Focus();
                MessageBox.Show("请输入新密码");
                return;
            }

            if (newpass1.Length == 0)
            {
                txtNewPass1.Focus();
                MessageBox.Show("请输入确认密码");
                return;
            }

            if (newpass != newpass1)
            {
                MessageBox.Show("两次输入密码不一致");
                return;
            }

            try
            {
                UserDAC dac = new UserDAC();
                dac.ChangePwd(Global.LoginUser.userid, newpass);
            }
            catch (Exception ex)
            {
                MessageBox.Show("更改失败!");
                return;
            }

            this.Close();
        }

    }
}
