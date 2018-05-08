using NRpcSerializer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：EnumerableDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 17:43:29
    /// </summary>
    public sealed class EnumerableDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var dicBytesData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (dicBytesData.Length > 0)
            {
                var typeName = ByteUtil.DecodeString(dicBytesData, 0, out int arrayDataStartOffset);
                int arrayDataFieldStartOffset = arrayDataStartOffset;
                var count = ByteUtil.DecodeInt(dicBytesData, arrayDataFieldStartOffset, out arrayDataStartOffset);
                arrayDataFieldStartOffset = arrayDataStartOffset;
                return SetList(typeName, count, dicBytesData, arrayDataFieldStartOffset);
            }
            else
            {
                return null;
            }
        }

        private object SetList(string typeName, int count, byte[] inputBytes, int startOffset)
        {
            var listType = SerializerUtil.GetType(typeName);
            if (listType.DeclaringType != null && listType.DeclaringType == typeof(Enumerable))
            {
                var list = new List<int>();
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var value = SerializerFactory.Deserializer(inputBytes, startOffset, out int nextStartOffset);
                        startOffset = nextStartOffset;
                        list.Add((int)value);
                    }
                }
                return list;
            }
            else
            {
                var list = Activator.CreateInstance(listType);
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var value = SerializerFactory.Deserializer(inputBytes, startOffset, out int nextStartOffset);
                        startOffset = nextStartOffset;
                        listType.InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new object[] { value });
                    }
                }
                return list;
            }
        }
    }
}