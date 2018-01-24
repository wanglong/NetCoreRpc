using NRpc.Logger.NLogger;

namespace NRpc.Logger
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：LoggerRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 10:37:59
    /// </summary>
    internal static class LoggerRegisterExtension
    {
        /// <summary>
        /// 使用Nlog
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static DependencyManage UseNlog(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<ILoggerFactory, NLoggerFactory>();
        }
    }
}