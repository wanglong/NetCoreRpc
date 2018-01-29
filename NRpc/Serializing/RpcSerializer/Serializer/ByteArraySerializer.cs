using NRpc.Utils;
using System;

namespace NRpc.Serializing.RpcSerializer.Serializer
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
        public override byte[] GeteObjectBytes(object obj)
        {
            var bytes = (byte[])obj;
            var length = bytes.Length;
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_ByteArray, BitConverter.GetBytes(length), bytes);
        }
    }
}