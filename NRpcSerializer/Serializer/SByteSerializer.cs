namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：SByteSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 11:04:42
    /// </summary>
    public sealed class SByteSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_SByte);
            var value = (sbyte)obj;
            int intValue = value < 0 ? (127 - value) : value;
            serializerInputStream.Write(intValue);
        }
    }
}