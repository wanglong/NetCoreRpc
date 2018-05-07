using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ShortSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:08:18
    /// </summary>
    public sealed class ShortSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Short);
            serializerInputStream.Write(BitConverter.GetBytes((short)obj));
        }
    }
}