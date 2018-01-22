using NetCoreRpc.ZK;
using System;

namespace NetCoreRpc.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("===");
            ZkServerTest.StartAsync().Wait();
            Console.ReadLine();
        }
    }
}