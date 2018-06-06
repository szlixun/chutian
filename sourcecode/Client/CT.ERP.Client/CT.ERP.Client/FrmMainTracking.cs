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
using CT.ERP.Client.Util;
using DevComponents.DotNetBar;


namespace CT.ERP.Client
{
    public partial class FrmMainTracking : Office2007Form
    {
        private int mCurId=0;
        public FrmMainTracking()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            frmQualityTracking frmAdd = new frmQualityTracking();
            //新增
            frmAdd.EditMode = 0;
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                Refresh();
            }
            frmAdd.Dispose();
            frmAdd = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh() 
        {
            InitHeader();
            try
            {
                QualityTrackingDAC dac = new QualityTrackingDAC();
                List<QualityTrackingEntity> lstResult = dac.Query(dtpStart.Value, dtpEnd.Value,batch.Text.Trim(),target.Text.Trim(),type.Text.Trim(),decision.Text.Trim());
                DataTable dt = ControlHelper.ConvertList2DataTable(lstResult);
                dataGridResult.DataSource = dt;

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

            DataGridViewTextBoxColumn Column0 = new DataGridViewTextBoxColumn();
            Column0.Width = 40;
            Column0.HeaderText = "序号";
            Column0.DataPropertyName = "id";
            dataGridResult.Columns.Add(Column0);

            DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
            Column1.Width = 0;
            Column1.HeaderText = "id";
            Column1.DataPropertyName = "qtid";
            Column1.Visible=false;
            dataGridResult.Columns.Add(Column1);

            DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            Column2.Width = 100;
            Column2.HeaderText = "日期";
            Column2.DataPropertyName = "qtdate";
            dataGridResult.Columns.Add(Column2);

            DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            Column3.Width = 60;
            Column3.HeaderText = "品种";
            Column3.DataPropertyName = "category";
            dataGridResult.Columns.Add(Column3);

            DataGridViewTextBoxColumn Column4 = new DataGridViewTextBoxColumn();
            Column4.Width = 70;
            Column4.HeaderText = "批次";
            Column4.DataPropertyName = "batch";
            dataGridResult.Columns.Add(Column4);

            DataGridViewTextBoxColumn Column5 = new DataGridViewTextBoxColumn();
            Column5.Width = 100;
            Column5.HeaderText = "规格";
            Column5.DataPropertyName = "specifications";
            dataGridResult.Columns.Add(Column5);

            //DataGridViewTextBoxColumn Column6 = new DataGridViewTextBoxColumn();
            //Column6.Width = 100;
            //Column6.HeaderText = "长度";
            //Column6.DataPropertyName = "length";
            //dataGridResult.Columns.Add(Column6);

            DataGridViewTextBoxColumn Column23 = new DataGridViewTextBoxColumn();
            Column23.Width = 60;
            Column23.HeaderText = "卷号";
            Column23.DataPropertyName = "volume";
            dataGridResult.Columns.Add(Column23);

            DataGridViewTextBoxColumn Column7 = new DataGridViewTextBoxColumn();
            Column7.Width = 60;
            Column7.HeaderText = "剥离";
            Column7.DataPropertyName = "stripping1";
            dataGridResult.Columns.Add(Column7);

            DataGridViewTextBoxColumn Column8 = new DataGridViewTextBoxColumn();
            Column8.Width = 60;
            Column8.HeaderText = "剥离";
            Column8.DataPropertyName = "stripping2";
            dataGridResult.Columns.Add(Column8);

            DataGridViewTextBoxColumn Column26 = new DataGridViewTextBoxColumn();
            Column26.Width = 120;
            Column26.HeaderText = "原材料延伸率";
            Column26.DataPropertyName = "elongation";
            dataGridResult.Columns.Add(Column26);

            DataGridViewTextBoxColumn Column9 = new DataGridViewTextBoxColumn();
            Column9.Width = 60;
            Column9.HeaderText = "样品1";
            Column9.DataPropertyName = "sample11";
            dataGridResult.Columns.Add(Column9);

            DataGridViewTextBoxColumn Column10 = new DataGridViewTextBoxColumn();
            Column10.Width = 60;
            Column10.HeaderText = "样品1";
            Column10.DataPropertyName = "sample12";
            dataGridResult.Columns.Add(Column10);

            DataGridViewTextBoxColumn Column11 = new DataGridViewTextBoxColumn();
            Column11.Width = 60;
            Column11.HeaderText = "样品1";
            Column11.DataPropertyName = "sample13";
            dataGridResult.Columns.Add(Column11);

            DataGridViewTextBoxColumn Column12 = new DataGridViewTextBoxColumn();
            Column12.Width = 60;
            Column12.HeaderText = "样品2";
            Column12.DataPropertyName = "sample21";
            dataGridResult.Columns.Add(Column12);

            DataGridViewTextBoxColumn Column13 = new DataGridViewTextBoxColumn();
            Column13.Width = 60;
            Column13.HeaderText = "样品2";
            Column13.DataPropertyName = "sample22";
            dataGridResult.Columns.Add(Column13);

            DataGridViewTextBoxColumn Column14 = new DataGridViewTextBoxColumn();
            Column14.Width = 60;
            Column14.HeaderText = "样品2";
            Column14.DataPropertyName = "sample23";
            dataGridResult.Columns.Add(Column14);

            DataGridViewTextBoxColumn Column15 = new DataGridViewTextBoxColumn();
            Column15.Width = 80;
            Column15.HeaderText = "基带厚度";
            Column15.DataPropertyName = "baseheight";
            dataGridResult.Columns.Add(Column15);

            DataGridViewTextBoxColumn Column16 = new DataGridViewTextBoxColumn();
            Column16.Width = 80;
            Column16.HeaderText = "实测厚度";
            Column16.DataPropertyName = "measuredheight";
            dataGridResult.Columns.Add(Column16);

            DataGridViewTextBoxColumn Column17 = new DataGridViewTextBoxColumn();
            Column17.Width = 80;
            Column17.HeaderText = "复合厚度";
            Column17.DataPropertyName = "compositeheight";
            dataGridResult.Columns.Add(Column17);

            DataGridViewTextBoxColumn Column18 = new DataGridViewTextBoxColumn();
            Column18.Width = 80;
            Column18.HeaderText = "分切厚度";
            Column18.DataPropertyName = "cutheight";
            dataGridResult.Columns.Add(Column18);

            DataGridViewTextBoxColumn Column19 = new DataGridViewTextBoxColumn();
            Column19.Width = 60;
            Column19.HeaderText = "泡水";
            Column19.DataPropertyName = "bubblewater1";
            dataGridResult.Columns.Add(Column19);

            DataGridViewTextBoxColumn Column20 = new DataGridViewTextBoxColumn();
            Column20.Width = 60;
            Column20.HeaderText = "泡水";
            Column20.DataPropertyName = "bubblewater2";
            dataGridResult.Columns.Add(Column20);

            DataGridViewTextBoxColumn Column21 = new DataGridViewTextBoxColumn();
            Column21.Width = 60;
            Column21.HeaderText = "泡油";
            Column21.DataPropertyName = "bubbleoil";
            dataGridResult.Columns.Add(Column21);

            DataGridViewTextBoxColumn Column22 = new DataGridViewTextBoxColumn();
            Column22.Width = 100;
            Column22.HeaderText = "备注";
            Column22.DataPropertyName = "descrtiption";
            dataGridResult.Columns.Add(Column22);

            DataGridViewTextBoxColumn Column24 = new DataGridViewTextBoxColumn();
            Column24.Width = 100;
            Column24.HeaderText = "制单人";
            Column24.DataPropertyName = "loginid";
            dataGridResult.Columns.Add(Column24);

            DataGridViewTextBoxColumn Column25 = new DataGridViewTextBoxColumn();
            Column25.Width = 100;
            Column25.HeaderText = "去向";
            Column25.DataPropertyName = "target";
            dataGridResult.Columns.Add(Column25);

            DataGridViewTextBoxColumn Column27 = new DataGridViewTextBoxColumn();
            Column27.Width = 100;
            Column27.HeaderText = "成品延伸率";
            Column27.DataPropertyName = "elongation1";
            dataGridResult.Columns.Add(Column27);

            DataGridViewTextBoxColumn Column28 = new DataGridViewTextBoxColumn();
            Column28.Width = 100;
            Column28.HeaderText = "类型";
            Column28.DataPropertyName = "type";
            dataGridResult.Columns.Add(Column28);

            DataGridViewTextBoxColumn Column29 = new DataGridViewTextBoxColumn();
            Column29.Width = 100;
            Column29.HeaderText = "品质判定";
            Column29.DataPropertyName = "decision";
            dataGridResult.Columns.Add(Column29);

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            SysDictDAC dacSys = new SysDictDAC();
            target.Items.Clear();

            try
            {
                List<SysDictEntity> lstDict = dacSys.SelectList("target");
                foreach (SysDictEntity entity in lstDict)
                {
                    target.Items.Add(entity.dictvalue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库错误，请联系管理员");
            }
            

            Refresh();
        }

        private void dataGridResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) 
            {
                if (e.RowIndex >= 0)
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

                    mCurId=int.Parse(dataGridResult.Rows[e.RowIndex].Cells[1].Value.ToString());
                    //弹出操作菜单
                    menuGrid.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void dataGridResult_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = int.Parse(dataGridResult.Rows[e.RowIndex].Cells[1].Value.ToString());
                EditRecord(id);
            }
        }

        private void EditRecord(int id)
        {
            QualityTrackingEntity entity;
            try
            {
                QualityTrackingDAC dac = new QualityTrackingDAC();
                entity = dac.Select(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }
            if (entity != null) 
            {
                frmQualityTracking frm = new frmQualityTracking();
                //修改模式
                frm.EditMode = 2;
                frm.objTracking = entity;
                if (frm.ShowDialog() == DialogResult.OK) 
                {
                    Refresh();
                }
                frm.Dispose();
                frm = null;
            }
        }

        private void CopyAdd(int id)
        {
            QualityTrackingEntity entity;
            try
            {
                QualityTrackingDAC dac = new QualityTrackingDAC();
                entity = dac.Select(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }
            if (entity != null)
            {
                frmQualityTracking frm = new frmQualityTracking();
                //修改模式
                frm.EditMode = 1;
                frm.objTracking = entity;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Refresh();
                }
                frm.Dispose();
                frm = null;
            }
        }

        private void Remove(int id)
        {
            if (MessageBox.Show("确认是否删除当前记录?", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            int iRet;
            try
            {
                QualityTrackingDAC dac = new QualityTrackingDAC();
                iRet = dac.Delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                return;
            }
            if (iRet > 0)
            {
                MessageBox.Show("删除成功");
                Refresh();
            }
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


        

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件|*.xls";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                DataTable dt;
                try
                {
                    QualityTrackingDAC dac = new QualityTrackingDAC();
                    List<QualityTrackingEntity> lstResult = dac.Query(dtpStart.Value, dtpEnd.Value,batch.Text.Trim(),target.Text.Trim(),type.Text,decision.Text);
                    dt = ControlHelper.ConvertList2ExcelTable(lstResult);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取数据库出错，请检查网络;\r\n原因:" + ex.Message);
                    return;
                }

                try
                {
                    NPOIHelper.Export(dt, "", sfd.FileName.ToString());
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("导出Excel出错;\r\n原因:" + ex.Message);
                    return;
                }
                MessageBox.Show("导出成功");
            }

            
        }

    }
}
