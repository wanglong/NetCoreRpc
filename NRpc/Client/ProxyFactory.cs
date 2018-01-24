namespace NRpc.Client
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ProxyFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 14:19:26
    /// </summary>
    public class ProxyFactory
    {
        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return (T)(new RpcProxyImpl(typeof(T)).GetTransparentProxy());
        }
    }
}