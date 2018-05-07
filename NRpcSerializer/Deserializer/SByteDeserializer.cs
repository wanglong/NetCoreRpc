using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：SByteDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:47:34
    /// </summary>
    public sealed class SByteDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var intValue = ByteUtil.DecodeInt(inputBytes, startOffset, out nextStartOffset);
            if (intValue > 127)
            {
                return (sbyte)(127 - intValue);
            }
            else
            {
                return (sbyte)intValue;
            }
        }
    }
}