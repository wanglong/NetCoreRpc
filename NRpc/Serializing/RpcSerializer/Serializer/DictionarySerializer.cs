using NRpc.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：DictionarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：DictionarySerializer
    /// 创建标识：yjq 2018/1/28 13:41:29
    /// </summary>
    public sealed class DictionarySerializer : BaseSerializer
    {
        private readonly Type _type;

        public DictionarySerializer(Type type)
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
            var dic = (IDictionary)obj;
            var count = dic.Count;
            var countBytes = BitConverter.GetBytes(count);
            if (count > 0)
            {
                List<byte[]> valueByteList = new List<byte[]>();
                var enumerator = dic.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var keyBytes = SerializerFactory.Serializer(enumerator.Key);
                    var valueBytes = SerializerFactory.Serializer(enumerator.Value);
                    valueByteList.Add(keyBytes);
                    valueByteList.Add(valueBytes);
                }
                var totalfieldBytes = ByteUtil.Combine(valueByteList);
                var objBytesLength = typeNameBytes.Length + totalfieldBytes.Length + 4;
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Dictionary, BitConverter.GetBytes(objBytesLength), typeNameBytes, countBytes, totalfieldBytes);
            }
            else
            {
                var objBytesLength = typeNameBytes.Length + 4;
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Dictionary, BitConverter.GetBytes(objBytesLength), typeNameBytes, countBytes);
            }
        }
    }
}