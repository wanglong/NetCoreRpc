using Microsoft.Extensions.DependencyInjection;

namespace NetCoreRpc.LockManage
{
    /// <summary>
    /// 类名：LockRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 15:58:40
    /// </summary>
    public static class LockRegisterExtension
    {
        public static IServiceCollection UseDefaultLock(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ILock, LocalLock>();
        }
    }
}