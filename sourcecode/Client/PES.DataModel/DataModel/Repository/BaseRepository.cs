using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using PES.DataModel;

namespace PES.DataModel
{
    #region BaseRepository

    public class BaseRepository : AbstractRepository, IBaseRepository
    {
        #region 私有字段

        private IUnitOfWork unitOfWork;

        #endregion 私有字段

        #region 构造函数

        public BaseRepository()
        {
            this.unitOfWork = new DMUnitOfWork();
        }

        public BaseRepository(string connectionString)
        {
            this.unitOfWork = new DMUnitOfWork(connectionString);
        }

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~BaseRepository()
        {
            Dispose(false);
        }

        #endregion 构造函数

        #region IUnitOfWork 成员

        public override int Add<TEntity>(TEntity t, Expression<Func<TEntity, Columns>> select)
        {
            return this.unitOfWork.Add<TEntity>(t, select);
        }

        public override int Add<TEntity>(TEntity t)
        {
            return this.unitOfWork.Add<TEntity>(t);
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }

        public override int Remove<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return this.unitOfWork.Remove<TEntity>(where);
        }

        public void Rollback()
        {
            unitOfWork.Rollback();
        }

        public override int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return this.unitOfWork.Save<TEntity>(entity, where, select);
        }

        public override int Save<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return this.unitOfWork.Save<TEntity>(entity, where);
        }

        #endregion IUnitOfWork 成员

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
                    this.unitOfWork.Dispose();
                    this.unitOfWork = null;
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
    }

    #endregion BaseRepository

    #region BaseRepository<TEntity>

    public class BaseRepository<TEntity> : AbstractRepository, IBaseRepository<TEntity>
    {
        #region 构造函数

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~BaseRepository()
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

        #region IBaseRepository 成员

        public int Add(TEntity entity, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(entity, null, trans);
        }

        public int Add(TEntity entity, string connectionString)
        {
            return DMContext.Insert<TEntity>(entity, null, null, connectionString);
        }

        public int Add(TEntity t, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Insert<TEntity>(t, select, trans);
        }

        public int Add(TEntity t, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Insert<TEntity>(t, select);
        }

        public int Add(TEntity t, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Insert<TEntity>(t, select, null, connectionString);
        }

        public int Add(TEntity t)
        {
            return DMContext.Insert<TEntity>(t);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).Select(select).SetConnectionString(connectionString).Single();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().OrderBy(order).Select(select).SetConnectionString(connectionString).ToList();
        }

        public virtual List<TEntity> GetList(int top, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToList(top);
        }

        public virtual PageList<TEntity> GetPageList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, Columns>> order = null, Expression<Func<TEntity, Columns>> select = null, string connectionString = null)
        {
            return DMContext.Query<TEntity>().Where(where).OrderBy(order).Select(select).SetConnectionString(connectionString).ToPageList(pageIndex, pageSize);
        }

        public int Remove(Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Delete<TEntity>(where);
        }

        public int Remove(Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Delete<TEntity>(where, trans);
        }

        public int Remove(Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Delete<TEntity>(where, null, connectionString);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, select, trans);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction trans)
        {
            return DMContext.Update<TEntity>(entity, where, null, trans);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, null, null, connectionString);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select, string connectionString)
        {
            return DMContext.Update<TEntity>(entity, where, select, null, connectionString);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Columns>> select)
        {
            return DMContext.Update<TEntity>(entity, where, select);
        }

        public int Save(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return DMContext.Update<TEntity>(entity, where);
        }

        #endregion IBaseRepository 成员
    }

    #endregion BaseRepository<TEntity>
}