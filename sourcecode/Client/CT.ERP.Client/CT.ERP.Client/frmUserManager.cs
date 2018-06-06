using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CT.ERP.Client.Entity;
using CT.ERP.Client.BLL;
using DevComponents.DotNetBar;

namespace CT.ERP.Client
{
    public partial class frmUserManager :Office2007Form
    {
        public frmUserManager()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            userid.Text = "";
            loginid.Text = "";
            username.Text = "";
            password.Text = "";
            dodelivery.Checked = false;
            dotracking.Checked = false;
            dousermanage.Checked = false;

        }

        private void RefreshData()
        {
            lvUser.Items.Clear();
            List<User> lstUser;
            try
            {
                UserDAC dac = new UserDAC();
                lstUser = dac.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库失败，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                return;
            }
            int i = 0;
            foreach (User user in lstUser)
            {
                //lvUser.Items.Add
                ListViewItem item = new ListViewItem(user.userid.ToString());
                item.SubItems.Add(user.loginid);
                item.SubItems.Add(user.username);
                item.SubItems.Add(user.dodelivery.ToString());
                item.SubItems.Add(user.dotracking.ToString());
                item.SubItems.Add(user.dousermanage.ToString());
                if (i == 0) item.Selected = true;                
                lvUser.Items.Add(item);
                i++;
            }

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (loginid.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登陆名");
                loginid.Focus();
                return;
            }

            if (username.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入真实姓名");
                username.Focus();
                return;
            }

            if (password.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入密码");
                password.Focus();
                return;
            }

            bool isAdd = true;
            User user = new User();
            if (userid.Text.Length > 0) 
            {
                user.userid = int.Parse(userid.Text);
                isAdd = false;
            }
            user.loginid = loginid.Text.Trim();
            user.username = username.Text.Trim();
            user.password = password.Text.Trim();
            user.dodelivery = dodelivery.Checked;
            user.dotracking = dotracking.Checked;
            user.dousermanage = dousermanage.Checked;

            UserDAC dac=new UserDAC();
            try
            {
                User dbUser = dac.Select(user.loginid,user.userid);
                if (dbUser != null)
                {
                    MessageBox.Show("登陆名已存在!");
                    return;
                }
                if (isAdd)
                {
                    dac.Add(user);
                }
                else
                    dac.Update(user);

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库失败，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                return;
            }

            RefreshData();


        }

        private void frmUserManager_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void lvUser_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvUser.SelectedItems.Count == 0)
                return;

            int userId=int.Parse(lvUser.SelectedItems[0].Text);
            User user;
            try
            {
                UserDAC dac = new UserDAC();
                user = dac.Select(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库失败，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                return;
            }
            userid.Text = user.userid.ToString();
            loginid.Text = user.loginid;
            username.Text = user.username;
            password.Text = user.password;
            dodelivery.Checked = user.dodelivery;
            dotracking.Checked = user.dotracking;
            dousermanage.Checked = user.dousermanage;


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvUser.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要删除的用户");
                return;
            }

            int userId = int.Parse(lvUser.SelectedItems[0].Text);
            try
            {
                UserDAC dac = new UserDAC();
                dac.Delete(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库失败，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                return;
            }
            RefreshData();
            MessageBox.Show("删除成功");

        }
    }
}
