using System;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SerializerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 13:26:31
    /// </summary>
    public static class SerializerFactory
    {
        public static BaseSerializer GetSerializer(this Type type)
        {
            if (type == typeof(string))
            {
                return new StringSerializer();
            }
            else if (type == typeof(int))
            {
                return new IntSerializer();
            }
            return new ObjectSerializer(type);
        }

        public static BaseDserializer GetDserializer(this byte[] data, int startOffset, out int nextStartOffset)
        {
            nextStartOffset = startOffset + 1;
            var typeChar = data[startOffset];
            switch (typeChar)
            {
                case (byte)SerializerConstants.TYPE_CHAR_STRING:
                    return new StringDserializer();
                case (byte)SerializerConstants.TYPE_CHAR_INT:
                    return new IntDeserializer();
                default:
                    return new ObjectDserializer();
            }
        }
    }
}