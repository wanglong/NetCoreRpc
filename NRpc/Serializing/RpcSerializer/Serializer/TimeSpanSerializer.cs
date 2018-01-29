using NRpc.Utils;
using System;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：TimeSpanSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 16:51:41
    /// </summary>
    public sealed class TimeSpanSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_TimeSpan, ByteUtil.EncodeTimeSpan((TimeSpan)obj));
        }
    }
}