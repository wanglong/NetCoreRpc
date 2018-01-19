namespace NetCoreRpc.Transport.Socketing.BufferManagement
{
    internal class BufferPool : IntelliPool<byte[]>, IBufferPool
    {
        public int BufferSize { get; private set; }

        public BufferPool(int bufferSize, int initialCount)
            : base(initialCount, new BufferItemCreator(bufferSize))
        {
            BufferSize = bufferSize;
        }
    }
}