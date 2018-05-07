using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：ByteArrayDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:35:27
    /// </summary>
    public sealed class ByteArrayDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
        }
    }
}