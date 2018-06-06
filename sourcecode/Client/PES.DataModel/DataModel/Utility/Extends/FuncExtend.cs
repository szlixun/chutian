using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PES.DataModel
{
    // 摘要:
    //     封装一个方法，该方法具有五个参数，并返回 TResult 参数所指定的类型的值。
    //
    // 参数:
    //   arg1:
    //     此委托封装的方法的第一个参数。
    //
    //   arg2:
    //     此委托封装的方法的第二个参数。
    //
    //   arg3:
    //     此委托封装的方法的第三个参数。
    //
    //   arg4:
    //     此委托封装的方法的第四个参数。
    //
    //   arg5:
    //     此委托封装的方法的第五个参数。
    //
    // 类型参数:
    //   T1:
    //     此委托封装的方法的第一个参数类型。
    //
    //   T2:
    //     此委托封装的方法的第二个参数类型。
    //
    //   T3:
    //     此委托封装的方法的第三个参数类型。
    //
    //   T4:
    //     此委托封装的方法的第四个参数类型。
    //
    //   T5:
    //     此委托封装的方法的第五个参数的类型。
    //
    //   TResult:
    //     此委托封装的方法的返回值类型。
    //
    // 返回结果:
    //     此委托封装的方法的返回值。
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}
