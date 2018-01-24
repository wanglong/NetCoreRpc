using System;

namespace NRpc.Serializing.Attributes
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：BinarySerializerAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/19 16:31:12
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class BinarySerializerAttribute : Attribute
    {
    }
}