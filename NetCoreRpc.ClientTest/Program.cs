using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreRpc.Application;
using NetCoreRpc.Client;
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
            DependencyManage.SetServiceProvider(servicesProvider);

            Console.WriteLine("Welcome to use NetCoreRpc!");
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

            Console.WriteLine("Input exit to exit");
            var str = Console.ReadLine();
            while (!string.Equals(str, "exit", StringComparison.OrdinalIgnoreCase))
            {
                str = Console.ReadLine();
            }
        }

        private static IServiceProvider BuildDi()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<RemoteEndPointConfig>(Configuration.GetSection("NetCoreRpc"));
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.UseRpc();
            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog("nlog.config");

            return serviceProvider;
        }
    }
}