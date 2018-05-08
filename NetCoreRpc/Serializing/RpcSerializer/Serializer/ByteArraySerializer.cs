using System;

namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ByteArraySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 17:08:51
    /// </summary>
    public sealed class ByteArraySerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_ByteArray);
            var bytes = (byte[])obj;
            var length = bytes.Length;
            serializerInputStream.Write(BitConverter.GetBytes(length), 0, 4);
            serializerInputStream.Write(bytes, 0, length);
        }
    }
}