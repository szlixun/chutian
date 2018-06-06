using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using PES.DataModel.Extends;
using PES.DataModel;

namespace PES.DataModel
{
    public abstract class AbstractTranslator : AbstractVisitor, IDisposable
    {
        #region 字段

        protected List<DbParameter> parameter;
        protected int paramIndex = 0;
        protected StringBuilder sb;
        protected int tableIndex = 0;
        protected List<TableName> tableNames;

        #endregion 字段

        #region 构造函数

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~AbstractTranslator()
        {
            Dispose(false);
        }

        #endregion 构造函数

        #region 保护方法

        public string GetTableAliasName(string name)
        {
            var tableName = this.tableNames.Where(p => p.Name == name).FirstOrDefault();
            return tableName == null ? name : tableName.AliasName;
        }

        protected void AddTable(TableMapping tms)
        {
            if (tms == null) return;
            if (this.tableNames.Where(p => p.AliasName.Equals(tms.AliasName)).FirstOrDefault() == null)
            {
                this.tableNames.Add(new TableName() { Name = tms.Name, AliasName = tms.AliasName, Index = tableIndex++ });
            }
        }

        protected void AddTable(List<JoinTable> join)
        {
            if (join == null) return;
            foreach (var item in join)
            {
                Type right = item.Expression.Parameters[1].Type;
                TableMapping rightMt = right.GetTableMapping();
                this.AddTable(rightMt);
            }
        }

        protected TranResult GetModifyTranResult(TranResult tr)
        {
            foreach (var table in this.tableNames)
            {
                this.sb.Replace(table.AliasName + ".", "");
            }
            tr.Parameter = this.parameter;
            tr.TableNames = this.tableNames;
            tr.CmdText = this.sb.ToString();
            return tr;
        }

        protected TranResult GetTranResult(TranResult tr)
        {
            foreach (var table in this.tableNames)
            {
                this.sb.Replace(table.AliasName, "t" + table.Index);
            }
            tr.Parameter = this.parameter;
            tr.TableNames = this.tableNames;
            tr.CmdText = this.sb.ToString();
            return tr;
        }

        protected void Reset()
        {
            this.sb = new StringBuilder();
            this.parameter = new List<DbParameter>();
            this.tableNames = new List<TableName>();
            this.paramIndex = 0;
            this.tableIndex = 0;
        }

        #endregion 保护方法

        #region IDispose 成员

        /// <summary>
        /// 一个类型的Dispose方法应该允许被多次调用而不抛异常。鉴于这个原因，类型内部维护了一个私有的布尔型变量disposed
        /// </summary>
        private bool disposed = false;

        public void Dispose()
        {
            //用户显式调用清理
            Dispose(true);

            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 之所以提供这样一个受保护的虚方法，是为了考虑到这个类型会被其他类继承的情况。如果类型存在一个子类，
        /// 子类也许会实现自己的Dispose模式。受保护的虚方法用来提醒子类必须在实现自己的清理方法的时候注意到父类的清理工作，即子类需要在自己的释放方法中调用base.Dispose方法。
        /// 还有，我们应该已经注意到了真正撰写资源释放代码的那个虚方法是带有一个布尔参数的。
        /// 之所以提供这个参数，是因为我们在资源释放时要区别对待托管资源和非托管资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //该函数为手动调用，此处可进行托管资源的清理
                    //比如此类中有一个类型为 DataSet 的变量 ds
                    //此处可调用该对象的 Dispose 方法来清理托管资源
                    this.parameter = null;
                    this.sb = null;
                    this.tableNames = null;
                    this.paramIndex = 0;
                    this.tableIndex = 0;
                }

                //进行非托管资源的清理
                //非托管的资源主要为一些用 API 打开的文件句柄，设备场景句柄等
                //该类资源 GC 是无法管理的，只能依靠程序员自已释放
                //不同的资源， 释放方法不一样
                //比如 释放文件句柄
                //CloseHandle(handle)
            }

