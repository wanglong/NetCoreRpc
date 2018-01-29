using System;

namespace NetCoreRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：EnumDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EnumDeserializer
    /// 创建标识：yjq 2018/1/27 18:04:14
    /// </summary>
    public sealed class EnumDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var enumTypeName = RpcSerializerUtil.DecodeString(inputBytes, startOffset, out nextStartOffset);
            startOffset = nextStartOffset;
            var enumValue = RpcSerializerUtil.DecodeString(inputBytes, startOffset, out nextStartOffset);
            var type = RpcSerializerUtil.GetType(enumTypeName);
            return Enum.Parse(type, enumValue, false);
        }
    }
}