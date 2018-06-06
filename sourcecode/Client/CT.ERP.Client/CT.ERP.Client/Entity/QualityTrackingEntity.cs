using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_qualitytracking", "qtid", true)]
    public class QualityTrackingEntity
    {
        public int qtid { get; set; }

        public DateTime qtdate { get; set; }

        public string category { get; set; }

        public string batch { get; set; }

        public string specifications { get; set; }

        public int length { get; set; }

        public string volume { get; set; }

        public string stripping1 { get; set; }

        public string stripping2 { get; set; }

        public string sample11 { get; set; }

        public string sample12 { get; set; }

        public string sample13 { get; set; }

        public string sample21 { get; set; }

        public string sample22 { get; set; }

        public string sample23 { get; set; }

        public string baseheight { get; set; }

        public string measuredheight { get; set; }

        public string compositeheight { get; set; }
        public string cutheight { get; set; }
        public string bubblewater1 { get; set; }
        public string bubblewater2 { get; set; }
        public string bubbleoil { get; set; }
        public string descrtiption { get; set; }
        public string loginid { get; set; }
        public string target { get; set; }
        public string elongation { get; set; }
        public string elongation1 { get; set; }
        public string type { get; set; }
        public string decision { get; set; }
    }
}
