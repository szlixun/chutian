namespace CT.ERP.Client
{
    partial class frmServerSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serverIp = new DevComponents.Editors.IpAddressInput();
            this.label2 = new System.Windows.Forms.Label();
            this.serverUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serverPass = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverIp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverPass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.serverUser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.serverIp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // serverIp
            // 
            this.serverIp.AutoOverwrite = true;
            // 
            // 
            // 
            this.serverIp.BackgroundStyle.Class = "DateTimeInputBackground";
            this.serverIp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.serverIp.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.serverIp.ButtonFreeText.Visible = true;
            this.serverIp.Location = new System.Drawing.Point(76, 32);
            this.serverIp.Name = "serverIp";
            this.serverIp.Size = new System.Drawing.Size(140, 21);
            this.serverIp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.serverIp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名：";
            // 
            // serverUser
            // 
            this.serverUser.Location = new System.Drawing.Point(76, 63);
            this.serverUser.Name = "serverUser";
            this.serverUser.Size = new System.Drawing.Size(140, 21);
            this.serverUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // serverPass
            // 
            this.serverPass.Location = new System.Drawing.Point(76, 96);
            this.serverPass.Name = "serverPass";
            this.serverPass.PasswordChar = '*';
            this.serverPass.Size = new System.Drawing.Size(140, 21);
            this.serverPass.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(88, 186);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmServerSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 221);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmServerSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverIp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox serverPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverUser;
        private System.Windows.Forms.Label label2;
        private DevComponents.Editors.IpAddressInput serverIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
    }
}