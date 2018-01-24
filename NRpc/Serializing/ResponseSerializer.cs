using System;
using System.Reflection;

namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ResponseSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 13:45:59
    /// </summary>
    public class ResponseSerializer : IResponseSerailizer
    {
        public object Deserialize(byte[] responseBody, Type resultType, MethodInfo calledMethodInfo)
        {
            return GetSerailizer(calledMethodInfo).Deserialize(responseBody, resultType);
        }

        public byte[] Serialize(object obj, MethodInfo calledMethodInfo)
        {
            return GetSerailizer(calledMethodInfo).Serialize(obj);
        }

        private IBinarySerializer GetSerailizer(MethodInfo calledMethodInfo)
        {
            return calledMethodInfo.GetSerailizer();
        }
    }
}