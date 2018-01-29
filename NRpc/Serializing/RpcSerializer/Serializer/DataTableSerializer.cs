using NRpc.Utils;
using System;
using System.Data;
using System.IO;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DataTableSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/29 16:55:30
    /// </summary>
    public sealed class DataTableSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            var data = (DataTable)obj;
            using (MemoryStream stream = new MemoryStream())
            {
                var tableName = data.TableName;
                var tableNameBytes = RpcSerializerUtil.EncodeString(tableName);
                stream.Write(tableNameBytes, 0, tableNameBytes.Length);
                var columnCount = data.Columns.Count;
                stream.Write(BitConverter.GetBytes(columnCount), 0, 4);
                for (int i = 0; i < columnCount; i++)
                {
                    var columnName = data.Columns[i].ColumnName;
                    var columnType = data.Columns[i].DataType;
                    ByteUtil.EncodeString(columnName, out byte[] lengthBytes, out byte[] dataBytes);
                    stream.Write(lengthBytes, 0, lengthBytes.Length);
                    ByteUtil.EncodeString(columnType.FullName, out lengthBytes, out dataBytes);
                    stream.Write(lengthBytes, 0, lengthBytes.Length);
                }
                var rowCount = data.Rows.Count;
                stream.Write(BitConverter.GetBytes(rowCount), 0, 4);
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        var objBytes = SerializerFactory.Serializer(data.Rows[i][j]);
                        stream.Write(objBytes, 0, objBytes.Length);
                    }
                }
                var tableBytes = stream.ToArray();
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_DataTable, BitConverter.GetBytes(tableBytes.Length), tableBytes);
            }
        }
    }
}