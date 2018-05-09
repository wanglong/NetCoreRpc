using NetCoreRpc.Client.ConfigManage;
using NetCoreRpc.Utils;

namespace NetCoreRpc.ServerRoute.ZK
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
        private readonly IRemoteEndPointConfigProvider _remoteEndPointConfigProvider;

        public ZkCoordinatorFactory(IRemoteEndPointConfigProvider remoteEndPointConfigProvider)
        {
            _remoteEndPointConfigProvider = remoteEndPointConfigProvider;
        }

        public IRouteCoordinator Create()
        {
            var config = _remoteEndPointConfigProvider.GetConfig();
            var connectionStr = config?.Zookeeper?.Connection;
            var parentName = config?.Zookeeper?.ParentName; 
            Ensure.NotNullAndNotEmpty(connectionStr, "NetCoreRpc:Zookeeper:Connection");
            Ensure.NotNullAndNotEmpty(parentName, "NetCoreRpc:Zookeeper:ParentName");
            var option = new RouteCoordinatorOption
            {
                ConnectionString = connectionStr,
                ParentName = parentName
            };
            return new ZkCoordinator(option);
        }
    }
}