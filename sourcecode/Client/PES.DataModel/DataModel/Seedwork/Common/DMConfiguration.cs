using PES.DataModel.Helpers;

namespace PES.DataModel
{
    internal class DMConfiguration
    {
        public static string DefaultConnectionString
        {
            get
            {
                return DMConnectionString.DefaultConnectionString;
            }
        }

        public static EnumProviderType ProviderType
        {
            get
            {
                return DMProviderType.ProviderType;
            }
        }

        public static string GetConnectionString<T>()
        {
            return DMConnectionString.GetConnectionString<T>();
        }

        public static void SetDefaultConnectionString(string connectionString)
        {
            DMConnectionString.SetDefaultConnectionString(connectionString);
            DMProviderType.SetProviderType(connectionString);
        }
    }
}