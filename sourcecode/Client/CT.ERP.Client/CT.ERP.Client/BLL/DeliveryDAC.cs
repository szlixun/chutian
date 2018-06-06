using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Client.Entity;
using CT.ERP.Client.Util;

namespace CT.ERP.Client.BLL
{
    public class DeliveryDAC
    {
        public DeliveryResutl SelectList(string customer, DateTime sStart, DateTime sEnd, string goodname, string specifications)
        {
            DeliveryResutl result = new DeliveryResutl();
            string strSql = "";

            if (specifications.Length > 0)
            {
                strSql = "select a.* from ct_deliverynote a,ct_deliveryitem b where a.noteid=b.noteid and a.deliverdate>='" + sStart.ToString() + "' and a.deliverdate<='" + sEnd.ToString() + "'";
                if (customer.Length > 0)
                {
                    strSql += " and a.customer = '" + customer + "'";
                }
                if (goodname.Length > 0)
                {
                    strSql += " and a.goodname = '" + goodname + "'";
                }
                strSql+= " and b.specifications = '" + specifications + "'";
                strSql += " order by a.noteid desc";
            }
            else 
            {
                strSql = "select * from ct_deliverynote where deliverdate>='" + sStart.ToString() + "' and deliverdate<='" + sEnd.ToString() + "'";
                if (customer.Length > 0)
                {
                    strSql += " and customer = '" + customer + "'";
                }
                if (goodname.Length > 0)
                {
                    strSql += " and goodname = '" + goodname + "'";
                }
                strSql += " order by noteid desc";
            }
            

            result.Notes=DMContext.TSqlCommand(strSql).ToList<DeliveryNote>();

            string strSql1 = "SELECT SUM(b.lenght * b.discnum), SUM(b.discnum),SUM(b.totalprice),SUM(b.weight) FROM ct_deliverynote a,ct_deliveryitem b where a.noteid=b.noteid and a.deliverdate>='" + sStart.ToString() + "' and a.deliverdate<='" + sEnd.ToString() + "'";
            if (customer.Length > 0)
            {
                strSql1 += " and a.customer = '" + customer + "'";
            }
            if (goodname.Length > 0)
            {
                strSql1 += " and a.goodname = '" + goodname + "'";
            }
            if (specifications.Length > 0)
            {
                strSql1 += " and b.specifications = '" + specifications + "'";
            }
            DataTable dtSum = DMContext.TSqlCommand(strSql1).ToDataTable();
            if (dtSum != null) 
            {
                if (dtSum.Rows.Count > 0) 
                {
                    result.TotalLength = ControlHelper.Object2Int(dtSum.Rows[0][0]);
                    result.TotalDisc = ControlHelper.Object2Int(dtSum.Rows[0][1]);
                    result.TotalPrice = ControlHelper.Object2Double(dtSum.Rows[0][2]);
                    result.TotalWeight = ControlHelper.Object2Double(dtSum.Rows[0][3]);
                }
            }

            return result;
        }


