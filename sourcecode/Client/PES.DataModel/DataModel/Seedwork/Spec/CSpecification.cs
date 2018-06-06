using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PES.DataModel.Extends;

namespace PES.DataModel
{
    public class CSpec<T1>
    {
        protected ColumnsExpression ces = new ColumnsExpression();
        private List<CSpecProperty> spis = new List<CSpecProperty>();

        public CSpec()
        {
            this.AddSpecProperty(typeof(T1));
        }

        public Expression<Func<T1, Columns>> Exp
        {
            get
            {
                return ces.LambdaExpression as Expression<Func<T1, Columns>>;
            }
        }

        public virtual TranResult TranResult
        {
            get
            {
                using (var translator = DMObjectContainer.GetTSQLTranslator())
                {
                    return translator.CreateSpecTSQL<T1>(this.Exp);
                }
            }
        }

        public CSpec<T1> And<TKey>(Expression<Func<T1, TKey>> exp)
        {
            this.ces.Add(exp);
            return this;
        }

        public CSpec<T1> And(string name)
        {
            CSpecProperty sp = this.GetSpecProperty(name);
            ParameterExpression p = Expression.Parameter(sp.Property.DeclaringType, "p");
            Expression e = Expression.MakeMemberAccess(p, sp.Property);
            var exp = Expression.Lambda<Func<T1, object>>(Expression.Convert(e, typeof(object)), p);
            this.ces.Add(exp);
            return this;
        }

        public CSpec<T1> And(string name, string orderType)
        {
            string methodName = "Desc";
            if (orderType.IsNullOrEmpty()) orderType = "Asc";
            if ("ASC".Equals(orderType.ToUpper())) methodName = "Asc";
            CSpecProperty sp = this.GetSpecProperty(name);
            ParameterExpression p = Expression.Parameter(sp.Property.DeclaringType, "p");
            Expression e = Expression.MakeMemberAccess(p, sp.Property);
            MethodCallExpression mc = Expression.Call(null, typeof(MethodExtend).GetMethod(methodName).MakeGenericMethod(sp.Property.PropertyType), e);
            var exp = Expression.Lambda(mc, p);
            this.ces.Add(exp);
            return this;
        }

        public override string ToString()
        {
            return this.TranResult.CmdText;
        }

        protected void AddSpecProperty(Type t)
        {
            if (t == null) return;
            var tr = t.GetTableMapping();
            PropertyInfo[] pis = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo pi in pis)
            {
                if (pi.IsIgnore())
                    continue;

                this.spis.Add(new CSpecProperty { TableName = tr.Name, Name = pi.Name, Property = pi });
            }
        }

        protected CSpecProperty GetSpecProperty(string name)
        {
            CSpecProperty sp;
            if (name.IsNullOrEmpty()) throw new ArgumentNullException("参数不能为空");

            if (name.IndexOf(".") > 0)
            {
                var names = name.Split('.');
                sp = spis.Where(p => p.TableName == names[0] && p.Name == names[1]).FirstOrDefault();
            }
            else
            {
                sp = spis.Where(p => p.Name == name).FirstOrDefault();
            }

            if (sp == null) throw new NullReferenceException(string.Format("没有找到属性{0}", name[1]));
            return sp;
        }
    }

    public class CSpecProperty
    {
        public string Name { get; set; }

        public PropertyInfo Property { get; set; }

        public string TableName { get; set; }
    }
}