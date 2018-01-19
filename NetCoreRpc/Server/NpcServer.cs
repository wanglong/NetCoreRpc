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

        public NRpcServer(int port)
        {
            _iPEndPoint = new IPEndPoint(SocketUtils.GetLocalIPV4(), port);
            _socketRemotingServer = new SocketRemotingServer(_iPEndPoint).RegisterRequestHandler(100, new NRpcHandle());
        }

        public void Start(params string[] assemblyNameList)
        {
            ServerAssemblyUtil.AddAssemblyList(assemblyNameList);
            _socketRemotingServer.Start();
        }

        public void ShutDown()
        {
            _socketRemotingServer?.Shutdown();
        }
    }
}