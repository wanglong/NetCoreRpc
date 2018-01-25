using NRpc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcSerializerUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 16:30:25
    /// </summary>
    public static class RpcSerializerUtil
    {
        public static string DserializerStr(this byte[] data, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeString(data, startOffset, out nextStartOffset);
        }
    }
}
