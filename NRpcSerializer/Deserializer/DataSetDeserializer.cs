using NRpcSerializer.Utils;
using System.Data;

namespace NRpcSerializer.Deserializer
{
    /// <summary>
    /// 类名：DataSetDeserializer.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 17:41:06
    /// </summary>
    public sealed class DataSetDeserializer : BaseDeserializer
    {
        public override object GetObject(byte[] inputBytes, int startOffset, out int nextStartOffset)
        {
            var objectByteData = ByteUtil.DecodeBytes(inputBytes, startOffset, out nextStartOffset);
            if (objectByteData.Length > 0)
            {
                var count = ByteUtil.DecodeInt(objectByteData, 0, out int setNextOffset);
                var setOffsetIndex = setNextOffset;
                var set = new DataSet();
                for (int i = 0; i < count; i++)
                {
                    var table = SerializerFactory.Deserializer(objectByteData, setOffsetIndex, out setNextOffset);
                    if (table != null)
                    {
                        set.Tables.Add((DataTable)table);
                    }
                    setOffsetIndex = setNextOffset;
                }
                return set;
            }
            else
            {
                return null;
            }
        }
    }
}