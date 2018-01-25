using NRpc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ObjectSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 13:28:22
    /// </summary>
    public sealed class ObjectSerializer : BaseSerializer
    {
        private readonly Type _type;
        private List<FieldInfo> _FieldList = new List<FieldInfo>();
        public ObjectSerializer(Type type)
        {
            _type = type;
            for (; type != null; type = type.BaseType)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField |
                    BindingFlags.DeclaredOnly);
                if (fields != null)
                {
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if ((fields[i].Attributes & FieldAttributes.NotSerialized) == 0)
                        {
                            if (!_FieldList.Contains(fields[i]))
                            {
                                _FieldList.Add(fields[i]);
                            }
                        }
                    }
                }
            }
        }

        public override byte[] GeteObjectBytes(object obj, SerializerOutput serializerOutput)
        {
            var objStartBytes = serializerOutput.GetObjectStartBytes();
            var typeNameBytes = serializerOutput.GetTypeNameBytes(_type.FullName);
            List<byte[]> fieldByteList = new List<byte[]>();
            foreach (var item in _FieldList)
            {
                fieldByteList.Add(GetFieldBytes(obj, item, serializerOutput));
            }
            var totalfieldBytes = ByteUtil.Combine(fieldByteList);
            var objBytesLength = typeNameBytes.Length + totalfieldBytes.Length;
            var objLengthBytes = BitConverter.GetBytes(objBytesLength);
            return ByteUtil.Combine(objStartBytes, objLengthBytes, typeNameBytes, totalfieldBytes);
        }

        private byte[] GetFieldBytes(object obj, FieldInfo fieldInfo, SerializerOutput serializerOutput)
        {
            var fieldNameBytes = serializerOutput.GetStringBytes(fieldInfo.Name);
            var fieldValueBytes = serializerOutput.GetObjectBytes(fieldInfo.GetValue(obj));
            return ByteUtil.Combine(fieldNameBytes, fieldValueBytes);
        }
    }
}
