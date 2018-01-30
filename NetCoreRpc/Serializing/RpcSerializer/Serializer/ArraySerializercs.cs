using NetCoreRpc.Utils;
using System;
using System.IO;

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
            using (MemoryStream stream = new MemoryStream())
            {
                stream.WriteString(_type.GetElementType().FullName);
                stream.Write(BitConverter.GetBytes(array.Length), 0, 4);
                foreach (var item in array)
                {
                    var bytes = SerializerFactory.Serializer(item);
                    stream.Write(bytes, 0, bytes.Length);
                }
                var arrayBytes = stream.ToArray();
                var objLengthBytes = BitConverter.GetBytes(arrayBytes.Length);
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_Array, objLengthBytes, arrayBytes);
            }
        }
    }
}