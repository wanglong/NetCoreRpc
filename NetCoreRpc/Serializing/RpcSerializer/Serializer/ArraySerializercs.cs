using NetCoreRpc.Utils;
using System;
using System.Collections.Generic;

namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
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

        public override byte[] GeteObjectBytes(object obj)
        {
            var array = (Array)obj;
            var arrayTypeName = _type.GetElementType().FullName;
            var arrayTypeNameBytes = RpcSerializerUtil.EncodeString(arrayTypeName);
            var arrayCountBytes = BitConverter.GetBytes(array.Length);
            List<byte[]> arrayByteList = new List<byte[]>();
            foreach (var item in array)
            {
                arrayByteList.Add(SerializerFactory.Serializer(item));
            }
            var totalfieldBytes = ByteUtil.Combine(arrayByteList);
            var objBytesLength = totalfieldBytes.Length + arrayTypeNameBytes.Length + 4;
            var objLengthBytes = BitConverter.GetBytes(objBytesLength);
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_Array, objLengthBytes, arrayTypeNameBytes, arrayCountBytes, totalfieldBytes);
        }
    }
}