using NRpc.ServerRoute.ZK;

namespace NRpc.ServerRoute
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
        public static DependencyManage UseDefaultServerRoute(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<ICoordinatorFactory, DefalutCoordinatorFactory>();
        }

        public static DependencyManage UseZKServerRoute(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<ICoordinatorFactory, ZkCoordinatorFactory>();
        }
    }
}