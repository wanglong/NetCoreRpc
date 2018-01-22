using Microsoft.Extensions.DependencyInjection;
using NetCoreRpc.ServerRoute.ZK;

namespace NetCoreRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ServerRouteRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 20:09:09
    /// </summary>
    public static class ServerRouteRegisterExtension
    {
        public static IServiceCollection UseDefaultServerRoute(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ICoordinatorFactory, DefalutCoordinatorFactory>();
        }

        public static IServiceCollection UseZKServerRoute(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ICoordinatorFactory, ZkCoordinatorFactory>();
        }
    }
}