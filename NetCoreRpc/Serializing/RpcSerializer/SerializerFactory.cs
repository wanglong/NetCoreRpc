using NetCoreRpc.Serializing.RpcSerializer.Deserializer;
using NetCoreRpc.Serializing.RpcSerializer.Serializer;
using NetCoreRpc.Utils;

namespace NetCoreRpc.Serializing.RpcSerializer
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
                    if (type.IsEnum)
                    {
                        return new EnumSerializer(type).GeteObjectBytes(obj);
                    }
                    else if (type.IsArray)
                    {
                        return new ArraySerializercs(type).GeteObjectBytes(obj);
                    }
                    else if (TypeUtil.IDictionaryType.IsAssignableFrom(type))
                    {
                        return new DictionarySerializer(type).GeteObjectBytes(obj);
                    }
                    else if (TypeUtil.IEnumerableType.IsAssignableFrom(type))
                    {
                        return new EnumerableSerializer(type).GeteObjectBytes(obj);
                    }

                    return new ObjectSerializer(type).GeteObjectBytes(obj);
                }
            }
        }

        public static object Deserializer(byte[] data, int startOffset = 0)
        {
            return Deserializer(data, startOffset, out int nextStartOffset);
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