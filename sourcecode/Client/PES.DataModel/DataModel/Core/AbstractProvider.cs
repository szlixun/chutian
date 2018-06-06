using System.Collections.Generic;
using System.Data;

namespace PES.DataModel
{
    public abstract class AbstractProvider
    {
        public abstract IDbDataParameter CreateParameter(string name, object value);

        public abstract IDataReader ExecuteDataReader(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract DataSet ExecuteDataSet(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract DataTable ExecuteDataTable(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract int ExecuteNonQuery(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract int ExecuteNonQuery(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract object ExecuteScalar(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract object ExecuteScalar(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter = null);

        public abstract IDbConnection GetConnection(string connectionString);
    }
}