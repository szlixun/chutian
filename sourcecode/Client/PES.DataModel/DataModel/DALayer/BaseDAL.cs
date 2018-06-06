using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PES.DataModel
{
    #region BaseDAL

    public class BaseDAL : AbstractDAL
    {
    }

    #endregion BaseDAL

    #region BaseDAL<TEntity>

    public class BaseDAL<TEntity> : AbstractDAL
    {
        #region 构造函数

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~BaseDAL()
        {
            Dispose(false);
        }

        #endregion 构造函数

        #region IDispose 成员

        /// <summary>
        /// 一个类型的Dispose方法应该允许被多次调用而不抛异常。鉴于这个原因，类型内部维护了一个私有的布尔型变量disposed
        /// </summary>
        private bool disposed = false;

        public virtual void Dispose()
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

        #region IBaseDAL 成员

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMDelete(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Delete<TEntity>(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMDelete(Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Delete<TEntity>(where, trans);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns></returns>
        protected virtual int DMDelete(Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Delete<TEntity>(where, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="trans">事务</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(t, select, trans);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="trans">事务</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(t, null, trans);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Insert<TEntity>(t, select);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Insert<TEntity>(t, select, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t, string connectionString)
        {
            return DMContext.Insert<TEntity>(t, null, null, connectionString);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>如果主键是自增长的话,返回自增长的id 同时item里面的主键也被设置为id</returns>
        protected virtual int DMInsert(TEntity t)
        {
            return DMContext.Insert<TEntity>(t);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).SetConnectionString(connectionString).Single();
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).Single();
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>一条记录 或者 null</returns>
        protected virtual TEntity DMSelect(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Query<TEntity>().Where(where).Single();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll(Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).SetConnectionString(connectionString).ToList();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll(Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).ToList();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll(Expression<Func<TEntity, Columns>> order)
        {
            return DMContext.Query<TEntity>().OrderBy(order).ToList();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectAll()
        {
            return DMContext.Query<TEntity>().ToList();
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToList(top);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).ToList(top);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList(int top, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).ToList(top);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="top">要查询多少条记录</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList(int top, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Query<TEntity>().Where(where).ToList(top);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="top">要查询多少条记录</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual List<TEntity> DMSelectList(int top)
        {
            return DMContext.Query<TEntity>().ToList(top);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize"></param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize"></param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize"></param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="order">选择排序列表达式 Get(p => new Columns(p.ID.Desc(), p.Name.Asc()))</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> order)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize"></param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Query<TEntity>().Where(where).ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取记录集合
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize"></param>
        /// <returns>符合条件的记录集合</returns>
        protected virtual PageList<TEntity> DMSelectPageList(int pageIndex, int pageSize)
        {
            return DMContext.Query<TEntity>().ToPageList(pageIndex, pageSize);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, select, trans);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="trans">事务</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, null, trans);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, null, null, connectionString);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <param name="connectionString">自定义连接</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, select, null, connectionString);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <param name="select">选择列表达式 Get(p => new Columns(p.ID, p.Name))</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Update<TEntity>(entity, where, select);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式 Get(p => p.ID == 100 &amp;&amp; p.Name == "hhahh2011") 或者使用Spec</param>
        /// <returns>受影响的行数</returns>
        protected virtual int DMUpdate(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Update<TEntity>(entity, where);
        }

        #endregion IBaseDAL 成员
    }

    #endregion BaseDAL<TEntity>
}