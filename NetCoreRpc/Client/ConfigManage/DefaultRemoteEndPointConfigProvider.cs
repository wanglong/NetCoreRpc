namespace NetCoreRpc.Client.ConfigManage
{
    /// <summary>
    /// 类名：DefaultRemoteEndPointConfigProvider.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 16:31:34
    /// </summary>
    public class DefaultRemoteEndPointConfigProvider : IRemoteEndPointConfigProvider
    {
        public RpcConfig GetConfig()
        {
            return ConfigurationManage.GetOption<RpcConfig>();
        }
    }
}