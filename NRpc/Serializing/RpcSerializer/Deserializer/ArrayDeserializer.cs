using NRpc.Utils;
using System;

namespace NRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：ArrayDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ArrayDeserializer
    /// 创建标识：yjq 2018/1/27 21:44:50
    /// </summary>
    public sealed class ArrayDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var arrayBytesData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (arrayBytesData.Length > 0)
            {
                var typeName = ByteUtil.DecodeString(arrayBytesData, 0, out int arrayDataStartOffset);
                int arrayDataFieldStartOffset = arrayDataStartOffset;
                var arrayCount = ByteUtil.DecodeInt(arrayBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                arrayDataFieldStartOffset = arrayDataStartOffset;
                var array = GetArray(typeName, arrayCount);
                for (int i = 0; i < arrayCount; i++)
                {
                    var value = SerializerFactory.Deserializer(arrayBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                    arrayDataFieldStartOffset = arrayDataStartOffset;
                    array.SetValue(value, i);
                }
                return array;
            }
            else
            {
                return null;
            }
        }

        private Array GetArray(string typeName, int length)
        {
            try
            {
                var type = RpcSerializerUtil.GetType(typeName);
                return Array.CreateInstance(type, length);
            }
            catch
            {
                return new object[length];
            }
        }
    }
}