using NRpcSerializer.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：ObjectDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:53:47
    /// </summary>
    public sealed class ObjectDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objectByteData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (objectByteData.Length > 0)
            {
                var typeName = ByteUtil.DecodeString(objectByteData, 0, out int objDataStartOffset);
                var type = SerializerUtil.GetType(typeName);
                var result = SerializerUtil.CreateInstance(type);
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
                var fieldName = ByteUtil.DecodeString(data, startOffset, out nextStartOffset);
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