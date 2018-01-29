using NetCoreRpc.Application;
using NRpc.Serializing.Attributes;
using NRpc.Server;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NRpc.ServerTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("请输入监听端口:");
            var strPort = Console.ReadLine();
            DependencyManage.UseAutofacContainer().UseNRpc().RegisterType<IStudentApplication, StudentApplication>();
            NRpcServer nrpcServer = new NRpcServer(int.Parse(strPort));
            nrpcServer.RegisterServerType(typeof(IStudentApplication));
            nrpcServer.Start();
            Console.WriteLine("Welcome to use NetCoreRpc!");
            Console.WriteLine("Input exit to exit");
            var str = Console.ReadLine();
            while (!string.Equals(str, "exit", StringComparison.OrdinalIgnoreCase))
            {
                str = Console.ReadLine();
            }
            nrpcServer.ShutDown();
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

    public class StudentApplication : IStudentApplication
    {
        public int Age()
        {
            return 10;
        }

        public bool IsYongPeople(int age)
        {
            return age < 18;
        }

        public Task<int> RunAsync(int sleepTime)
        {
            //await Task.Delay(sleepTime);
            //return sleepTime;
            return Task.FromResult(sleepTime);
        }

        public void Say(string msg)
        {
            Console.WriteLine($"Say:{msg}");
        }

        public Task Sleep()
        {
            return Task.Delay(10);
        }

        public void Say(byte[] msg)
        {
            Console.WriteLine(Encoding.UTF8.GetString(msg));
        }

        public byte[] Say()
        {
            return Encoding.UTF8.GetBytes("Good Job!");
        }

        public TestModel Test()
        {
            return new TestModel
            {
                Age = 10,
                Msg = Encoding.UTF8.GetBytes("Hello")
            };
        }
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