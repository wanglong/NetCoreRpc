namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：BoolSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 10:28:45
    /// </summary>
    public sealed class BoolSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_Bool);
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