using System;
using System.Text;

namespace NetCoreRpc.Extensions
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：StringBuilderExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 19:45:30
    /// </summary>
    internal static class StringBuilderExtension
    {
        /// <summary>
        /// 根据条件进行拼接
        /// </summary>
        /// <param name="sb">要拼接的StringBuilder</param>
        /// <param name="condition">拼接的条件</param>
        /// <param name="value">拼接的内容</param>
        /// <returns>拼接后的值</returns>
        internal static StringBuilder AppendIf(this StringBuilder sb, bool condition, string value)
        {
            if (condition)
            {
                return sb.Append(value);
            }

            return sb;
        }

        /// <summary>
        /// 根据条件进行拼接
        /// </summary>
        /// <param name="sb">要拼接的StringBuilder</param>
        /// <param name="condition">委托方法</param>
        /// <param name="value">拼接的内容</param>
        /// <returns>拼接后的值</returns>
        internal static StringBuilder AppendIf(this StringBuilder sb, Func<bool> condition, string value)
        {
            if (condition != null && condition())
            {
                return sb.Append(value);
            }

            return sb;
        }

        /// <summary>
        /// 根据条件进行拼接
        /// </summary>
        /// <param name="sb">要拼接的StringBuilder</param>
        /// <param name="condition">拼接的条件</param>
        /// <param name="format">符合格式字符串（参见“备注”）。</param>
        /// <param name="args">要设置其格式的对象的数组。</param>
        /// <returns>拼接后的值</returns>
        internal static StringBuilder AppendFormatIf(this StringBuilder sb, bool condition, string format, params object[] args)
        {
            if (condition)
            {
                return sb.AppendFormat(format, args);
            }

            return sb;
        }

        /// <summary>
        /// 根据条件进行拼接
        /// </summary>
        /// <param name="sb">要拼接的StringBuilder</param>
        /// <param name="condition">委托方法</param>
        /// <param name="format">符合格式字符串（参见“备注”）。</param>
        /// <param name="args">要设置其格式的对象的数组。</param>
        /// <returns>拼接后的值</returns>
        internal static StringBuilder AppendFormatIf(this StringBuilder sb, Func<bool> condition, string format, params object[] args)
        {
            if (condition != null && condition())
            {
                return sb.AppendFormat(format, args);
            }

            return sb;
        }
    }
}