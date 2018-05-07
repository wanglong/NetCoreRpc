using System.Data;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：DataSetSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 15:48:03
    /// </summary>
    public sealed class DataSetSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_DataSet);
            var startLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//当前流长度占位
            var dataSet = (DataSet)obj;
            serializerInputStream.Write(dataSet.Tables.Count);
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                SerializerFactory.Serializer(dataSet.Tables[i], serializerInputStream);
            }
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
        }
    }
}