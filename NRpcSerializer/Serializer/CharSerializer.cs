using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：CharSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:38:27
    /// </summary>
    public sealed class CharSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Char);
            serializerInputStream.Write(BitConverter.GetBytes((char)obj));
        }
    }
}