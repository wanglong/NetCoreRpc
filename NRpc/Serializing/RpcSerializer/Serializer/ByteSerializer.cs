using NRpc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ByteSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 13:49:36
    /// </summary>
    public sealed class ByteSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            if (obj == null)
            {
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Byte, ByteUtil.ZeroLengthBytes, ByteUtil.EmptyBytes);
            }
            else
            {
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Byte, new byte[1] { (byte)obj });
            }
        }
    }
}
