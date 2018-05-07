using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：ShortDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:50:22
    /// </summary>
    public sealed class ShortDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeShort(inputBytes, startOffset, out nextStartOffset);
        }
    }
}