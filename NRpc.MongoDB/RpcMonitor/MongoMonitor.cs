using NRpc.RpcMonitor;

namespace NRpc.MongoDB.RpcMonitor
{
    /// <summary>
    /// 类名：MongoMonitor.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 15:58:41
    /// </summary>
    public class MongoMonitor : DefaultRpcMonitor, IRpcMonitor
    {
        public MongoMonitor()
        {
        }

        public override void AddError(RpcMonitorRequestErrorInfo baseRpcMonitorRequestInfo)
        {
            base.AddError(baseRpcMonitorRequestInfo);
            var rpcMonitorRequestErrorRepository = new RpcMonitorRequestErrorRepository();
            rpcMonitorRequestErrorRepository.AddOneRequestInfo(baseRpcMonitorRequestInfo);
        }

        public override void AddRpcRequest(RpcMonitorRequestInfo rpcRequestInfo)
        {
            base.AddRpcRequest(rpcRequestInfo);
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            rpcMonitorRequestRepository.AddOneRequestInfo(rpcRequestInfo);
        }
    }
}