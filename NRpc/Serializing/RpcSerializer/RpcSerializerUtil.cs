using NRpc.Extensions;
using NRpc.Utils;
using System;
using System.Collections.Concurrent;

namespace NRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcSerializerUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:06:28
    /// </summary>
    public static partial class RpcSerializerUtil
    {
        public static byte[] EncodeString(string data)
        {
            ByteUtil.EncodeString(data, out byte[] lengthBytes, out byte[] dataBytes);
            return ByteUtil.Combine(Bytes_String, lengthBytes, dataBytes);
        }

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
    }
}