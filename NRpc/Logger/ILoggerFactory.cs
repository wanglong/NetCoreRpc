using System;

namespace NRpc.Logger
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ILoggerFactory.cs
    /// 类属性：接口
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 10:13:30
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 根据loggerName创建ILogger
        /// </summary>
        /// <param name="loggerName">logger名字</param>
        /// <returns>ILogger</returns>
        ILogger Create(string loggerName);

        /// <summary>
        /// 根据类型创建ILogger
        /// </summary>
        /// <param name="loggerType">logger类型</param>
        /// <returns>ILogger</returns>
        ILogger Create(Type loggerType);
    }
}