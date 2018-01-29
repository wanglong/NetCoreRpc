namespace NetCoreRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：BaseDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/26 10:05:10
    /// </summary>
    public abstract class BaseDeserializer
    {
        public abstract object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset);
    }
}