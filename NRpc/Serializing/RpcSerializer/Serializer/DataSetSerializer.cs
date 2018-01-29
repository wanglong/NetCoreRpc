using NRpc.Utils;
using System;
using System.Data;
using System.IO;

namespace NRpc.Serializing.RpcSerializer.Serializer
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DataSetSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/29 17:51:50
    /// </summary>
    public sealed class DataSetSerializer : BaseSerializer
    {
        public override byte[] GeteObjectBytes(object obj)
        {
            var dataSet = (DataSet)obj;
            using (MemoryStream stream = new MemoryStream())
            {
                var count = dataSet.Tables.Count;
                var countBytes = BitConverter.GetBytes(count);
                stream.Write(countBytes, 0, countBytes.Length);
                for (int i = 0; i < count; i++)
                {
                    var tableBytes = SerializerFactory.Serializer(dataSet.Tables[i]);
                    stream.Write(tableBytes, 0, tableBytes.Length);
                }
                var dataSetBytes = stream.ToArray();
                return ByteUtil.Combine(RpcSerializerUtil.Bytes_DataSet, BitConverter.GetBytes(dataSetBytes.Length), dataSetBytes);
            }
        }
    }
}