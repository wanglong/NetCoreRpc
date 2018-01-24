using NRpc.Extensions;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace NRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：MethodUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:34:40
    /// </summary>
    internal static class MethodUtil
    {
        /// <summary>
        /// 方法名字列表
        /// </summary>
        private static readonly ConcurrentDictionary<RuntimeMethodHandle, string> _MethodNameDic = new ConcurrentDictionary<RuntimeMethodHandle, string>();

        /// <summary>
        /// 获取全部的参数类型
        /// </summary>
        /// <param name="arrArgs">参数列表</param>
        /// <returns>全部的参数类型</returns>
        public static Type[] GetArgTypes(object[] arrArgs)
        {
            if (null == arrArgs)
            {
                return new Type[0];
            }
            Type[] result = new Type[arrArgs.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                if (arrArgs[i] == null)
                {
                    result[i] = null;
                }
                else
                {
                    result[i] = arrArgs[i].GetType();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取方法的返回类型
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static MethodType GetMethodReturnType(this MethodInfo methodInfo)
        {
            var returnType = methodInfo.ReturnType;
            if (returnType == TypeUtil.AsyncActionType)
                return MethodType.AsyncAction;
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == TypeUtil.AsyncFunctionType)
                return MethodType.AsyncFunction;
            if (returnType == TypeUtil.SyncActionType)
            {
                return MethodType.SyncAction;
            }
            return MethodType.SyncFunction;
        }

        /// <summary>
        /// 获取拼接后的方法名字
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static string GetCombineMethodName(this MethodInfo methodInfo)
        {
            return _MethodNameDic.GetValue(methodInfo.MethodHandle, () =>
            {
                StringBuilder methodBuilder = new StringBuilder();
                methodBuilder.Append(methodInfo.Name);
                var parameterList = methodInfo.GetParameters();
                methodBuilder.Append("_").Append(parameterList.Length.ToString()).Append("|");
                foreach (var item in parameterList)
                {
                    methodBuilder.Append(item.ParameterType.Name).Append('|');
                }
                methodBuilder.Remove(methodBuilder.Length - 1, 1);
                return methodBuilder.ToString();
            });
        }
    }

    /// <summary>
    /// 方法类型
    /// </summary>
    internal enum MethodType
    {
        /// <summary>
        /// 同步方法(无返回值)
        /// </summary>
        SyncAction,

        /// <summary>
        /// 同步方法(有返回值)
        /// </summary>
        SyncFunction,

        /// <summary>
        /// 异步(无返回值)
        /// </summary>
        AsyncAction,

        /// <summary>
        /// 异步方法(有返回值)
        /// </summary>
        AsyncFunction
    }
}