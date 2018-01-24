using System;

namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IBinarySerializer.cs
    /// 接口属性：公共
    /// 类功能描述：IBinarySerializer接口
    /// 创建标识：yjq 2017/11/27 17:17:05
    /// </summary>
    public interface IBinarySerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] data, Type type);

        T Deserialize<T>(byte[] data) where T : class;
    }
}