using System;
using System.Text;

namespace NRpc.Extensions
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ExceptionExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 10:17:43
    /// </summary>
    internal static class ExceptionExtension
    {
        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <returns>错误异常信息</returns>
        internal static string ToErrMsg(this Exception ex, string memberName = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            if (ex.InnerException != null)
            {
                if (!string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase))
                {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            return errorBuilder.ToString();
        }
    }
}