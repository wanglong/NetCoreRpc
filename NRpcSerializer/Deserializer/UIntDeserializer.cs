using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：UIntDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:51:54
    /// </summary>
    public sealed class UIntDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeUInt(inputBytes, startOffset, out nextStartOffset);
        }
    }
}