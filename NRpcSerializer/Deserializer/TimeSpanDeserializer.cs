using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：TimeSpanDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:51:28
    /// </summary>
    public sealed class TimeSpanDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeTimeSpan(inputBytes, startOffset, out nextStartOffset);
        }
    }
}