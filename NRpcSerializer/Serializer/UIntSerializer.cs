using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：UIntSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:13:14
    /// </summary>
    public sealed class UIntSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_UInt);
            serializerInputStream.Write(BitConverter.GetBytes((uint)obj));
        }
    }
}