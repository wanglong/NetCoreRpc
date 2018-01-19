namespace NetCoreRpc.Client
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcClientConfig.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 13:15:18
    /// </summary>
    public class ClientConfig
    {
        public int TimeouMillis { get; set; } = 10000;

        public static ClientConfig CurrentClientConfig { get; set; } = new ClientConfig();

        public static ClientConfig SetClientConfig(ClientConfig clientConfig)
        {
            CurrentClientConfig = clientConfig;
            return CurrentClientConfig;
        }
    }
}