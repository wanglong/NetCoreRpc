using NetCoreRpc.Extensions;
using NetCoreRpc.Transport.Remoting;
using NRpc;
using System;

namespace NetCoreRpc.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRequestErrorInfo.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:38:24
    /// </summary>
    public class RpcMonitorRequestErrorInfo : BaseRpcMonitorRequestInfo
    {
        public RpcMonitorRequestErrorInfo()
        {
        }

        public RpcMonitorRequestErrorInfo(RpcMethodCallInfo rpcMethodCallInfo, RemotingRequest remotingRequest, string errorMsg) : base(rpcMethodCallInfo, remotingRequest)
        {
            ErrorMsg = errorMsg;
        }

        public RpcMonitorRequestErrorInfo(RpcMethodCallInfo rpcMethodCallInfo, RemotingRequest remotingRequest, Exception ex) : this(rpcMethodCallInfo, remotingRequest, ex.ToErrMsg())
        {
        }

        public string ErrorMsg { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}-{ErrorMsg}";
        }
    }
}