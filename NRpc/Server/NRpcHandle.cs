using NRpc.Transport.Remoting;
using System.Collections.Generic;

namespace NRpc.Server
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：NRpcHandle.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 14:35:27
    /// </summary>
    internal sealed class NRpcHandle : IRequestHandler
    {
        private readonly ServerMethodCaller _serverMethodCaller;

        public NRpcHandle(List<IServerFilter> list)
        {
            _serverMethodCaller = new ServerMethodCaller(list);
        }

        public RemotingResponse HandleRequest(IRequestHandlerContext context, RemotingRequest remotingRequest)
        {
            return _serverMethodCaller.HandleRequest(remotingRequest);
        }
    }
}