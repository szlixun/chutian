using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PES.DataModel
{
    public interface IAbstractRepository : IDbAccess
    {
        #region Remove<TEntity>

        /// <summary>
        /// 删除 使用事务
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="trans">事务</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Remove<TEntity>(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 删除 使用事务
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="trans">事务</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Remove<TEntity>(Expression<Func<TEntity, bool>> where, IDbTransaction trans);

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="trans">事务</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Remove<TEntity>(Expression<Func<TEntity, bool>> where, string connectionString);

        #endregion Remove<TEntity>

        #region Add<TEntity>

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要添加的实体</param>
        /// <param name="trans">事务</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity entity, IDbTransaction trans);

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要添加的实体</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity entity, string connectionString);

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="t">要添加的实体</param>
        /// <param name="trans">事务</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填这为插入所有字段 除主键外</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select, IDbTransaction trans);

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="t">要添加的实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填这为插入所有字段 除主键外</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select);

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="t">要添加的实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填这为插入所有字段 除主键外</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select, string connectionString);

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="t">要添加的实体</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<TEntity>(TEntity t);

        #endregion Add<TEntity>

        #region Get<TEntity>

        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>一条记录 或者 null</returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select = null, string connectionString = null);

        #endregion Get<TEntity>

        #region GetAll<TEntity>

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>所有记录集合 或者 null</returns>
        List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null);

        #endregion GetAll<TEntity>

        #region GetList<TEntity>

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合 或者 null</returns>
        List<TEntity> GetList<TEntity>(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null);

        #endregion GetList<TEntity>

        #region GetPageList<TEntity>

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合 或者 null</returns>
        PageList<TEntity> GetPageList<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null);

        #endregion GetPageList<TEntity>

        #region Save<TEntity>

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="trans">事务</param>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">要更新的列 Save(p => new Columns(p.ID, p.Name)) 不填这为更新所有字段 除主键外</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, IDbTransaction trans);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction trans);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, string connectionString);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">要更新的列 Save(p => new Columns(p.ID, p.Name)) 不填这为更新所有字段 除主键外</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">要更新的列 Save(p => new Columns(p.ID, p.Name)) 不填这为更新所有字段 除主键外</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where);

        #endregion Save<TEntity>
    }
}