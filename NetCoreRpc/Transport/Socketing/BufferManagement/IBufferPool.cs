namespace NetCoreRpc.Transport.Socketing.BufferManagement
{
    public interface IBufferPool : IPool<byte[]>
    {
        int BufferSize { get; }
    }
}