using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CT.ERP.Client.Util;
using CT.ERP.Client.Entity;
using CT.ERP.Client.BLL;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace CT.ERP.Client
{
    public partial class frmEditDelivery : Office2007Form
    {
        //0--普通新增 1--复制新增 2--修改
        public byte EditMode { get; set; }
        public DeliveryNote objNote { get; set; }

        
        public frmEditDelivery()
        {
            InitializeComponent();
        }

        private void frmEditDelivery_Load(object sender, EventArgs e)
        {
            InitVar();
            InitGrid();
            InitDict();
        }


        private void InitDict()
        {
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
                model.Items.Clear();
                List<SysDictEntity> lstDict = dacSys.SelectList("model");
                foreach (SysDictEntity entity in lstDict)
                {
                    model.Items.Add(entity.dictvalue);
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
                MessageBox.Show("提取字典出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);
            }
        }

        private void InitVar()
        {
            deliverdate.Value = DateTime.Now;
            noteid.Text = "0";
            deliverid.Text = "";
        }

        private void InitGrid()
        {
            GridPanel panel = superGrid.PrimaryGrid;
            

            try
            {
                SysDictDAC dacSys = new SysDictDAC();
                List<SysDictEntity> lstDict = dacSys.SelectList("deliveryspec");

                if (lstDict.Count > 0)
                {
                    string[] specArray = new string[lstDict.Count];
                    int i = 0;
                    foreach (SysDictEntity entity in lstDict)
                    {
                        specArray[i] = entity.dictvalue;
                        i++;
                    }

                    panel.Columns["specifications"].EditorType = typeof(FragrantComboBox);
                    panel.Columns["specifications"].EditorParams = new object[] { specArray };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("提取字典出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);

            }


            if (objNote != null & EditMode>0)
            {
                if (EditMode == 2)
                {
                    noteid.Text = objNote.noteid.ToString();
                    deliverid.Text = objNote.deliverid.ToString().PadLeft(3, '0');
                    customer.Enabled = false;
                }
                customer.Text = objNote.customer;
                model.Text = objNote.model;
                deliverdate.Value = objNote.deliverdate;
                goodname.Text = objNote.goodname;
                batch.Text = objNote.batch;
                description.Text = objNote.description;
                description1.Text = objNote.description1;
                if (objNote.items != null) 
                {
                    foreach (DeliveryItem item in objNote.items)
                    {
                        GridRow newRow=new GridRow(
                            item.jiannum,
                            item.specifications,
                            item.lenght,
                            item.discnum,
                            item.weight,
                            Math.Round(item.price,5),
                            Math.Round(item.totalprice,5),
                            item.contractno,
                            item.netweight,
                            item.coreweight
                            );

                        panel.Rows.Add(newRow);
                    }
                }
            }
        }

        private string CheckForm(out bool saveInsert)
        {
            string sRet = "";
            saveInsert = false;
            if (customer.Text.Length == 0) 
            {
                sRet = "请输入客户名称";
                return sRet;
            }

            GridPanel panel = superGrid.PrimaryGrid;
            if (panel.Rows.Count > 0)
            {
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    GridRow curRow = panel.Rows[i] as GridRow;
                    string sJianNum = ControlHelper.Object2String(curRow["jiannum"].Value);
                    string sSpec = ControlHelper.Object2String(curRow["specifications"].Value);
                    int iLength = ControlHelper.Object2Int(curRow["lenght"].Value);
                    int iDiscNum = ControlHelper.Object2Int(curRow["discnum"].Value);
                    double dWeight = ControlHelper.Object2Double(curRow["weight"].Value);
                    double dPrice = ControlHelper.Object2Double(curRow["price"].Value);
                    string sContact = ControlHelper.Object2String(curRow["contractno"].Value);
                    double dNetWeight = ControlHelper.Object2Double(curRow["netweight"].Value);
                    double dCoreWeight = ControlHelper.Object2Double(curRow["coreweight"].Value);

                    bool bCheck = false;

                    if (curRow.IsInsertRow)
                    {
                        if (sContact.Length > 0 || dPrice > 0 || dWeight > 0 || iDiscNum > 0 || iLength > 0 || sSpec.Length > 0 || dCoreWeight > 0 || dNetWeight > 0)
                        {
                            bCheck = true;
                            saveInsert = true;
                        }
                    }
                    else
                        bCheck = true;

                    if (bCheck)
                    {
                        if (sJianNum.Length == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的件号";
                            break;
                        }

                        if (sSpec.Length == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的规格";
                            break;
                        }

                        if (iLength == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的长度";
                            break;
                        }

                        if (iDiscNum == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的盘数";
                            break;
                        }

                        if (dWeight == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的净重";
                            break;
                        }

                        if (dCoreWeight == 0)
                        {
                            sRet = "请输入第" + (i + 1) + "行的管芯";
                            break;
                        }
                    }



                }
            }
            return sRet;
        }



        private void superGrid_CellActivated(object sender, GridCellActivatedEventArgs e)
        {
            if (e.NewActiveCell.ColumnIndex == 0) 
            {
                RefreshJianNum();
            }

        }

        private void RefreshJianNum()
        {
            GridPanel panel = superGrid.PrimaryGrid;
            for (int i = 0; i < panel.Rows.Count; i++)
            {
                GridRow curRow = panel.Rows[i] as GridRow;
                curRow["jiannum"].Value = "CT-" + (i + 1).ToString().PadLeft(2, '0');
            }
        }

        private void superGrid_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            
            if (e.GridCell.ColumnIndex == 4) 
            {
                if (e.GridCell.GridRow["price"].Value!=null)
                    e.GridCell.GridRow["totalprice"].Value = Math.Round(double.Parse(e.NewValue.ToString()) * double.Parse(e.GridCell.GridRow["price"].Value.ToString()),5);
            }
            else if (e.GridCell.ColumnIndex == 5)
            {
                if (e.GridCell.GridRow["weight"].Value!=null)
                    e.GridCell.GridRow["totalprice"].Value = Math.Round(double.Parse(e.NewValue.ToString()) * double.Parse(e.GridCell.GridRow["weight"].Value.ToString()),5);
            }
        }

       


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool saveInsert;
            string sRet = CheckForm(out saveInsert);
            if (sRet.Length > 0)
            {
                MessageBox.Show(sRet);
                return;
            }


            DeliveryNote EditNote = new DeliveryNote();
            if (EditMode == 2)
            {
                EditNote.noteid = int.Parse(noteid.Text);
                EditNote.deliverid = int.Parse(deliverid.Text);

                if (objNote == null)
                    EditNote.sdate = DateTime.Now;
                else
                    EditNote.sdate = objNote.sdate;
            }
            else
            {
                EditNote.sdate = DateTime.Now;
            }

            EditNote.customer = customer.Text.Trim();
            EditNote.model = model.Text.Trim();
            EditNote.deliverdate = deliverdate.Value;
            EditNote.goodname = goodname.Text.Trim();
            EditNote.batch = batch.Text;
            EditNote.description = description.Text;
            EditNote.description1 = description1.Text;
            EditNote.loginid = Global.LoginUser.loginid;

            SysDictDAC dac = new SysDictDAC();
            SysDictEntity dictEntity = dac.Select("model", EditNote.model);
            if (dictEntity == null)
            {
                dictEntity = new SysDictEntity();
                dictEntity.dictype = "model";
                dictEntity.dictvalue = EditNote.model;
                dac.Add(dictEntity);
            }
            dictEntity = null;

            dictEntity = dac.Select("goodname", EditNote.goodname);
            if (dictEntity == null)
            {
                dictEntity = new SysDictEntity();
                dictEntity.dictype = "goodname";
                dictEntity.dictvalue = EditNote.goodname;
                dac.Add(dictEntity);
            }
            dictEntity = null;

            GridPanel panel = superGrid.PrimaryGrid;
            if (panel.Rows.Count > 0)
            {
                List<DeliveryItem> DeliveryItems = new List<DeliveryItem>();
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    DeliveryItem item = new DeliveryItem();
                    GridRow curRow = panel.Rows[i] as GridRow;
                    bool bSave = false;
                    if (!curRow.IsInsertRow)
                        bSave = true;
                    else
                        bSave = saveInsert;

                    if (bSave)
                    {
                        item.noteid = EditNote.noteid;
                        item.jiannum = ControlHelper.Object2String(curRow["jiannum"].Value);
                        string spec = ControlHelper.Object2String(curRow["specifications"].Value);

                        dictEntity = dac.Select("deliveryspec", spec);
                        if (dictEntity == null)
                        {
                            dictEntity = new SysDictEntity();
                            dictEntity.dictype = "deliveryspec";
                            dictEntity.dictvalue = spec;
                            dac.Add(dictEntity);
                        }
                        dictEntity = null;

                        item.specifications = spec;
                        item.lenght = ControlHelper.Object2Int(curRow["lenght"].Value);
                        item.discnum = ControlHelper.Object2Int(curRow["discnum"].Value);
                        item.weight = ControlHelper.Object2Double(curRow["weight"].Value);
                        item.price = ControlHelper.Object2Double(curRow["price"].Value.ToString());
                        item.totalprice = ControlHelper.Object2Double(curRow["totalprice"].Value);
                        item.contractno = ControlHelper.Object2String(curRow["contractno"].Value);
                        item.netweight = ControlHelper.Object2Double(curRow["netweight"].Value);
                        item.coreweight = ControlHelper.Object2Double(curRow["coreweight"].Value.ToString());
                        DeliveryItems.Add(item);
                    }
                }
                EditNote.items = DeliveryItems;
            }

            DeliveryDAC dacDelivery = new DeliveryDAC();
            if (EditMode == 0 || EditMode == 1)
            {
                bool bRet = false;
                try
                {
                    bRet = dacDelivery.Add(EditNote);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                    return;
                }
                if (bRet)
                {
                    if (MessageBox.Show("保存成功，是否继续新建送货单?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        EditMode = 1;
                        InitVar();
                    }
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            else
            {
                try
                {
                    dacDelivery.Update(EditNote);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作数据库出错，请检查网络;\r\n原因:" + ex.Message);
                    return;
                }
                MessageBox.Show("保存成功");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (superGrid.PrimaryGrid.ActiveRow != null)
            {
                if (superGrid.PrimaryGrid.Rows.Count > 1)
                {
                    if (MessageBox.Show("确认删除当前行!", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        superGrid.PrimaryGrid.SetDeleted(superGrid.PrimaryGrid.ActiveRow, true);
                        superGrid.PrimaryGrid.PurgeDeletedRows();
                        RefreshJianNum();
                    }
                }

            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (superGrid.PrimaryGrid.ActiveRow != null)
            {
                int iCurRowIndex = superGrid.PrimaryGrid.ActiveRow.RowIndex;
                if (iCurRowIndex == 0)
                {
                    MessageBox.Show("第一行不能复制");
                    return;
                }

                if (superGrid.PrimaryGrid.Rows.Count > 1)
                {
                    GridRow curRow = superGrid.PrimaryGrid.Rows[iCurRowIndex] as GridRow;
                    GridRow preRow = superGrid.PrimaryGrid.Rows[iCurRowIndex - 1] as GridRow;
                    curRow["specifications"].Value = preRow["specifications"].Value;
                    curRow["lenght"].Value = preRow["lenght"].Value;
                    curRow["discnum"].Value = preRow["discnum"].Value;
                    curRow["weight"].Value = preRow["weight"].Value;
                    curRow["price"].Value = preRow["price"].Value;
                    curRow["totalprice"].Value = preRow["totalprice"].Value;
                    curRow["contractno"].Value = preRow["contractno"].Value;
                    curRow["netweight"].Value = preRow["netweight"].Value;
                    curRow["coreweight"].Value = preRow["coreweight"].Value;

                }
            }
            else
            {
                MessageBox.Show("请先选中行!");
            }
        }


    }

    #region FragrantComboBox

    internal class FragrantComboBox : GridComboBoxExEditControl
    {
        public FragrantComboBox(IEnumerable orderArray)
        {
            DataSource = orderArray;
        }
    }

    #endregion
}
