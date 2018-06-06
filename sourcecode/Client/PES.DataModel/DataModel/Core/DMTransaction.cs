using System;
using System.Data;
using PES.DataModel.Extends;
using PES.DataModel;

namespace PES.DataModel
{
    internal class DMTransaction : IDMTransaction
    {
        #region 私有字段

        private IDbTransaction trans;

        #endregion 私有字段

        private string connectionString;

        public string ConnectionString
        {
            get
            {
                if (connectionString.IsNullOrEmpty())
                {
                    connectionString = DMConfiguration.DefaultConnectionString;
                }
                return connectionString;
            }
            set { connectionString = value; }
        }

        #region 构造函数

        public DMTransaction()
        {
        }

        public DMTransaction(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~DMTransaction()
        {
            //系统调用清理 这个时候要disposing=false
            //是因为如果系统在调用的时候可能if (disposing) 里面的对象已经被清理了
            Dispose(false);
        }

        #endregion 构造函数

        #region 私有方法

        private void BeginNewTransaction()
        {
            var conn = DMObjectContainer.GetProvider().GetConnection(this.ConnectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            this.trans = conn.BeginTransaction();
        }

        #endregion 私有方法

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
                //如果当前对象没有被清理
                if (disposing)
                {
                    //如果是用户显式调用清理
                    //该函数为手动调用，此处可进行托管资源的清理
                    //比如此类中有一个类型为 DataSet 的变量 ds
                    //此处可调用该对象的 Dispose 方法来清理托管资源
                    if (this.trans != null)
                    {
                        if (this.trans.Connection != null && this.trans.Connection.State == ConnectionState.Open)
                        {
                            this.trans.Connection.Close();
                        }
                        this.trans.Dispose();
                        this.trans = null;
                    }
                }

                //进行非托管资源的清理
                //非托管的资源主要为一些用 API 打开的文件句柄，设备场景句柄等
                //该类资源 GC 是无法管理的，只能依靠程序员自已释放
                //不同的资源， 释放方法不一样
                //比如 释放文件句柄
                //CloseHandle(handle)
            }
            disposed = true;
        }

        #endregion IDispose 成员

        #region IDMTransaction 成员

        /// <summary>
        /// 开启一个事务
        /// </summary>
        /// <returns>事务对象</returns>
        public IDbTransaction BeginTransaction()
        {
            if (this.trans == null)
            {
                this.BeginNewTransaction();
            }
            return this.trans;
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                if (this.trans.Connection != null && this.trans.Connection.State == ConnectionState.Open)
                {
                    this.trans.Connection.Close();
                }
                this.trans.Dispose();
                this.trans = null;
            }
        }

        public void Rollback()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                if (this.trans.Connection != null && this.trans.Connection.State == ConnectionState.Open)
                {
                    this.trans.Connection.Close();
                }
                this.trans.Dispose();
                this.trans = null;
            }
        }

        #endregion IDMTransaction 成员
    }
}