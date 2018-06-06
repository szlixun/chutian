namespace CT.ERP.Client
{
    partial class frmMainDelivery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainDelivery));
            this.panelTop = new System.Windows.Forms.Panel();
            this.goodname = new System.Windows.Forms.ComboBox();
            this.btnStat = new System.Windows.Forms.Button();
            this.specifications = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.customer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelDisco = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelWeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelPrice = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridResult = new System.Windows.Forms.DataGridView();
            this.menuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopyAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).BeginInit();
            this.menuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnStat);
            this.panelTop.Controls.Add(this.label6);
            this.panelTop.Controls.Add(this.specifications);
            this.panelTop.Controls.Add(this.goodname);
            this.panelTop.Controls.Add(this.label5);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Controls.Add(this.btnExport);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.dtpEnd);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.dtpStart);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.customer);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1031, 86);
            this.panelTop.TabIndex = 0;
            // 
            // goodname
            // 
            this.goodname.FormattingEnabled = true;
            this.goodname.Location = new System.Drawing.Point(92, 45);
            this.goodname.Name = "goodname";
            this.goodname.Size = new System.Drawing.Size(160, 20);
            this.goodname.TabIndex = 15;
            // 
            // btnStat
            // 
            this.btnStat.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455697761;
            this.btnStat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStat.Location = new System.Drawing.Point(489, 42);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(75, 25);
            this.btnStat.TabIndex = 13;
            this.btnStat.Text = "   统计";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // specifications
            // 
            this.specifications.FormattingEnabled = true;
            this.specifications.Location = new System.Drawing.Point(344, 45);
            this.specifications.Name = "specifications";
            this.specifications.Size = new System.Drawing.Size(135, 20);
            this.specifications.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "货物名称：";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589121;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(940, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "   新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589091;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(748, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "   导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnSearch.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455588976;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(667, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "   查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(510, 15);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(128, 21);
            this.dtpEnd.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(487, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "到";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(344, 15);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(128, 21);
            this.dtpStart.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "送货日期从";
            // 
            // customer
            // 
            this.customer.FormattingEnabled = true;
            this.customer.Location = new System.Drawing.Point(92, 15);
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(160, 20);
            this.customer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户名称：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.StatusLabelDisco,
            this.toolStripStatusLabel2,
            this.StatusLabelLength,
            this.toolStripStatusLabel4,
            this.StatusLabelWeight,
            this.toolStripStatusLabel5,
            this.StatusLabelPrice});
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1031, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(111, 17);
            this.toolStripStatusLabel1.Text = "------结果汇总-----";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel3.Text = "总盘数：";
            // 
            // StatusLabelDisco
            // 
            this.StatusLabelDisco.Name = "StatusLabelDisco";
            this.StatusLabelDisco.Size = new System.Drawing.Size(15, 17);
            this.StatusLabelDisco.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel2.Text = "总长度：";
            // 
            // StatusLabelLength
            // 
            this.StatusLabelLength.Name = "StatusLabelLength";
            this.StatusLabelLength.Size = new System.Drawing.Size(15, 17);
            this.StatusLabelLength.Text = "0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel4.Text = "总重量：";
            // 
            // StatusLabelWeight
            // 
            this.StatusLabelWeight.Name = "StatusLabelWeight";
            this.StatusLabelWeight.Size = new System.Drawing.Size(15, 17);
            this.StatusLabelWeight.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel5.Text = "总金额：";
            // 
            // StatusLabelPrice
            // 
            this.StatusLabelPrice.Name = "StatusLabelPrice";
            this.StatusLabelPrice.Size = new System.Drawing.Size(15, 17);
            this.StatusLabelPrice.Text = "0";
            // 
            // dataGridResult
            // 
            this.dataGridResult.AllowUserToAddRows = false;
            this.dataGridResult.AllowUserToDeleteRows = false;
            this.dataGridResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LemonChiffon;
            this.dataGridResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridResult.BackgroundColor = System.Drawing.Color.White;
            this.dataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridResult.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridResult.Location = new System.Drawing.Point(0, 86);
            this.dataGridResult.Name = "dataGridResult";
            this.dataGridResult.RowHeadersWidth = 12;
            this.dataGridResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridResult.RowTemplate.Height = 23;
            this.dataGridResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridResult.Size = new System.Drawing.Size(1031, 439);
            this.dataGridResult.TabIndex = 2;
            this.dataGridResult.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridResult_CellMouseDoubleClick);
            this.dataGridResult.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridResult_CellMouseDown);
            // 
            // menuGrid
            // 
            this.menuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUpdate,
            this.menuItemDel,
            this.toolStripSeparator1,
            this.menuItemAdd,
            this.menuItemCopyAdd});
            this.menuGrid.Name = "menuGrid";
            this.menuGrid.Size = new System.Drawing.Size(125, 98);
            this.menuGrid.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuGrid_ItemClicked);
            // 
            // menuItemUpdate
            // 
            this.menuItemUpdate.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589317;
            this.menuItemUpdate.Name = "menuItemUpdate";
            this.menuItemUpdate.Size = new System.Drawing.Size(124, 22);
            this.menuItemUpdate.Text = "修改记录";
            // 
            // menuItemDel
            // 
            this.menuItemDel.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589127;
            this.menuItemDel.Name = "menuItemDel";
            this.menuItemDel.Size = new System.Drawing.Size(124, 22);
            this.menuItemDel.Text = "删除记录";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // menuItemAdd
            // 
            this.menuItemAdd.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1455589121;
            this.menuItemAdd.Name = "menuItemAdd";
            this.menuItemAdd.Size = new System.Drawing.Size(124, 22);
            this.menuItemAdd.Text = "新增记录";
            // 
            // menuItemCopyAdd
            // 
            this.menuItemCopyAdd.Image = global::CT.ERP.Client.Properties.Resources.ooopic_1456392375;
            this.menuItemCopyAdd.Name = "menuItemCopyAdd";
            this.menuItemCopyAdd.Size = new System.Drawing.Size(124, 22);
            this.menuItemCopyAdd.Text = "复制新增";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "规格:";
            // 
            // frmMainDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 547);
            this.Controls.Add(this.dataGridResult);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "送货单管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainDelivery_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).EndInit();
            this.menuGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox customer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnStat;
        private System.Windows.Forms.ComboBox specifications;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelDisco;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelLength;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelWeight;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelPrice;
        private System.Windows.Forms.DataGridView dataGridResult;
        private System.Windows.Forms.ContextMenuStrip menuGrid;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuItemDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopyAdd;
        private System.Windows.Forms.ComboBox goodname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;

    }
}