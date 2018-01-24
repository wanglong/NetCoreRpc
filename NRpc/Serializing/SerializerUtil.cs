using NRpc.Extensions;
using NRpc.Serializing.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SerializerUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/20 13:49:26
    /// </summary>
    internal static class SerializerUtil
    {
        private static readonly DefaultBinarySerializer _DefaultBinarySerializer = new DefaultBinarySerializer();

        private static readonly JsonBinarySerializer _JsonBinarySerializer = new JsonBinarySerializer();

        private static readonly RuntimeTypeHandle _BinarySerializerAttributeTypeHandle = typeof(BinarySerializerAttribute).TypeHandle;

        private static readonly RuntimeTypeHandle _JsonSerializerAttributeTypeHandle = typeof(JsonSerializerAttribute).TypeHandle;

        private static readonly Dictionary<RuntimeTypeHandle, IBinarySerializer> _AttributeTypeSerializerMap = new Dictionary<RuntimeTypeHandle, IBinarySerializer>()
        {
            [_BinarySerializerAttributeTypeHandle] = _DefaultBinarySerializer,
            [_JsonSerializerAttributeTypeHandle] = _JsonBinarySerializer,
        };

        /// <summary>
        /// 方法序列化列表
        /// </summary>
        private static readonly ConcurrentDictionary<RuntimeMethodHandle, IBinarySerializer> _MethodSerializerDic = new ConcurrentDictionary<RuntimeMethodHandle, IBinarySerializer>();

        /// <summary>
        /// 根据方法获取序列化类
        /// </summary>
        /// <param name="calledMethodInfo"></param>
        /// <returns></returns>
        public static IBinarySerializer GetSerailizer(this MethodInfo calledMethodInfo)
        {
            return _MethodSerializerDic.GetValue(calledMethodInfo.MethodHandle, () =>
            {
                var methodJsonAttribute = calledMethodInfo.GetCustomAttribute<JsonSerializerAttribute>();
                if (methodJsonAttribute != null)
                {
                    return _JsonBinarySerializer;
                }
                else if (calledMethodInfo.GetCustomAttribute<BinarySerializerAttribute>() != null)
                {
                    return _DefaultBinarySerializer;
                }
                else if (calledMethodInfo.DeclaringType.GetCustomAttribute<JsonSerializerAttribute>() != null)
                {
                    return _JsonBinarySerializer;
                }
                else if (calledMethodInfo.DeclaringType.GetCustomAttribute<BinarySerializerAttribute>() != null)
                {
                    return _DefaultBinarySerializer;
                }
                return _JsonBinarySerializer;
            });
        }
    }
}