using NRpcSerializer.Utils;
using System;
using System.Collections.Concurrent;

namespace NRpcSerializer
{
    /// <summary>
    /// 类名：SerializerUtil.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:19:06
    /// </summary>
    internal static partial class SerializerUtil
    {
        private static ConcurrentDictionary<string, RuntimeTypeHandle> _RuntimeTypeHandleDic = new ConcurrentDictionary<string, RuntimeTypeHandle>();

        public static Type GetType(string typeName)
        {
            var typeHandle = _RuntimeTypeHandleDic.GetValue(typeName, () =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    var type = assembly.GetType(typeName);
                    if (type != null)
                    {
                        return type.TypeHandle;
                    }
                }
                throw new Exception("not fount typename " + typeName);
            });
            return Type.GetTypeFromHandle(typeHandle);
        }

        public static object CreateInstance(Type returnType)
        {
            return Activator.CreateInstance(returnType);
        }
    }
}