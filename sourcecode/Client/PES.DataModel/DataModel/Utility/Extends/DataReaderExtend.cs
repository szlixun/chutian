using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using PES.DataModel.Helpers;

namespace PES.DataModel.Extends
{
    #region IDataReader扩展

    /// <summary>
    /// IDataReader扩展
    /// </summary>
    internal static class DataReaderExtend
    {
        public static Nullable<T> GetNullable<T>(this IDataReader dr, string name)
             where T : struct
        {
            object obj = dr[name];
            if (obj == DBNull.Value)
            {
                return null;
            }
            return new Nullable<T>((T)ConvertHelper.ChangeType(obj, typeof(T)));
        }

        public static Nullable<T> GetNullable<T>(this IDataReader dr, int index)
             where T : struct
        {
            object obj = dr[index];
            if (obj == DBNull.Value)
            {
                return null;
            }
            return new Nullable<T>((T)ConvertHelper.ChangeType(obj, typeof(T)));
        }

        public static T GetValue<T>(this IDataReader dr, string name)
        {
            object obj = dr[name];
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)ConvertHelper.ChangeType(obj, typeof(T));
        }

        public static T GetValue<T>(this IDataReader dr, int index)
        {
            object obj = dr[index];
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)ConvertHelper.ChangeType(obj, typeof(T));
        }

        public static T ToEntity<T>(this IDataReader dr)
        {
            Func<IDataReader, T> predicate = CreateDbDataReaderBindPredicate<T>(dr);
            if (dr.Read())
            {
                return predicate(dr);
            }
            else
            {
                return default(T);
            }
        }

        public static T ToEntity<T>(this IDataReader dr, Func<IDataReader, T> predicate)
        {
            if (dr.Read())
            {
                return predicate(dr);
            }
            else
            {
                return default(T);
            }
        }

        public static List<T> ToList<T>(this IDataReader dr)
        {
            Func<IDataReader, T> de = CreateDbDataReaderBindPredicate<T>(dr);

            List<T> list = new List<T>();

            while (dr.Read())
            {
                T t = de(dr);
                list.Add(t);
            }

            return list;
        }

        public static List<T> ToList<T>(this IDataReader dr, Func<IDataReader, T> predicate)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                T t = predicate(dr);
                list.Add(t);
            }
            return list;
        }

        private static Func<IDataReader, T> CreateDbDataReaderBindPredicate<T>(IDataReader dr)
        {
            MethodInfo getValueMethod = typeof(DataReaderExtend).GetMethod("GetValue", new Type[] { typeof(IDataReader), typeof(int) });
            MethodInfo isDBNullMethod = typeof(DbDataReader).GetMethod("IsDBNull", new Type[] { typeof(int) });

            //生成一个方法
            DynamicMethod method = new DynamicMethod("BindToEntityListForEmit", typeof(T), new Type[] { typeof(IDataReader) }, typeof(T), true);

            //开始构造方法的主体IL代码
            ILGenerator generator = method.GetILGenerator();

            //生成一个本地变量 result
            LocalBuilder result = generator.DeclareLocal(typeof(T));

            //实例化本地变量 result
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));

            //设置堆栈中的值 到本地变量
            generator.Emit(OpCodes.Stloc, result);

            for (int i = 0; i < dr.FieldCount; i++)
            {
                PropertyInfo pi = typeof(T).GetProperty(dr.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                //定义一个标记 这里是如果是null的时候跳转使用的标记
                System.Reflection.Emit.Label endIfLabel = generator.DefineLabel();
                if (pi != null && pi.GetSetMethod() != null)
                {
                    //加载参数1 即dr  到堆栈
                    generator.Emit(OpCodes.Ldarg_0);

                    //加载整形 i 到堆栈
                    generator.Emit(OpCodes.Ldc_I4, i);

                    //调用方dr.IsNull(i) 并把返回值保存到堆栈中 真=1 假=0
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);

                    //判断堆栈里面的值是否为真 真=1 假=0 如果不是那么跳转到endIfLabel标记
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    //加载本地变量result 到堆栈
                    generator.Emit(OpCodes.Ldloc, result);

                    //加载参数1 即dr  到堆栈
                    generator.Emit(OpCodes.Ldarg_0);

                    //加载整形 i 到堆栈
                    generator.Emit(OpCodes.Ldc_I4, i);

                    //调用方法dr.get_Item(i) 并把返回值保存到堆栈中
                    generator.Emit(OpCodes.Call, getValueMethod.MakeGenericMethod(pi.PropertyType));

                    //拆箱操作 堆栈里面的值(int)dr.getValue(0) 使用了上面的DataReaderExtend 中的getValue 扩展方法 以后就不需要拆箱操作了
                    //如果仅仅使用系统的dr.getValue方法的话这里需要拆箱操作
                    //generator.Emit(OpCodes.Unbox_Any, pi.PropertyType);

                    //调用属性的Set方法 把堆栈中的值设置到属性中
                    generator.Emit(OpCodes.Callvirt, pi.GetSetMethod());

                    //null标记 如果数据为空那么程序将会直接跳转到这个标记这里
                    generator.MarkLabel(endIfLabel);
                }
            }

            //加载本地变量 到堆栈
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);
            return (Func<IDataReader, T>)method.CreateDelegate(typeof(Func<IDataReader, T>));
        }
    }

    #endregion IDataReader扩展
}