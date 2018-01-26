using NRpc.Serializing.RpcSerializer.Deserializer;
using NRpc.Serializing.RpcSerializer.Serializer;
using System;
using System.Collections.Generic;

namespace NRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcSerializerConstants.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:11:41
    /// </summary>
    public static partial class RpcSerializerUtil
    {
        public static readonly byte[] SizeBytes = BitConverter.GetBytes(4);

        public static readonly char Char_Byte = 'B';
        public static readonly byte Byte_Byte = (byte)Char_Byte;
        public static readonly byte[] Bytes_Byte = new byte[1] { Byte_Byte };

        public static readonly char Char_SByte = 'b';
        public static readonly byte Byte_SByte = (byte)Char_SByte;
        public static readonly byte[] Bytes_SByte = new byte[1] { Byte_SByte };

        public static readonly char Char_Int = 'I';
        public static readonly byte Byte_Int = (byte)Char_Int;
        public static readonly byte[] Bytes_Int = new byte[1] { Byte_Int };

        public static readonly char Char_UInt = 'i';
        public static readonly byte Byte_UInt = (byte)Char_UInt;
        public static readonly byte[] Bytes_UInt = new byte[1] { Byte_UInt };

        public static readonly char Char_Short = 'S';
        public static readonly byte Byte_Short = (byte)Char_Short;
        public static readonly byte[] Bytes_Short = new byte[1] { Byte_Short };

        public static readonly char Char_UShort = 's';
        public static readonly byte Byte_UShort = (byte)Char_UShort;
        public static readonly byte[] Bytes_UShort = new byte[1] { Byte_UShort };

        public static readonly char Char_Long = 'L';
        public static readonly byte Byte_Long = (byte)Char_Long;
        public static readonly byte[] Bytes_Long = new byte[1] { Byte_Long };

        public static readonly char Char_ULong = 'l';
        public static readonly byte Byte_ULong = (byte)Char_ULong;
        public static readonly byte[] Bytes_ULong = new byte[1] { Byte_ULong };

        public static readonly char Char_Decimal = 'M';
        public static readonly byte Byte_Decimal = (byte)Char_Decimal;
        public static readonly byte[] Bytes_Decimal = new byte[1] { Byte_Decimal };

        public static readonly char Char_Float = 'F';
        public static readonly byte Byte_Float = (byte)Char_Float;
        public static readonly byte[] Bytes_Float = new byte[1] { Byte_Float };

        public static readonly char Char_Double = 'D';
        public static readonly byte Byte_Double = (byte)Char_Double;
        public static readonly byte[] Bytes_Double = new byte[1] { Byte_Double };

        public static readonly char Char_Bool = 'A';
        public static readonly byte Byte_Bool = (byte)Char_Bool;
        public static readonly byte[] Bytes_Bool = new byte[1] { Byte_Bool };

        public static readonly char Char_Char = 'C';
        public static readonly byte Byte_Char = (byte)Char_Char;
        public static readonly byte[] Bytes_Char = new byte[1] { Byte_Char };

        public static readonly char String_String = 'T';
        public static readonly byte Byte_String = (byte)String_String;
        public static readonly byte[] Bytes_String = new byte[1] { Byte_String };

        public static readonly char Char_Object = 'O';
        public static readonly byte Byte_Object = (byte)Char_Object;
        public static readonly byte[] Bytes_Object = new byte[1] { Byte_Object };

        public static readonly char Char_DateTime = 'E';
        public static readonly byte Byte_DateTime = (byte)Char_DateTime;
        public static readonly byte[] Bytes_DateTime = new byte[1] { Byte_DateTime };

        public static readonly char Char_TimeSpan = 'e';
        public static readonly byte Byte_TimeSpan = (byte)Char_TimeSpan;
        public static readonly byte[] Bytes_TimeSpan = new byte[1] { Byte_TimeSpan };

        public static readonly char Char_ByteArray = 'J';
        public static readonly byte Byte_ByteArray = (byte)Char_ByteArray;
        public static readonly byte[] Bytes_ByteArray = new byte[1] { Byte_ByteArray };

        public static readonly Dictionary<RuntimeTypeHandle, BaseSerializer> SerializerMap = new Dictionary<RuntimeTypeHandle, BaseSerializer>()
        {
            [typeof(string).TypeHandle] = new StringSerializer(),
            [typeof(int).TypeHandle] = new IntSerializer(),
            [typeof(byte).TypeHandle] = new ByteSerializer(),
            [typeof(sbyte).TypeHandle] = new SByteSerializer(),
            [typeof(uint).TypeHandle] = new UIntSerializer(),
            [typeof(ushort).TypeHandle] = new UShortSerializer(),
            [typeof(short).TypeHandle] = new ShortSerializer(),
            [typeof(long).TypeHandle] = new LongSerializer(),
            [typeof(ulong).TypeHandle] = new ULongSerializer(),
            [typeof(char).TypeHandle] = new CharSerializer(),
            [typeof(bool).TypeHandle] = new BoolSerializer(),
            [typeof(double).TypeHandle] = new DoubleSerializer(),
            [typeof(float).TypeHandle] = new FloatSerializer(),
            [typeof(DateTime).TypeHandle] = new DateTimeSerializer(),
            [typeof(TimeSpan).TypeHandle] = new TimeSpanSerializer(),
            [typeof(byte[]).TypeHandle] = new ByteArraySerializer(),
        };

        public static readonly Dictionary<byte, BaseDeserializer> DeserializerMap = new Dictionary<byte, BaseDeserializer>()
        {
            [Byte_Object] = new ObjectDeserializer(),
            [Byte_Int] = new IntDeserializer(),
            [Byte_String] = new StringDeserializer(),
            [Byte_Byte] = new ByteDeserializer(),
            [Byte_SByte] = new SByteDeserializer(),
            [Byte_UInt] = new UIntDeserializer(),
            [Byte_UShort] = new UShortDeserializer(),
            [Byte_Short] = new ShortDeserializer(),
            [Byte_Long] = new LongDeserializer(),
            [Byte_ULong] = new ULongDeserializer(),
            [Byte_Char] = new CharDeserializer(),
            [Byte_Bool] = new BoolDeserializer(),
            [Byte_Double] = new DoubleDeserializer(),
            [Byte_Float] = new FloatDeserializer(),
            [Byte_DateTime] = new DateTimeDeserializer(),
            [Byte_TimeSpan] = new TimeSpanDeserializer(),
            [Byte_ByteArray] = new ByteArrayDeserializer(),
        };
    }
}