using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PES.DataModel
{
    #region Common

    public class PageList<T>
    {
        public PageList()
        {
            this.List = new List<T>();
        }

        public int Index { get; set; }

        public IList<T> List { get; set; }

        public int Size { get; set; }

        public int Total { get; set; }
    }

    #endregion Common

    #region Expression

    public class Columns
    {
        public Columns(params object[] columns)
        {
        }
    }

    public class ColumnsExpression
    {
        public ColumnsExpression()
        {
            @params = new List<ParameterExpression>();
            expressions = new List<Expression>();
        }

        public LambdaExpression LambdaExpression
        {
            get
            {
                if (this.expressions.Count == 0) return null;
                var ctors = typeof(Columns).GetConstructors();
                var pse = Expression.NewArrayInit(typeof(object), this.expressions);
                var body = Expression.New(ctors.First(), pse);
                return LambdaExpression.Lambda(body, this.@params.ToArray());
            }
        }

        private List<ParameterExpression> @params { get; set; }

        private List<Expression> expressions { get; set; }

        public void Add(LambdaExpression lambda)
        {
            if (lambda == null) return;

            if (lambda.Body.NodeType == ExpressionType.New)
            {
                var ne = lambda.Body as NewExpression;
                var nee = ne.Arguments[0] as NewArrayExpression;
                this.AddParameter(lambda.Parameters.ToList());
                this.AddExpression(nee.Expressions.ToList());
            }
            else
            {
                this.AddParameter(lambda.Parameters.ToList());
                if (lambda.Body.Type.IsValueType)
                {
                    this.AddExpression(Expression.Convert(lambda.Body, typeof(object)));
                }
                else
                {
                    this.AddExpression(lambda.Body);
                }
            }
        }

        private void AddExpression(Expression exp)
        {
            this.expressions.Add(exp);
        }

        private void AddExpression(List<Expression> exps)
        {
            this.expressions.AddRange(exps);
        }

        private void AddParameter(ParameterExpression param)
        {
            if (this.@params.Where(p => p.Name == param.Name && p.Type == param.Type).Count() == 0)
            {
                this.@params.Add(param);
            }
        }

        private void AddParameter(List<ParameterExpression> @params)
        {
            foreach (var item in @params)
            {
                this.AddParameter(item);
            }
        }
    }

    public class JoinExpression
    {
        private List<JoinTable> lambdaExpression;

        public JoinExpression()
        {
            lambdaExpression = new List<JoinTable>();
        }

        public List<JoinTable> LambdaExpression
        {
            get
            {
                if (lambdaExpression.Count == 0) return null;
                return lambdaExpression;
            }
        }

        public void Add(LambdaExpression lambda, string joinType = "left join")
        {
            lambdaExpression.Add(new JoinTable() { JoinType = joinType, Expression = lambda });
        }
    }

    public class JoinTable
    {
        public LambdaExpression Expression { get; set; }

        public string JoinType { get; set; }
    }

    public class WhereExpression
    {
        private LambdaExpression lambdaExpression;

        public LambdaExpression LambdaExpression { get { return lambdaExpression; } }

        public void And(LambdaExpression where)
        {
            if (lambdaExpression == null)
            {
                lambdaExpression = where;
            }
            else
            {
                var @params = lambdaExpression.Parameters.Concat(where.Parameters);
                lambdaExpression = Expression.Lambda(Expression.And(lambdaExpression.Body, where.Body), @params.ToArray());
            }
        }

        public void Or(LambdaExpression where)
        {
            if (lambdaExpression == null)
            {
                lambdaExpression = where;
            }
            else
            {
                var @params = lambdaExpression.Parameters.Concat(where.Parameters);
                lambdaExpression = Expression.Lambda(Expression.Or(lambdaExpression.Body, where.Body), @params.ToArray());
            }
        }
    }

    #endregion Expression

    #region Query

    public class AQuery<T1, TResult> : ADMCommand<T1>
    {
        protected ColumnsExpression group = new ColumnsExpression();
        protected JoinExpression join = new JoinExpression();
        protected ColumnsExpression order = new ColumnsExpression();
        protected ColumnsExpression select = new ColumnsExpression();
        protected WhereExpression where = new WhereExpression();

        #region ConnectionString

        public AQuery<T1, TResult> SetConnectionString(string connectionString)
        {
            base.ConnectionString = connectionString;
            return this;
        }

        #endregion ConnectionString

        #region Excute

        public TResult Excute()
        {
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                using (TranResult tr = translator.CreateAggregateTSQL<T1>(where.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression))
                {
                    using (ATSqlCommand cmd = new ATSqlCommand())
                    {
                        cmd.SetCmdText(tr.CmdText);
                        cmd.SetParameters(tr.Parameter.ToArray());
                        cmd.SetConnectionString(base.ConnectionString);
                        return cmd.ExecuteScalar<TResult>();
                    }
                }
            }
        }

        public virtual TResult Single()
        {
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                using (TranResult tr = translator.CreateSelectTSQL<T1>(where.LambdaExpression, select.LambdaExpression, join.LambdaExpression))
                {
                    using (ATSqlCommand cmd = new ATSqlCommand())
                    {
                        cmd.SetCmdText(tr.CmdText);
                        cmd.SetParameters(tr.Parameter.ToArray());
                        cmd.SetConnectionString(base.ConnectionString);
                        return cmd.ToEntity<TResult>();
                    }
                }
            }
        }
        public virtual List<TResult> ToList()
        {
            return this.ToList(0);
        }

        public virtual List<TResult> ToList(int top)
        {
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                using (TranResult tr = translator.CreateSelectListTSQL<T1>(top, where.LambdaExpression, order.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression))
                {
                    using (ATSqlCommand cmd = new ATSqlCommand())
                    {
                        cmd.SetCmdText(tr.CmdText);
                        cmd.SetParameters(tr.Parameter.ToArray());
                        cmd.SetConnectionString(base.ConnectionString);
                        return cmd.ToList<TResult>();
                    }
                }
            }
        }

        public virtual PageList<TResult> ToPageList()
        {
            return this.ToPageList(0, 15);
        }

        public virtual PageList<TResult> ToPageList(int pageIndex, int pageSize)
        {
            PageList<TResult> pl = new PageList<TResult>();
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                Expression<Func<T1, int>> count = (p) => p.DMCount("1");
                using (TranResult countTr = translator.CreateAggregateTSQL<T1>(where.LambdaExpression, count, group.LambdaExpression, join.LambdaExpression))
                {
                    using (ATSqlCommand countCmd = new ATSqlCommand())
                    {
                        countCmd.SetCmdText(countTr.CmdText);
                        countCmd.SetParameters(countTr.Parameter.ToArray());
                        countCmd.SetConnectionString(base.ConnectionString);
                        if (group.LambdaExpression != null)
                        {
                            pl.Total = countCmd.ToDataTable().Rows.Count;
                        }
                        else
                        {
                            pl.Total = countCmd.ExecuteScalar<int>();
                        }
                        pl.Index = pageIndex;
                        pl.Size = pageSize;
                    }
                }

                if (pl.Total != 0)
                {
                    using (TranResult tr = translator.CreateSelectPageListTSQL<T1>(pageIndex, pageSize, where.LambdaExpression, order.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression))
                    {
                        using (ATSqlCommand cmd = new ATSqlCommand())
                        {
                            cmd.SetCmdText(tr.CmdText);
                            cmd.SetParameters(tr.Parameter.ToArray());
                            cmd.SetConnectionString(base.ConnectionString);
                            pl.List = cmd.ToList<TResult>();
                        }
                    }
                }
                return pl;
            }
        }

        #endregion Query

        #region Debug

        public TranResult ExcuteDebug
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSelectListTSQL<T1>(0, where.LambdaExpression, order.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression);
                }
            }
        }

        public TranResult ListDebug
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSelectListTSQL<T1>(0, where.LambdaExpression, order.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression);
                }
            }
        }

        public TranResult PageListDebug
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSelectPageListTSQL<T1>(0, 15, where.LambdaExpression, order.LambdaExpression, select.LambdaExpression, group.LambdaExpression, join.LambdaExpression);
                }
            }
        }

        public TranResult SingleDebug
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSelectTSQL<T1>(where.LambdaExpression, select.LambdaExpression, join.LambdaExpression);
                }
            }
        }

        public override string ToString()
        {
            return this.ExcuteDebug.CmdText;
        }

        #endregion Debug
    }

    public class Query<T1> : AQuery<T1, T1>
    {
        #region Where

        public Query<T1> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select
    }

    public class Query<T1, TResult> : AQuery<T1, TResult>
    {
        #region Where

        public Query<T1, TResult> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, TResult> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1, TResult> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1, TResult> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1, TResult> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, TResult> Select<TSelect>(Expression<Func<T1, TSelect>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select
    }

    public class Query<T1, T2, TResult> : AQuery<T1, TResult>
    {
        #region Where

        public Query<T1, T2, TResult> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, TResult> Or(Expression<Func<T1, T2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, TResult> Or<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, TResult> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, TResult> Where(Expression<Func<T1, T2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, TResult> Where<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1, T2, TResult> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, TResult> OrderBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, TResult> OrderBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1, T2, TResult> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, TResult> GroupBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, TResult> GroupBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1, T2, TResult> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, TResult> Select(Expression<Func<T1, T2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, TResult> Select<TS1>(Expression<Func<TS1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select

        #region Join

        public Query<T1, T2, TResult> InnerJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, TResult> LeftJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, TResult> RightJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        #endregion Join
    }

    public class Query<T1, T2, T3, TResult> : AQuery<T1, TResult>
    {
        #region Where

        public Query<T1, T2, T3, TResult> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);

            return this;
        }

        public Query<T1, T2, T3, TResult> Or(Expression<Func<T1, T2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, TResult> Or(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, TResult> Or<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.Or(where);

            return this;
        }

        public Query<T1, T2, T3, TResult> Or<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, TResult> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);

            return this;
        }

        public Query<T1, T2, T3, TResult> Where(Expression<Func<T1, T2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, TResult> Where(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, TResult> Where<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.And(where);

            return this;
        }

        public Query<T1, T2, T3, TResult> Where<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1, T2, T3, TResult> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> OrderBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> OrderBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> OrderBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> OrderBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1, T2, T3, TResult> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> GroupBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> GroupBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> GroupBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, TResult> GroupBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1, T2, T3, TResult> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, TResult> Select(Expression<Func<T1, T2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, TResult> Select(Expression<Func<T1, T2, T3, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, TResult> Select<TS1>(Expression<Func<TS1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, TResult> Select<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select

        #region Join

        public Query<T1, T2, T3, TResult> InnerJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, TResult> InnerJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, TResult> LeftJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, TResult> LeftJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, TResult> RightJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        public Query<T1, T2, T3, TResult> RightJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        #endregion Join
    }

    public class Query<T1, T2, T3, T4, TResult> : AQuery<T1, TResult>
    {
        #region Where

        public Query<T1, T2, T3, T4, TResult> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or(Expression<Func<T1, T2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or(Expression<Func<T1, T2, T3, T4, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Or<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where(Expression<Func<T1, T2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where(Expression<Func<T1, T2, T3, T4, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Where<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1, T2, T3, T4, TResult> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy(Expression<Func<T1, T2, T3, T4, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> OrderBy<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1, T2, T3, T4, TResult> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy(Expression<Func<T1, T2, T3, T4, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> GroupBy<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1, T2, T3, T4, TResult> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select(Expression<Func<T1, T2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select(Expression<Func<T1, T2, T3, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select(Expression<Func<T1, T2, T3, T4, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select<TS1>(Expression<Func<TS1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> Select<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select

        #region Join

        public Query<T1, T2, T3, T4, TResult> InnerJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> InnerJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> LeftJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> LeftJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> RightJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        public Query<T1, T2, T3, T4, TResult> RightJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        #endregion Join
    }

    public class Query<T1, T2, T3, T4, T5, TResult> : AQuery<T1, TResult>
    {
        #region Where

        public Query<T1, T2, T3, T4, T5, TResult> Or(Expression<Func<T1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or(Expression<Func<T1, T2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or(Expression<Func<T1, T2, T3, T4, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or(Expression<Func<T1, T2, T3, T4, T5, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Or<TS1, TS2, TS3, TS4>(Expression<Func<TS1, TS2, TS3, TS4, bool>> where)
        {
            this.where.Or(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where(Expression<Func<T1, T2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where(Expression<Func<T1, T2, T3, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where(Expression<Func<T1, T2, T3, T4, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where<TS1>(Expression<Func<TS1, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where<TS1, TS2>(Expression<Func<TS1, TS2, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Where<TS1, TS2, TS3, TS4>(Expression<Func<TS1, TS2, TS3, TS4, bool>> where)
        {
            this.where.And(where);
            return this;
        }

        #endregion Where

        #region OrderBy

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy(Expression<Func<T1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy(Expression<Func<T1, T2, T3, T4, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy(Expression<Func<T1, T2, T3, T4, T5, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> OrderBy<TS1, TS2, TS3, TS4>(Expression<Func<TS1, TS2, TS3, TS4, Columns>> exp)
        {
            this.order.Add(exp);
            return this;
        }

        #endregion OrderBy

        #region GroupBy

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy(Expression<Func<T1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy(Expression<Func<T1, T2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy(Expression<Func<T1, T2, T3, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy(Expression<Func<T1, T2, T3, T4, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy(Expression<Func<T1, T2, T3, T4, T5, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy<TS1>(Expression<Func<TS1, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> GroupBy<TS1, TS2, TS3, TS4>(Expression<Func<TS1, TS2, TS3, TS4, Columns>> exp)
        {
            this.group.Add(exp);
            return this;
        }

        #endregion GroupBy

        #region Select

        public Query<T1, T2, T3, T4, T5, TResult> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select(Expression<Func<T1, T2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select(Expression<Func<T1, T2, T3, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select(Expression<Func<T1, T2, T3, T4, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select(Expression<Func<T1, T2, T3, T4, T5, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select<TS1>(Expression<Func<TS1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select<TS1, TS2>(Expression<Func<TS1, TS2, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select<TS1, TS2, TS3>(Expression<Func<TS1, TS2, TS3, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> Select<TS1, TS2, TS3, TS4>(Expression<Func<TS1, TS2, TS3, TS4, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        #endregion Select

        #region Join

        public Query<T1, T2, T3, T4, T5, TResult> InnerJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> InnerJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "inner join");
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> LeftJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> LeftJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "left join");
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> RightJoin<TRight>(Expression<Func<T1, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        public Query<T1, T2, T3, T4, T5, TResult> RightJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> on)
        {
            join.Add(on, "right join");
            return this;
        }

        #endregion Join
    }

    #endregion Query
}