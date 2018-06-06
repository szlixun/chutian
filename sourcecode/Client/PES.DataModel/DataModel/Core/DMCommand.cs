using PES.DataModel.Extends;
using PES.DataModel;

namespace PES.DataModel
{
    public class ADMCommand<T>
    {
        private string connectionString;

        protected virtual string ConnectionString
        {
            get
            {
                if (this.connectionString.IsNullOrEmpty())
                {
                    connectionString = DMConfiguration.GetConnectionString<T>();
                }
                return connectionString;
            }
            set { connectionString = value; }
        }
    }
}