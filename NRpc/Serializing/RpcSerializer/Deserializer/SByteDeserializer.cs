using NRpc.Utils;

namespace NRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SByteDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 14:01:50
    /// </summary>
    public sealed class SByteDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var intValue = ByteUtil.DecodeInt(inputBytes, startOffset, out nextStartOffset);
            if (intValue >= 127)
            {
                return 127 - intValue;
            }
            else
            {
                return (sbyte)intValue;
            }
        }
    }
}