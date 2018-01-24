using System;
using System.Reflection;

namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：IBodySerailizer.cs
    /// 类属性：接口
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 13:38:13
    /// </summary>
    public interface IResponseSerailizer
    {
        object Deserialize(byte[] responseBody, Type resultType, MethodInfo calledMethodInfo);

        byte[] Serialize(object obj, MethodInfo calledMethodInfo);
    }
}