using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_user", "userid", true)]
    public class User
    {
        public int userid { get; set; }
        public string loginid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool dodelivery { get; set; }
        public bool dotracking { get; set; }
        public bool dousermanage { get; set; }

    }
}
