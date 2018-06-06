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
    public partial class frmQualityTracking :Office2007Form
    {
        //0--普通新增 1--复制新增 2--修改
        public byte EditMode { get; set; }
        public QualityTrackingEntity objTracking { get; set; }
        
        public frmQualityTracking()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            foreach (Control ctl in this.groupBox1.Controls)
            {
                if ((ctl is TextBox) || (ctl is ComboBox) || (ctl is DateTimePicker)) 
                {
                    ctl.KeyDown += (sender, args) =>
                    {
                        if (args.KeyCode == Keys.Enter)
                        {
                            //this.SelectNextControl(ctl, true, true, false, true);
                            SendKeys.Send("{tab}");
                        }
                    };
                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ControlHelper.isNumeric(length.Text))
            {
                MessageBox.Show("规格必须为数字!");
                length.Focus();
                return;
            }

            if (batch.Text.Trim().Length == 0)
            {
                MessageBox.Show("必须输入批号");
                batch.Focus();
                return;
            }

            try
            {
                SysDictDAC dacSys = new SysDictDAC();
                SysDictEntity objEntity = dacSys.Select("qtspec", specifications.Text.Trim());
                if (objEntity == null)
                {
                    SysDictEntity newSysEntity = new SysDictEntity();
                    newSysEntity.dictype = "qtspec";
                    newSysEntity.dictvalue = specifications.Text.Trim();
                    dacSys.Add(newSysEntity);
                }

                objEntity = dacSys.Select("qtcategory", category.Text.Trim());
                if (objEntity == null)
                {
                    SysDictEntity newSysEntity = new SysDictEntity();
                    newSysEntity.dictype = "qtcategory";
                    newSysEntity.dictvalue = category.Text.Trim();
                    dacSys.Add(newSysEntity);
                }

                objEntity = dacSys.Select("target", target.Text.Trim());
                if (objEntity == null)
                {
                    SysDictEntity newSysEntity = new SysDictEntity();
                    newSysEntity.dictype = "target";
                    newSysEntity.dictvalue = target.Text.Trim();
                    dacSys.Add(newSysEntity);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("保存字典出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);
            }


            QualityTrackingDAC dac = new QualityTrackingDAC();
            if (EditMode == 0 || EditMode==1)
            {
                QualityTrackingEntity entity = new QualityTrackingEntity();
                entity.qtdate = qtdate.Value;
                entity.category = category.Text;
                entity.batch = batch.Text;
                entity.specifications = specifications.Text;
                if (length.Text.Trim().Length == 0)
                {
                    entity.length = 0;
                }
                else
                {
                    entity.length = int.Parse(length.Text);
                }
                entity.volume = volume.Text;
                entity.stripping1 = stripping1.Text;
                entity.stripping2 = stripping2.Text;
                entity.sample11 = sample11.Text;
                entity.sample12 = sample12.Text;
                entity.sample13 = sample13.Text;
                entity.sample21 = sample21.Text;
                entity.sample22 = sample22.Text;
                entity.sample23 = sample23.Text;
                entity.baseheight = baseheight.Text;
                entity.measuredheight = measuredheight.Text;
                entity.compositeheight = compositeheight.Text;
                entity.cutheight = cutheight.Text;
                entity.bubblewater1 = bubblewater1.Text;
                entity.bubblewater2 = bubblewater2.Text;
                entity.bubbleoil = bubbleoil.Text;
                entity.descrtiption = descrtiption.Text;
                entity.loginid = Global.LoginUser.loginid;
                entity.target = target.Text;
                entity.elongation = elongation.Text.Trim();
                entity.elongation1 = elongation1.Text.Trim();
                entity.type = type.Text;
                entity.decision = decision.Text;
                int id = 0;
                try
                {
                    id = dac.Insert(entity);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("新增出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                }
                if (id > 0)
                {
                    if (MessageBox.Show("保存成功,是否继续添加?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        batch.Text = "";
                        qtdate.Focus();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
            else 
            {
                if (objTracking == null) 
                {
                    MessageBox.Show("系统错误");
                    return;
                }

                objTracking.qtdate = qtdate.Value;
                objTracking.category = category.Text;
                objTracking.batch = batch.Text;
                objTracking.specifications = specifications.Text;
                if (length.Text.Trim().Length == 0)
                {
                    objTracking.length = 0;
                }
                else
                {
                    objTracking.length = int.Parse(length.Text);
                }
                objTracking.volume = volume.Text;
                objTracking.stripping1 = stripping1.Text;
                objTracking.stripping2 = stripping2.Text;
                objTracking.sample11 = sample11.Text;
                objTracking.sample12 = sample12.Text;
                objTracking.sample13 = sample13.Text;
                objTracking.sample21 = sample21.Text;
                objTracking.sample22 = sample22.Text;
                objTracking.sample23 = sample23.Text;
                objTracking.baseheight = baseheight.Text;
                objTracking.measuredheight = measuredheight.Text;
                objTracking.compositeheight = compositeheight.Text;
                objTracking.cutheight = cutheight.Text;
                objTracking.bubblewater1 = bubblewater1.Text;
                objTracking.bubblewater2 = bubblewater2.Text;
                objTracking.bubbleoil = bubbleoil.Text;
                objTracking.descrtiption = descrtiption.Text;
                objTracking.loginid = Global.LoginUser.loginid;
                objTracking.target = target.Text;
                objTracking.elongation = elongation.Text;
                objTracking.elongation1 = elongation1.Text;
                objTracking.type = type.Text;
                objTracking.decision = decision.Text;
                int id = 0;
                try
                {
                    id = dac.Update(objTracking);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("保存出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);
                }
                if (id > 0)
                {
                    MessageBox.Show("保存成功");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmQualityTracking_Load(object sender, EventArgs e)
        {
            try
            {
                SysDictDAC dacSys = new SysDictDAC();
                specifications.Items.Clear();
                List<SysDictEntity> lstDict = dacSys.SelectList("qtspec");
                foreach (SysDictEntity entity in lstDict)
                {
                    specifications.Items.Add(entity.dictvalue);
                }

                category.Items.Clear();
                lstDict = dacSys.SelectList("qtcategory");
                foreach (SysDictEntity entity in lstDict)
                {
                    category.Items.Add(entity.dictvalue);
                }

                target.Items.Clear();
                lstDict = dacSys.SelectList("target");
                foreach (SysDictEntity entity in lstDict)
                {
                    target.Items.Add(entity.dictvalue);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("提取字典出错，请检查网络是否异常！\r\n原因如下:" + ex.Message);
            }


            if (objTracking != null & EditMode >0) 
            {
                qtdate.Value = objTracking.qtdate;
                category.Text=objTracking.category;
                if (EditMode == 1)
                    batch.Text = "";
                else
                    batch.Text=objTracking.batch;
                specifications.Text=objTracking.specifications;
                length.Text=objTracking.length.ToString();
                volume.Text=objTracking.volume;
                stripping1.Text=objTracking.stripping1;
                stripping2.Text=objTracking.stripping2;
                sample11.Text=objTracking.sample11;
                sample12.Text=objTracking.sample12;
                sample13.Text=objTracking.sample13;
                sample21.Text=objTracking.sample21;
                sample22.Text=objTracking.sample22;
                sample23.Text=objTracking.sample23;
                baseheight.Text=objTracking.baseheight;
                measuredheight.Text=objTracking.measuredheight;
                compositeheight.Text=objTracking.compositeheight;
                cutheight.Text=objTracking.cutheight;
                bubblewater1.Text=objTracking.bubblewater1;
                bubblewater2.Text=objTracking.bubblewater2;
                bubbleoil.Text=objTracking.bubbleoil;
                descrtiption.Text=objTracking.descrtiption;
                target.Text = objTracking.target;
                elongation.Text = objTracking.elongation;
                elongation1.Text = objTracking.elongation1;
                if (objTracking.type!=null) 
                {
                    type.Text = objTracking.type;
                }
                if (objTracking.decision!=null)
                {
                    decision.Text = objTracking.decision;
                }
            }
        }
    }
}
