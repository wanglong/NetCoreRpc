using Microsoft.Extensions.DependencyInjection;
using NetCoreRpc.Transport.Socketing.Framing;

namespace NetCoreRpc.Transport
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SocketRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 15:02:47
    /// </summary>
    public static class SocketRegisterExtension
    {
        /// <summary>
        /// 使用socket
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static IServiceCollection UseSocket(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMessageFramer, LengthPrefixMessageFramer>();
            return serviceCollection;
        }
    }
}