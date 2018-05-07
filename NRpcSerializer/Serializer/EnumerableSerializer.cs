using System;
using System.Collections;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：EnumerableSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 14:59:15
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
            serializerInputStream.Write(SerializerUtil.Byte_Enumerable);
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
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength-4, startLength);//填补流长度
        }
    }
}