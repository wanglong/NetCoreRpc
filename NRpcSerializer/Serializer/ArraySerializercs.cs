using System;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ArraySerializercs.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 13:04:51
    /// </summary>
    public sealed class ArraySerializercs : BaseSerializer
    {
        private readonly Type _type;

        public ArraySerializercs(Type type)
        {
            _type = type;
        }

        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Bytes_Array);
            var startLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//当前流长度占位
            serializerInputStream.Write(_type.GetElementType().FullName);
            var array = (Array)obj;
            serializerInputStream.Write(array.Length);
            foreach (var item in array)
            {
                SerializerFactory.Serializer(item, serializerInputStream);
            }
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
        }
    }
}