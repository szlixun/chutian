using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace CT.ERP.Entity
{
    [ServiceContract]
    public interface IQualityTracking
    {
        [OperationContract]
        //int Add(QualityTrackingEntity entity);
        int Add(int x,int y);
    }
}
