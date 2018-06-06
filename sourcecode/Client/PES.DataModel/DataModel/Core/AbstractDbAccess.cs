using System.Collections.Generic;
using System.Data;
using PES.DataModel;

namespace PES.DataModel
{
    public abstract class AbstractDbAccess : IDbAccess
    {
        #region 属性

        public string ConnectionString { get { return DMConfiguration.DefaultConnectionString; } }

        #endregion 属性

        #region CommandText

        public virtual ATSqlCommand GetTSqlCommand(string cmdText, IList<IDbDataParameter> parameter = null, IDbTransaction trans = null)
        {
            return DMContext.TSqlCommand(cmdText, parameter, trans);
        }

        #endregion CommandText
    }
}