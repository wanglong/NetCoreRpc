using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：ULongDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:52:19
    /// </summary>
    public sealed class ULongDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeULong(inputBytes, startOffset, out nextStartOffset);
        }
    }
}