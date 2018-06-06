using System;
using System.Configuration;
using PES.DataModel.Extends;

namespace PES.DataModel.Helpers
{
    public enum EnumProviderType
    {
        MySql = 0,
        MsSql = 1,
        Access = 2
    }

    internal static class DbHelper
    {
        public static EnumProviderType GetProviderType(string connectionString)
        {
            var providerType = GetAppSetting("DMProviderType");

            if (providerType.IsNotNullAndEmpty())
            {
                try
                {
                    return (EnumProviderType)Enum.Parse(typeof(EnumProviderType), providerType, true);
                }
                catch
                {
                    return EnumProviderType.MySql;
                }
            }
            else
            {
                return EnumProviderType.MySql;
            }
        }

        public static string GetConnectString(string key)
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings[key];
            if (connectionSettings == null) throw new NullReferenceException("数据库配置错误!ConfigurationManager.ConnectionStrings[\"" + key + "\"]==null");
            return connectionSettings.ConnectionString;
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}