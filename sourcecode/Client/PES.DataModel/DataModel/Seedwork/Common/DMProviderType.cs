using PES.DataModel.Helpers;

namespace PES.DataModel
{
    internal class DMProviderType
    {
        private static object o = new object();
        private static EnumProviderType? providerType;

        public static EnumProviderType ProviderType
        {
            get
            {
                if (!providerType.HasValue)
                {
                    //通过数据库连接字符串获取并设置数据库类型
                    providerType = DbHelper.GetProviderType(DMConnectionString.DefaultConnectionString);
                }
                return providerType.Value;
            }
        }

        public static void SetProviderType(string connectionString)
        {
            SetProviderType(DbHelper.GetProviderType(connectionString));
        }

        public static void SetProviderType(EnumProviderType type)
        {
            lock (o)
            {
                providerType = type;
                DMObjectContainer.RegisterProvider();
            }
        }
    }
}