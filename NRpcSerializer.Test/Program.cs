using NetCoreRpc.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NRpcSerializer.Test
{
    class Program
    {
        private delegate object CreateDelegate();
        static void Main(string[] args)
        {
            CodeTimer.Time("Serializer", 100000, new CodeTestClass().Action);
            CodeTimer.Time("json", 100000, new JsonCodeTestClass().Action);
            CodeTimer.Time("json", 100000, new JsonCodeTestClass().Action);
            CodeTimer.Time("Serializer", 100000, new CodeTestClass().Action);
            CodeTimer.Time("json", 100000, new JsonCodeTestClass().Action);
            CodeTimer.Time("Serializer", 100000, new CodeTestClass().Action);
            CodeTimer.Time("json", 100000, new JsonCodeTestClass().Action);
            CodeTimer.Time("Serializer", 100000, new CodeTestClass().Action);
            CodeTimer.Time("json", 100000, new JsonCodeTestClass().Action);
            Console.ReadLine();
        }
    }



    public class StreamTestClass : IAction
    {
        public void Action()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                for (int i = 0; i < 100; i++)
                {
                    stream.Write(BitConverter.GetBytes(i), 0, 4);
                }
            }
        }
    }

    public class ByteTestClass : IAction
    {
        public void Action()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                for (int i = 0; i < 100; i++)
                {
                    foreach (var item in BitConverter.GetBytes(i))
                    {
                        stream.WriteByte(item);
                    }
                }
            }
        }
    }

    public class CodeTestClass : IAction
    {
        public void Action()
        {
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
                    P5 = Enumerable.Range(0, 10),
                    P6 = Guid.NewGuid(),
                    P7 = ObjectId.GenerateNewId()
                }
            };
            var bytes =  SerializerFactory.Serializer(test);
            var obj =  SerializerFactory.Deserializer(bytes);
        }
    }

    public class JsonCodeTestClass : IAction
    {
        public void Action()
        {
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
                P17 = string.Empty,
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
                    P5 = Enumerable.Range(0, 10),
                    P6 = Guid.NewGuid(),
                    P7 = ObjectId.GenerateNewId()
                }
            };
            var bytes = new JsonBinarySerializer().Serialize(test);
            //Console.WriteLine(bytes.Length);
            var obj = new JsonBinarySerializer().Deserialize<Test>(bytes);
            //Console.WriteLine(obj?.ToString());
        }
    }
}
