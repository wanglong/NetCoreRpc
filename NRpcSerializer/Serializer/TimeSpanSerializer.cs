using NRpcSerializer.Utils;
using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：TimeSpanSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:11:56
    /// </summary>
    public sealed class TimeSpanSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_TimeSpan);
            serializerInputStream.Write(ByteUtil.EncodeTimeSpan((TimeSpan)obj));
        }
    }
}