        public bool Add(DeliveryNote note)
        {
            bool bRet = false;
            bool bNewCustomer = false; ;
            int iDeliveryId;
            CustomerDAC dac = new CustomerDAC();
            CustomerEntity objCustomer = dac.Select(note.customer);
            if (objCustomer == null)
            {
                bNewCustomer = true;
                objCustomer = new CustomerEntity();
                objCustomer.customer = note.customer;
                objCustomer.cyear = DateTime.Now.Year;
                objCustomer.sequence = 1;
                iDeliveryId = 1;
            }
            else 
            {
                bNewCustomer = false;
                if (objCustomer.cyear == DateTime.Now.Year)
                {
                    objCustomer.sequence = objCustomer.sequence + 1;
                    iDeliveryId = objCustomer.sequence;
                }
                else
                {
                    objCustomer.cyear = DateTime.Now.Year;
                    objCustomer.sequence = 1;
                    iDeliveryId = 1;
                }
            }

            using (var dmt = DMContext.GetTransaction())
            {
                var trans = dmt.BeginTransaction();
                if (bNewCustomer)
                    DMContext.Insert(objCustomer, null, trans);
                else
                    DMContext.Update<CustomerEntity>(objCustomer,p=>p.cid==objCustomer.cid, null, trans);

                note.deliverid = iDeliveryId;
                int noteId=DMContext.Insert(note, null, trans);
                if (noteId > 0)
                {
                    if (note.items != null)
                    {
                        foreach (DeliveryItem item in note.items)
                        {
                            item.noteid = noteId;
                            DMContext.Insert(item, null, trans);
                        }
                    }
                    trans.Commit();
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool CheckIsLast(int noteid)
        {
            bool bRet = false;
            using (var repo = DMRepository.Get())
            {
                DeliveryNote note = repo.Get<DeliveryNote>(p => p.noteid == noteid);
                if (note != null)
                {
                    Spec<CustomerEntity> where = new Spec<CustomerEntity>();
                    where.And(p => p.customer == note.customer);
                    where.And(p => p.cyear == note.sdate.Year);
                    CustomerEntity customer = repo.Get<CustomerEntity>(where.Exp);
                    if (customer != null)
                    {
                        if (customer.sequence == note.deliverid)
                        {
                            bRet = true;
                        }
                    }
                }
            }


            return bRet;
        }

        public String Delete(int noteid, bool isdecrease)
        {
            String bErr = "";
            CustomerEntity customer=null;

            if (isdecrease)
            {
                using (var repo = DMRepository.Get())
                {
                    DeliveryNote note = repo.Get<DeliveryNote>(p => p.noteid == noteid);
                    if (note == null)
                        return "当前送货单不存在";

                    Spec<CustomerEntity> where = new Spec<CustomerEntity>();
                    where.And(p => p.customer == note.customer);
                    if (note.sdate.Year <= 1)
                    {
                        where.And(p => p.cyear == DateTime.Now.Year);
                    }
                    else 
                    {
                        where.And(p => p.cyear == note.sdate.Year);
                    }
                    customer = repo.Get<CustomerEntity>(where.Exp);
                    if (customer == null)
                        return "系统中没有此客户信息";

                    if (customer.sequence != note.deliverid) return "当前送货单已归档，不能删除";
                    customer.sequence = customer.sequence - 1;

                }
            }
            
            using (var dmt = DMContext.GetTransaction())
            {
                 
                var trans = dmt.BeginTransaction();
                if(customer!=null & isdecrease)
                    DMContext.Update<CustomerEntity>(customer,p=>p.cid==customer.cid,null,trans);

                DMContext.Delete<DeliveryItem>(p => p.noteid == noteid, trans);
                DMContext.Delete<DeliveryNote>(p => p.noteid == noteid, trans);
                trans.Commit();
            }

            return bErr;
        }

        public DeliveryNote Select(int noteId) 
        {
            DeliveryNote note;
            using (var repo = DMRepository.Get())
            {
                note = repo.Get<DeliveryNote>(p => p.noteid == noteId);
                note.items = repo.GetList<DeliveryItem>(50, p => p.noteid == noteId, p => p.itemid.Asc());
            }

            return note;
        }

        public void Update(DeliveryNote note)
        {
            using (var dmt = DMContext.GetTransaction())
            {
                int noteid = note.noteid;
                var trans = dmt.BeginTransaction();
                DMContext.Delete<DeliveryItem>(p => p.noteid == noteid, trans);

                DMContext.Update<DeliveryNote>(note,p=>p.noteid==noteid , null, trans);
                if (note.items != null)
                {
                    foreach (DeliveryItem item in note.items)
                    {
                        item.noteid = note.noteid;
                        DMContext.Insert(item, null, trans);
                    }
                }         

                trans.Commit();
            }
        }

        public DeliveryResutl Statistic(string customer, DateTime sStart, DateTime sEnd, string specifications,string goodname)
        {
            DeliveryResutl result = new DeliveryResutl();

            string strSql1 = "SELECT SUM(b.lenght*b.discnum), SUM(b.discnum),SUM(b.totalprice),SUM(b.weight) FROM ct_deliverynote a,ct_deliveryitem b where a.noteid=b.noteid and a.deliverdate>='" + sStart.ToString() + "' and a.deliverdate<='" + sEnd.ToString() + "'";
            if (customer.Length > 0)
            {
                strSql1 += " and a.customer = '" + customer + "'";
            }
            if (goodname.Length > 0)
            {
                strSql1 += " and a.goodname = '" + goodname + "'";
            }
            if (specifications.Length > 0)
            {
                strSql1 += " and b.specifications = '" + specifications + "'";
            }

            DataTable dtSum = DMContext.TSqlCommand(strSql1).ToDataTable();
            if (dtSum != null)
            {
                if (dtSum.Rows.Count > 0)
                {
                    result.TotalLength = ControlHelper.Object2Int(dtSum.Rows[0][0]);
                    result.TotalDisc = ControlHelper.Object2Int(dtSum.Rows[0][1]);
                    result.TotalPrice = ControlHelper.Object2Double(dtSum.Rows[0][2]);
                    result.TotalWeight = ControlHelper.Object2Double(dtSum.Rows[0][3]);
                }
            }

            return result;
        }




    }
}
