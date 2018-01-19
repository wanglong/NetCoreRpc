using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Text;

namespace NetCoreRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：JsonBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/19 16:33:54
    /// </summary>
    public class JsonBinarySerializer : IBinarySerializer
    {
        public static JsonSerializerSettings Settings { get; private set; }

        static JsonBinarySerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        public object Deserialize(byte[] data, Type type)
        {
            var jsonStr = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(jsonStr, type);
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            return (T)Deserialize(data, typeof(T));
        }

        public byte[] Serialize(object obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj, Settings);
            return Encoding.UTF8.GetBytes(jsonStr);
        }

        private class CustomContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jsonProperty = base.CreateProperty(member, memberSerialization);
                if (jsonProperty.Writable) return jsonProperty;
                var property = member as PropertyInfo;
                if (property == null) return jsonProperty;
                var hasPrivateSetter = property.GetSetMethod(true) != null;
                jsonProperty.Writable = hasPrivateSetter;
                return jsonProperty;
            }
        }
    }
}