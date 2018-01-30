using NetCoreRpc.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetCoreRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ObjectDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 11:10:51
    /// </summary>
    public sealed class ObjectDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objectByteData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (objectByteData.Length > 0)
            {
                var typeName = ByteUtil.DecodeString(objectByteData, 1, out int objDataStartOffset);
                var type = RpcSerializerUtil.GetType(typeName);
                var result = RpcSerializerUtil.CreateInstance(type);
                int objDataFieldStartOffset = objDataStartOffset;
                SetObjectField(type.GetRpcFieldList(), result, objectByteData, objDataFieldStartOffset, objectByteData.Length, out objDataStartOffset);
                return result;
            }
            else
            {
                return null;
            }
        }

        private void SetObjectField(List<FieldInfo> fieldList, object result, byte[] data, int startOffset, int totalLength, out int nextStartOffset)
        {
            if (startOffset < totalLength)
            {
                var fieldName = ByteUtil.DecodeString(data, startOffset + 1, out nextStartOffset);
                startOffset = nextStartOffset;
                var fieldValue = SerializerFactory.Deserializer(data, startOffset, out nextStartOffset);
                var fieldInfo = fieldList.FirstOrDefault(m => m.Name == fieldName);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(result, fieldValue);
                }
                startOffset = nextStartOffset;
                SetObjectField(fieldList, result, data, startOffset, totalLength, out nextStartOffset);
            }
            else
            {
                nextStartOffset = startOffset;
            }
        }
    }
}