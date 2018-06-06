using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PES.DataModel
{
    public class Spec<T1>
    {
        private Expression<Func<T1, bool>> where;

        public Spec()
        {
        }

        public Expression<Func<T1, bool>> Exp
        {
            get
            {
                return this.where;
            }
        }

        public TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.where);
                }
            }
        }

        public Spec<T1> And(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1> Or(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }
    }

    public class Spec<T1, T2>
    {
        private Expression<Func<T1, T2, bool>> where;

        public Spec()
        {
        }

        public Expression<Func<T1, T2, bool>> Exp
        {
            get
            {
                return this.where;
            }
        }

        public TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.where, new List<Type> { typeof(T2) });
                }
            }
        }

        public Spec<T1, T2> And(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2> And(Expression<Func<T1, T2, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2> Or(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2> Or(Expression<Func<T1, T2, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }

        private List<ParameterExpression> GetParameters(Expression<Func<T1, bool>> exp)
        {
            List<ParameterExpression> list = new List<ParameterExpression>();
            var p2 = Expression.Parameter(typeof(T2), typeof(T2).Name.Substring(0, 1));
            list.AddRange(exp.Parameters);
            list.Add(p2);
            return list;
        }
    }

    public class Spec<T1, T2, T3>
    {
        private Expression<Func<T1, T2, T3, bool>> where;

        public Spec()
        {
        }

        public Expression<Func<T1, T2, T3, bool>> Exp
        {
            get
            {
                return this.where;
            }
        }

        public TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.where, new List<Type> { typeof(T2), typeof(T3) });
                }
            }
        }

        public Spec<T1, T2, T3> And(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3> And(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3> Or(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3> Or(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }

        private List<ParameterExpression> GetParameters(Expression<Func<T1, bool>> exp)
        {
            List<ParameterExpression> list = new List<ParameterExpression>();
            var p2 = Expression.Parameter(typeof(T2), typeof(T2).Name.Substring(0, 1));
            var p3 = Expression.Parameter(typeof(T3), typeof(T3).Name.Substring(0, 1));
            list.AddRange(exp.Parameters);
            list.Add(p2);
            list.Add(p3);
            return list;
        }
    }

    public class Spec<T1, T2, T3, T4>
    {
        private Expression<Func<T1, T2, T3, T4, bool>> where;

        public Spec()
        {
        }

        public Expression<Func<T1, T2, T3, T4, bool>> Exp
        {
            get
            {
                return this.where;
            }
        }

        public TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.where, new List<Type> { typeof(T2), typeof(T3), typeof(T4) });
                }
            }
        }

        public Spec<T1, T2, T3, T4> And(Expression<Func<T1, T2, T3, T4, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4> And(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4> Or(Expression<Func<T1, T2, T3, T4, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4> Or(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }

        private List<ParameterExpression> GetParameters(Expression<Func<T1, bool>> exp)
        {
            List<ParameterExpression> list = new List<ParameterExpression>();
            var p2 = Expression.Parameter(typeof(T2), typeof(T2).Name.Substring(0, 1));
            var p3 = Expression.Parameter(typeof(T3), typeof(T3).Name.Substring(0, 1));
            var p4 = Expression.Parameter(typeof(T4), typeof(T4).Name.Substring(0, 1));
            list.AddRange(exp.Parameters);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            return list;
        }
    }

    public class Spec<T1, T2, T3, T4, T5>
    {
        private Expression<Func<T1, T2, T3, T4, T5, bool>> where;

        public Spec()
        {
        }

        public Expression<Func<T1, T2, T3, T4, T5, bool>> Exp
        {
            get
            {
                return this.where;
            }
        }

        public TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.where, new List<Type> { typeof(T2), typeof(T3), typeof(T4), typeof(T5) });
                }
            }
        }

        public Spec<T1, T2, T3, T4, T5> And(Expression<Func<T1, T2, T3, T4, T5, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4, T5> And(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(Expression.And(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4, T5> Or(Expression<Func<T1, T2, T3, T4, T5, bool>> exp)
        {
            if (where == null)
            {
                this.where = exp;
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public Spec<T1, T2, T3, T4, T5> Or(Expression<Func<T1, bool>> exp)
        {
            if (where == null)
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(exp.Body, GetParameters(exp));
            }
            else
            {
                this.where = Expression.Lambda<Func<T1, T2, T3, T4, T5, bool>>(Expression.Or(this.where.Body, exp.Body), this.where.Parameters);
            }
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }

        private List<ParameterExpression> GetParameters(Expression<Func<T1, bool>> exp)
        {
            List<ParameterExpression> list = new List<ParameterExpression>();
            var p2 = Expression.Parameter(typeof(T2), typeof(T2).Name.Substring(0, 1));
            var p3 = Expression.Parameter(typeof(T3), typeof(T3).Name.Substring(0, 1));
            var p4 = Expression.Parameter(typeof(T4), typeof(T4).Name.Substring(0, 1));
            var p5 = Expression.Parameter(typeof(T5), typeof(T5).Name.Substring(0, 1));
            list.AddRange(exp.Parameters);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            list.Add(p5);
            return list;
        }
    }
}