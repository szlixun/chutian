using System;
using PES.DataModel.Helpers;

namespace PES.DataModel.Extends
{
    #region 对象扩展

    internal static class ObjectExtend
    {
        #region 转化类型

        public static bool ToBool(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj=null");
            }

            bool result = false;

            if (bool.TryParse(obj.ToString().ToLower(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static bool ToBool(this object obj, bool defaultValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj=null");
            }

            bool result = false;

            if (bool.TryParse(obj.ToString().ToLower(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        public static DateTime ToDataTime(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj=null");
            }

            DateTime result = DateTime.Now;

            if (DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static DateTime ToDataTime(this object obj, DateTime defaultValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj=null");
            }

            DateTime result = DateTime.Now;

            if (DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        public static int ToInt(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj=null");
            }

            int result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static int ToInt(this object obj, int defaultValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return defaultValue;
            }

            int result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        public static T ToType<T>(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)ConvertHelper.ChangeType(obj, typeof(T));
        }

        #endregion 转化类型
    }

    #endregion 对象扩展
}