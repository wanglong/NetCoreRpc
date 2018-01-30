using NetCoreRpc.ServerRoute;
using NetCoreRpc.Transport.Remoting;
using NetCoreRpc.Transport.Socketing;
using System.Net;

namespace NetCoreRpc.Server
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
            _socketRemotingServer = new SocketRemotingServer(_iPEndPoint).RegisterRequestHandler(100, new NetCoreRpcHandle());
            _routeCoordinator = DependencyManage.Resolve<ICoordinatorFactory>().Create();
        }

        public NRpcServer(string ip, int port)
        {
            _iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _socketRemotingServer = new SocketRemotingServer(_iPEndPoint).RegisterRequestHandler(100, new NetCoreRpcHandle());
        }

        public void Start(params string[] assemblyNameList)
        {
            ServerAssemblyUtil.AddAssemblyList(assemblyNameList);
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