using NRpc.Extensions;
using NRpc.Logger;
using System;
using System.Runtime.CompilerServices;

namespace NRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：LogUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 19:03:26
    /// </summary>
    internal static class LogUtil
    {
        private static Logger.ILogger GetLogger(string loggerName = null)
        {
            var loggerFactory = DependencyManage.Resolve<ILoggerFactory>();

            if (string.IsNullOrWhiteSpace(loggerName))
            {
                loggerName = "NRpc.Public";
            }
            return loggerFactory.Create(loggerName);
        }

        /// <summary>
        /// 输出调试日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Debug(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Debug(msg);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="args">格式参数</param>
        public static void DebugFormat(string format, params object[] args)
        {
            GetLogger().DebugFormat(format, args);
        }

        /// <summary>
        /// 输出普通日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Info(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Info(msg);
        }

        /// <summary>
        /// 记录普通日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void InfoFormat(string format, params object[] args)
        {
            GetLogger().InfoFormat(format, args);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="loggerName"></param>
        public static void Warn(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Warn(msg);
        }

        /// <summary>
        /// 输出警告日志信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="memberName"></param>
        /// <param name="loggerName"></param>
        public static void Warn(Exception ex, [CallerMemberName]string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Warn(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Error(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Error(msg);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="loggerName"></param>
        public static void Error(string msg, Exception ex, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Error(msg, ex);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="args">格式参数</param>
        public static void ErrorFormat(string format, params object[] args)
        {
            GetLogger().ErrorFormat(format, args);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Error(Exception ex, [CallerMemberName]string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Error(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出严重错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Fatal(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Fatal(msg);
        }

        /// <summary>
        /// 输出严重错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Fatal(Exception ex, [CallerMemberName]string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Fatal(ex.ToErrMsg(memberName: memberName));
        }
    }
}