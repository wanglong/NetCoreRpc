using NetCoreRpc.Utils;
using System;
using System.Collections.Generic;
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
            var typeNameBytes = RpcSerializerUtil.EncodeString(_type.FullName);
            var fieldList = _type.GetRpcFieldList();
            List<byte[]> fieldByteList = new List<byte[]>();
            if (fieldList != null && fieldList.Any())
            {
                foreach (var fieldInfo in fieldList)
                {
                    var fieldNameBytes = RpcSerializerUtil.EncodeString(fieldInfo.Name);
                    var fieldValueBytes = SerializerFactory.Serializer(fieldInfo.GetValue(obj));
                    fieldByteList.Add(fieldNameBytes);
                    fieldByteList.Add(fieldValueBytes);
                }
            }
            var totalfieldBytes = ByteUtil.Combine(fieldByteList);
            var objBytesLength = typeNameBytes.Length + totalfieldBytes.Length;
            var objLengthBytes = BitConverter.GetBytes(objBytesLength);
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_Object, objLengthBytes, typeNameBytes, totalfieldBytes);
        }
    }
}