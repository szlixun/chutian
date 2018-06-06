using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CT.ERP.Client.Entity
{
    public class DeliveryResutl
    {
        public List<DeliveryNote> Notes { get; set; }
        public int TotalLength { get; set; }
        public double TotalPrice { get; set; }
        public int TotalDisc { get; set; }
        public double TotalWeight { get; set; }
    }
}
