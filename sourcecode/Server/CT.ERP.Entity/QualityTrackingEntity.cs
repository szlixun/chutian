using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using PES.DataModel;

namespace CT.ERP.Entity
{
    [DMTable("ct_qualitytracking", "qtid", true)]
    [DataContract]
    public class QualityTrackingEntity
    {
        [DataMember]
        public int qtid { get; set; }
        [DataMember]
        public DateTime qtdate { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string batch { get; set; }
        [DataMember]
        public string specifications { get; set; }
        [DataMember]
        public int length { get; set; }
        [DataMember]
        public string volume { get; set; }
        [DataMember]
        public string stripping1 { get; set; }
        [DataMember]
        public string stripping2 { get; set; }
        [DataMember]
        public string sample11 { get; set; }
        [DataMember]
        public string sample12 { get; set; }
        [DataMember]
        public string sample13 { get; set; }
        [DataMember]
        public string sample21 { get; set; }
        [DataMember]
        public string sample22 { get; set; }
        [DataMember]
        public string sample23 { get; set; }
        [DataMember]
        public string baseheight { get; set; }
        [DataMember]
        public string measuredheight { get; set; }
        [DataMember]
        public string compositeheight { get; set; }
        [DataMember]
        public string cutheight { get; set; }
        [DataMember]
        public string bubblewater1 { get; set; }
        [DataMember]
        public string bubblewater2 { get; set; }
        [DataMember]
        public string bubbleoil { get; set; }
        [DataMember]
        public string descrtiption { get; set; }
    }
}
