using System.Data.SqlClient;

namespace PES.DataModel
{
    public class MsSqlExpressionTSQLTranslator : AbstractTranslator
    {
        #region CreateTSQL

        public override string GetIdentitySQL(string tableName = null)
        {
            return ";SELECT SCOPE_IDENTITY();";
        }

        #endregion CreateTSQL

        #region VisitExpression

        protected override void CreateParameter(object obj)
        {
            //生成参数 @p1
            string paramName = "@p" + paramIndex++;

            //设置参数
            base.parameter.Add(new SqlParameter(paramName, obj));
            this.Append(paramName);
        }

        #endregion VisitExpression
    }
}