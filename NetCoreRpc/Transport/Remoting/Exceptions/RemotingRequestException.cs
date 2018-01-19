using System;
using System.Net;

namespace NetCoreRpc.Transport.Remoting.Exceptions
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RemotingRequestException.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:10:04
    /// </summary>
    public class RemotingRequestException : Exception
    {
        public RemotingRequestException(EndPoint serverEndPoint, RemotingRequest request, string errorMessage)
            : base(string.Format("Send request {0} to server [{1}] failed, errorMessage:{2}", request, serverEndPoint, errorMessage))
        {
        }

        public RemotingRequestException(EndPoint serverEndPoint, RemotingRequest request, Exception exception)
            : base(string.Format("Send request {0} to server [{1}] failed.", request, serverEndPoint), exception)
        {
        }
    }
}