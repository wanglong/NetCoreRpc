using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：StringSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:10:13
    /// </summary>
    public sealed class StringSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_String);
            serializerInputStream.Write((String)obj);
        }
    }
}