using System;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：ArraySerializercs.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ArraySerializercs
    /// 创建标识：yjq 2018/1/27 18:18:53
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
            serializerInputStream.Write(RpcSerializerUtil.Byte_Array);
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