using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_deliveryitem", "itemid", true)]
    public class DeliveryItem
    {
        public int itemid { get; set;}
        public int noteid { get; set; }
        public string jiannum { get; set; }
        public string specifications { get; set; }
        public int lenght { get; set; }
        public int discnum { get; set; }
        public double weight { get; set; }
        public double price { get; set; }
        public double totalprice { get; set; }
        public string contractno { get; set; }
        public double netweight { get; set; }
        public double coreweight { get; set; }

    }
}
