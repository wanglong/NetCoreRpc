using NRpc.ServerRoute;
using NRpc.Transport.Remoting;
using NRpc.Transport.Socketing;
using System;
using System.Net;
using System.Reflection;

namespace NRpc.Server
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：Server.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 14:34:51
    /// </summary>
    public class NRpcServer
    {
        private IPEndPoint _iPEndPoint;
        private readonly SocketRemotingServer _socketRemotingServer;
        private readonly IRouteCoordinator _routeCoordinator;

        public NRpcServer(int port)
        {
            _iPEndPoint = new IPEndPoint(SocketUtils.GetLocalIPV4(), port);
            _socketRemotingServer = new SocketRemotingServer(_iPEndPoint).RegisterRequestHandler(100, new NRpcHandle());
            _routeCoordinator = DependencyManage.Resolve<ICoordinatorFactory>().Create();
        }

        public NRpcServer(string ip, int port)
        {
            _iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _socketRemotingServer = new SocketRemotingServer(_iPEndPoint).RegisterRequestHandler(100, new NRpcHandle());
        }

        public void RegisterServerType(params Type[] serverType)
        {
            foreach (var item in serverType)
            {
                ServerAssemblyUtil.InstallType(item);
            }
        }

        public void RegisterServerAssembly(Assembly assembly)
        {
            ServerAssemblyUtil.InstallAssembly(assembly);
        }

        public void Start()
        {
            NRpcConfigWatcher.Install();
            _socketRemotingServer.Start();
            _routeCoordinator.RegisterAsync(_iPEndPoint).Wait();
        }

        public void ShutDown()
        {
            _socketRemotingServer?.Shutdown();
            _routeCoordinator.DeleteAsync(_iPEndPoint).Wait();
            _routeCoordinator.CloseAsync().Wait();
        }
    }
}