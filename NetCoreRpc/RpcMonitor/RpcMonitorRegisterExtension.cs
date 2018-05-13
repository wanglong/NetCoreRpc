using Microsoft.Extensions.DependencyInjection;

namespace NetCoreRpc.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRegisterExyension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/11 16:24:33
    /// </summary>
    public static class RpcMonitorRegisterExtension
    {
        public static IServiceCollection UseDefaultMonitor(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IRpcMonitor, DefaultRpcMonitor>();
        }
    }
}