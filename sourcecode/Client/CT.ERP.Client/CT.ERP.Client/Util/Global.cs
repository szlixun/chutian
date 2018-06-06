using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.Util
{
    public class Global
    {
        public static Byte[] iv = new byte[] { 2, 0, 1, 3, 0, 6, 1, 9 };
        public static Byte[] key = new byte[] { 2, 0, 1, 4, 0, 6, 0, 1 };

        public static User LoginUser;
    }
}
