namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：IntSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:00:37
    /// </summary>
    public sealed class IntSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Int);
            serializerInputStream.Write((int)obj);
        }
    }
}