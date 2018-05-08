using System.Data;

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
        public override void WriteBytes(object obj, SerializerInputStream serializerInputStream)
        {
            serializerInputStream.Write(RpcSerializerUtil.Byte_DataSet);
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