using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PES.DataModel
{
    public abstract class AbstractDAL : AbstractDbAccess
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMDelete<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Delete<TEntity>(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMDelete<TEntity>(Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Delete<TEntity>(where, trans);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="connectionString">自定义数据库连接</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMDelete<TEntity>(Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Delete<TEntity>(where, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填则为插入所有字段 除主键外</param>
        /// <param name="trans">事务</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(entity, select, trans);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="trans">事务</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(entity, null, trans);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填则为插入所有字段 除主键外</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Insert<TEntity>(entity, select);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填则为插入所有字段 除主键外</param>
        /// <param name="connectionString">自定义数据库连接</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Insert<TEntity>(entity, select, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="connectionString">自定义数据库连接</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity, string connectionString)
        {
            return DMContext.Insert<TEntity>(entity, null, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert<TEntity>(TEntity entity)
        {
            return DMContext.Insert<TEntity>(entity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">查询参数类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T> DMQuery<T>()
        {
            return DMContext.Query<T>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1">查询参数类型1</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T1, TResult> DMQuery<T1, TResult>()
        {
            return DMContext.Query<T1, TResult>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1">查询参数类型1</typeparam>
        /// <typeparam name="T2">查询参数类型2</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T1, T2, TResult> DMQuery<T1, T2, TResult>()
        {
            return DMContext.Query<T1, T2, TResult>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1">查询参数类型1</typeparam>
        /// <typeparam name="T2">查询参数类型2</typeparam>
        /// <typeparam name="T3">查询参数类型3</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T1, T2, T3, TResult> DMQuery<T1, T2, T3, TResult>()
        {
            return DMContext.Query<T1, T2, T3, TResult>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1">查询参数类型1</typeparam>
        /// <typeparam name="T2">查询参数类型2</typeparam>
        /// <typeparam name="T3">查询参数类型3</typeparam>
        /// <typeparam name="T4">查询参数类型4</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T1, T2, T3, T4, TResult> DMQuery<T1, T2, T3, T4, TResult>()
        {
            return DMContext.Query<T1, T2, T3, T4, TResult>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1">查询参数类型1</typeparam>
        /// <typeparam name="T2">查询参数类型2</typeparam>
        /// <typeparam name="T3">查询参数类型3</typeparam>
        /// <typeparam name="T4">查询参数类型4</typeparam>
        /// <typeparam name="T5">查询参数类型5</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <returns>查询器</returns>
        protected virtual Query<T1, T2, T3, T4, T5, TResult> DMQuery<T1, T2, T3, T4, T5, TResult>()
        {
            return DMContext.Query<T1, T2, T3, T4, T5, TResult>();
        }

        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect<TEntity>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).SetConnectionString(connectionString).Single();
        }

        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect<TEntity>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).Single();
        }

        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Query<TEntity>().Where(where).Single();
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll<TEntity>(Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).SetConnectionString(connectionString).ToList();
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll<TEntity>(Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).ToList();
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        protected virtual List<TEntity> DMSelectAll<TEntity>(Expression<Func<TEntity, Columns>> order)
        {
            return DMContext.Query<TEntity>().OrderBy(order).ToList();
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        protected virtual List<TEntity> DMSelectAll<TEntity>()
        {
            return DMContext.Query<TEntity>().ToList();
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList<TEntity>(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToList(top);
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList<TEntity>(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).ToList(top);
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList<TEntity>(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).ToList(top);
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList<TEntity>(int top, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Query<TEntity>().Where(where).ToList(top);
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList<TEntity>(int top)
        {
            return DMContext.Query<TEntity>().ToList(top);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null)
        {
            return DMContext.Query<TEntity>().Where(where).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList<TEntity>(int pageIndex, int pageSize)
        {
            return DMContext.Query<TEntity>().ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, select, trans);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, null, trans);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, null, null, connectionString);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, select, null, connectionString);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Update<TEntity>(entity, where, select);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Update<TEntity>(entity, where);
        }
    }
}