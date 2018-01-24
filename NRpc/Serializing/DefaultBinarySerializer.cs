using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DefaultBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/27 17:17:38
    /// </summary>
    public class DefaultBinarySerializer : IBinarySerializer
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        /// <summary>Serialize an object to byte array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>Deserialize an object from a byte array.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(byte[] data, Type type)
        {
            using (var stream = new MemoryStream(data))
            {
                return _binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>Deserialize a typed object from a byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] data) where T : class
        {
            using (var stream = new MemoryStream(data))
            {
                return _binaryFormatter.Deserialize(stream) as T;
            }
        }
    }
}