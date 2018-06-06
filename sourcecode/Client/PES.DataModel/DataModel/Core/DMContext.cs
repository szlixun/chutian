using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using PES.DataModel.Helpers;
using PES.DataModel.IoC;

namespace PES.DataModel
{
    public static class DMContext
    {
        #region Query

        public static Query<T> Query<T>()
        {
            return new Query<T>();
        }

        public static Query<T1, TResult> Query<T1, TResult>()
        {
            return new Query<T1, TResult>();
        }

        public static Query<T1, T2, TResult> Query<T1, T2, TResult>()
        {
            return new Query<T1, T2, TResult>();
        }

        public static Query<T1, T2, T3, TResult> Query<T1, T2, T3, TResult>()
        {
            return new Query<T1, T2, T3, TResult>();
        }

        public static Query<T1, T2, T3, T4, TResult> Query<T1, T2, T3, T4, TResult>()
        {
            return new Query<T1, T2, T3, T4, TResult>();
        }

        public static Query<T1, T2, T3, T4, T5, TResult> Query<T1, T2, T3, T4, T5, TResult>()
        {
            return new Query<T1, T2, T3, T4, T5, TResult>();
        }

        #endregion Query

        #region Insert

        public static int Insert<T1>(T1 t, Expression<Func<T1, Columns>> select = null, IDbTransaction trans = null, string connectionString = null)
        {
            return new Insert<T1>(t).Select(select).SetTrans(trans).SetConnectionString(connectionString).Excute();
        }

        #endregion Insert

        #region Update

        public static int Update<T1>(T1 t, Expression<Func<T1, bool>> where, Expression<Func<T1, Columns>> select = null, IDbTransaction trans = null, string connectionString = null)
        {
            return new Update<T1>(t).Where(where).Select(select).SetTrans(trans).SetConnectionString(connectionString).Excute();
        }

        #endregion Update

        #region Delete

        public static int Delete<T1>(Expression<Func<T1, bool>> where, IDbTransaction trans = null, string connectionString = null)
        {
            return new Delete<T1>().Where(where).SetTrans(trans).SetConnectionString(connectionString).Excute();
        }

        #endregion Delete

        #region Transaction

        public static IDMTransaction GetTransaction()
        {
            return new DMTransaction();
        }

        public static IDMTransaction GetTransaction(string connectionString)
        {
            return new DMTransaction(connectionString);
        }

        #endregion Transaction

        #region ATSqlCommand

        public static ATSqlCommand TSqlCommand(string cmdText, IList<IDbDataParameter> parameter = null, IDbTransaction trans = null)
        {
            return new ATSqlCommand().SetCmdText(cmdText).SetParameters(parameter).SetTrans(trans).SetConnectionString(DMConfiguration.DefaultConnectionString);
        }

        public static ATSqlCommand TSqlCommand(string cmdText, params object[] args)
        {
            return new ATSqlCommand().SetCmdText(string.Format(cmdText, args)).SetConnectionString(DMConfiguration.DefaultConnectionString);
        }

        #endregion ATSqlCommand

        #region Configuration

        public static string DefaultConnectionString { get { return DMConfiguration.DefaultConnectionString; } }

        public static EnumProviderType ProviderType { get { return DMConfiguration.ProviderType; } }

        public static string GetConnectionString<T>()
        {
            return DMConnectionString.GetConnectionString<T>();
        }

        public static void SetConnectionString(string connectionString)
        {
            DMConfiguration.SetDefaultConnectionString(connectionString);
        }

        public static void SetConnectionStringByKey(string key)
        {
            DMConfiguration.SetDefaultConnectionString(DbHelper.GetConnectString(key));
        }

        #endregion Configuration

        #region ObjectContainer

        public static TService GetService<TService>() where TService : class
        {
            return DMObjectContainer.Resolve<TService>();
        }

        public static void Register<TService, TImpl>()
            where TService : class
            where TImpl : class, TService
        {
            DMObjectContainer.Register<TService, TImpl>(LifeStyle.Transient);
        }

        #endregion ObjectContainer
    }
}