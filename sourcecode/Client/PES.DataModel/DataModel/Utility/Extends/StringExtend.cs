namespace PES.DataModel.Extends
{
    #region 字符串扩展

    /// <summary>
    /// 字符串扩展
    /// </summary>
    internal static class StringExtend
    {
        #region 空值判断

        /// <summary>
        /// 判断是否不存在中字符
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>存在字符：true代表是，false代表否</returns>
        public static bool IsNotNullAndEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断是否存在中字符
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>存在字符：true代表是，false代表否</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        #endregion 空值判断
    }

    #endregion 字符串扩展
}