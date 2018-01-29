using NRpc.Utils;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：StringSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:28:52
    /// </summary>
    public sealed class StringSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            ByteUtil.EncodeString((string)obj, out byte[] lengthBytes, out byte[] dataBytes);
            return ByteUtil.Combine(RpcSerializerUtil.Bytes_String, lengthBytes, dataBytes);
        }
    }
}