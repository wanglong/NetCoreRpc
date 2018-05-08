using System;
using System.Collections;

namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：EnumerableSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EnumerableSerializer
    /// 创建标识：yjq 2018/1/28 15:31:06
    /// </summary>
    public sealed class EnumerableSerializer : BaseSerializer
    {
        private Type _type;

        public EnumerableSerializer(Type type)
        {
            _type = type;
        }

        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_Enumerable);
            var startLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//当前流长度占位
            serializerInputStream.Write(_type.FullName);
            var count = 0;
            var enumerable = (IEnumerable)obj;
            var enumerator = enumerable.GetEnumerator();
            var currentLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//总个数占位
            while (enumerator.MoveNext())
            {
                count++;
                SerializerFactory.Serializer(enumerator.Current, serializerInputStream);
            }
            serializerInputStream.UpdateCurrentLength(count, currentLength);//填补数量
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
        }
    }
}