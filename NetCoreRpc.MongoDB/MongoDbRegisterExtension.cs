using Microsoft.Extensions.DependencyInjection;
using NetCoreRpc.MongoDB.RpcMonitor;
using NetCoreRpc.RpcMonitor;
using System;

namespace NetCoreRpc.MongoDB
{
    /// <summary>
    /// 类名：MongoDbRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:57:07
    /// </summary>
    public static class MongoDbRegisterExtension
    {
        public static IServiceCollection UseMongoDBMonitor(this IServiceCollection serviceCollection, Func<MonogoDbConfig> func)
        {
            MonogoDbConfig.SetConfig(func);
            return serviceCollection.AddSingleton<IRpcMonitor, MongoMonitor>();
        }
    }
}