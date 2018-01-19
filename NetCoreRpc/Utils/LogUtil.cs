using Microsoft.Extensions.Logging;
using NetCoreRpc.Extensions;
using System;
using System.Runtime.CompilerServices;

namespace NetCoreRpc.Utils
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
        private static ILogger GetLogger(string loggerName = null)
        {
            var loggerFactory = DependencyManage.Resolve<ILoggerFactory>();

            if (string.IsNullOrWhiteSpace(loggerName))
            {
                loggerName = "NetCoreRpc.Public";
            }
            return loggerFactory.CreateLogger(loggerName);
        }

        /// <summary>
        /// 输出追踪日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Trace(string msg)
        {
            GetLogger().LogTrace(msg);
        }

        /// <summary>
        /// 输出调试日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Debug(string msg)
        {
            GetLogger().LogDebug(msg);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="args">格式参数</param>
        public static void DebugFormat(string format, params object[] args)
        {
            GetLogger().LogDebug(format, args);
        }

        /// <summary>
        /// 输出普通日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Info(string msg)
        {
            GetLogger().LogInformation(msg);
        }

        /// <summary>
        /// 记录普通日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void InfoFormat(string format, params object[] args)
        {
            GetLogger().LogInformation(format, args);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Warn(string msg)
        {
            GetLogger().LogWarning(msg);
        }

        /// <summary>
        /// 输出警告日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="memberName">调用方法名字</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Warn(Exception ex, string memberName = null)
        {
            GetLogger().LogWarning(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Error(string msg)
        {
            GetLogger().LogError(msg);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="memberName">调用方法名字</param>
        public static void Error(Exception ex, [CallerMemberName]string memberName = null)
        {
            GetLogger().LogError(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="loggerName"></param>
        public static void Error(string msg, Exception ex)
        {
            GetLogger().LogError(ex, msg);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="args">格式参数</param>
        public static void ErrorFormat(string format, params object[] args)
        {
            GetLogger().LogError(format, args);
        }
    }
}