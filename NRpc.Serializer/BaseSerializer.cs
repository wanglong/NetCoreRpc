using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：BaseSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 13:24:21
    /// </summary>
    public abstract class BaseSerializer
    {
        public abstract byte[] GeteObjectBytes(object obj, SerializerOutput serializerOutput);
    }
}
