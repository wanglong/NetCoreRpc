using MongoDB.Bson.Serialization.Attributes;
using NetCoreRpc.RpcMonitor;
using System;

namespace NetCoreRpc.MongoDB.Models
{
    /// <summary>
    /// 类名：RpcMonitorRequestModel.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/14 10:46:20
    /// </summary>
    public class RpcMonitorRequestModel
    {
        public string RequestID { get; set; }

        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int RequestParameterCount { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestStartTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestEndTime { get; set; }

        public string RequestTimeMinute { get; set; }

        public bool IsSuccess { get; set; } = true;

        public double TotalMillisecond { get; set; }

        public static RpcMonitorRequestModel Create(RpcMonitorRequestInfo rpcMonitorRequestInfo)
        {
            return new RpcMonitorRequestModel
            {
                IsSuccess = rpcMonitorRequestInfo.IsSuccess,
                RequestEndTime = rpcMonitorRequestInfo.RequestEndTime,
                RequestID = rpcMonitorRequestInfo.RequestID,
                RequestMethodName = rpcMonitorRequestInfo.RequestMethodName,
                RequestParameterCount = rpcMonitorRequestInfo.RequestParameterCount,
                RequestStartTime = rpcMonitorRequestInfo.RequestStartTime,
                RequestTypeName = rpcMonitorRequestInfo.RequestTypeName,
                TotalMillisecond = rpcMonitorRequestInfo.TotalMillisecond,
                RequestTimeMinute = rpcMonitorRequestInfo.RequestStartTime.ToString("yyyyMMddHHmm")
            };
        }
    }
}