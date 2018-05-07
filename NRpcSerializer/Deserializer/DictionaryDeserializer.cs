using NRpcSerializer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：DictionaryDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 17:42:26
    /// </summary>
    public sealed class DictionaryDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var dicBytesData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (dicBytesData.Length > 0)
            {
                var dicTypeName = ByteUtil.DecodeString(dicBytesData, 0, out int arrayDataStartOffset);
                int arrayDataFieldStartOffset = arrayDataStartOffset;
                var count = ByteUtil.DecodeInt(dicBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                var dic = CreateDic(dicTypeName, count);
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        arrayDataFieldStartOffset = arrayDataStartOffset;
                        var key = SerializerFactory.Deserializer(dicBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                        arrayDataFieldStartOffset = arrayDataStartOffset;
                        var value = SerializerFactory.Deserializer(dicBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                        dic.Add(key, value);
                    }
                }
                return dic;
            }
            else
            {
                return null;
            }
        }

        private IDictionary CreateDic(string typeName, int length)
        {
            try
            {
                var type = SerializerUtil.GetType(typeName);
                return (IDictionary)Activator.CreateInstance(type);
            }
            catch
            {
                return new Dictionary<Object, Object>();
            }
        }
    }
}