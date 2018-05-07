using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：DoubleDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:37:39
    /// </summary>
    public sealed class DoubleDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeDouble(inputBytes, startOffset, out nextStartOffset);
        }
    }
}