using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.BLL
{
    public class QualityTrackingDAC : BaseDAL<QualityTrackingEntity>
    {
        public int Insert(QualityTrackingEntity entity)
        {
            int iRet = 0;
            if (entity != null)
            {
                iRet = base.DMInsert(entity);
            }
            return iRet;
        }

        public List<QualityTrackingEntity> SelectAll()
        {
            return base.DMSelectAll(p => p.qtdate.Asc());
        }

        public List<QualityTrackingEntity> Query(DateTime sStart, DateTime sEnd, string batch, string target, string type, string decision) 
        {
            string strSql = "select * from ct_qualitytracking where qtdate>='" + sStart.ToString() + "' and qtdate<='" + sEnd.ToString() + "'";
            if (batch.Length > 0)
            {
                strSql = strSql + " and batch = '" + batch + "'";
            }
            if (target.Length > 0)
            {
                strSql = strSql + " and target ='" + target + "'";
            }
            if (type.Length > 0)
            {
                strSql = strSql + " and type ='" + type + "'";
            }
            if (decision.Length > 0)
            {
                strSql = strSql + " and decision ='" + decision + "'";
            }

            strSql = strSql + " order by qtid";

            
            //CSpec<QualityTrackingEntity> order = new CSpec<QualityTrackingEntity>();
            //order.And(p => p.qtdate.Asc());

            //Spec<QualityTrackingEntity> where = new Spec<QualityTrackingEntity>();
            //where.And(p => p.qtdate.Between(sStart, sEnd));
            //return base.DMSelectList(1000, where.Exp, order.Exp);

            return DMContext.TSqlCommand(strSql).ToList<QualityTrackingEntity>();
        }


        public int Update(QualityTrackingEntity entity)
        {
            return base.DMUpdate(entity, p => p.qtid == entity.qtid);
        }

        public QualityTrackingEntity Select(int id)
        {
            return base.DMSelect(p => p.qtid == id);
        }


        public int Delete(int id)
        {
            return base.DMDelete(p => p.qtid == id);
        }

        
    }
}
