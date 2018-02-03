using System;
using System.Text;

namespace NRpc.Transport.Remoting
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DefalutResponse.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/27 17:43:02
    /// </summary>
    public static class ResponseUtil
    {
        public static readonly byte[] DealErrorResponseBody = Encoding.UTF8.GetBytes("Remoting Deal Error.");

        //public static readonly byte[] NotFoundResponseBody = Encoding.UTF8.GetBytes("Not Found Method.");

        public static readonly byte[] NoneBodyResponse = new byte[0];

        /// <summary>
        /// 创建处理出错的RemotingResponse
        /// </summary>
        /// <param name="remotingRequest"></param>
        /// <returns></returns>
        public static RemotingResponse CreateDealErrorResponse(this RemotingRequest remotingRequest)
        {
            return new RemotingResponse(remotingRequest.Type, remotingRequest.Code, remotingRequest.Sequence, remotingRequest.CreatedTime, ResponseState.Error, DealErrorResponseBody, DateTime.Now, remotingRequest.Header, null);
        }

        public static RemotingResponse CreateDealErrorResponse(this RemotingRequest remotingRequest, byte[] data)
        {
            return new RemotingResponse(remotingRequest.Type, remotingRequest.Code, remotingRequest.Sequence, remotingRequest.CreatedTime, ResponseState.Error, data, DateTime.Now, remotingRequest.Header, null);
        }

        /// <summary>
        /// 创建未找到方法的RemotingResponse
        /// </summary>
        /// <param name="remotingRequest"></param>
        /// <param name="methodName">方法名字</param>
        /// <returns></returns>
        public static RemotingResponse CreateNotFoundResponse(this RemotingRequest remotingRequest, string methodName)
        {
            string messag = $"Not Fount Method:[{methodName}].";
            return new RemotingResponse(remotingRequest.Type, remotingRequest.Code, remotingRequest.Sequence, remotingRequest.CreatedTime, ResponseState.NotFound, Encoding.UTF8.GetBytes(messag), DateTime.Now, remotingRequest.Header, null);
        }

        /// <summary>
        /// 创建成功的输出对象
        /// </summary>
        /// <param name="remotingRequest"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static RemotingResponse CreateSuccessResponse(this RemotingRequest remotingRequest, byte[] body)
        {
            return new RemotingResponse(remotingRequest.Type, remotingRequest.Code, remotingRequest.Sequence, remotingRequest.CreatedTime, ResponseState.Success, body, DateTime.Now, remotingRequest.Header, null);
        }

        /// <summary>
        /// 判断反馈内容是否为空的
        /// </summary>
        /// <param name="remotingResponse"></param>
        /// <returns></returns>
        public static bool IsEmptyBody(this RemotingResponse remotingResponse)
        {
            return remotingResponse == null || remotingResponse.ResponseBody == null || remotingResponse.ResponseBody.Length == 0;
        }
    }
}