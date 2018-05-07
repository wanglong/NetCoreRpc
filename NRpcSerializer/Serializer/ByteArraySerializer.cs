using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ByteArraySerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:33:57
    /// </summary>
    public sealed class ByteArraySerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_ByteArray);
            var bytes = (byte[])obj;
            var length = bytes.Length;
            serializerInputStream.Write(BitConverter.GetBytes(length), 0, 4);
            serializerInputStream.Write(bytes, 0, length);
        }
    }
}