namespace NetCoreRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：BoolSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 16:26:28
    /// </summary>
    public sealed class BoolSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_Bool);
            if ((bool)obj)
            {
                serializerInputStream.Write((byte)1);
            }
            else
            {
                serializerInputStream.Write((byte)0);
            }
        }
    }
}