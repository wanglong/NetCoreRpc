using NRpc.Utils;
using System;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SByteSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 13:59:41
    /// </summary>
    public sealed class SByteSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            if (obj == null)
            {
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_SByte, ByteUtil.ZeroLengthBytes, ByteUtil.EmptyBytes);
            }
            else
            {
                var value = (sbyte)obj;
                int intValue = 0;
                if (value < 0)
                {
                    intValue = (127 - (int)value);
                }
                else
                {
                    intValue = value;
                }
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_SByte, BitConverter.GetBytes(intValue));
            }
        }
    }
}