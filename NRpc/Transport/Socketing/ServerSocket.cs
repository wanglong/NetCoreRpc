using NRpc.Extensions;
using NRpc.Transport.Socketing.BufferManagement;
using NRpc.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NRpc.Transport.Socketing
{
    public class ServerSocket
    {
        #region Private Variables

        private readonly Socket _socket;
        private readonly SocketSetting _setting;
        private readonly IPEndPoint _listeningEndPoint;
        private readonly SocketAsyncEventArgs _acceptSocketArgs;
        private readonly IList<IConnectionEventListener> _connectionEventListeners;
        private readonly Action<ITcpConnection, byte[], Action<byte[]>> _messageArrivedHandler;
        private readonly IBufferPool _receiveDataBufferPool;
        private readonly ConcurrentDictionary<Guid, ITcpConnection> _connectionDict;

        #endregion Private Variables

        public ServerSocket(IPEndPoint listeningEndPoint, SocketSetting setting, IBufferPool receiveDataBufferPool, Action<ITcpConnection, byte[], Action<byte[]>> messageArrivedHandler)
        {
            Ensure.NotNull(listeningEndPoint, "listeningEndPoint");
            Ensure.NotNull(setting, "setting");
            Ensure.NotNull(receiveDataBufferPool, "receiveDataBufferPool");
            Ensure.NotNull(messageArrivedHandler, "messageArrivedHandler");

            _listeningEndPoint = listeningEndPoint;
            _setting = setting;
            _receiveDataBufferPool = receiveDataBufferPool;
            _connectionEventListeners = new List<IConnectionEventListener>();
            _messageArrivedHandler = messageArrivedHandler;
            _connectionDict = new ConcurrentDictionary<Guid, ITcpConnection>();
            _socket = SocketUtils.CreateSocket(_setting.SendBufferSize, _setting.ReceiveBufferSize);
            _acceptSocketArgs = new SocketAsyncEventArgs();
            _acceptSocketArgs.Completed += AcceptCompleted;
        }

        public void RegisterConnectionEventListener(IConnectionEventListener listener)
        {
            _connectionEventListeners.Add(listener);
        }

        public void Start()
        {
            LogUtil.InfoFormat("Socket server is starting, listening on TCP endpoint: {0}.", _listeningEndPoint);

            try
            {
                _socket.Bind(_listeningEndPoint);
                _socket.Listen(5000);
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Failed to listen on TCP endpoint: {0}.", _listeningEndPoint), ex);
                SocketUtils.ShutdownSocket(_socket);
                throw;
            }

            StartAccepting();
        }

        public void Shutdown()
        {
            SocketUtils.ShutdownSocket(_socket);
            LogUtil.InfoFormat("Socket server shutdown, listening TCP endpoint: {0}.", _listeningEndPoint);
        }

        public void PushMessageToAllConnections(byte[] message)
        {
            foreach (var connection in _connectionDict.Values)
            {
                connection.QueueMessage(message);
            }
        }

        public void PushMessageToConnection(Guid connectionId, byte[] message)
        {
            ITcpConnection connection;
            if (_connectionDict.TryGetValue(connectionId, out connection))
            {
                connection.QueueMessage(message);
            }
        }

        public IList<ITcpConnection> GetAllConnections()
        {
            return _connectionDict.Values.ToList();
        }

        private void StartAccepting()
        {
            try
            {
                var firedAsync = _socket.AcceptAsync(_acceptSocketArgs);
                if (!firedAsync)
                {
                    ProcessAccept(_acceptSocketArgs);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ObjectDisposedException))
                {
                    LogUtil.Info("Socket accept has exception." + ex);
                }
                Task.Factory.StartNew(() => StartAccepting());
            }
        }

        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError == SocketError.Success)
                {
                    var acceptSocket = e.AcceptSocket;
                    e.AcceptSocket = null;
                    OnSocketAccepted(acceptSocket);
                }
                else
                {
                    SocketUtils.ShutdownSocket(e.AcceptSocket);
                    e.AcceptSocket = null;
                }
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                LogUtil.Error("Process socket accept has exception.", ex);
            }
            finally
            {
                StartAccepting();
            }
        }

        private void OnSocketAccepted(Socket socket)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var connection = new TcpConnection(socket, _setting, _receiveDataBufferPool, OnMessageArrived, OnConnectionClosed);
                    if (_connectionDict.TryAdd(connection.Id, connection))
                    {
                        LogUtil.InfoFormat("Socket accepted, remote endpoint:{0}", socket.RemoteEndPoint);

                        foreach (var listener in _connectionEventListeners)
                        {
                            try
                            {
                                listener.OnConnectionAccepted(connection);
                            }
                            catch (Exception ex)
                            {
                                LogUtil.Error(string.Format("Notify connection accepted failed, listener type:{0}", listener.GetType().Name), ex);
                            }
                        }
                    }
                    else
                    {
                        LogUtil.InfoFormat("Duplicated tcp connection, remote endpoint:{0}", socket.RemoteEndPoint);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    LogUtil.Info("Accept socket client has unknown exception." + ex);
                }
            });
        }

        private void OnMessageArrived(ITcpConnection connection, byte[] message)
        {
            try
            {
                _messageArrivedHandler(connection, message, reply =>
                {
                    Task.Factory.StartNew(() => connection.QueueMessage(reply));
                });
            }
            catch (Exception ex)
            {
                LogUtil.Error("Handle message error.", ex);
            }
        }

        private void OnConnectionClosed(ITcpConnection connection, SocketError socketError)
        {
            _connectionDict.Remove(connection.Id);
            foreach (var listener in _connectionEventListeners)
            {
                try
                {
                    listener.OnConnectionClosed(connection, socketError);
                }
                catch (Exception ex)
                {
                    LogUtil.Error(string.Format("Notify connection closed failed, listener type:{0}", listener.GetType().Name), ex);
                }
            }
        }
    }
}