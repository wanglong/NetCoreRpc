using NRpc.Utils;
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

        public override byte[] GeteObjectBytes(object obj)
        {
            if (obj == null)
            {
                return NullBytes();
            }
            else
            {
                var typeNameBytes = RpcSerializerUtil.EncodeString(_type.FullName);
                var enumValue = ((Enum)obj).ToString();
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Enum, typeNameBytes, RpcSerializerUtil.EncodeString(enumValue));
            }
        }
    }
}