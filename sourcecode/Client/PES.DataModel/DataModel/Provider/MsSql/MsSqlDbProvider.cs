using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PES.DataModel.Extends;
using PES.DataModel.Helpers;

namespace PES.DataModel
{
    public class MsSqlDbProvider : AbstractProvider
    {
        public override IDbDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter("@" + name, value);
        }

        public override IDataReader ExecuteDataReader(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteReader((SqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override DataSet ExecuteDataSet(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteDataSet((SqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override DataTable ExecuteDataTable(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteDataTable((SqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override int ExecuteNonQuery(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteNonQuery(trans as SqlTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray()).ToInt();
        }

        public override int ExecuteNonQuery(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteNonQuery((SqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteScalar(trans as SqlTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMsSqlHelper.ExecuteScalar((SqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<SqlParameter>().ToArray());
        }

        public override IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}