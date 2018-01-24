using NRpc.Utils;

namespace NRpc.ServerRoute.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ZkCoordinatorFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 20:01:32
    /// </summary>
    public sealed class ZkCoordinatorFactory : ICoordinatorFactory
    {
        public IRouteCoordinator Create()
        {
            var connectionStr = NRpcConfigWatcher.CurrentConfig.Zookeeper.Connection;
            var parentName = NRpcConfigWatcher.CurrentConfig.Zookeeper.ParentName;
            Ensure.NotNullAndNotEmpty(connectionStr, "NRpc:Zookeeper:Connection");
            Ensure.NotNullAndNotEmpty(parentName, "NRpc:Zookeeper:ParentName");
            var option = new RouteCoordinatorOption
            {
                ConnectionString = connectionStr,
                ParentName = parentName
            };
            return new ZkCoordinator(option);
        }
    }
}