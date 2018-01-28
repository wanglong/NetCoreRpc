using System;

namespace NRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：RpcSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RpcSerializer
    /// 创建标识：yjq 2018/1/28 20:05:57
    /// </summary>
    internal sealed class RpcSerializer : IMethodCallSerializer
    {
        public object Deserialize(byte[] data, Type type)
        {
            return SerializerFactory.Deserializer(data);
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            return (T)SerializerFactory.Deserializer(data);
        }

        public byte[] Serialize(object obj)
        {
            return SerializerFactory.Serializer(obj);
        }
    }
}