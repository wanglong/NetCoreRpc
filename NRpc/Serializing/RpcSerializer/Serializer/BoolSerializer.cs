﻿using NRpc.Utils;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：BoolSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 16:26:28
    /// </summary>
    public sealed class BoolSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            if ((bool)obj)
            {
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Bool, new byte[1] { 1 });
            }
            else
            {
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Bool, new byte[1] { 0 });
            }
        }
    }
}