using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using PES.DataModel.Extends;

namespace PES.DataModel
{
    public class ATSqlCommand : IDisposable
    {
        #region IDispose 成员

        #region 构造函数

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~ATSqlCommand()
        {
            Dispose(false);
        }

        #endregion 构造函数

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
                    this.connectionString = null;
                    this.Parameters = null;
                    this.CmdText = null;
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

        private string connectionString;

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                if (this.Trans == null)
                {
                    connectionString = value;
                }
            }
        }

        public IList<IDbDataParameter> Parameters { get; set; }

        protected string CmdText { get; set; }

        protected IDbTransaction Trans { get; set; }

        public virtual ATSqlCommand AddParameter<T>(T entity, bool isAddPrimaryKey = true)
        {
            Type type = typeof(T);
            TranResult tr = new TranResult();
            tr.TableMapping = type.GetTableMapping();

            //不获取继承属性 为实例属性 为公开的
            PropertyInfo[] pis = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var item in pis)
            {
                if (isAddPrimaryKey)
                {
                    if (item.IsIgnore() || (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)))
                        continue;
                }
                else
                {
                    if (item.IsIgnore() || (item.Name == tr.TableMapping.PrimaryKey.Name && tr.TableMapping.PrimaryKey.IsIdentity) || (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)))
                        continue;
                }

                this.AddParameter(item.Name, item.GetValue(entity, null));
            }
            return this;
        }

        public virtual ATSqlCommand AddParameter(string name, object value)
        {
            var param = DMObjectContainer.GetProvider().CreateParameter(name, value);
            if (this.CmdText.Contains(param.ParameterName))
            {
                if (this.Parameters == null) this.Parameters = new List<IDbDataParameter>();
                this.Parameters.Add(param);
            }
            return this;
        }

        public virtual ATSqlCommand ClearParameters()
        {
            if (this.Parameters != null && this.Parameters.Count > 0) { this.Parameters.Clear(); }

            return this;
        }

        public virtual int ExecuteNonQuery()
        {
            if (this.Trans == null)
            {
                using (var conn = this.GetConnection())
                {
                    return DMObjectContainer.GetProvider().ExecuteNonQuery(conn, this.CmdText, this.Parameters);
                }
            }
            else
            {
                return DMObjectContainer.GetProvider().ExecuteNonQuery(this.Trans, this.CmdText, this.Parameters);
            }
        }

        public virtual TResult ExecuteScalar<TResult>()
        {
            if (this.Trans == null)
            {
                using (var conn = this.GetConnection())
                {
                    return DMObjectContainer.GetProvider().ExecuteScalar(conn, this.CmdText, this.Parameters).ToType<TResult>();
                }
            }
            else
            {
                return DMObjectContainer.GetProvider().ExecuteScalar(this.Trans, this.CmdText, this.Parameters).ToType<TResult>();
            }
        }

        public virtual int ExecuteScalar()
        {
            return this.ExecuteScalar<int>();
        }

        public virtual ATSqlCommand SetCmdText(string cmdText)
        {
            this.CmdText = cmdText;
            return this;
        }

        public virtual ATSqlCommand SetConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
            return this;
        }

        public virtual ATSqlCommand SetParameters(IList<IDbDataParameter> parameter)
        {
            this.Parameters = parameter;
            return this;
        }

        public virtual ATSqlCommand SetTrans(IDbTransaction trans)
        {
            this.Trans = trans;
            return this;
        }

        public virtual DataSet ToDataSet()
        {
            using (var conn = this.GetConnection())
            {
                return DMObjectContainer.GetProvider().ExecuteDataSet(conn, this.CmdText, this.Parameters);
            }
        }

        public virtual DataTable ToDataTable()
        {
            using (var conn = this.GetConnection())
            {
                return DMObjectContainer.GetProvider().ExecuteDataTable(conn, this.CmdText, this.Parameters);
            }
        }

        public virtual TResult ToEntity<TResult>()
        {
            using (var conn = this.GetConnection())
            {
                return DMObjectContainer.GetProvider().ExecuteDataReader(conn, this.CmdText, this.Parameters).ToEntity<TResult>();
            }
        }

        public virtual List<TResult> ToList<TResult>()
        {
            using (var conn = this.GetConnection())
            {
                return DMObjectContainer.GetProvider().ExecuteDataReader(conn, this.CmdText, this.Parameters).ToList<TResult>();
            }
        }

        public override string ToString()
        {
            return this.CmdText;
        }

        private IDbConnection GetConnection()
        {
            return DMObjectContainer.GetProvider().GetConnection(this.ConnectionString);
        }
    }
}