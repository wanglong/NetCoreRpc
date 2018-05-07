using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：UShortDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:52:42
    /// </summary>
    public sealed class UShortDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeUShort(inputBytes, startOffset, out nextStartOffset);
        }
    }
}