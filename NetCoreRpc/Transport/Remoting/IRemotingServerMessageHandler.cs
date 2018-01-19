namespace NetCoreRpc.Transport.Remoting
{
    public interface IRemotingServerMessageHandler
    {
        void HandleMessage(RemotingServerMessage message);
    }
}