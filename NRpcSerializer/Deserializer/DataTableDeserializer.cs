using NRpcSerializer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：DataTableDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 17:33:15
    /// </summary>
    public sealed class DataTableDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objectByteData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (objectByteData.Length > 0)
            {
                var tableName = ByteUtil.DecodeString(objectByteData, 0, out int tableNextStartOffset);
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
                    var type = SerializerUtil.GetType(columnTypeName);
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
