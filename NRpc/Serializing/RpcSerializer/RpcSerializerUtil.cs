using NRpc.Extensions;
using NRpc.Utils;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;

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

        public static string DecodeString(byte[] sourceBuffer, int startOffset, out int nextStartOffset)
        {
            return ByteUtil.DecodeString(sourceBuffer, startOffset + 1, out nextStartOffset);
        }

        public static byte[] GetNullByte()
        {
            return ByteUtil.Combine(Bytes_Object, ByteUtil.ZeroLengthBytes, ByteUtil.EmptyBytes);
        }

        public static MemoryStream WriteString(this MemoryStream stream, string data)
        {
            stream.Write(Bytes_String, 0, 1);
            if (data != null)
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var lengthBytes = BitConverter.GetBytes(dataBytes.Length);
                stream.Write(lengthBytes, 0, 4);
                stream.Write(dataBytes, 0, dataBytes.Length);
            }
            else
            {
                return stream.WriteNull();
            }
            return stream;
        }

        public static MemoryStream WriteNull(this MemoryStream stream)
        {
            stream.Write(ByteUtil.ZeroLengthBytes, 0, 4);
            stream.Write(ByteUtil.EmptyBytes, 0, 0);
            return stream;
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

        public static object CreateInstance(Type returnType)
        {
            return Activator.CreateInstance(returnType);
        }
    }
}