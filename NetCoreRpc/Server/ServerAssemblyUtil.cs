using NetCoreRpc.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NetCoreRpc.Server
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ServerTypeUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 19:36:35
    /// </summary>
    public class ServerAssemblyUtil
    {
        /// <summary>
        /// 程序及中运行时类型和名字的关系表（初始化时加载程序集）
        /// </summary>
        private static readonly Dictionary<string, RuntimeTypeHandle> _RuntimeTypeDic = new Dictionary<string, RuntimeTypeHandle>();

        /// <summary>
        /// 运行时类型方法列表（初始化时加载程序集）
        /// </summary>
        private static readonly Dictionary<RuntimeTypeHandle, Dictionary<string, RuntimeMethodHandle>> _RuntimeMethodHandle = new Dictionary<RuntimeTypeHandle, Dictionary<string, RuntimeMethodHandle>>();

        public static Type GetType(string typeName)
        {
            if (_RuntimeTypeDic.ContainsKey(typeName))
            {
                return Type.GetTypeFromHandle(_RuntimeTypeDic[typeName]);
            }
            return null;
        }

        public static void AddAssemblyList(params string[] assemblyNameList)
        {
            if (assemblyNameList != null)
            {
                foreach (var assemblyName in assemblyNameList)
                {
                    Add(assemblyName);
                }
            }
        }

        public static void Add(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            if (assembly != null)
            {
                foreach (var item in assembly.GetTypes())
                {
                    if (!_RuntimeTypeDic.ContainsKey(item.FullName))
                    {
                        _RuntimeTypeDic.Add(item.FullName, item.TypeHandle);
                    }
                    InstallMethod(item);
                }
            }
        }

        public static MethodBase GetMethod(string methodName, Type declaringType)
        {
            if (_RuntimeMethodHandle.ContainsKey(declaringType.TypeHandle))
            {
                var runtimeTypeDic = _RuntimeMethodHandle[declaringType.TypeHandle];
                if (runtimeTypeDic.ContainsKey(methodName))
                {
                    return MethodBase.GetMethodFromHandle(runtimeTypeDic[methodName]);
                }
            }
            return null;
        }

        private static void InstallMethod(Type type)
        {
            if (type != null)
            {
                var runtimeHandle = type.TypeHandle;
                var methods = type.GetMethods();
                StringBuilder methodBuilder = new StringBuilder();
                foreach (var methodInfo in methods)
                {
                    methodBuilder.Append(methodInfo.Name);
                    var parameterList = methodInfo.GetParameters();
                    methodBuilder.Append("_").Append(parameterList.Length.ToString()).Append("|");
                    foreach (var item in parameterList)
                    {
                        methodBuilder.Append(item.ParameterType.Name).Append('|');
                    }
                    methodBuilder.Remove(methodBuilder.Length - 1, 1);
                    if (!_RuntimeMethodHandle.ContainsKey(runtimeHandle))
                    {
                        _RuntimeMethodHandle.Add(runtimeHandle, new Dictionary<string, RuntimeMethodHandle>());
                    }
                    var runtimeTypeDic = _RuntimeMethodHandle[runtimeHandle];
                    runtimeTypeDic[methodBuilder.ToString()] = methodInfo.MethodHandle;
                    methodBuilder.Clear();
                    methodInfo.GetSerailizer();  //用于初始化方法的属性查找
                }
            }
        }
    }
}