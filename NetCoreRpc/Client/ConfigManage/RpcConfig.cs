using NetCoreRpc.ServerRoute;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NetCoreRpc.Client.ConfigManage
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RemoteEndPointConfig.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务端地址配置
    /// 创建标识：yjq 2018/1/19 14:33:42
    /// </summary>
    public class RpcConfig
    {
        private static IRouteCoordinator _RouteCoordinator;

        static RpcConfig()
        {
            _RouteCoordinator = DependencyManage.Resolve<ICoordinatorFactory>().Create();
        }

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int RequestTimeouMillis { get; set; }

        public string Default { get; set; }

        public int MaxClientPoolCount { get; set; } = 200;

        public List<RemoteEndPointConfigGroupInfo> Group { get; set; }

        public ZkConfig Zookeeper { get; set; }

        public IPEndPoint GetEndPoint(string typeName)
        {
            if (Group != null && Group.Any())
            {
                var groupInfo = Group.Where(m => m.NameSpace.Split(',').Contains(typeName)).FirstOrDefault();
                if (groupInfo != null && !string.IsNullOrWhiteSpace(groupInfo.Address))
                {
                    var serverList = groupInfo.Address.Split(',');
                    var availableServer = _RouteCoordinator.GetAvailableServerListAsync(serverList.ToList()).Result;
                    var ipPointInfo = availableServer.Split(':');
                    if (ipPointInfo.Length == 2)
                    {
                        return new IPEndPoint(IPAddress.Parse(ipPointInfo[0]), int.Parse(ipPointInfo[1]));
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(Default))
            {
                var serverList = Default.Split(',');
                var availableServer = _RouteCoordinator.GetAvailableServerListAsync(serverList.ToList()).Result;
                var ipPointInfo = availableServer.Split(':');
                if (ipPointInfo.Length == 2)
                {
                    return new IPEndPoint(IPAddress.Parse(ipPointInfo[0]), int.Parse(ipPointInfo[1]));
                }
            }
            throw new System.Exception("服务端地址配置错误");
        }
    }

    public class RemoteEndPointConfigGroupInfo
    {
        public string NameSpace { get; set; }

        public string Address { get; set; }
    }

    public class ZkConfig
    {
        public string Connection { get; set; }

        public string ParentName { get; set; }
    }
}