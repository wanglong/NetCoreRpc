using NRpc.Utils;
using System;
using System.Text;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SerializerOutput.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 9:43:15
    /// </summary>
    public sealed class SerializerOutput : SerializerConstants
    {
        public static readonly byte[] ZeroLengthBytes = BitConverter.GetBytes(0);
        public static readonly byte[] EmptyBytes = new byte[0];
        public static readonly byte[] IntSizeBytes = BitConverter.GetBytes(4);

        //先知道类型，类型里面的字段和属性的值
        //假如是数组
        //字典
        //集合
        //枚举
        //普通类型

        public byte[] GetIntBytes(object obj)
        {
            if (obj == null)
            {
                return ByteUtil.Combine(Byte_Int, ZeroLengthBytes, EmptyBytes);
            }
            else
            {
                return ByteUtil.Combine(Byte_Int, IntSizeBytes, BitConverter.GetBytes((int)obj));
            }
        }

        public byte[] GetStringBytes(string str)
        {
            if (str == null)
            {
                return ByteUtil.Combine(Byte_String, ZeroLengthBytes, EmptyBytes);
            }
            else
            {
                var strBytes = Encoding.UTF8.GetBytes(str);
                var lengthBytes = BitConverter.GetBytes(strBytes.Length);
                return ByteUtil.Combine(Byte_String, lengthBytes, strBytes);
            }
        }

        public byte[] GetTypeNameBytes(string str)
        {
            if (str == null)
            {
                return ByteUtil.Combine( ZeroLengthBytes, EmptyBytes);
            }
            else
            {
                var strBytes = Encoding.UTF8.GetBytes(str);
                var lengthBytes = BitConverter.GetBytes(strBytes.Length);
                return ByteUtil.Combine( lengthBytes, strBytes);
            }
        }

        public byte[] GetObjectStartBytes()
        {
            return Byte_ObjectStart;
        }

        public byte[] GetObjectBytes(object obj)
        {
            if (obj == null)
            {
                return GetNullBytes();
            }
            return obj.GetType().GetSerializer().GeteObjectBytes(obj, this);
        }

        private byte[] GetNullBytes()
        {
            return ByteUtil.Combine(ZeroLengthBytes, EmptyBytes);
        }
    }
}