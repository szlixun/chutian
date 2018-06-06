using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_customer", "cid", true)]
    public class CustomerEntity
    {
        public int cid { get; set; }
        public string customer { get; set; }
        public int cyear {get;set;}
        public int sequence { get; set; }
    }
}
