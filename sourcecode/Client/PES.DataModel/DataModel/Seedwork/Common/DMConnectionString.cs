using System;
using System.IO;
using PES.DataModel;
using PES.DataModel.Extends;
using PES.DataModel.Helpers;

namespace PES.DataModel
{
    internal class DMConnectionString
    {
        private static SafeDictionary<Type, string> connectionStrings = new SafeDictionary<Type, string>();
        private static string defaultConnectionString;
        private static object o = new object();

        public static string DefaultConnectionString
        {
            get
            {
                if (defaultConnectionString.IsNullOrEmpty())
                {
                    defaultConnectionString = DbHelper.GetConnectString("SelpConnectionStr");
                    if(defaultConnectionString.Length==0)
                        throw new NullReferenceException("未指定数据库!");
                }
                return defaultConnectionString;
            }
        }


        public static string GetConnectionString<T>()
        {
            Type type = typeof(T);
            TableMapping tm = type.GetTableMapping();
            if (!tm.IsUseCustomConnection)
            {
                return DefaultConnectionString;
            }
            else
            {
                return TryGetConnectionString(type, () =>
                {
                    string key = tm.ConnectionKey.IsNullOrEmpty() ? string.Format("{0}ConnectionString", type.Name) : tm.ConnectionKey;
                    return DbHelper.GetConnectString(key);
                });
            }
        }

        public static void SetDefaultConnectionString(string connectionString)
        {
            lock (o)
            {
                defaultConnectionString = connectionString;
            }
        }

        private static string TryGetConnectionString(Type key, Func<string> func)
        {
            string connectionString = string.Empty;
            if (!connectionStrings.TryGetValue(key, out connectionString))
            {
                connectionString = func();
                connectionStrings[key] = connectionString;
            }
            return connectionString;
        }
    }
}