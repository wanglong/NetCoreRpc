using NRpcSerializer.Deserializer;
using NRpcSerializer.Serializer;
using NRpcSerializer.Utils;
using System.Linq;

namespace NRpcSerializer
{
    /// <summary>
    /// 类名：SerializerFactory.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:21:56
    /// </summary>
    public class SerializerFactory
    {
        internal static void Serializer(object obj, SerializerInputStream serializerInputStream)
        {
            if (obj == null)
            {
                new ObjectSerializer(null).WriteBytes(obj, serializerInputStream);
            }
            else
            {
                var type = obj.GetType();
                if (type.IsGenericType && TypeUtil.NullableType.IsAssignableFrom(type.GetGenericTypeDefinition()))
                {
                    type = type.GetGenericArguments()[0];
                }
                var typeHandle = type.TypeHandle;
                if (SerializerUtil.SerializerMap.ContainsKey(typeHandle))
                {
                    SerializerUtil.SerializerMap[typeHandle].WriteBytes(obj, serializerInputStream);
                }
                else
                {
                    if (type.IsEnum)
                    {
                        new EnumSerializer(type).WriteBytes(obj, serializerInputStream);
                    }
                    else if (type.IsArray)
                    {
                        new ArraySerializercs(type).WriteBytes(obj, serializerInputStream);
                    }
                    else if (TypeUtil.IDictionaryType.IsAssignableFrom(type))
                    {
                        new DictionarySerializer(type).WriteBytes(obj, serializerInputStream);
                    }
                    else if (TypeUtil.IEnumerableType.IsAssignableFrom(type) || type.DeclaringType == typeof(Enumerable))
                    {
                        new EnumerableSerializer(type).WriteBytes(obj, serializerInputStream);
                    }
                    else
                    {
                        new ObjectSerializer(type).WriteBytes(obj, serializerInputStream);
                    }
                }
            }
        }

        public static byte[] Serializer(object obj)
        {
            using (SerializerInputStream serializerInputStream = new SerializerInputStream())
            {
                Serializer(obj, serializerInputStream);
                return serializerInputStream.ToBytes();
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
                if (SerializerUtil.DeserializerMap.ContainsKey(objTypeByte))
                {
                    return SerializerUtil.DeserializerMap[objTypeByte].GetObject(data, startOffset, out nextStartOffset);
                }
                else
                {
                    return new ObjectDeserializer().GetObject(data, startOffset, out nextStartOffset);
                }
            }
        }
    }
}