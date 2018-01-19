using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreRpc.Application;
using NetCoreRpc.Server;
using NLog.Extensions.Logging;
using System;

namespace NetCoreRpc.ServerTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var servicesProvider = BuildDi();
            DependencyManage.SetServiceProvider(servicesProvider);
            NRpcServer nrpcServer = new NRpcServer(12345);
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