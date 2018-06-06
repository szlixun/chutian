using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using PES.DataModel;

namespace PES.DataModel
{
    public abstract class AbstractRepository : AbstractDbAccess, IAbstractRepository
    {
        #region IAbstractRepository 成员

        public virtual int Add<TEntity>(TEntity entity, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(entity, null, trans);
        }

        public virtual int Add<TEntity>(TEntity entity, string connectionString)
        {
            return DMContext.Insert<TEntity>(entity, null, null, connectionString);
        }

        public virtual int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(t, select, trans);
        }

        public virtual int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Insert<TEntity>(t, select);
        }

        public virtual int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Insert<TEntity>(t, select, null, connectionString);
        }

        public virtual int Add<TEntity>(TEntity t)
        {
            return DMContext.Insert<TEntity>(t);
        }

        public virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).SetConnectionString(connectionString).Single();
        }

        public virtual List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).SetConnectionString(connectionString).ToList();
        }

        public virtual List<TEntity> GetList<TEntity>(int top, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToList(top);
        }

        public virtual PageList<TEntity> GetPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToPageList(pageIndex, pageSize);
        }

        public virtual int Remove<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Delete<TEntity>(where);
        }

        public virtual int Remove<TEntity>(Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Delete<TEntity>(where, trans);
        }

        public virtual int Remove<TEntity>(Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Delete<TEntity>(where, null, connectionString);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, select, trans);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, null, trans);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, null, null, connectionString);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, select, null, connectionString);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Update<TEntity>(entity, where, select);
        }

        public virtual int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Update<TEntity>(entity, where);
        }

        #endregion IAbstractRepository 成员
    }
}