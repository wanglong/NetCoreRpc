using NRpc.LockManage;
using NRpc.Transport.Remoting;
using NRpc.Transport.Socketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NRpc.Client.CilentManage
{
    /// <summary>
    /// 类名：ClientPool.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 16:06:57
    /// </summary>
    public class ClientPool : SelfDisposable
    {
        public int CurrentRequestCount = 0;
        public int PrepareCreateCount = 0;

        public ClientPool(int maxCount, IPEndPoint iPEndPoint)
        {
            IPEndPoint = iPEndPoint;
            MaxCount = maxCount;
            InstallPool();
        }

        public int CurrentCount { get; private set; } = 0;
        public int MaxCount { get; }
        public List<SocketRemotingClient> ConnectedClientList { get; set; } = new List<SocketRemotingClient>();
        public IPEndPoint IPEndPoint { get; }

        public bool IsCanCreateMore
        {
            get
            {
                return CurrentCount + PrepareCreateCount < MaxCount;
            }
        }

        public SocketRemotingClient GetCilent()
        {
            Interlocked.Increment(ref CurrentRequestCount);
            if (CurrentCount > 0)
            {
                return ConnectedClientList[CurrentRequestCount % CurrentCount];
            }
            else
            {
                return CreateClient();
            }
        }

        public void InstallPool()
        {
            if (IsCanCreateMore)
            {
                Task.Factory.StartNew(() => { CreateClientPool(); });
            }
        }

        private void CreateClientPool()
        {
            for (int i = CurrentCount + PrepareCreateCount; i < MaxCount; i++)
            {
                if (IsCanCreateMore)
                {
                    CreateClient();
                }
            }
        }

        private SocketRemotingClient CreateClient()
        {
            SocketRemotingClient client = new SocketRemotingClient(IPEndPoint);
            client.RegisterConnectionEventListener(new ClientPoolConnectionLister(client, this));
            Interlocked.Increment(ref PrepareCreateCount);
            client.Start();
            return client;
        }

        protected override void DisposeCode()
        {
            if (ConnectedClientList.Any())
            {
                foreach (var item in ConnectedClientList)
                {
                    if (item.IsConnected)
                        item.Shutdown();
                }
            }
        }

        private class ClientPoolConnectionLister : IConnectionEventListener
        {
            private ILock _lock;
            private SocketRemotingClient _client;
            private ClientPool _clientPool;

            public ClientPoolConnectionLister(SocketRemotingClient client, ClientPool clientPool)
            {
                _client = client;
                _clientPool = clientPool;
                _lock = DependencyManage.Resolve<ILock>();
            }

            public void OnConnectionAccepted(ITcpConnection connection)
            {
            }

            public void OnConnectionClosed(ITcpConnection connection, SocketError socketError)
            {
                var operateID = Guid.NewGuid().ToString();
                var lockkey = connection.RemotingEndPoint.ToString();
                if (_lock.LockTake(lockkey, operateID, TimeSpan.FromSeconds(10)))
                {
                    _clientPool.ConnectedClientList.Remove(_client);
                    _clientPool.CurrentCount -= 1;
                    _clientPool.InstallPool();
                    _lock.LockRelease(lockkey, operateID);
                }
                else
                {
                    OnConnectionClosed(connection, socketError);
                }
            }

            public void OnConnectionEstablished(ITcpConnection connection)
            {
                var operateID = Guid.NewGuid().ToString();
                var lockkey = connection.RemotingEndPoint.ToString();
                if (_lock.LockTake(lockkey, operateID, TimeSpan.FromSeconds(10)))
                {
                    _clientPool.ConnectedClientList.Add(_client);
                    Interlocked.Decrement(ref _clientPool.PrepareCreateCount);
                    _clientPool.CurrentCount += 1;
                    _lock.LockRelease(lockkey, operateID);
                }
                else
                {
                    OnConnectionEstablished(connection);
                }
            }

            public void OnConnectionFailed(EndPoint remotingEndPoint, SocketError socketError)
            {
                Interlocked.Decrement(ref _clientPool.PrepareCreateCount);
                _clientPool.InstallPool();
            }
        }
    }
}