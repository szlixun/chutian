using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq.Expressions;
using PES.DataModel;

namespace PES.DataModel
{
    public class AccessExpressionTSQLTranslator : AbstractTranslator
    {
        #region CreateTSQL

        public override TranResult CreateSelectPageListTSQL<TEntity>(int pageIndex, int pageSize, LambdaExpression where, LambdaExpression order = null, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
            if (pageIndex <= 1)
            {
                return base.CreateSelectListTSQL<TEntity>(pageSize, where, order, select, group, join);
            }

            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT TOP " + pageSize + " ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.Append(" WHERE ");
            this.Append(tr.TableMapping.AliasName + ".");
            this.Append(tr.TableMapping.PrimaryKey.Name + " NOT IN(SELECT TOP ");
            this.Append(pageIndex* pageSize + " ");
            this.Append(tr.TableMapping.AliasName + ".");
            this.Append(tr.TableMapping.PrimaryKey.Name + " ");
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.VisitGroupByExpression(group);
            this.VisitOrderByExpression(order);
            this.Append(")");
            if (where != null) { this.Append(" AND "); this.Visit(where); }
            this.VisitGroupByExpression(group);
            this.VisitOrderByExpression(order);
            return this.GetTranResult(tr);
        }

        public override string GetIdentitySQL(string tableName = null)
        {
            return "SELECT @@IDENTITY;";
        }

        #endregion CreateTSQL

        #region VisitExpression

        public override void VisitTableExpression(TranResult tr, List<JoinTable> join = null)
        {
            if (join != null && join.Count > 0)
            {
                this.Append(" FROM ");
                for (int i = 0; i < join.Count; i++)
                {
                    this.Append("(");
                }

                this.Append(tr.TableMapping.Name + " ");
                this.Append(" AS " + tr.TableMapping.AliasName + " ");

                foreach (var item in join)
                {
                    Type right = item.Expression.Parameters[1].Type;
                    TableMapping rightMt = right.GetTableMapping();
                    this.Append(item.JoinType.ToUpper() + " ");
                    this.Append(rightMt.Name + " ");
                    this.Append(" AS " + rightMt.AliasName + " ");
                    this.Append(" ON ");
                    BinaryExpression be = item.Expression.Body as BinaryExpression;
                    this.Visit(be.Left);
                    this.Append(" = ");
                    this.Visit(be.Right);
                    this.Append(") ");
                }
            }
            else
            {
                this.Append(" FROM " + tr.TableMapping.Name + " ");
                this.Append(" AS " + tr.TableMapping.AliasName + " ");
            }
        }

        protected override void CreateParameter(object obj)
        {
            //生成参数 @p1
            string paramName = "@p" + paramIndex++;

            //设置参数
            base.parameter.Add(new OleDbParameter(paramName, obj));
            this.Append(paramName);
        }

        #endregion VisitExpression
    }
}