using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：DateTimeDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:37:00
    /// </summary>
    public sealed class DateTimeDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeDateTime(inputBytes, startOffset, out nextStartOffset);
        }
    }
}