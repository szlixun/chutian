using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CT.ERP.Entity;
using CT.ERP.Bussiness.DAL;


namespace CT.ERP.WCFService
{
    public class QualityTrackingService :IQualityTracking
    {
        public int Add(int x,int y)
        {
            return x + y;
            //QualityTrackingDAC dac=new QualityTrackingDAC();
            //return dac.Insert(entity);            
        }
    }
}
