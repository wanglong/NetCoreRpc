namespace NRpc.Transport.Remoting
{
    public interface IRemotingServerMessageHandler
    {
        void HandleMessage(RemotingServerMessage message);
    }
}