using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SerializerConstants.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 11:48:24
    /// </summary>
    public class SerializerConstants
    {
        public const char TYPE_CHAR_BYTE = 'B';
        public const char TYPE_CHAR_SBYTE = 'b';
        public const char TYPE_CHAR_INT = 'I';
        public const char TYPE_CHAR_UINT = 'i';
        public const char TYPE_CHAR_SHORT = 'S';
        public const char TYPE_CHAR_USHORT = 's';
        public const char TYPE_CHAR_LONG = 'L';
        public const char TYPE_CHAR_ULONG = 'l';
        public const char TYPE_CHAR_DECIMAL = 'M';
        public const char TYPE_CHAR_FLOAT = 'F';
        public const char TYPE_CHAR_DOUBLE = 'D';
        public const char TYPE_CHAR_BOOL = 'A';
        public const char TYPE_CHAR_CHAR = 'C';
        public const char TYPE_CHAR_STRING = 'T';
        public const char TYPE_CHAR_OBJECT = 'O';

        public readonly byte[] Byte_String = new byte[1] { (byte)TYPE_CHAR_STRING };

        public readonly byte[] Byte_ObjectStart = new byte[1] { (byte)TYPE_CHAR_OBJECT };

        public readonly byte[] Byte_Int = new byte[1] { (byte)TYPE_CHAR_INT };
    }
}
