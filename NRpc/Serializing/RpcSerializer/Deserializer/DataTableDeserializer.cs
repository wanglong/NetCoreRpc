using NRpc.Utils;
using System.Data;

namespace NRpc.Serializing.RpcSerializer.Deserializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DataTableDeserializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/29 17:21:30
    /// </summary>
    public sealed class DataTableDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objectByteData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (objectByteData.Length > 0)
            {
                var tableName = RpcSerializerUtil.DecodeString(objectByteData, 0, out int tableNextStartOffset);
                var tabeOffsetIndex = tableNextStartOffset;
                var table = new DataTable(tableName);
                var columnCount = ByteUtil.DecodeInt(objectByteData, tabeOffsetIndex, out tableNextStartOffset);
                tabeOffsetIndex = tableNextStartOffset;
                for (int i = 0; i < columnCount; i++)
                {
                    var columnName = ByteUtil.DecodeString(objectByteData, tabeOffsetIndex, out tableNextStartOffset);
                    tabeOffsetIndex = tableNextStartOffset;
                    var columnTypeName = ByteUtil.DecodeString(objectByteData, tabeOffsetIndex, out tableNextStartOffset);
                    tabeOffsetIndex = tableNextStartOffset;
                    var type = RpcSerializerUtil.GetType(columnTypeName);
                    table.Columns.Add(columnName, type);
                }
                var rowCount = ByteUtil.DecodeInt(objectByteData, tabeOffsetIndex, out tableNextStartOffset);
                tabeOffsetIndex = tableNextStartOffset;
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = table.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        row[j] = SerializerFactory.Deserializer(objectByteData, tabeOffsetIndex, out tableNextStartOffset);
                        tabeOffsetIndex = tableNextStartOffset;
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                return null;
            }
        }
    }
}