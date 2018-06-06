using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Linq;
namespace PES.DataModel
{
    public static class DMCommon
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo mi) where T : Attribute
        {
            var attributes = mi.GetCustomAttributes(typeof(T), true);
            if (attributes != null && attributes.Count() > 0)
            {
                foreach (var attribute in attributes)
                {
                    T a = attribute as T;
                    if (a != null)
                    {
                        yield return a;
                    }
                }
            }
        }

        public static TableMapping GetTableMapping(this Type type)
        {
            return DMTableMapping.GetTableMapping(type);
        }

        public static bool IsIgnore(this PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(typeof(DMIgnoreAttribute), true);
            if (attributes != null && attributes.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }

    public static class MethodExtend
    {
        /// <summary>
        /// As
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string As<T>(this T t, string name)
        {
            return name;
        }

        /// <summary>
        /// Order ASC
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Columns Asc<T>(this T t)
        {
            return null;
        }

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DMAverage<T>(this T t)
        {
            return t;
        }

        /// <summary>
        /// Between
        /// </summary>
        /// <returns></returns>
        public static bool Between<T>(this T t, T min, T max)
        {
            return true;
        }

        /// <summary>
        /// 位运算
        /// </summary>
        /// <param name="op"> & | ^</param>
        /// <returns></returns>
        public static string Bitwise(this object obj, string op, int value)
        {
            return op;
        }

        /// <summary>
        /// 动态字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Column<T>(this object obj, string fieldName)
        {
            return default(T);
        }

        /// <summary>
        /// 动态字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Column(this object obj, string fieldName)
        {
            return default(string);
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int DMCount(this object obj, string value)
        {
            return 0;
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int DMCount(this object obj)
        {
            return 0;
        }

        /// <summary>
        /// Order DESC
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Columns Desc<T>(this T t)
        {
            return null;
        }

        /// <summary>
        /// In
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool In(this object obj, string array)
        {
            return true;
        }

        /// <summary>
        /// IsNull DB NULL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDBNull<T>(this T t)
        {
            return true;
        }

        /// <summary>
        /// IsNotNull DB NULL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotDBNull<T>(this T t)
        {
            return true;
        }

        /// <summary>
        /// Like
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Like(this string str, string value)
        {
            return true;
        }

        /// <summary>
        /// Max
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DMMax<T>(this T t)
        {
            return t;
        }

        /// <summary>
        /// Min
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DMMin<T>(this T t)
        {
            return t;
        }

        /// <summary>
        /// NotBetween
        /// </summary>
        /// <returns></returns>
        public static bool NotBetween<T>(this T t, T min, T max)
        {
            return true;
        }

        /// <summary>
        /// NotIn
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool NotIn(this object obj, string array)
        {
            return true;
        }

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DMSum<T>(this T t)
        {
            return t;
        }
    }

    public class FieldMapping
    {
        public string Comment { get; set; }

        public Type DataType { get; set; }

        public string DefaultValue { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsNotNull { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int Length { get; set; }

        public string Name { get; set; }
    }

    public class TableMapping
    {
        public TableMapping()
        {
            PrimaryKey = new FieldMapping();
            Fields = new List<FieldMapping>();
        }

        public string AliasName { get; set; }

        public string ConnectionKey { get; set; }

        public List<FieldMapping> Fields { get; set; }

        public bool IsUseCustomConnection { get; set; }

        public string Name { get; set; }

        public FieldMapping PrimaryKey { get; set; }
    }

    public class TableName
    {
        public string AliasName { get; set; }

        public int Index { get; set; }

        public string IndexName { get { return string.Format("t{0} is {1}", Index, Name); } }

        public string Name { get; set; }
    }

    public class TranResult : IDisposable
    {
        #region IDispose 成员

        #region 构造函数

        /// <summary>
        /// 终结器会被垃圾回收器调用 传说中的 Finalize 函数
        /// </summary>
        ~TranResult()
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
                    this.CmdText = null;
                    this.Parameter = null;
                    this.TableMapping = null;
                    this.TableNames = null;
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

        public string CmdText { get; set; }

        public List<DbParameter> Parameter { get; set; }

        public TableMapping TableMapping { get; set; }

        public List<TableName> TableNames { get; set; }

        public override string ToString()
        {
            return this.CmdText;
        }
    }
}