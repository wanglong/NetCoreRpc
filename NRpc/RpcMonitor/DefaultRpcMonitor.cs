using NRpc.Utils;
using System;

namespace NRpc.RpcMonitor
{
    /// <summary>
    /// 类名：DefaultRpcMonitor.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/11 16:23:35
    /// </summary>
    public class DefaultRpcMonitor : IRpcMonitor
    {
        public virtual void AddRpcRequest(RpcMonitorRequestInfo rpcRequestInfo)
        {
            LogUtil.Debug(rpcRequestInfo.ToString());
        }

        public virtual void AddError(RpcMonitorRequestErrorInfo baseRpcMonitorRequestInfo)
        {
            LogUtil.Error(baseRpcMonitorRequestInfo.ToString());
        }
    }
}