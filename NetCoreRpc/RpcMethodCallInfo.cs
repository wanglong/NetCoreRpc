using System;
using System.Reflection;
using System.Text;
using NetCoreRpc.Utils;

namespace NetCoreRpc
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：MethodCallInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:36:27
    /// </summary>
    [Serializable]
    public class RpcMethodCallInfo
    {
        public RpcMethodCallInfo()
        {
        }

        public string TypeName { get; set; }

        public string MethodName { get; set; }

        public object[] Parameters { get; set; }

        public static RpcMethodCallInfo Create(object[] arrMethodArgs, MethodInfo methodInfo, string typeName)
        {
            return new RpcMethodCallInfo
            {
                MethodName = methodInfo.GetCombineMethodName(),
                Parameters = arrMethodArgs,
                TypeName = typeName
            };
        }
    }
}