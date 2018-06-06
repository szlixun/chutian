using System;
using System.Data;

namespace PES.DataModel
{
    public interface IDMTransaction : ITransaction, IDisposable
    {
        /// <summary>
        /// 获取一个事务
        /// </summary>
        /// <returns>事务对象</returns>
        IDbTransaction BeginTransaction();
    }
}