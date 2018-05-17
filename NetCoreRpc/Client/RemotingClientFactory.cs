using NetCoreRpc.Client.CilentManage;
using NetCoreRpc.Client.ConfigManage;
using NetCoreRpc.Extensions;
using NetCoreRpc.Transport.Remoting;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace NetCoreRpc.Client
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RemotingClientFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:57:38
    /// </summary>
    internal class RemotingClientFactory
    {
        private static ConcurrentDictionary<EndPoint, ClientPool> ClientPoolList = new ConcurrentDictionary<EndPoint, ClientPool>();

        public static SocketRemotingClient GetClient(Type classType)
        {
            var className = classType.FullName;
            //TODO 根据类名字动态获取IP端口信息
            var remoteEndPointConfigProvider = DependencyManage.Resolve<IRemoteEndPointConfigProvider>();
            var config = remoteEndPointConfigProvider.GetConfig();
            var ipEndPoint = config.GetEndPoint(classType.FullName);
            var clientPool = ClientPoolList.GetValue(ipEndPoint, () =>
            {
                return new ClientPool(config.MaxClientPoolCount, ipEndPoint);
            });
            return clientPool.GetCilent();
        }

        internal static void RegisterUnLoad()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            foreach (var item in ClientPoolList)
            {
                item.Value?.Dispose();
            }
        }
    }
}