            //让类型知道自己已经被释放
            disposed = true;
        }

        #endregion IDispose 成员

        #region Create TSQL

        public virtual TranResult CreateAggregateTSQL<TEntity>(LambdaExpression where, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
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

        public virtual TranResult CreateDeleteTSQL<TEntity>(LambdaExpression where)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.Append("DELETE ");
            this.Append("FROM " + tr.TableMapping.Name + " ");
            this.VisitWhereExpression(where);
            return this.GetModifyTranResult(tr);
        }

        public virtual TranResult CreateInsertTSQL<TEntity>(TEntity entity, LambdaExpression select = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.Append("INSERT INTO ");
            this.Append(tr.TableMapping.Name);
            this.Append("(");
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (select != null)
            {
                NewExpression ue = select.Body as NewExpression;
                NewArrayExpression ne = ue.Arguments[0] as NewArrayExpression;

                foreach (var item in ne.Expressions)
                {
                    string name = string.Empty;

                    if (item.NodeType == ExpressionType.MemberAccess)
                    {
                        MemberExpression me = item as MemberExpression;
                        name = me.Member.Name;
                    }
                    else if (item.NodeType == ExpressionType.Convert)
                    {
                        UnaryExpression iue = item as UnaryExpression;
                        MemberExpression me = iue.Operand as MemberExpression;
                        name = me.Member.Name;
                    }

                    if (string.IsNullOrEmpty(name)) continue;

                    PropertyInfo pi = type.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

                    if (pi == null || pi.IsIgnore() || (pi.Name == tr.TableMapping.PrimaryKey.Name && tr.TableMapping.PrimaryKey.IsIdentity) || (!pi.PropertyType.IsValueType && pi.PropertyType != typeof(string)))
                        continue;

                    if (pi.PropertyType.IsEnum)
                    {
                        dic.Add(pi.Name, (int)Enum.Parse(pi.PropertyType, pi.GetValue(entity, null).ToString()));
                    }
                    else
                    {
                        dic.Add(pi.Name, pi.GetValue(entity, null));
                    }
                }
            }
            else
            {
                //不获取继承属性 为实例属性 为公开的
                PropertyInfo[] pis = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                foreach (var item in pis)
                {
                    if (item.IsIgnore() || (item.Name == tr.TableMapping.PrimaryKey.Name && tr.TableMapping.PrimaryKey.IsIdentity) || (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)))
                        continue;

                    if (item.PropertyType.IsEnum)
                    {
                        dic.Add(item.Name, (int)Enum.Parse(item.PropertyType, item.GetValue(entity, null).ToString()));
                    }
                    else
                    {
                        dic.Add(item.Name, item.GetValue(entity, null));
                    }
                }
            }

            int index = 0;
            int count = dic.Count;

            foreach (var item in dic)
            {
                this.Append(item.Key);
                if (index++ != count - 1)
                {
                    this.Append(",");
                }
            }

            this.Append(") VALUES (");
            index = 0;
            foreach (var item in dic)
            {
                this.CreateParameter(item.Value);
                if (index++ != count - 1)
                {
                    this.Append(",");
                }
            }

            this.Append(")");
            return this.GetTranResult(tr);
        }

        public virtual TranResult CreateSelectListTSQL<TEntity>(int top, LambdaExpression where, LambdaExpression order = null, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT ");
            if (top != 0) { this.Append("TOP " + top + " "); }
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            this.VisitGroupByExpression(group);
            this.VisitOrderByExpression(order);
            return this.GetTranResult(tr);
        }

        public virtual TranResult CreateSelectPageListTSQL<TEntity>(int pageIndex, int pageSize, LambdaExpression where, LambdaExpression order = null, LambdaExpression select = null, LambdaExpression group = null, List<JoinTable> join = null)
        {
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
            this.Append(pageIndex * pageSize + " ");
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

        public virtual TranResult CreateSelectTSQL<TEntity>(LambdaExpression where, LambdaExpression select = null, List<JoinTable> join = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.AddTable(join);
            this.Append("SELECT TOP 1 ");
            this.VisitSelectExpression(select);
            this.VisitTableExpression(tr, join);
            this.VisitWhereExpression(where);
            return this.GetTranResult(tr);
        }

        public virtual TranResult CreateSpecTSQL<TEntity>(LambdaExpression exp, List<Type> types = null, string prefix = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            if (types != null)
            {
                foreach (var t in types)
                {
                      this.AddTable(t.GetTableMapping());
                }
            }
            if (exp != null)
            {
                if (prefix.IsNotNullAndEmpty()) this.Append(prefix + " ");
                this.Visit(exp);
            }
            return this.GetTranResult(tr);
        }

        public virtual TranResult CreateUpdateTSQL<TEntity>(TEntity entity, LambdaExpression where = null, LambdaExpression select = null)
        {
            this.Reset();
            Type type = typeof(TEntity);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();
            this.AddTable(tr.TableMapping);
            this.Append("UPDATE ");
            this.Append(tr.TableMapping.Name);
            this.Append(" SET ");
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (select != null)
            {
                NewExpression ue = select.Body as NewExpression;
                NewArrayExpression ne = ue.Arguments[0] as NewArrayExpression;

                foreach (var item in ne.Expressions)
                {
                    string name = string.Empty;

                    if (item.NodeType == ExpressionType.MemberAccess)
                    {
                        MemberExpression me = item as MemberExpression;
                        name = me.Member.Name;
                    }
                    else if (item.NodeType == ExpressionType.Convert)
                    {
                        UnaryExpression iue = item as UnaryExpression;
                        MemberExpression me = iue.Operand as MemberExpression;
                        name = me.Member.Name;
                    }

                    if (string.IsNullOrEmpty(name)) continue;

                    PropertyInfo pi = type.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

                    if (pi == null || pi.IsIgnore() || (pi.Name == tr.TableMapping.PrimaryKey.Name && tr.TableMapping.PrimaryKey.IsIdentity) || (!pi.PropertyType.IsValueType && pi.PropertyType != typeof(string)))
                        continue;

                    if (pi.PropertyType.IsEnum)
                    {
                        dic.Add(pi.Name, (int)Enum.Parse(pi.PropertyType, pi.GetValue(entity, null).ToString()));
                    }
                    else
                    {
                        dic.Add(pi.Name, pi.GetValue(entity, null));
                    }
                }
            }
            else
            {
                //不获取继承属性 为实例属性 为公开的
                PropertyInfo[] pis = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                foreach (var item in pis)
                {
                    if (item.IsIgnore() || (item.Name == tr.TableMapping.PrimaryKey.Name && tr.TableMapping.PrimaryKey.IsIdentity) || (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)))
                        continue;

                    if (item.PropertyType.IsEnum)
                    {
                        dic.Add(item.Name, (int)Enum.Parse(item.PropertyType, item.GetValue(entity, null).ToString()));
                    }
                    else
                    {
                        dic.Add(item.Name, item.GetValue(entity, null));
                    }
                }
            }
            int index = 0;
            int count = dic.Count;
            foreach (var item in dic)
            {
                this.Append(item.Key);
                this.Append(" = ");
                this.CreateParameter(item.Value);
                if (index++ != count - 1)
                {
                    this.Append(",");
                }
            }

            this.VisitWhereExpression(where);
            return this.GetModifyTranResult(tr);
        }

        public abstract string GetIdentitySQL(string tableName = null);

        protected abstract void CreateParameter(object obj);

        #endregion Create TSQL

        #region Append

        protected virtual void Append(object obj)
        {
            this.sb.Append(obj.ToString());
        }

        #endregion Append

        #region Result

        public virtual DbParameter[] GetParameter()
        {
            return this.parameter.ToArray();
        }

        #endregion Result

        #region VisitExpression

        public virtual void VisitTableExpression(TranResult tr, List<JoinTable> join = null)
        {
            this.Append(" FROM " + tr.TableMapping.Name + " ");
            this.Append(" AS " + tr.TableMapping.AliasName + " ");

            if (join != null && join.Count > 0)
            {
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
                    this.Append(" ");
                }
            }
        }

        protected virtual Expression VisitAggregateMethod(MethodCallExpression m)
        {
            if (m.Method.Name == "DMCount")
            {
                this.Append(m.Method.Name.ToUpper());
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
                this.Append(m.Method.Name.ToUpper());
                this.Append("(");
                this.Visit(m.Arguments[0]);
                this.Append(") ");
            }
            else if (m.Method.Name == "DMAverage")
            {
                this.Append(" AVG");
                this.Append("(");
                this.Visit(m.Arguments[0]);
                this.Append(") ");
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

        protected override Expression VisitBinary(BinaryExpression b)
        {
            this.Append("(");
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    this.Append(" AND ");
                    break;

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    this.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    this.Append(" = ");
                    break;

                case ExpressionType.NotEqual:
                    this.Append(" <> ");
                    break;

                case ExpressionType.LessThan:
                    this.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    this.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    this.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    this.Append(" >= ");
                    break;

                default:
                    throw new NotSupportedException(string.Format("不支持操作符{0}", b.NodeType));
            }

            //这里处理最后一级右边的参数输入可能性
            if (b.Right.NodeType == ExpressionType.MemberAccess || b.Right.NodeType == ExpressionType.ArrayLength || b.Right.NodeType == ExpressionType.Convert)
            {
                var value = Expression.Lambda(b.Right).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(value);
                this.Visit(ce);
            }
            else
            {
                this.Visit(b.Right);
            }
            this.Append(")");
            return b;
        }

        protected override IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
        {
            for (int i = 0; i < original.Count; i++)
            {
                MemberBinding b = this.VisitBinding(original[i]);
                if (i != original.Count - 1)
                {
                    this.Append(",");
                }
            }
            return original;
        }

        protected virtual Expression VisitBitwiseMethod(MethodCallExpression m)
        {
            this.Append(" ( ");
            this.Visit(m.Arguments[0]);
            this.Append(" " + Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke() + " ");
            this.Visit(m.Arguments[2]);
            this.Append(" ) ");
            return m;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            this.CreateParameter(c.Value);
            return c;
        }

        protected virtual Expression VisitDBNullMethod(MethodCallExpression m)
        {
            if (m.Method.Name == "IsDBNull")
            {
                this.Append("(");
                this.Visit(m.Arguments[0]);
                this.Append(" IS NULL ");
                this.Append(")");
            }
            else if (m.Method.Name == "IsNotDBNull")
            {
                this.Append("(");
                this.Visit(m.Arguments[0]);
                this.Append(" IS NOT NULL ");
                this.Append(")");
            }
            return m;
        }

        protected virtual Expression VisitDynamicMethod(MethodCallExpression m)
        {
            string name = Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke().ToString();

            if (name.IndexOf(".") > 0)
            {
                string[] arrayName = name.Split('.');
                this.Append(GetTableAliasName(arrayName[0]) + "." + arrayName[1]);
            }
            else
            {
                this.Append(m.Arguments[0].Type.GetTableMapping().AliasName + "." + name);
            }
            return m;
        }

        protected override ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            for (int i = 0; i < original.Count; i++)
            {
                Expression p = this.Visit(original[i]);
                if (i != original.Count - 1)
                {
                    this.Append(",");
                }
            }

            return original;
        }

        protected virtual Expression VisitGroupByExpression(LambdaExpression group)
        {
            if (group != null)
            {
                this.Append(" GROUP BY ");
                this.Visit(group.Body);
            }
            return group;
        }

        protected virtual Expression VisitInMethod(MethodCallExpression m)
        {
            string arrayStr = Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke().ToString();
            this.Append("(");
            this.Visit(m.Arguments[0]);
            if (m.Method.Name == "In")
            {
                this.Append(" IN ");
            }
            else
            {
                this.Append(" NOT IN ");
            }
            this.Append("(");
            this.Append(arrayStr);
            this.Append("))");
            return m;
        }

        protected virtual Expression VisitLikeMethod(MethodCallExpression m)
        {
            if (m.Method.Name == "Like")
            {
                this.Append("(");
                this.Visit(m.Arguments[0]);
                this.Append(" Like ");
                var value = Expression.Lambda(m.Arguments[1]).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(string.Format("%{0}%", value));
                this.Visit(ce);
                this.Append(")");
            }
            else if (m.Method.Name == "Contains")
            {
                this.Append("(");
                this.Visit(m.Object);
                this.Append(" Like ");
                var value = Expression.Lambda(m.Arguments[0]).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(string.Format("%{0}%", value));
                this.Visit(ce);
                this.Append(")");
            }
            else if (m.Method.Name == "StartsWith")
            {
                this.Append("(");
                this.Visit(m.Object);
                this.Append(" Like ");
                var value = Expression.Lambda(m.Arguments[0]).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(string.Format("{0}%", value));
                this.Visit(ce);
                this.Append(")");
            }
            else if (m.Method.Name == "EndsWith")
            {
                this.Append("(");
                this.Visit(m.Object);
                this.Append(" Like ");
                var value = Expression.Lambda(m.Arguments[0]).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(string.Format("%{0}", value));
                this.Visit(ce);
                this.Append(")");
            }
            return m;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType != ExpressionType.Parameter && m.Expression.NodeType != ExpressionType.Constant)
            {
                //处理时间函数
                if ("DAYOFWEEK".Equals(m.Member.Name.ToUpper()))
                {
                    this.Append("DATEPART(WEEKDAY,");
                    this.Visit(m.Expression);
                    this.Append(")");
                }
                else if ("DATE".Equals(m.Member.Name.ToUpper()))
                {
                    this.Append("CONVERT(VARCHAR(12),");
                    this.Visit(m.Expression);
                    this.Append(",112)");
                }
                else
                {
                    this.Append("DATEPART(" + m.Member.Name.ToUpper() + ",");
                    this.Visit(m.Expression);
                    this.Append(")");
                }
            }
            else
            {
                TableMapping tr = m.Member.DeclaringType.GetTableMapping();
                this.Append(tr.AliasName + ".");
                this.Append(m.Member.Name);
            }
            return m;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "DMCount":
                case "DMSum":
                case "DMMax":
                case "DMMin":
                case "DMAverage":
                case "As":
                    return this.VisitAggregateMethod(m);
                case "Bitwise":
                    return this.VisitBitwiseMethod(m);
                case "Desc":
                case "Asc":
                    return this.VisitOrderByMethod(m);
                case "Column":
                    return this.VisitDynamicMethod(m);
                case "IsDBNull":
                case "IsNotDBNull":
                    return this.VisitDBNullMethod(m);
                case "Like":
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    return this.VisitLikeMethod(m);
                case "In":
                case "NotIn":
                    return this.VisitInMethod(m);
                case "Between":
                case "NotBetween":
                    return this.VisitBetweenMethod(m);
                default:
                    return this.VisitOtherMethod(m);
            }
        }

        protected virtual Expression VisitOrderByExpression(LambdaExpression order)
        {
            if (order != null)
            {
                this.Append(" ORDER BY ");
                this.Visit(order.Body);
            }
            return order;
        }

        protected virtual Expression VisitOrderByMethod(MethodCallExpression orderMethod)
        {
            if (orderMethod.Method.Name == "Desc" || orderMethod.Method.Name == "Asc")
            {
                this.Visit(orderMethod.Arguments[0]);
                this.Append(" " + orderMethod.Method.Name.ToUpper() + " ");
            }
            else if (orderMethod.Method.Name == "OrderBy")
            {
                this.Visit(orderMethod.Arguments[1]);
            }
            return orderMethod;
        }

        protected virtual Expression VisitOtherMethod(MethodCallExpression m)
        {
            try
            {
                var value = Expression.Lambda(m).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(value);
                this.Visit(ce);
            }
            catch
            {
                this.Visit(m.Object);
            }
            return m;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            var tm = p.Type.GetTableMapping();
            this.AddTable(tm);
            this.Append(" " + tm.AliasName + ".*" + " ");
            return p;
        }

        protected virtual Expression VisitSelectExpression(LambdaExpression select)
        {
            if (select != null)
            {
                if (select.Body.NodeType == ExpressionType.New)
                {
                    var newExpression = select.Body as NewExpression;

                    var newArrayExpression = newExpression.Arguments[0] as NewArrayExpression;

                    this.Visit(newArrayExpression);
                }
                else if (select.Body.NodeType == ExpressionType.Call) 
                {
                    this.Visit(select.Body);
                }
                else
                {
                    this.Append(" * ");
                }
            }
            else
            {
                this.Append(" * ");
            }
            return select;
        }

        protected virtual Expression VisitWhereExpression(LambdaExpression where)
        {
            if (where != null)
            {
                if (where.Body.NodeType != ExpressionType.Constant)
                {
                    this.Append(" WHERE ");
                    this.Visit(where.Body);
                }
            }
            return where;
        }

        private Expression VisitBetweenMethod(MethodCallExpression m)
        {
            this.Visit(m.Arguments[0]);

            if (m.Method.Name == "NotBetween")
            {
                this.Append(" NOT ");
            }

            this.Append(" Between ");
            this.Visit(m.Arguments[1]);
            this.Append(" AND ");
            this.Visit(m.Arguments[2]);
            return m;
        }

        #endregion VisitExpression
    }
}