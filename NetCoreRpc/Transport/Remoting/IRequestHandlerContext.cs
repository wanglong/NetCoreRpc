using NetCoreRpc.Transport.Socketing;
using System;

namespace NetCoreRpc.Transport.Remoting
{
    public interface IRequestHandlerContext
    {
        ITcpConnection Connection { get; }
        Action<RemotingResponse> SendRemotingResponse { get; }
    }
}