using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreRpc.Client;
using NetCoreRpc.Scheduling;
using NetCoreRpc.Serializing;
using NetCoreRpc.Transport;
using NetCoreRpc.Utils;
using System;
using System.Collections.Generic;

namespace NetCoreRpc
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DependencyManage.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 18:52:22
    /// </summary>
    public static class DependencyManage
    {
        public static IServiceCollection UseRpc(this IServiceCollection serviceCollection)
        {
            RemotingClientFactory.RegisterUnLoad();
            return serviceCollection.UseDefaultSerializer().UseDefaultMethodSerializer().UseDefaultResponseSerializer().UseDefaultSchedule().UseSocket();
        }

        private static IServiceProvider _ServiceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            Ensure.NotNull(serviceProvider, "ServiceProvider");
            _ServiceProvider = serviceProvider;
        }

        public static void SetServiceProvider(IServiceProvider serviceProvider, IConfigurationRoot configurationRoot)
        {
            Ensure.NotNull(serviceProvider, "ServiceProvider");
            _ServiceProvider = serviceProvider;
            Ensure.NotNull(configurationRoot, "ConfigurationRoot");
            ConfigurationManage.SetConfiguration(configurationRoot);
        }

        internal static IServiceProvider ServiceProvider
        {
            get
            {
                return _ServiceProvider;
            }
        }

        public static IServiceScope BeginScope()
        {
            return ServiceProvider.CreateScope();
        }

        internal static T Resolve<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        internal static object Resolve(Type type)
        {
            return ServiceProvider.GetRequiredService(type);
        }

        internal static IEnumerable<T> ResolveServices<T>()
        {
            return ServiceProvider.GetServices<T>();
        }

        internal static IEnumerable<object> ResolveServices(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType);
        }
    }
}