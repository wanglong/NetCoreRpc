namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：LongSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:01:46
    /// </summary>
    public sealed class LongSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Long);
            serializerInputStream.Write((long)obj);
        }
    }
}