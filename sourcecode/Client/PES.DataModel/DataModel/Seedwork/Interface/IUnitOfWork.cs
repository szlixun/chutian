using System;
using System.Linq.Expressions;

namespace PES.DataModel
{
    internal interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t">要添加的实体</param>
        /// <param name="select">要插入的列 Add(p => new Columns(p.ID, p.Name)) 不填这为插入所有字段 除主键外</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        int Add<T>(T t, Expression<Func<T, Columns>> select = null);

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        int Remove<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">要更新的对象实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">要更新的列 Save(p => new Columns(p.ID, p.Name)) 不填这为更新所有字段 除主键外</param>
        /// <returns>受影响的行数</returns>
        int Save<T>(T t, Expression<Func<T, bool>> where, Expression<Func<T, Columns>> select = null);
    }
}