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
    /// 类名：ObjectDserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 16:28:48
    /// </summary>
    public sealed class ObjectDserializer : BaseDserializer
    {
        private List<FieldInfo> _FieldList = new List<FieldInfo>();

        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objBytes = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);//获取整个object对象的值
            var typeName = ByteUtil.DecodeString(objBytes, 0, out int currentNextStartOffset);
            var type = GetByte(typeName);
            if (type == null)
            {
                return null;
            }
            var result = Activator.CreateInstance(type.Assembly.FullName, type.FullName).Unwrap();
            SetObjectField(result, objBytes, currentNextStartOffset, objBytes.Length);
            return result;
        }

        private void SetObjectField(object result, byte[] data, int startOffset, int totalLength)
        {
            if (startOffset < totalLength)
            {
                var fieldName = ByteUtil.DecodeString(data, startOffset + 1, out startOffset);
                var fieldValue = data.GetDserializer(startOffset, out startOffset).GetObject(data, startOffset, out startOffset);
                var fieldInfo = _FieldList.FirstOrDefault(m => m.Name == fieldName);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(result, fieldValue);
                }
                SetObjectField(result, data, startOffset, totalLength);
            }
        }

        private void SetFieldList(Type type)
        {
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

        public static Type GetByte(string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
