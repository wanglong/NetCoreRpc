using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：EnumSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:46:24
    /// </summary>
    public sealed class EnumSerializer : BaseSerializer
    {
        private readonly Type _type;

        public EnumSerializer(Type type)
        {
            _type = type;
        }

        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Enum);
            serializerInputStream.Write(_type.FullName);
            serializerInputStream.Write(Convert.ToInt32(((Enum)obj).ToString("d")));
        }
    }
}