using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_deliverynote", "noteid", true)]
    public class DeliveryNote
    {
        public int noteid { get; set; }
        public int deliverid { get; set; }
        public string customer { get; set; }
        public string model { get; set; }
        public DateTime deliverdate { get; set; }
        public string goodname { get; set; }
        public string batch { get; set; }
        public string description { get; set; }
        public string description1 { get; set; }
        public DateTime sdate { get; set; }
        public string loginid { get; set; }

        [DMIgnore]
        public List<DeliveryItem> items { get; set; }

    }
}
