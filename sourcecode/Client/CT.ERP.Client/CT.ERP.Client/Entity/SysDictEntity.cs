using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;

namespace CT.ERP.Client.Entity
{
    [DMTable("ct_sys_dictionary", "dicid", true)]
    public class SysDictEntity
    {
        public int dicid { get; set; }
        public string dictype { get; set; }
        public string dictvalue { get; set; }
    }
}
