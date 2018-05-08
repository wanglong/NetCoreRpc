using NRpcSerializer.Deserializer;
using NRpcSerializer.Serializer;
using System;
using System.Collections.Generic;
using System.Data;

namespace NRpcSerializer
{
    /// <summary>
    /// 类名：Serializer_Const.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:21:36
    /// </summary>
    internal static partial class SerializerUtil
    {
        public static readonly byte[] SizeBytes = BitConverter.GetBytes(4);

        public static readonly byte Byte_Byte = 1;
        public static readonly byte[] Bytes_Byte = new byte[1] { Byte_Byte };

        public static readonly byte Byte_SByte = 2;
        public static readonly byte[] Bytes_SByte = new byte[1] { Byte_SByte };

        public static readonly byte Byte_Int = 3;
        public static readonly byte[] Bytes_Int = new byte[1] { Byte_Int };

        public static readonly byte Byte_UInt = 4;
        public static readonly byte[] Bytes_UInt = new byte[1] { Byte_UInt };

        public static readonly byte Byte_Short = 5;
        public static readonly byte[] Bytes_Short = new byte[1] { Byte_Short };

        public static readonly byte Byte_UShort = 6;
        public static readonly byte[] Bytes_UShort = new byte[1] { Byte_UShort };

        public static readonly byte Byte_Long = 7;
        public static readonly byte[] Bytes_Long = new byte[1] { Byte_Long };

        public static readonly byte Byte_ULong = 8;
        public static readonly byte[] Bytes_ULong = new byte[1] { Byte_ULong };

        public static readonly byte Byte_Decimal = 9;
        public static readonly byte[] Bytes_Decimal = new byte[1] { Byte_Decimal };

        public static readonly byte Byte_Float = 10;
        public static readonly byte[] Bytes_Float = new byte[1] { Byte_Float };

        public static readonly byte Byte_Double = 11;
        public static readonly byte[] Bytes_Double = new byte[1] { Byte_Double };

        public static readonly byte Byte_Bool = 12;
        public static readonly byte[] Bytes_Bool = new byte[1] { Byte_Bool };

        public static readonly byte Byte_Char = 13;
        public static readonly byte[] Bytes_Char = new byte[1] { Byte_Char };

        public static readonly byte Byte_String = 14;
        public static readonly byte[] Bytes_String = new byte[1] { Byte_String };

        public static readonly byte Byte_Object = 15;
        public static readonly byte[] Bytes_Object = new byte[1] { Byte_Object };

        public static readonly byte Byte_DateTime = 16;
        public static readonly byte[] Bytes_DateTime = new byte[1] { Byte_DateTime };

        public static readonly byte Byte_TimeSpan = 17;
        public static readonly byte[] Bytes_TimeSpan = new byte[1] { Byte_TimeSpan };

        public static readonly byte Byte_ByteArray = 18;
        public static readonly byte[] Bytes_ByteArray = new byte[1] { Byte_ByteArray };

        public static readonly byte Byte_Enum = 19;
        public static readonly byte[] Bytes_Enum = new byte[1] { Byte_Enum };

        public static readonly byte Byte_Array = 20;
        public static readonly byte[] Bytes_Array = new byte[1] { Byte_Array };

        public static readonly byte Byte_Dictionary = 21;
        public static readonly byte[] Bytes_Dictionary = new byte[1] { Byte_Dictionary };

        public static readonly byte Byte_Enumerable = 22;
        public static readonly byte[] Bytes_Enumerable = new byte[1] { Byte_Enumerable };

        public static readonly byte Byte_DataTable = 23;
        public static readonly byte[] Bytes_DataTable = new byte[1] { Byte_DataTable };

        public static readonly byte Byte_DataSet = 24;
        public static readonly byte[] Bytes_DataSet = new byte[1] { Byte_DataSet };

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
            [typeof(DataTable).TypeHandle] = new DataTableSerializer(),
            [typeof(DataSet).TypeHandle] = new DataSetSerializer(),
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
            [Byte_Enum] = new EnumDeserializer(),
            [Byte_Array] = new ArrayDeserializer(),
            [Byte_Dictionary] = new DictionaryDeserializer(),
            [Byte_Enumerable] = new EnumerableDeserializer(),
            [Byte_DataTable] = new DataTableDeserializer(),
            [Byte_DataSet] = new DataSetDeserializer(),
        };
    }
}