using NRpc.Serializing.RpcSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRpc.Serializer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //IEnumerator<>;
            //IEnumerable<>;
            //Console.WriteLine((typeof(TT)).FullName);
            //Console.WriteLine(sizeof(Double));
            //Console.WriteLine(sizeof(float));
            //Console.WriteLine(sizeof(decimal));
            //Console.WriteLine(typeof(byte[][]));
            //var type = typeof(byte[][]);
            //Console.WriteLine(type.GetElementType().Name);
            //Console.ReadLine();
            var test = new Test
            {
                P1 = null,
                P2 = 2,
                P3 = 3,
                P4 = 4,
                P5 = 5,
                P6 = 6,
                P7 = 7,
                P8 = 8,
                P9 = 9,
                P10 = 10,
                P11 = 'a',
                P12 = false,
                P13 = 1.1,
                P14 = 2,
                P15 = DateTime.Now,
                P16 = TimeSpan.FromTicks(1025233),
                P17 = "测试数据",
                P18 = Encoding.UTF8.GetBytes("你好啊"),
                P19 = new Aa
                {
                    P1 = 19,
                    P2 = Sex.Woman,
                    P3 = new int[][] {
                        new int[]{ },new int[]{ 1,2,4,5}
                    },
                    P4 = new Dictionary<int, string>
                    {
                        [1] = "1",
                        [2] = "2"
                    },
                    P5 =Enumerable.Range(0,10)
                }
            };
            var bytes = SerializerFactory.Serializer(test);
            var obj = SerializerFactory.Deserializer(bytes);
            Console.WriteLine(obj?.ToString());
            var bytes2 = SerializerFactory.Serializer(null);
            var obj2 = SerializerFactory.Deserializer(bytes2);
            Console.ReadLine();
        }
    }

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
            return $"{P1},{P2},{string.Join(",", P3[1])}";
        }
    }

    public enum Sex
    {
        Man = 1,
        Woman = 2
    }
}