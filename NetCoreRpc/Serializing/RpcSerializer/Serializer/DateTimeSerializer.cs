using NetCoreRpc.Utils;
using System;

namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DateTimeSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 16:46:47
    /// </summary>
    public sealed class DateTimeSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_DateTime, ByteUtil.EncodeDateTime((DateTime)obj));
        }
    }
}