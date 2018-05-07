using System;
using System.Collections;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：DictionarySerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 14:53:12
    /// </summary>
    public sealed class DictionarySerializer : BaseSerializer
    {
        private readonly Type _type;

        public DictionarySerializer(Type type)
        {
            _type = type;
        }

        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Dictionary);
            var startLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//当前流长度占位
            var dic = (IDictionary)obj;
            serializerInputStream.Write(_type.FullName);
            serializerInputStream.Write(dic.Count);
            var enumerator = dic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SerializerFactory.Serializer(enumerator.Key, serializerInputStream);
                SerializerFactory.Serializer(enumerator.Value, serializerInputStream);
            }
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
        }
    }
}