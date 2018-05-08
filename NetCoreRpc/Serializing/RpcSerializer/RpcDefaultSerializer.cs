using NetCoreRpc.Utils;
using System;

namespace NetCoreRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：RpcDefaultSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RpcSerializer
    /// 创建标识：yjq 2018/1/28 20:05:57
    /// </summary>
    public sealed class RpcDefaultSerializer : IMethodCallSerializer
    {
        private static readonly byte[] CompressedBytes = new byte[] { 1 };

        private static readonly byte[] UnCompressedBytes = new byte[] { 0 };

        public object Deserialize(byte[] data, Type type)
        {
            if (data[0] == 1)
            {
                var totalLength = data.Length - 1;
                var needDecompressBytes = new byte[totalLength];
                Buffer.BlockCopy(data, 1, needDecompressBytes, 0, totalLength);
                var bytes = CompressionUtil.Decompress(needDecompressBytes);
                return SerializerFactory.Deserializer(bytes);
            }
            else
            {
                return SerializerFactory.Deserializer(data, startOffset: 1);
            }
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            return (T)Deserialize(data, typeof(T));
        }

        public byte[] Serialize(object obj)
        {
            var data = SerializerFactory.Serializer(obj);
            if (data.Length > 1024 * 1 * 1024)
            {
                return ByteUtil.Combine(CompressedBytes, CompressionUtil.Compress(data));
            }
            else
            {
                return ByteUtil.Combine(UnCompressedBytes, data);
            }
        }
    }
}