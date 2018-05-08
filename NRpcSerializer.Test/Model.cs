using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NRpcSerializer.Test
{
    /// <summary>
    /// 类名：Model.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/7 17:02:42
    /// </summary>
    public class TT
    {
        public int P3 { get; set; }
    }

    public class BaseTest : TT
    {
        public int? P1 { get; set; }
    }

    public class Test : BaseTest
    {
        public int P2 { get; set; }

        public byte? P4 { get; set; }
        public sbyte? P5 { get; set; }

        public uint? P6 { get; set; }

        public short? P7 { get; set; }
        public ushort? P8 { get; set; }
        public long? P9 { get; set; }
        public ulong? P10 { get; set; }
        public char? P11 { get; set; }
        public bool? P12 { get; set; }
        public double? P13 { get; set; }
        public float? P14 { get; set; }
        public DateTime? P15 { get; set; }
        public TimeSpan? P16 { get; set; }

        public string P17 { get; set; }

        public byte[] P18 { get; set; }

        public Aa P19 { get; set; }

        public override string ToString()
        {
            var msg = (P18 != null && P18.Length > 0) ? Encoding.UTF8.GetString(P18) : "";
            return $"{P1},{P2},{P3},{P4},{P5},{P6},{P7},{P8},{P9},{P10},{P11},{P12},{P13},{P14},{P15},{P16},{P17},{msg},{P19?.ToString()}";
        }
    }

    public class Aa
    {
        public int P1 { get; set; }

        public Sex P2 { get; set; }

        public int[][] P3 { get; set; }

        public Dictionary<int, string> P4 { get; set; }

        public IEnumerable<int> P5 { get; set; }

        public Guid P6 { get; set; }

        public ObjectId P7 { get; set; }

        public DataTable P8 { get; set; } = new DataTable();
        public DataSet P9 { get; set; } = new DataSet();

        public override string ToString()
        {
            if (P4 != null)
            {
                foreach (var item in P4)
                {
                    Console.WriteLine($"{item.Key},{item.Value}");
                }
            }
            if (P5 != null)
            {
                Console.WriteLine(string.Join("|", P5));
            }
            if (P8 != null)
            {
                Console.WriteLine(P8.TableName);
            }
            return $"{P1},{P2},{string.Join(",", P3[1])},{P6.ToString()},{P7.ToString()}";
        }
    }

    public enum Sex
    {
        Man = 1,
        Woman = 2
    }
}