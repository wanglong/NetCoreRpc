namespace NetCoreRpc.Client.ConfigManage
{
    /// <summary>
    /// 接口名：IRemoteEndPointConfigProvider.cs
    /// 接口功能描述：
    /// 创建标识：yjq 2018/5/9 16:30:48
    /// </summary>
    public interface IRemoteEndPointConfigProvider
    {
        RpcConfig GetConfig();
    }
}