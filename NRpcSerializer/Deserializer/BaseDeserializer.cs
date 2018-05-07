namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：BaseDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 16:17:06
    /// </summary>
    public abstract class BaseDeserializer
    {
        public abstract object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset);
    }
}