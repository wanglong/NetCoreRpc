using NetCoreRpc.Application;
using NRpc.Client;
using NRpc.MongoDB;
using NRpc.Serializing.Attributes;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.ClientTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DependencyManage.UseAutofacContainer().UseNRpc().UseMongoDBMonitor(() =>
            {
                return new MonogoDbConfig("mongodb://root:root@192.168.100.125:27017", "Rpc_Monitor");
            });
            NRpcConfigWatcher.Install();
            Console.WriteLine("Welcome to use NetCoreRpc!");
            Console.WriteLine("Input exit to exit");
            var str = Console.ReadLine();
            while (!string.Equals(str, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Send();
                str = Console.ReadLine();
            }
        }

        private static void Send()
        {
            var studentApplication = ProxyFactory.Create<IStudentApplication>();
            Console.WriteLine(studentApplication.Age());
            Console.WriteLine(studentApplication.IsYongPeople(15));
            var runTask = studentApplication.RunAsync(111);
            studentApplication.Say("Hello world");
            studentApplication.Say(Encoding.UTF8.GetBytes("Hi!"));
            Console.WriteLine(Encoding.UTF8.GetString(studentApplication.Say()));
            var test = studentApplication.Test();
            Console.WriteLine(test.ToString());
            studentApplication.Sleep();
            Console.WriteLine(runTask.Result);
        }
    }
}

namespace NetCoreRpc.Application
{
    public interface IStudentApplication
    {
        int Age();

        bool IsYongPeople(int age);

        void Say(string msg);

        Task Sleep();

        Task<int> RunAsync(int sleepTime);

        void Say(byte[] msg);

        byte[] Say();

        [BinarySerializer]
        TestModel Test();
    }

    public class TestModel
    {
        public int Age { get; set; }

        public byte[] Msg { get; set; }

        public override string ToString()
        {
            return $"{Age}|{Encoding.UTF8.GetString(Msg)}";
        }
    }
}