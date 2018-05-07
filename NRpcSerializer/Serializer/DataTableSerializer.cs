using System.Data;

namespace NRpcSerializer.Serializer
{
    /// <summary>
    /// 类名：DataTableSerializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 13:28:56
    /// </summary>
    public sealed class DataTableSerializer : BaseSerializer
    {
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(SerializerUtil.Byte_DataTable);
            var startLength = serializerInputStream.Length;
            serializerInputStream.Write(0);//当前流长度占位
            var data = (DataTable)obj;
            serializerInputStream.Write(data.TableName);
            serializerInputStream.Write(data.Columns.Count);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                serializerInputStream.Write(data.Columns[i].ColumnName);
                serializerInputStream.Write(data.Columns[i].DataType.FullName);
            }
            serializerInputStream.Write(data.Rows.Count);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    SerializerFactory.Serializer(data.Rows[i][j], serializerInputStream);
                }
            }
            serializerInputStream.UpdateCurrentLength(serializerInputStream.Length - startLength - 4, startLength);//填补流长度
        }
    }
}