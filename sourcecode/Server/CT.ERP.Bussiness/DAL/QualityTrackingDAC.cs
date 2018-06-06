using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Entity;

namespace CT.ERP.Bussiness.DAL
{
    public class QualityTrackingDAC: BaseDAL<QualityTrackingEntity>
    {
        public int Insert(QualityTrackingEntity entity)
        {
            int iRet=0;
            if (entity != null)
            {
                iRet = base.DMInsert(entity);
            }
            return iRet;
        }
    }
}
