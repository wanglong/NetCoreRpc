using NRpcSerializer.Utils;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：CharDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:35:57
    /// </summary>
    public sealed class CharDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeChar(inputBytes, startOffset, out nextStartOffset);
        }
    }
}