using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CT.ERP.Client.Entity;
using CT.ERP.Client.BLL;
using CT.ERP.Client.Util;
using DevComponents.DotNetBar;

namespace CT.ERP.Client
{
    public partial class frmLogin : Office2007Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void InitFile()
        {
            try
            {
                if (File.Exists("config.ini"))
                {
                    checkSave.Checked = true;
                    using (StreamReader sr = new StreamReader("config.ini"))
                    {
                        txtUserName.Text = DESEncrypt.DeDes(sr.ReadLine(), Global.key, Global.iv);
                        txtPassword.Text = DESEncrypt.DeDes(sr.ReadLine(), Global.key, Global.iv);
                    }
                }
            }
            catch { }
        }


        private void SaveUserInfo2File(string sUser,string sPass)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("config.ini", false, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(DESEncrypt.EnDES(sUser, Global.key, Global.iv));
                    sw.WriteLine(DESEncrypt.EnDES(sPass, Global.key, Global.iv));
                }
            }
            catch { }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InitFile();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string sLoginId = txtUserName.Text.Trim();
            string sPass = txtPassword.Text.Trim();
            if (sLoginId.Length == 0)
            {
                txtUserName.Focus();
                MessageBox.Show("请输入用户名!");
                return;
            }

            if (sPass.Length == 0)
            {
                txtPassword.Focus();
                MessageBox.Show("请输入密码!");
                return;
            }

            User user;
            try
            {
                UserDAC dac = new UserDAC();
                user = dac.Select(sLoginId, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }

            if (user == null)
            {
                MessageBox.Show("用户不存在!");
                return;
            }

            if (user.password != sPass)
            {
                MessageBox.Show("密码不正确!");
                return;
            }

            if (checkSave.Checked)
            {
                SaveUserInfo2File(sLoginId, sPass);
            }
            else
            {
                try
                {
                    File.Delete("config.ini");
                }
                catch { }
            }

            Global.LoginUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
