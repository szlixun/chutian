/// <summary>
/// 类说明：OleDbHelper类
/// 编码日期：2012-12-19
/// 编 码 人：hhahh2011
/// 联系方式：359875450
/// 修改日期：2013-03-22
/// </summary>
using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace PES.DataModel.Helpers
{
    /// <summary>
    /// 数据库的通用访问代码
    /// 此类为抽象类，不允许实例化，在应用时直接调用即可
    /// </summary>
    internal abstract class DbAccessHelper
    {
        // 哈希表用来存储缓存的参数信息，哈希表可以存储任意类型的参数。
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #region Public methods for command.

        /// <summary>
        /// 缓存参数数组
        /// </summary>
        /// <param name="cacheKey">参数缓存的键值</param>
        /// <param name="cmdParms">被缓存的参数列表</param>
        public static void CacheParameters(string cacheKey, params OleDbParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// DateSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的DataSet</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// DateSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的DataSet</returns>
        public static DataSet ExecuteDataSet(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// DataTable dt = ExecuteDataTable(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的DataSet</returns>
        public static DataTable ExecuteDataTable(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// DataTable dt = ExecuteDataTable(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的DataSet</returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        ///执行一个不需要返回值的OleDbCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此OleDbCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();

            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                return cmd.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        ///执行一条不返回结果的OleDbCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此OleDbCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                return cmd.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条不返回结果的OleDbCommand，通过一个已经存在的数据库事物处理
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个存在的 OleDb 事物处理</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此OleDbCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                cmd.Parameters.Clear();
                //trans.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// OleDbDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connectionString);

            // 在这里使用try/catch处理是因为如果方法出现异常，则OleDbDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                cmd.Parameters.Clear();
                conn.Dispose();
                throw;
            };
        }

        /// <summary>
        /// 执行一条返回结果集的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// OleDbDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();

            // 在这里使用try/catch处理是因为如果方法出现异常，则OleDbDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                cmd.Parameters.Clear();
                conn.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的OleDbCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();

            OleDbConnection conn = new OleDbConnection(connectionString);

            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的OleDbCommand命令，通过已经存在的数据库连接。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个已经存在的数据库连接</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型</returns>
        public static object ExecuteScalar(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                return cmd.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                cmd.Parameters.Clear();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的OleDbCommand命令，通过已经存在的数据库连接。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个已经存在的数据库连接</param>
        /// <param name="commandType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-OleDb 语句</param>
        /// <param name="commandParameters">以数组形式提供OleDbCommand命令中用到的参数列表</param>
        /// <returns>返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型</returns>
        public static object ExecuteScalar(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                return cmd.ExecuteScalar();
            }
            catch
            {
                cmd.Parameters.Clear();
                //trans.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 获取被缓存的参数
        /// </summary>
        /// <param name="cacheKey">用于查找参数的KEY值</param>
        /// <returns>返回缓存的参数数组</returns>
        public static OleDbParameter[] GetCachedParameters(string cacheKey)
        {
            OleDbParameter[] cachedParms = (OleDbParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            //新建一个参数的克隆列表
            OleDbParameter[] clonedParms = new OleDbParameter[cachedParms.Length];

            //通过循环为克隆参数列表赋值
            for (int i = 0, j = cachedParms.Length; i < j; i++)

                //使用clone方法复制参数列表中的参数
                clonedParms[i] = (OleDbParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 获取架构信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="collectionName"></param>
        /// <param name="restrictionValues"></param>
        /// <returns></returns>
        public static DataTable GetSchema(string connectionString, string collectionName, string[] restrictionValues)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            DataTable schema = new DataTable();
            try
            {
                connection.Open();
                if (!string.IsNullOrEmpty(collectionName))
                {
                    if (restrictionValues != null && restrictionValues != null && restrictionValues.Length > 0)
                    {
                        schema = connection.GetSchema(collectionName, restrictionValues);
                    }
                    else
                    {
                        schema = connection.GetSchema(collectionName);
                    }
                }
                else
                {
                    schema = connection.GetSchema();
                }
            }
            catch
            {
                schema = null;
            }
            finally
            {
                connection.Close();
            }
            return schema;
        }

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">OleDbCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">OleDbCommand命令类型 (存储过程， T-OleDb语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-OleDb语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                // foreach (OleDbParameter parm in cmdParms)
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        #endregion Public methods for command.

        #region Public methods for transaction.

        public OleDbTransaction BeginTransaction(string connectionString)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            return this.BeginTransaction(conn);
        }

        public OleDbTransaction BeginTransaction(string connectionString, IsolationLevel isolationLevel)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            return this.BeginTransaction(conn, isolationLevel);
        }

        public OleDbTransaction BeginTransaction(OleDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction();
        }

        public OleDbTransaction BeginTransaction(OleDbConnection conn, IsolationLevel isolationLevel)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction(OleDbTransaction trans)
        {
            this.CommitTransaction(trans, true);
        }

        public void CommitTransaction(OleDbTransaction trans, bool closeConnection)
        {
            trans.Commit();

            if (closeConnection && trans.Connection.State != ConnectionState.Closed)
            {
                trans.Connection.Close();
            }

            trans = null;
        }

        public void RollbackTransaction(OleDbTransaction trans)
        {
            this.RollbackTransaction(trans, true);
        }

        public void RollbackTransaction(OleDbTransaction trans, bool closeConnection)
        {
            trans.Rollback();

            if (closeConnection && trans.Connection.State != ConnectionState.Closed)
            {
                trans.Connection.Close();
            }

            trans = null;
        }

        #endregion Public methods for transaction.
    }
}