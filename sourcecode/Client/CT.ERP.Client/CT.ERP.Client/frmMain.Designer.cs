namespace CT.ERP.Client
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.imgUserManage = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.imgChangePass = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.imgDelivery = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.imgTracking = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SuspendLayout();
            // 
            // imgUserManage
            // 
            // 
            // 
            // 
            this.imgUserManage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.imgUserManage.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.imgUserManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgUserManage.Image = global::CT.ERP.Client.Properties.Resources.UserManage;
            this.imgUserManage.Location = new System.Drawing.Point(336, 396);
            this.imgUserManage.Name = "imgUserManage";
            this.imgUserManage.Size = new System.Drawing.Size(207, 194);
            this.imgUserManage.TabIndex = 3;
            this.imgUserManage.Click += new System.EventHandler(this.imgUserManage_Click);
            // 
            // imgChangePass
            // 
            // 
            // 
            // 
            this.imgChangePass.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.imgChangePass.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.imgChangePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgChangePass.Image = global::CT.ERP.Client.Properties.Resources.ChangePass;
            this.imgChangePass.Location = new System.Drawing.Point(637, 235);
            this.imgChangePass.Name = "imgChangePass";
            this.imgChangePass.Size = new System.Drawing.Size(207, 194);
            this.imgChangePass.TabIndex = 2;
            this.imgChangePass.Click += new System.EventHandler(this.imgChangePass_Click);
            // 
            // imgDelivery
            // 
            // 
            // 
            // 
            this.imgDelivery.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.imgDelivery.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.imgDelivery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgDelivery.Image = global::CT.ERP.Client.Properties.Resources.Delivery;
            this.imgDelivery.Location = new System.Drawing.Point(337, 84);
            this.imgDelivery.Name = "imgDelivery";
            this.imgDelivery.Size = new System.Drawing.Size(207, 194);
            this.imgDelivery.TabIndex = 1;
            this.imgDelivery.Click += new System.EventHandler(this.imgDelivery_Click);
            // 
            // imgTracking
            // 
            // 
            // 
            // 
            this.imgTracking.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.imgTracking.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.imgTracking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgTracking.Image = global::CT.ERP.Client.Properties.Resources.Tracking;
            this.imgTracking.Location = new System.Drawing.Point(40, 227);
            this.imgTracking.Name = "imgTracking";
            this.imgTracking.Size = new System.Drawing.Size(207, 194);
            this.imgTracking.TabIndex = 0;
            this.imgTracking.Click += new System.EventHandler(this.imgTracking_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(503, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "送货单管理";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(801, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 27);
            this.label2.TabIndex = 6;
            this.label2.Text = "更改密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(503, 447);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "用户管理";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(208, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "品质跟踪管理";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(912, 590);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgUserManage);
            this.Controls.Add(this.imgChangePass);
            this.Controls.Add(this.imgDelivery);
            this.Controls.Add(this.imgTracking);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "湖北楚天通讯材料有限公司业务管理信息系统";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ReflectionImage imgTracking;
        private DevComponents.DotNetBar.Controls.ReflectionImage imgDelivery;
        private DevComponents.DotNetBar.Controls.ReflectionImage imgChangePass;
        private DevComponents.DotNetBar.Controls.ReflectionImage imgUserManage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.StyleManager styleManager1;

    }
}