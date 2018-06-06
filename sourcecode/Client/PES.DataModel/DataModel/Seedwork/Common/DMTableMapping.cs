using System;
using System.Linq;
using PES.DataModel;

namespace PES.DataModel
{
    internal static class DMTableMapping
    {
        //线程安全集合
        private static SafeDictionary<Type, TableMapping> tableMappings = new SafeDictionary<Type, TableMapping>();

        public static TableMapping GetTableMapping(Type type)
        {
            TableMapping tm = null;
            if (!tableMappings.TryGetValue(type, out tm))
            {
                tm = new TableMapping();
                tm.AliasName = "T" + type.Name.GetHashCode();
                tm.Name = type.Name;
                tm.PrimaryKey.Name = "ID";
                tm.PrimaryKey.IsIdentity = true;
                tm.PrimaryKey.IsPrimaryKey = true;
                var dma = type.GetCustomAttributes<DMTableAttribute>().FirstOrDefault();
                if (dma != null)
                {
                    tm.Name = dma.Name;
                    tm.PrimaryKey.Name = dma.PrimaryKey;
                    tm.PrimaryKey.IsIdentity = dma.IsIdentity;
                    tm.IsUseCustomConnection = dma.IsUseCustomConnection;
                    tm.ConnectionKey = dma.ConnectionKey;
                }

                tableMappings[type] = tm;
            }
            return tm;
        }
    }
}