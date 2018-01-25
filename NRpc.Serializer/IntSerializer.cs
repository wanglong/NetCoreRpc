namespace NRpc.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：IntSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/25 15:08:29
    /// </summary>
    public sealed class IntSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj, SerializerOutput serializerOutput)
        {
            return serializerOutput.GetIntBytes(obj);
        }
    }
}