using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using PES.DataModel;

namespace PES.DataModel
{
    public class MySqlExpressionTSQLTranslator : AbstractTranslator
    {
        #region Create TSQL

        public override TranResult CreateAggregateTSQL<TEntity>(LambdaExpression where, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.VisitGroupByExpression(group);
            return this.GetTranResult(tr);
        }

        public override TranResult CreateSelectListTSQL<TEntity>(int top, LambdaExpression where, LambdaExpression order = null, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.VisitGroupByExpression(group);
            this.VisitOrderByExpression(order);
            if (top != 0) { this.Append(" LIMIT 0," + top); }
            return this.GetTranResult(tr);
        }

        public override TranResult CreateSelectPageListTSQL<TEntity>(int pageIndex, int pageSize, LambdaExpression where, LambdaExpression order = null, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.VisitGroupByExpression(group);
            this.VisitOrderByExpression(order);
            this.Append(" LIMIT " + pageIndex * pageSize + "," + pageSize);
            return this.GetTranResult(tr);
        }

        public override TranResult CreateSelectTSQL<TEntity>(LambdaExpression where, LambdaExpression select = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.Append(" LIMIT 0,1");
            return this.GetTranResult(tr);
        }

        public override string GetIdentitySQL(string tableName = null)
        {
            return ";SELECT LAST_INSERT_ID();";
        }

        #endregion Create TSQL

        #region VisitExpression

        protected override void CreateParameter(object obj)
        {
            //生成参数 ?p1
            string paramName = "?p" + paramIndex++;

            //设置参数
            base.parameter.Add(new MySqlParameter(paramName, obj));
            this.Append(paramName);
        }

        protected override Expression VisitAggregateMethod(MethodCallExpression m)
        {
            if (m.Method.Name == "DMCount")
            {
                this.Append(m.Method.Name.ToUpper().Substring(2));
                this.Append("(");
                if (m.Arguments.Count > 1)
                {
                    ConstantExpression ce = m.Arguments[1] as ConstantExpression;
                    this.Append(ce.Value);
                }
                else
                {
                    this.Append("*");
                }
                this.Append(") ");
            }
            else if (m.Method.Name == "DMSum" || m.Method.Name == "DMMax" || m.Method.Name == "DMMin")
            {
                this.Append(m.Method.Name.ToUpper().Substring(2));
                this.Append("(CAST(");
                this.Visit(m.Arguments[0]);
                this.Append(" AS DECIMAL(65,2))) ");
            }
            else if (m.Method.Name == "DMAverage")
            {
                this.Append("AVG");
                this.Append("(CAST(");
                this.Visit(m.Arguments[0]);
                this.Append(" AS DECIMAL(65,2))) ");
            }
            else if (m.Method.Name == "As")
            {
                string name = string.Empty;
                this.Visit(m.Arguments[0]);
                this.Append(" AS ");
                name = Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke().ToString();
                this.Append(name);
                this.Append(" ");
            }
            return m;
        }

        protected override Expression VisitInMethod(MethodCallExpression m)
        {
            string arrayStr = Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke().ToString();
            this.Append("(");

            if (m.Method.Name == "In")
            {
                this.Append(" FIND_IN_SET");
            }
            else
            {
                this.Append(" !FIND_IN_SET");
            }
            this.Append("(");
            this.Visit(m.Arguments[0]);
            this.Append(",");
            ConstantExpression ce = Expression.Constant(arrayStr);
            this.Visit(ce);
            this.Append("))");
            return m;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType != ExpressionType.Parameter && m.Expression.NodeType != ExpressionType.Constant)
            {
                this.Append(m.Member.Name.ToUpper());
                this.Append("(");
                this.Visit(m.Expression);
                this.Append(")");
            }
            else
            {
                this.Append(m.Member.DeclaringType.GetTableMapping().AliasName + ".");
                this.Append(m.Member.Name);
            }
            return m;
        }

        #endregion VisitExpression
    }
}