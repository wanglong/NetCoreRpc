using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：UShortSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:16:04
    /// </summary>
    public sealed class UShortSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_UShort);
            serializerInputStream.Write(BitConverter.GetBytes((ushort)obj));
        }
    }
}