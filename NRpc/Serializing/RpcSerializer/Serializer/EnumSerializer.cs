using System;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：EnumSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EnumSerializer
    /// 创建标识：yjq 2018/1/27 16:08:31
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
            serializerInputStream.Write(RpcSerializerUtil.Byte_Enum);
            serializerInputStream.Write(_type.FullName);
            serializerInputStream.Write(Convert.ToInt32(((Enum)obj).ToString("d")));
        }
    }
}