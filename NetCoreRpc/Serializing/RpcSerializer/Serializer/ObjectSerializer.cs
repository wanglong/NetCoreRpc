using NetCoreRpc.Utils;
using System;
using System.IO;
using System.Linq;

namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
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

        public override byte[] GeteObjectBytes(object obj)
        {
            if (obj == null)
            {
                return NullBytes();
            }
            using (MemoryStream stream = new MemoryStream())
            {
                stream.WriteString(_type.FullName);
                var fieldList = _type.GetRpcFieldList();
                if (fieldList != null && fieldList.Any())
                {
                    foreach (var fieldInfo in fieldList)
                    {
                        var fieldValue = fieldInfo.GetValue(obj);
                        if (fieldValue != null)
                        {
                            var fieldValueBytes = SerializerFactory.Serializer(fieldValue);
                            stream.WriteString(fieldInfo.Name);
                            stream.Write(fieldValueBytes, 0, fieldValueBytes.Length);
                        }
                    }
                }
                var objBytes = stream.ToArray();
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Object, BitConverter.GetBytes(objBytes.Length), objBytes);
            }
        }
    }
}