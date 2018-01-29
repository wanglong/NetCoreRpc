using NRpc.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NRpc.Serializing.RpcSerializer.Serializer
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

        public override byte[] GeteObjectBytes(object obj)
        {
            var typeNameBytes = RpcSerializerUtil.EncodeString(_type.FullName);
            var count = 0;
            var enumerable = (IEnumerable)obj;
            var enumerator = enumerable.GetEnumerator();
            List<byte[]> valueByteList = new List<byte[]>();
            while (enumerator.MoveNext())
            {
                count++;
                var valueBytes = SerializerFactory.Serializer(enumerator.Current);
                valueByteList.Add(valueBytes);
            }
            var totalfieldBytes = ByteUtil.Combine(valueByteList);
            var objBytesLength = typeNameBytes.Length + totalfieldBytes.Length + 4;
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_Enumerable, BitConverter.GetBytes(objBytesLength), typeNameBytes, BitConverter.GetBytes(count), totalfieldBytes);
        }
    }
}