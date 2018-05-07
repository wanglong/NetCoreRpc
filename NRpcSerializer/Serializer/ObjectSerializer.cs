using NRpcSerializer.Utils;
using System;
using System.Linq;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ObjectSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:51:56
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
            serializerInputStream.Write(SerializerUtil.Byte_Object);
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