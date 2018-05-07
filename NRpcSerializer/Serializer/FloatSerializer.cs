using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：FloatSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:59:07
    /// </summary>
    public sealed class FloatSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Float);
            serializerInputStream.Write(BitConverter.GetBytes((float)obj));
        }
    }
}