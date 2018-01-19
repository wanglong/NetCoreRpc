using System;

namespace NetCoreRpc.Serializing.Attributes
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：JsonSerializerAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/19 16:25:34
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class JsonSerializerAttribute : Attribute
    {
    }
}