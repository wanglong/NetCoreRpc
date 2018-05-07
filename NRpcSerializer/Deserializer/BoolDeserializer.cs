using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：BoolDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:20:08
    /// </summary>
    public sealed class BoolDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeBool(inputBytes, startOffset, out nextStartOffset);
        }
    }
}