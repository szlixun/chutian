using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CT.ERP.Client.BLL;
using CT.ERP.Client.Entity;
using CT.ERP.Client.Util;
using DevComponents.DotNetBar;

namespace CT.ERP.Client
{
    public partial class frmMainDelivery : Office2007Form
    {
        private int mCurId;
        
        public frmMainDelivery()
        {
            InitializeComponent();
        }


        private void InitCombo()
        {
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            try
            {
                CustomerDAC dacCustomer = new CustomerDAC();
                customer.Items.Clear();
                List<CustomerEntity> lstCustomer = dacCustomer.SelectAll();
                foreach (CustomerEntity entity in lstCustomer)
                {
                    customer.Items.Add(entity.customer);
                }

                SysDictDAC dacSys = new SysDictDAC();
                specifications.Items.Clear();
                List<SysDictEntity> lstDict = dacSys.SelectList("deliveryspec");
                foreach (SysDictEntity entity in lstDict)
                {
                    specifications.Items.Add(entity.dictvalue);
                }

                goodname.Items.Clear();
                lstDict = dacSys.SelectList("goodname");
                foreach (SysDictEntity entity in lstDict)
                {
                    goodname.Items.Add(entity.dictvalue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
            }
        }

        private void RefreshData()
        {
            InitHeader();
            try
            {
                DeliveryDAC dac = new DeliveryDAC();
                DeliveryResutl objResult = dac.SelectList(customer.Text.Trim(), dtpStart.Value, dtpEnd.Value, goodname.Text.Trim(), specifications.Text.Trim());
                DataTable dt = ControlHelper.ConvertList2DataTable(objResult.Notes);
                dataGridResult.DataSource = dt;
                StatusLabelDisco.Text = objResult.TotalDisc.ToString();
                StatusLabelLength.Text = objResult.TotalLength.ToString();
                StatusLabelWeight.Text = objResult.TotalWeight.ToString();
                StatusLabelPrice.Text = objResult.TotalPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
            }
        }

        private void InitHeader()
        {
            dataGridResult.Columns.Clear();
            dataGridResult.DataSource = null;

            DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
            Column1.Width = 0;
            Column1.HeaderText = "id";
            Column1.DataPropertyName = "noteid";
            Column1.Visible = false;
            dataGridResult.Columns.Add(Column1);

            DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            Column2.Width = 140;
            Column2.HeaderText = "客户名称";
            Column2.DataPropertyName = "customer";
            dataGridResult.Columns.Add(Column2);

            DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            Column3.Width = 80;
            Column3.HeaderText = "型号";
            Column3.DataPropertyName = "model";
            dataGridResult.Columns.Add(Column3);

            DataGridViewTextBoxColumn Column4 = new DataGridViewTextBoxColumn();
            Column4.Width = 100;
            Column4.HeaderText = "发货时间";
            Column4.DataPropertyName = "deliverdate";
            dataGridResult.Columns.Add(Column4);

            DataGridViewTextBoxColumn Column5 = new DataGridViewTextBoxColumn();
            Column5.Width = 120;
            Column5.HeaderText = "货物名称";
            Column5.DataPropertyName = "goodname";
            dataGridResult.Columns.Add(Column5);

            DataGridViewTextBoxColumn Column6 = new DataGridViewTextBoxColumn();
            Column6.Width = 80;
            Column6.HeaderText = "送货单号";
            Column6.DataPropertyName = "deliverid";
            dataGridResult.Columns.Add(Column6);

            DataGridViewTextBoxColumn Column7 = new DataGridViewTextBoxColumn();
            Column7.Width = 100;
            Column7.HeaderText = "出厂批号";
            Column7.DataPropertyName = "batch";
            dataGridResult.Columns.Add(Column7);

            DataGridViewTextBoxColumn Column8 = new DataGridViewTextBoxColumn();
            Column8.Width = 100;
            Column8.HeaderText = "OEM";
            Column8.DataPropertyName = "description";
            dataGridResult.Columns.Add(Column8);

            DataGridViewTextBoxColumn Column9 = new DataGridViewTextBoxColumn();
            Column9.Width = 100;
            Column9.HeaderText = "制单人";
            Column9.DataPropertyName = "loginid";
            dataGridResult.Columns.Add(Column9);

            DataGridViewTextBoxColumn Column10 = new DataGridViewTextBoxColumn();
            Column10.Width = 150;
            Column10.HeaderText = "备注";
            Column10.DataPropertyName = "description1";
            dataGridResult.Columns.Add(Column10);

        }

        private void frmMainDelivery_Load(object sender, EventArgs e)
        {
            InitCombo();
            RefreshData();
        }

        private void menuGrid_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (mCurId == 0) return;
            switch (e.ClickedItem.Name)
            {
                case "menuItemUpdate":
                    EditRecord(mCurId);
                    break;
                case "menuItemDel":
                    Remove(mCurId);
                    break;
                case "menuItemAdd":
                    Add();
                    break;
                case "menuItemCopyAdd":
                    CopyAdd(mCurId);
                    break;
            }
        }

        private void Remove(int id)
        {
            if (MessageBox.Show("确认是否删除当前记录?", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {


                DeliveryDAC dac = new DeliveryDAC();                
                String bErr=dac.Delete(id,true);
                if (bErr.Length > 0) 
                {
                    MessageBox.Show("删除失败!\r\n原因：" + bErr);
                    return;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }
            RefreshData();
            MessageBox.Show("删除成功");            
        }

        private void EditRecord(int id)
        {
            DeliveryNote objNote = null;
            try
            {
                DeliveryDAC dac = new DeliveryDAC();
                objNote = dac.Select(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }

            if (objNote == null)
            {
                MessageBox.Show("查询出错");
                return;
            }

            frmEditDelivery frm = new frmEditDelivery();
            frm.EditMode = 2;
            frm.objNote = objNote;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void CopyAdd(int id)
        {
            DeliveryNote objNote = null;
            try
            {
                DeliveryDAC dac = new DeliveryDAC();
                objNote = dac.Select(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }

            if (objNote == null)
            {
                MessageBox.Show("查询出错");
                return;
            }

            frmEditDelivery frm = new frmEditDelivery();
            frm.EditMode = 1;
            frm.objNote = objNote;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void Add()
        {
            frmEditDelivery frm = new frmEditDelivery();
            frm.EditMode = 0;
            if (frm.ShowDialog() == DialogResult.OK) 
            {
                RefreshData();
            }
        }

        private void dataGridResult_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = int.Parse(dataGridResult.Rows[e.RowIndex].Cells[0].Value.ToString());
                EditRecord(id);
            }
        }

        private void dataGridResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                mCurId = int.Parse(dataGridResult.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (e.Button == MouseButtons.Right)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridResult.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridResult.ClearSelection();
                        dataGridResult.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridResult.SelectedRows.Count == 1)
                    {
                        dataGridResult.CurrentCell = dataGridResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    
                    //弹出操作菜单
                    menuGrid.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            DeliveryResutl objResult = null;
            try
            {
                DeliveryDAC dac = new DeliveryDAC();
                objResult = dac.Statistic(customer.Text.Trim(), dtpStart.Value, dtpEnd.Value, specifications.Text.Trim(), goodname.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }
            if (objResult != null)
            {
                MessageBox.Show("统计结果如下：\r\n总盘数：" + objResult.TotalDisc.ToString() + "\r\n总长度：" + objResult.TotalLength.ToString()
                    + "\r\n总重量：" + objResult.TotalWeight.ToString() + "\r\n总金额：" + objResult.TotalPrice.ToString());
            }


        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridResult.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要导出的单据");
                return;
            }
                       
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件|*.xls";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DeliveryNote note;
                try
                {
                    int id = int.Parse(dataGridResult.SelectedRows[0].Cells[0].Value.ToString());
                    DeliveryDAC dac = new DeliveryDAC();
                    note = dac.Select(id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询单据出错,请检查网络;\r\n原因:" + ex.Message);
                    return;

                }
                try
                {
                    NPOIHelper.ExportDelivery(sfd.FileName.ToString(), note);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出Excel出错;\r\n原因:" + ex.Message);
                    return;
                }

                MessageBox.Show("导出成功");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }


    }
}
