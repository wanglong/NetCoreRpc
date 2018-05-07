using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：FloatDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:46:08
    /// </summary>
    public sealed class FloatDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeFloat(inputBytes, startOffset, out nextStartOffset);
        }
    }
}