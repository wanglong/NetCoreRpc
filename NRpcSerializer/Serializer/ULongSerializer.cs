using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ULongSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:14:45
    /// </summary>
    public sealed class ULongSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_ULong);
            serializerInputStream.Write(BitConverter.GetBytes((ulong)obj));
        }
    }
}