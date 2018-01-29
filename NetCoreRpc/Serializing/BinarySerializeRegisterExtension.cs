using Microsoft.Extensions.DependencyInjection;
using NetCoreRpc.Serializing.RpcSerializer;

namespace NetCoreRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BinarySerializeRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/27 17:18:27
    /// </summary>
    internal static class BinarySerializeRegisterExtension
    {
        public static IServiceCollection UseDefaultSerializer(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IBinarySerializer, DefaultBinarySerializer>();
        }

        public static IServiceCollection UseDefaultMethodSerializer(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IMethodCallSerializer, RpcDefaultSerializer>();
        }

        public static IServiceCollection UseDefaultResponseSerializer(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IResponseSerailizer, ResponseSerializer>();
        }
    }
}