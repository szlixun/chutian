using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using PES.DataModel.Helpers;

namespace PES.DataModel.Extends
{
    #region DataTable扩展

    /// <summary>
    /// DataTable扩展
    /// </summary>
    internal static class DataTableExtend
    {
        public static Nullable<T> GetNullable<T>(this DataRow dr, string name)
            where T : struct
        {
            object obj = dr[name];
            if (obj == DBNull.Value)
            {
                return null;
            }
            return new Nullable<T>((T)ConvertHelper.ChangeType(obj, typeof(T)));
        }

        public static Nullable<T> GetNullable<T>(this DataRow dr, int index)
             where T : struct
        {
            object obj = dr[index];
            if (obj == DBNull.Value)
            {
                return null;
            }
            return new Nullable<T>((T)ConvertHelper.ChangeType(obj, typeof(T)));
        }

        public static T GetValue<T>(this DataRow dr, string name)
        {
            object obj = dr[name];
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)ConvertHelper.ChangeType(obj, typeof(T));
        }

        public static T GetValue<T>(this DataRow dr, int index)
        {
            object obj = dr[index];
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)ConvertHelper.ChangeType(obj, typeof(T));
        }

        public static T ToEntity<T>(this DataRow dr)
        {
            Func<DataRow, T> de = CreateDataTableBindPredicate<T>(dr.Table);
            return de(dr);
        }

        public static T ToEntity<T>(this DataRow dr, Func<DataRow, T> predicate)
        {
            return predicate(dr);
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            Func<DataRow, T> predicate = CreateDataTableBindPredicate<T>(dt);
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T t = predicate(row);
                list.Add(t);
            }
            return list;
        }

        public static List<T> ToList<T>(this DataTable dt, Func<DataRow, T> predicate)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T t = predicate(row);
                list.Add(t);
            }
            return list;
        }

        private static Func<DataRow, T> CreateDataTableBindPredicate<T>(DataTable dt)
        {
            MethodInfo getValueMethod = typeof(DataTableExtend).GetMethod("GetValue", new Type[] { typeof(DataRow), typeof(int) });
            MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });

            //生成一个方法
            DynamicMethod method = new DynamicMethod("BindToEntityListForEmit", typeof(T), new Type[] { typeof(DataRow) }, typeof(T), true);

            //开始构造方法的主体IL代码
            ILGenerator generator = method.GetILGenerator();

            //生成一个本地变量 result
            LocalBuilder result = generator.DeclareLocal(typeof(T));

            //实例化本地变量 result
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));

            //设置堆栈中的值 到本地变量
            generator.Emit(OpCodes.Stloc, result);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo pi = typeof(T).GetProperty(dt.Columns[i].ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                //定义一个标记 这里是如果是null的时候跳转使用的标记
                System.Reflection.Emit.Label endIfLabel = generator.DefineLabel();
                if (pi != null && pi.GetSetMethod() != null)
                {
                    //加载参数1 即DataRow  到堆栈
                    generator.Emit(OpCodes.Ldarg_0);

                    //加载整形 i 到堆栈
                    generator.Emit(OpCodes.Ldc_I4, i);

                    //调用方DataRow.IsNull(i) 并把返回值保存到堆栈中 真=1 假=0
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);

                    //判断堆栈里面的值是否为真 真=1 假=0 如果不是那么跳转到endIfLabel标记
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    //加载本地变量result 到堆栈
                    generator.Emit(OpCodes.Ldloc, result);

                    //加载参数1 即DataRow  到堆栈
                    generator.Emit(OpCodes.Ldarg_0);

                    //加载整形 i 到堆栈
                    generator.Emit(OpCodes.Ldc_I4, i);

                    //调用方法DataRow.get_Item(i) 并把返回值保存到堆栈中
                    generator.Emit(OpCodes.Call, getValueMethod.MakeGenericMethod(pi.PropertyType));

                    //拆箱操作 堆栈里面的值(int)dr.getValue(0) 使用了上面的DataReaderExtend 中的getValue 扩展方法 以后就不需要拆箱操作了
                    //如果仅仅使用系统的DataRow.getValue方法的话这里需要拆箱操作
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
            return (Func<DataRow, T>)method.CreateDelegate(typeof(Func<DataRow, T>));
        }
    }

    #endregion DataTable扩展
}