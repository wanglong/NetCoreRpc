namespace NetCoreRpc.RpcMonitor
{
    /// <summary>
    /// 接口名：IRpcMonitor.cs
    /// 接口功能描述：
    /// 创建标识：yjq 2018/5/11 16:01:04
    /// </summary>
    public interface IRpcMonitor
    {
        void AddRpcRequest(RpcMonitorRequestInfo rpcRequestInfo);

        void AddError(RpcMonitorRequestErrorInfo baseRpcMonitorRequestInfo);
    }
}