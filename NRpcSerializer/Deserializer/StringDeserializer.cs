using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：StringDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:50:56
    /// </summary>
    public sealed class StringDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeString(inputBytes, startOffset, out nextStartOffset);
        }
    }
}