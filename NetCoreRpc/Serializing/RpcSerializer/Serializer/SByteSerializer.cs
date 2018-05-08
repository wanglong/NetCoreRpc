namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SByteSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 13:59:41
    /// </summary>
    public sealed class SByteSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_SByte);
            var value = (sbyte)obj;
            int intValue = value < 0 ? (127 - value) : value;
            serializerInputStream.Write(intValue);
        }
    }
}