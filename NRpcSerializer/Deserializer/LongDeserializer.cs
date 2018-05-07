using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：LongDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:47:05
    /// </summary>
    public sealed class LongDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeLong(inputBytes, startOffset, out nextStartOffset);
        }
    }
}