using NRpc.Utils;
using System;
using System.Linq;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ObjectSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:30:49
    /// </summary>
    public sealed class ObjectSerializer : BaseSerializer
    {
        private Type _type;

        public ObjectSerializer(Type type)
        {
            _type = type;
        }

        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_Object);
            if (obj == null)
            {
                serializerInputStream.Write(ByteUtil.ZeroLengthBytes);
                serializerInputStream.Write(ByteUtil.EmptyBytes);
            }
            else
            {
                var startLength = serializerInputStream.Length;
                serializerInputStream.Write(0);//当前流长度占位
                serializerInputStream.Write(_type.FullName);
                var fieldList = _type.GetRpcFieldList();
                if (fieldList != null && fieldList.Any())
                {
                    foreach (var fieldInfo in fieldList)
                    {
                        var fieldValue = fieldInfo.GetValue(obj);
                        if (fieldValue != null)
                        {
                            serializerInputStream.Write(fieldInfo.Name);
                            SerializerFactory.Serializer(fieldValue, serializerInputStream);
                        }
                    }
                }
                serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
            }
        }
    }
}