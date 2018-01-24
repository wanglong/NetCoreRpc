using NRpc.Transport.Socketing;
using System;

namespace NRpc.Transport.Remoting
{
    public interface IRequestHandlerContext
    {
        ITcpConnection Connection { get; }
        Action<RemotingResponse> SendRemotingResponse { get; }
    }
}