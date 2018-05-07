using NRpcSerializer.Utils;
using System;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：EnumDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:38:07
    /// </summary>
    public sealed class EnumDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var enumTypeName = ByteUtil.DecodeString(inputBytes, startOffset, out nextStartOffset);
            startOffset = nextStartOffset;
            var enumValue = ByteUtil.DecodeInt(inputBytes, startOffset, out nextStartOffset);
            var type = SerializerUtil.GetType(enumTypeName);
            return Enum.ToObject(type, enumValue);
        }
    }
}