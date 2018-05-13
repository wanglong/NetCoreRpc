using NRpc.Transport.Remoting;
using NRpc;

namespace NRpc.RpcMonitor
{
    /// <summary>
    /// 类名：BaseRpcMonitorRequestInfo.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/11 16:58:26
    /// </summary>
    public class BaseRpcMonitorRequestInfo
    {
        public BaseRpcMonitorRequestInfo()
        {
        }

        public BaseRpcMonitorRequestInfo(RpcMethodCallInfo rpcMethodCallInfo, RemotingRequest remotingRequest) : this(remotingRequest.Id, rpcMethodCallInfo.TypeName, rpcMethodCallInfo.MethodName, rpcMethodCallInfo.Parameters?.Length ?? 0)
        {
        }

        public BaseRpcMonitorRequestInfo(string requestID, string requestTypeName, string requestMethodName, int parameterCount) : this()
        {
            RequestID = requestID;
            RequestTypeName = requestTypeName;
            RequestMethodName = requestMethodName;
            RequestParameterCount = parameterCount;
        }

        public string RequestID { get; set; }

        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int RequestParameterCount { get; set; }

        public override string ToString()
        {
            return $"{RequestID}-{RequestTypeName}-{RequestMethodName}-{RequestParameterCount}";
        }
    }
}