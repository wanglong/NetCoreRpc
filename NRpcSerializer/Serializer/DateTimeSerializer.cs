using NRpcSerializer.Utils;
using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：DateTimeSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:41:14
    /// </summary>
    public sealed class DateTimeSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_DateTime);
            serializerInputStream.Write(ByteUtil.EncodeDateTime((DateTime)obj));
        }
    }
}