namespace CT.ERP.Client
{
    partial class frmEditDelivery
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditDelivery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.description1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.customer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.noteid = new System.Windows.Forms.TextBox();
            this.superGrid = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.description = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.batch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.deliverdate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.deliverid = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.goodname = new System.Windows.Forms.ComboBox();
            this.model = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnCopy = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.description1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.customer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.noteid);
            this.groupBox1.Controls.Add(this.superGrid);
            this.groupBox1.Controls.Add(this.description);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.batch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.deliverdate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.deliverid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.goodname);
            this.groupBox1.Controls.Add(this.model);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 481);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // description1
            // 
            this.description1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.description1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.description1.Location = new System.Drawing.Point(108, 200);
            this.description1.MaxLength = 200;
            this.description1.Name = "description1";
            this.description1.Size = new System.Drawing.Size(649, 23);
            this.description1.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(49, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 14);
            this.label9.TabIndex = 33;
            this.label9.Text = "备注：";
            // 
            // customer
            // 
            this.customer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.customer.FormattingEnabled = true;
            this.customer.Location = new System.Drawing.Point(108, 92);
            this.customer.MaxLength = 100;
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(227, 22);
            this.customer.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(325, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 29);
            this.label5.TabIndex = 31;
            this.label5.Text = "送   货   单";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CT.ERP.Client.Properties.Resources.ctlogo;
            this.pictureBox1.Location = new System.Drawing.Point(119, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // noteid
            // 
            this.noteid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.noteid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noteid.Location = new System.Drawing.Point(22, 20);
            this.noteid.Name = "noteid";
            this.noteid.Size = new System.Drawing.Size(57, 23);
            this.noteid.TabIndex = 29;
            this.noteid.Visible = false;
            // 
            // superGrid
            // 
            this.superGrid.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGrid.Location = new System.Drawing.Point(8, 229);
            this.superGrid.Name = "superGrid";
            this.superGrid.PrimaryGrid.AllowRowInsert = true;
            this.superGrid.PrimaryGrid.ColumnHeader.RowHeight = 30;
            this.superGrid.PrimaryGrid.ColumnHeaderClickBehavior = DevComponents.DotNetBar.SuperGrid.ColumnHeaderClickBehavior.None;
            gridColumn1.HeaderText = "件号";
            gridColumn1.Name = "jiannum";
            gridColumn1.Width = 50;
            gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridComboBoxExEditControl);
            gridColumn2.HeaderText = "规格";
            gridColumn2.Name = "specifications";
            gridColumn2.Width = 70;
            gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridIntegerInputEditControl);
            gridColumn3.HeaderText = "长度";
            gridColumn3.Name = "lenght";
            gridColumn3.Width = 70;
            gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridIntegerInputEditControl);
            gridColumn4.HeaderText = "盘数";
            gridColumn4.Name = "discnum";
            gridColumn4.Width = 70;
            gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            gridColumn5.HeaderText = "净重(KG)";
            gridColumn5.Name = "weight";
            gridColumn5.Width = 80;
            gridColumn6.DefaultNewRowCellValue = "0";
            gridColumn6.HeaderText = "单价";
            gridColumn6.Name = "price";
            gridColumn6.Width = 70;
            gridColumn7.DefaultNewRowCellValue = "0";
            gridColumn7.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            gridColumn7.HeaderText = "总价";
            gridColumn7.Name = "totalprice";
            gridColumn8.HeaderText = "合同号";
            gridColumn8.Name = "contractno";
            gridColumn8.Width = 120;
            gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            gridColumn9.HeaderText = "毛重";
            gridColumn9.Name = "netweight";
            gridColumn9.Width = 80;
            gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            gridColumn10.HeaderText = "管芯";
            gridColumn10.Name = "coreweight";
            gridColumn10.Width = 80;
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn9);
            this.superGrid.PrimaryGrid.Columns.Add(gridColumn10);
            this.superGrid.PrimaryGrid.DefaultRowHeight = 30;
            this.superGrid.PrimaryGrid.Header.RowHeight = 30;
            this.superGrid.PrimaryGrid.MultiSelect = false;
            this.superGrid.PrimaryGrid.ShowInsertRow = true;
            this.superGrid.PrimaryGrid.UseAlternateRowStyle = true;
            this.superGrid.Size = new System.Drawing.Size(815, 244);
            this.superGrid.TabIndex = 28;
            this.superGrid.CellActivated += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellActivatedEventArgs>(this.superGrid_CellActivated);
            this.superGrid.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.superGrid_CellValueChanged);
            // 
            // description
            // 
            this.description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.description.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.description.Location = new System.Drawing.Point(682, 90);
            this.description.MaxLength = 50;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(75, 23);
            this.description.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(632, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 14);
            this.label8.TabIndex = 26;
            this.label8.Text = "OEM：";
            // 
            // batch
            // 
            this.batch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.batch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.batch.Location = new System.Drawing.Point(583, 169);
            this.batch.MaxLength = 50;
            this.batch.Name = "batch";
            this.batch.Size = new System.Drawing.Size(174, 23);
            this.batch.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(492, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "出厂批号：";
            // 
            // deliverdate
            // 
            this.deliverdate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deliverdate.Location = new System.Drawing.Point(583, 130);
            this.deliverdate.Name = "deliverdate";
            this.deliverdate.Size = new System.Drawing.Size(174, 23);
            this.deliverdate.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(492, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "发货时间：";
            // 
            // deliverid
            // 
            this.deliverid.AutoSize = true;
            this.deliverid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deliverid.Location = new System.Drawing.Point(583, 93);
            this.deliverid.Name = "deliverid";
            this.deliverid.Size = new System.Drawing.Size(31, 14);
            this.deliverid.TabIndex = 21;
            this.deliverid.Text = "057";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(492, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "送货单号：";
            // 
            // goodname
            // 
            this.goodname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goodname.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.goodname.FormattingEnabled = true;
            this.goodname.Location = new System.Drawing.Point(108, 166);
            this.goodname.MaxLength = 50;
            this.goodname.Name = "goodname";
            this.goodname.Size = new System.Drawing.Size(227, 22);
            this.goodname.TabIndex = 19;
            // 
            // model
            // 
            this.model.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.model.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.model.FormattingEnabled = true;
            this.model.Location = new System.Drawing.Point(107, 130);
            this.model.MaxLength = 50;
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(228, 22);
            this.model.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(19, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "货物名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(49, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "型号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(49, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "客户：";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(746, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(652, 506);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDel.Location = new System.Drawing.Point(147, 506);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 21);
            this.btnDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDel.TabIndex = 35;
            this.btnDel.Text = "删除当前行";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopy.Location = new System.Drawing.Point(39, 506);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(89, 21);
            this.btnCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCopy.TabIndex = 36;
            this.btnCopy.Text = "复制上一行";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // frmEditDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(853, 549);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "送货单";
            this.Load += new System.EventHandler(this.frmEditDelivery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox batch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker deliverdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label deliverid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox goodname;
        private System.Windows.Forms.ComboBox model;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGrid;
        private System.Windows.Forms.TextBox noteid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox customer;
        private System.Windows.Forms.TextBox description1;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.ButtonX btnCopy;
    }
}