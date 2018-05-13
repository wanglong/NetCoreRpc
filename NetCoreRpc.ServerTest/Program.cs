using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreRpc.Application;
using NetCoreRpc.MongoDB;
using NetCoreRpc.Server;
using NLog.Extensions.Logging;
using System;
using System.IO;

namespace NetCoreRpc.ServerTest
{
    internal class Program
    {
        public static IConfigurationRoot Configuration;

        private static void Main(string[] args)
        {
            Console.WriteLine("请输入监听端口:");
            var strPort = Console.ReadLine();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory)).AddJsonFile("NetCoreRpc.json", optional: true);
            Configuration = builder.Build();
            var servicesProvider = BuildDi();
            DependencyManage.SetServiceProvider(servicesProvider, Configuration);
            NRpcServer nrpcServer = new NRpcServer(int.Parse(strPort));
            nrpcServer.Start("NetCoreRpc.Application");
            Console.WriteLine("Welcome to use NetCoreRpc!");
            Console.WriteLine("Input exit to exit");
            var str = Console.ReadLine();
            while (!string.Equals(str, "exit", StringComparison.OrdinalIgnoreCase))
            {
                str = Console.ReadLine();
            }
            nrpcServer.ShutDown();
        }

        private static IServiceProvider BuildDi()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton<IStudentApplication, StudentApplication>();
            var str = Configuration.GetValue<string>("MongoDB:Str");
            var dbName = Configuration.GetValue<string>("MongoDB:DatabaseName");
            services.UseRpc()
                    .UseMongoDBMonitor(() =>
                    {

                        return new MonogoDbConfig(str, dbName);
                    });//.UseZK();
            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog("NLog.config");

            return serviceProvider;
        }
    }
}