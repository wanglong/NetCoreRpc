using NRpc.Serializing.RpcSerializer.Deserializer;
using NRpc.Serializing.RpcSerializer.Serializer;
using NRpc.Utils;

namespace NRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SerializerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:46:33
    /// </summary>
    public sealed class SerializerFactory
    {
        public static byte[] Serializer(object obj)
        {
            if (obj == null)
            {
                return new ObjectSerializer(null).GeteObjectBytes(obj);
            }
            else
            {
                var type = obj.GetType();
                if (type.IsGenericType && TypeUtil.NullableType.IsAssignableFrom(type.GetGenericTypeDefinition()))
                {
                    type = type.GetGenericArguments()[0];
                }
                var typeHandle = type.TypeHandle;
                if (RpcSerializerUtil.SerializerMap.ContainsKey(typeHandle))
                {
                    return RpcSerializerUtil.SerializerMap[typeHandle].GeteObjectBytes(obj);
                }
                else
                {
                    return new ObjectSerializer(type).GeteObjectBytes(obj);
                }
            }
        }

        public static object Deserializer(byte[] data)
        {
            return Deserializer(data, 0, out int nextStartOffset);
        }

        internal static object Deserializer(byte[] data, int startOffset, out int nextStartOffset)
        {
            if (data == null || data.Length < 1)
            {
                nextStartOffset = startOffset;
                return null;
            }
            else
            {
                byte objTypeByte = data[startOffset];
                startOffset += 1;
                if (RpcSerializerUtil.DeserializerMap.ContainsKey(objTypeByte))
                {
                    return RpcSerializerUtil.DeserializerMap[objTypeByte].GetObject(data, startOffset, out nextStartOffset);
                }
                else
                {
                    return new ObjectDeserializer().GetObject(data, startOffset, out nextStartOffset);
                }
            }
        }
    }
}