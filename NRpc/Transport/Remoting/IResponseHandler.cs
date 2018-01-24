namespace NRpc.Transport.Remoting
{
    public interface IResponseHandler
    {
        void HandleResponse(RemotingResponse remotingResponse);
    }
}