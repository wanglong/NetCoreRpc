namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：BaseSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:12:51
    /// </summary>
    public abstract class BaseSerializer
    {
        public abstract void WriteBytes(object obj, SerializerInputStream serializerInputStream);
    }
}