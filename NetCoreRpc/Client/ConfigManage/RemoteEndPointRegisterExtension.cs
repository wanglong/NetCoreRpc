using Microsoft.Extensions.DependencyInjection;

namespace NetCoreRpc.Client.ConfigManage
{
    /// <summary>
    /// 类名：RemoteEndPointRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 16:37:15
    /// </summary>
    public static class RemoteEndPointRegisterExtension
    {
        public static IServiceCollection UseDefaultRemoteEndPointConfigProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IRemoteEndPointConfigProvider, DefaultRemoteEndPointConfigProvider>();
        }
    }
}