namespace CT.ERP.Client
{
    partial class frmUserManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserManager));
            this.lvUser = new System.Windows.Forms.ListView();
            this.coluserid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colloginid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colusername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coldodelivery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coldotracking = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coldousermanage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userid = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dousermanage = new System.Windows.Forms.CheckBox();
            this.dotracking = new System.Windows.Forms.CheckBox();
            this.dodelivery = new System.Windows.Forms.CheckBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loginid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvUser
            // 
            this.lvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.coluserid,
            this.colloginid,
            this.colusername,
            this.coldodelivery,
            this.coldotracking,
            this.coldousermanage});
            this.lvUser.FullRowSelect = true;
            this.lvUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUser.HideSelection = false;
            this.lvUser.Location = new System.Drawing.Point(12, 12);
            this.lvUser.MultiSelect = false;
            this.lvUser.Name = "lvUser";
            this.lvUser.Size = new System.Drawing.Size(461, 209);
            this.lvUser.TabIndex = 0;
            this.lvUser.UseCompatibleStateImageBehavior = false;
            this.lvUser.View = System.Windows.Forms.View.Details;
            this.lvUser.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvUser_ItemSelectionChanged);
            // 
            // coluserid
            // 
            this.coluserid.Text = "";
            this.coluserid.Width = 0;
            // 
            // colloginid
            // 
            this.colloginid.Text = "登陆名称";
            this.colloginid.Width = 80;
            // 
            // colusername
            // 
            this.colusername.Text = "真实姓名";
            this.colusername.Width = 80;
            // 
            // coldodelivery
            // 
            this.coldodelivery.Text = "送货单权限";
            this.coldodelivery.Width = 80;
            // 
            // coldotracking
            // 
            this.coldotracking.Text = "品质跟踪权限";
            this.coldotracking.Width = 100;
            // 
            // coldousermanage
            // 
            this.coldousermanage.Text = "用户管理权限";
            this.coldousermanage.Width = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userid);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dousermanage);
            this.groupBox1.Controls.Add(this.dotracking);
            this.groupBox1.Controls.Add(this.dodelivery);
            this.groupBox1.Controls.Add(this.username);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.loginid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户信息";
            // 
            // userid
            // 
            this.userid.Location = new System.Drawing.Point(224, 55);
            this.userid.MaxLength = 30;
            this.userid.Name = "userid";
            this.userid.PasswordChar = '*';
            this.userid.Size = new System.Drawing.Size(47, 21);
            this.userid.TabIndex = 9;
            this.userid.Visible = false;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(87, 55);
            this.password.MaxLength = 30;
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(100, 21);
            this.password.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码：";
            // 
            // dousermanage
            // 
            this.dousermanage.AutoSize = true;
            this.dousermanage.Location = new System.Drawing.Point(304, 86);
            this.dousermanage.Name = "dousermanage";
            this.dousermanage.Size = new System.Drawing.Size(96, 16);
            this.dousermanage.TabIndex = 6;
            this.dousermanage.Text = "用户管理权限";
            this.dousermanage.UseVisualStyleBackColor = true;
            // 
            // dotracking
            // 
            this.dotracking.AutoSize = true;
            this.dotracking.Location = new System.Drawing.Point(189, 86);
            this.dotracking.Name = "dotracking";
            this.dotracking.Size = new System.Drawing.Size(96, 16);
            this.dotracking.TabIndex = 5;
            this.dotracking.Text = "品质跟踪权限";
            this.dotracking.UseVisualStyleBackColor = true;
            // 
            // dodelivery
            // 
            this.dodelivery.AutoSize = true;
            this.dodelivery.Location = new System.Drawing.Point(87, 86);
            this.dodelivery.Name = "dodelivery";
            this.dodelivery.Size = new System.Drawing.Size(84, 16);
            this.dodelivery.TabIndex = 4;
            this.dodelivery.Text = "送货单权限";
            this.dodelivery.UseVisualStyleBackColor = true;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(330, 27);
            this.username.MaxLength = 30;
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 21);
            this.username.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "真实姓名：";
            // 
            // loginid
            // 
            this.loginid.Location = new System.Drawing.Point(87, 27);
            this.loginid.MaxLength = 30;
            this.loginid.Name = "loginid";
            this.loginid.Size = new System.Drawing.Size(100, 21);
            this.loginid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "登陆名称：";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589121;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(18, 364);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 29);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "   新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455674371;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(113, 364);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 29);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "   保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589127;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(342, 364);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(117, 29);
            this.btnRemove.TabIndex = 33;
            this.btnRemove.Text = "  删除选中用户";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmUserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 401);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvUser);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.frmUserManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvUser;
        private System.Windows.Forms.ColumnHeader coluserid;
        private System.Windows.Forms.ColumnHeader colloginid;
        private System.Windows.Forms.ColumnHeader colusername;
        private System.Windows.Forms.ColumnHeader coldodelivery;
        private System.Windows.Forms.ColumnHeader coldotracking;
        private System.Windows.Forms.ColumnHeader coldousermanage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox dousermanage;
        private System.Windows.Forms.CheckBox dotracking;
        private System.Windows.Forms.CheckBox dodelivery;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loginid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox userid;
    }
}