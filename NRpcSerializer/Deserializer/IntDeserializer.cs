using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：IntDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:46:36
    /// </summary>
    public sealed class IntDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeInt(inputBytes, startOffset, out nextStartOffset);
        }
    }
}