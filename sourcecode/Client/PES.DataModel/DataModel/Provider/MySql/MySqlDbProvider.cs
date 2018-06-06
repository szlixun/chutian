using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using PES.DataModel.Extends;
using PES.DataModel.Helpers;

namespace PES.DataModel
{
    public class MySqlDbProvider : AbstractProvider
    {
        public override IDbDataParameter CreateParameter(string name, object value)
        {
            return new MySqlParameter("?" + name, value);
        }

        public override IDataReader ExecuteDataReader(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteReader((MySqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override DataSet ExecuteDataSet(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteDataSet((MySqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override DataTable ExecuteDataTable(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteDataTable((MySqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override int ExecuteNonQuery(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteNonQuery(trans as MySqlTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray()).ToInt();
        }

        public override int ExecuteNonQuery(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteNonQuery((MySqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteScalar(trans as MySqlTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbMySqlHelper.ExecuteScalar((MySqlConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<MySqlParameter>().ToArray());
        }

        public override IDbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}