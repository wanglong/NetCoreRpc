using NetCoreRpc.Transport.Remoting;
using NetCoreRpc.Transport.Socketing;
using NetCoreRpc.Utils;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

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
        private static ConcurrentDictionary<EndPoint, SocketRemotingClient> WaitConnectingClientList = new ConcurrentDictionary<EndPoint, SocketRemotingClient>();
        private static ConcurrentDictionary<EndPoint, SocketRemotingClient> ConnectedClientList = new ConcurrentDictionary<EndPoint, SocketRemotingClient>();

        public static SocketRemotingClient GetClient(Type classType)
        {
            var className = classType.FullName;
            //TODO 根据类名字动态获取IP端口信息
            var ipEndPoint = RemoteEndPointConfig.GetServerEndPoint(classType.FullName); 
            if (ConnectedClientList.ContainsKey(ipEndPoint))
            {
                return ConnectedClientList[ipEndPoint];
            }
            SocketRemotingClient client = new SocketRemotingClient(ipEndPoint);
            client.RegisterConnectionEventListener(new ProxyClientConnectionLister());
            WaitConnectingClientList.TryAdd(client.ServerEndPoint, client);
            client.Start();
            if (!ConnectedClientList.ContainsKey(ipEndPoint) || ConnectedClientList[ipEndPoint] == null)
            {
                throw new ArgumentNullException("Client", "连接远程失败");
            }
            return client;
        }

        private class ProxyClientConnectionLister : IConnectionEventListener
        {
            public void OnConnectionAccepted(ITcpConnection connection)
            {
            }

            public void OnConnectionClosed(ITcpConnection connection, SocketError socketError)
            {
                if (ConnectedClientList.TryRemove(connection.RemotingEndPoint, out SocketRemotingClient client))
                {
                    client.Shutdown();
                }
            }

            public void OnConnectionEstablished(ITcpConnection connection)
            {
                if (WaitConnectingClientList.TryRemove(connection.RemotingEndPoint, out SocketRemotingClient client))
                {
                    ConnectedClientList.TryAdd(connection.RemotingEndPoint, client);
                }
            }

            public void OnConnectionFailed(EndPoint remotingEndPoint, SocketError socketError)
            {
                WaitConnectingClientList.TryRemove(remotingEndPoint, out SocketRemotingClient client);
            }
        }

        internal static void RegisterUnLoad()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            LogUtil.Debug("执行关闭客户端方法");
            foreach (var item in WaitConnectingClientList)
            {
                item.Value?.Shutdown();
            }
            foreach (var item in ConnectedClientList)
            {
                item.Value?.Shutdown();
            }
        }
    }
}