using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CT.ERP.Client
{
    public partial class frmServerSetting : Form
    {
        public frmServerSetting()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (serverIp.Value.Length== 0)
            {
                serverIp.Focus();
                MessageBox.Show("IP地址不能为空");
                return;
            }
            
            if (serverUser.Text.Trim().Length == 0)
            {
                serverUser.Focus();
                MessageBox.Show("用户名不能为空");
                return;
            }

            if (serverPass.Text.Trim().Length == 0)
            {
                serverPass.Focus();
                MessageBox.Show("密码不能为空");
                return;
            }

            string sConnectString="Server=" + serverIp.Value + ";Port=3306;Database=ChuTian;Uid=" + serverUser.Text.Trim() + ";Pwd=" + serverPass.Text.Trim();

            try
            {
                string sFilePath = @"C:\Program Files\ChuTianData\";
                string sDbFile="db.ini";

                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }

                using (StreamWriter sw = new StreamWriter(sFilePath + sDbFile, false, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(sConnectString);
                }

                MessageBox.Show("保存成功！请重启应用程序使设置生效");
            }
            catch(Exception ex) 
            {
                MessageBox.Show("保存失败!原因:" + ex.Message);
                return;
            }

            this.Close();
            Application.Exit();
        }
    }
}
