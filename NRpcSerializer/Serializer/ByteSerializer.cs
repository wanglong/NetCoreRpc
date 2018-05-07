namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：ByteSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:32:23
    /// </summary>
    public sealed class ByteSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Byte);
            serializerInputStream.Write((byte)obj);
        }
    }
}