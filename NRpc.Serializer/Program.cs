using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(typeof(int?).Name);
            //Console.WriteLine(typeof(int?).BaseType.Name);
            //Console.WriteLine(typeof(double?).BaseType.Name);
            //Console.WriteLine(typeof(double).Name);
            //Console.WriteLine(typeof(double?).Name);
            var test = new Test
            {
                P1 = 1,
                P2 = 2,
                P3 = 3
            };
            var bytes = SerializerFactory.GetSerializer(typeof(Test)).GeteObjectBytes(test, new SerializerOutput());
            var obj = SerializerFactory.GetDserializer(bytes, 0, out int nextStartOffset).GetObject(bytes, nextStartOffset, out nextStartOffset);
            Console.ReadLine();
        }
    }

    public class TT
    {
        public int P3 { get; set; }
    }

    public class BaseTest : TT
    {
        public int P1 { get; set; }
    }

    public class Test : BaseTest
    {
        public int P2 { get; set; }
    }
}
