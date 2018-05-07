using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：DoubleSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:44:16
    /// </summary>
    public sealed class DoubleSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Double);
            serializerInputStream.Write(BitConverter.GetBytes((double)obj));
        }
    }
}