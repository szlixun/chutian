using System.Collections.Generic;
using System.Data;

namespace PES.DataModel
{
    public interface IDbAccess
    {
        string ConnectionString { get; }

        ATSqlCommand GetTSqlCommand(string cmdText, IList<IDbDataParameter> parameter = null, IDbTransaction trans = null);
    }
}