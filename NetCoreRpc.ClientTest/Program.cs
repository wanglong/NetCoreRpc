using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreRpc.Application;
using NetCoreRpc.Client;
using NetCoreRpc.Client.ConfigManage;
using NetCoreRpc.MongoDB;
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace NetCoreRpc.ClientTest
{
    internal class Program
    {
        public static IConfigurationRoot Configuration;

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory)).AddJsonFile("NetCoreRpc.json", optional: true);
            Configuration = builder.Build();

            var servicesProvider = BuildDi();
            DependencyManage.SetServiceProvider(servicesProvider, Configuration);

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

        private static IServiceProvider BuildDi()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<RpcConfig>(Configuration.GetSection("NetCoreRpc"));
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.UseRpc().UseMongoDBMonitor(() =>
            {
                var str = Configuration.GetValue<string>("MongoDB:Str");
                var dbName = Configuration.GetValue<string>("MongoDB:DatabaseName");
                return new MonogoDbConfig(str, dbName);
            });//.UseZK();
            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog("NLog.config");

            return serviceProvider;
        }
    }
}

//namespace NetCoreRpc.Application
//{
//    public interface IStudentApplication
//    {
//        int Age();

//        bool IsYongPeople(int age);

//        void Say(string msg);

//        Task Sleep();

//        Task<int> RunAsync(int sleepTime);

//        void Say(byte[] msg);

//        byte[] Say();

//        [BinarySerializer]
//        TestModel Test();
//    }

//    public class StudentApplication : IStudentApplication
//    {
//        public int Age()
//        {
//            return 10;
//        }

//        public bool IsYongPeople(int age)
//        {
//            return age < 18;
//        }

//        public Task<int> RunAsync(int sleepTime)
//        {
//            //await Task.Delay(sleepTime);
//            //return sleepTime;
//            return Task.FromResult(sleepTime);
//        }

//        public void Say(string msg)
//        {
//            Console.WriteLine($"Say:{msg}");
//        }

//        public Task Sleep()
//        {
//            return Task.Delay(10);
//        }

//        public void Say(byte[] msg)
//        {
//            Console.WriteLine(Encoding.UTF8.GetString(msg));
//        }

//        public byte[] Say()
//        {
//            return Encoding.UTF8.GetBytes("Good Job!");
//        }

//        public TestModel Test()
//        {
//            return new TestModel
//            {
//                Age = 10,
//                Msg = Encoding.UTF8.GetBytes("Hello")
//            };
//        }
//    }

//    [Serializable]
//    public class TestModel
//    {
//        public int Age { get; set; }

//        public byte[] Msg { get; set; }

//        public override string ToString()
//        {
//            return $"{Age}|{Encoding.UTF8.GetString(Msg)}";
//        }
//    }
//}