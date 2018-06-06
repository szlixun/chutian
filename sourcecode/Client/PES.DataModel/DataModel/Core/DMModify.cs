using System;
using System.Data;
using System.Linq.Expressions;
using PES.DataModel.Helpers;
using PES.DataModel;

namespace PES.DataModel
{
    public abstract class AModify<T1> : ADMCommand<T1>
    {
        protected ColumnsExpression select = new ColumnsExpression();
        protected IDbTransaction trans;
        protected WhereExpression where = new WhereExpression();

        protected override string ConnectionString
        {
            get
            {
                if (this.trans == null)
                {
                    return base.ConnectionString;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (this.trans == null)
                {
                    base.ConnectionString = value;
                }
            }
        }

        public abstract int Excute();
    }

    public class Delete<T1> : AModify<T1>
    {
        public override int Excute()
        {
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                TranResult tr = translator.CreateDeleteTSQL<T1>(where.LambdaExpression);
                ATSqlCommand cmd = new ATSqlCommand();
                cmd.SetCmdText(tr.CmdText);
                cmd.SetParameters(tr.Parameter.ToArray());
                cmd.SetTrans(base.trans);
                cmd.SetConnectionString(base.ConnectionString);
                return cmd.ExecuteNonQuery();
            }
        }

        public Delete<T1> SetConnectionString(string connectionString)
        {
            base.ConnectionString = connectionString;
            return this;
        }

        public Delete<T1> SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
            return this;
        }

        public Delete<T1> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }
    }

    public class Insert<T1> : AModify<T1>
    {
        private T1 t;

        public Insert(T1 t)
        {
            this.t = t;
        }

        public override int Excute()
        {
            switch (DMConfiguration.ProviderType)
            {
                case EnumProviderType.MySql:
                case EnumProviderType.MsSql:
                    using (var translator = DMObjectContainer.GetTSQLTranslator())
                    {
                        int id = 0;
                        Type type = typeof(T1);
                        var tm = type.GetTableMapping();
                        var pk = type.GetProperty(tm.PrimaryKey.Name);
                        TranResult tr = translator.CreateInsertTSQL<T1>(this.t, select.LambdaExpression);
                        if (pk != null && tm.PrimaryKey.IsPrimaryKey && tm.PrimaryKey.IsIdentity) tr.CmdText += translator.GetIdentitySQL();
                        ATSqlCommand cmd = new ATSqlCommand();
                        cmd.SetCmdText(tr.CmdText);
                        cmd.SetParameters(tr.Parameter.ToArray());
                        cmd.SetTrans(base.trans);
                        cmd.SetConnectionString(base.ConnectionString);
                        id = cmd.ExecuteScalar();
                        if (pk != null && tm.PrimaryKey.IsPrimaryKey && tm.PrimaryKey.IsIdentity)
                        {
                            pk.SetValue(t, id, null);
                        }
                        return id;
                    }
                case EnumProviderType.Access:
                    return this.AccessExcute();
                default:
                    throw new NotSupportedException("不支持的数据库类型");
            }
        }

        public Insert<T1> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Insert<T1> SetConnectionString(string connectionString)
        {
            base.ConnectionString = connectionString;
            return this;
        }

        public Insert<T1> SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
            return this;
        }

        private int AccessExcute()
        {
            int id = 0;
            Type type = typeof(T1);
            var tm = type.GetTableMapping();
            var pk = type.GetProperty(tm.PrimaryKey.Name);
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                if (base.trans == null)
                {
                    using (var dmTrans = new DMTransaction(this.ConnectionString))
                    {
                        try
                        {
                            //这里使用事务主要是为了锁表 目的是为了获取自增ID
                            TranResult tr = translator.CreateInsertTSQL<T1>(this.t, select.LambdaExpression);
                            ATSqlCommand cmd = new ATSqlCommand();
                            cmd.SetCmdText(tr.CmdText);
                            cmd.SetParameters(tr.Parameter.ToArray());
                            cmd.SetTrans(dmTrans.BeginTransaction());
                            cmd.ExecuteScalar();
                            if (pk != null && tm.PrimaryKey.IsPrimaryKey && tm.PrimaryKey.IsIdentity)
                            {
                                var identitySQL = translator.GetIdentitySQL(tm.Name);
                                ATSqlCommand accessCmd = new ATSqlCommand();
                                accessCmd.SetCmdText(identitySQL);
                                accessCmd.SetTrans(dmTrans.BeginTransaction());
                                id = accessCmd.ExecuteScalar();
                                pk.SetValue(t, id, null);
                            }
                            dmTrans.Commit();
                        }
                        catch
                        {
                            dmTrans.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    TranResult tr = translator.CreateInsertTSQL<T1>(this.t, select.LambdaExpression);
                    ATSqlCommand cmd = new ATSqlCommand();
                    cmd.SetCmdText(tr.CmdText);
                    cmd.SetParameters(tr.Parameter.ToArray());
                    cmd.SetTrans(base.trans);
                    cmd.ExecuteScalar();
                    if (pk != null && tm.PrimaryKey.IsPrimaryKey && tm.PrimaryKey.IsIdentity)
                    {
                        var identitySQL = translator.GetIdentitySQL(tm.Name);
                        ATSqlCommand accessCmd = new ATSqlCommand();
                        accessCmd.SetCmdText(identitySQL);
                        accessCmd.SetTrans(base.trans);
                        id = accessCmd.ExecuteScalar();
                        pk.SetValue(t, id, null);
                    }
                }
            }
            return id;
        }
    }

    public class Update<T1> : AModify<T1>
    {
        private T1 t;

        public Update(T1 t)
        {
            this.t = t;
        }

        public override int Excute()
        {
            using (var translator = DMObjectContainer.GetTSQLTranslator())
            {
                TranResult tr = translator.CreateUpdateTSQL<T1>(this.t, where.LambdaExpression, select.LambdaExpression);
                ATSqlCommand cmd = new ATSqlCommand();
                cmd.SetCmdText(tr.CmdText);
                cmd.SetParameters(tr.Parameter.ToArray());
                cmd.SetTrans(base.trans);
                cmd.SetConnectionString(base.ConnectionString);
                return cmd.ExecuteNonQuery();
            }
        }

        public Update<T1> Select(Expression<Func<T1, Columns>> select)
        {
            this.select.Add(select);
            return this;
        }

        public Update<T1> SetConnectionString(string connectionString)
        {
            base.ConnectionString = connectionString;
            return this;
        }

        public Update<T1> SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
            return this;
        }

        public Update<T1> Where(Expression<Func<T1, bool>> where)
        {
            this.where.And(where);
            return this;
        }
    }
}