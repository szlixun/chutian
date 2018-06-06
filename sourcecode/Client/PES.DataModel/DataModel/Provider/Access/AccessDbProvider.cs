using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using PES.DataModel.Extends;
using PES.DataModel.Helpers;

namespace PES.DataModel
{
    public class AccessDbProvider : AbstractProvider
    {
        public override IDbDataParameter CreateParameter(string name, object value)
        {
            return new OleDbParameter("@" + name, value);
        }

        public override IDataReader ExecuteDataReader(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteReader((OleDbConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override DataSet ExecuteDataSet(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteDataSet((OleDbConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override DataTable ExecuteDataTable(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteDataTable((OleDbConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override int ExecuteNonQuery(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteNonQuery(trans as OleDbTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray()).ToInt();
        }

        public override int ExecuteNonQuery(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteNonQuery((OleDbConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbTransaction trans, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteScalar(trans as OleDbTransaction, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override object ExecuteScalar(IDbConnection connection, string cmdText, IList<IDbDataParameter> parameter)
        {
            return DbAccessHelper.ExecuteScalar((OleDbConnection)connection, CommandType.Text, cmdText, parameter == null ? null : parameter.Cast<OleDbParameter>().ToArray());
        }

        public override IDbConnection GetConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }
    }
}