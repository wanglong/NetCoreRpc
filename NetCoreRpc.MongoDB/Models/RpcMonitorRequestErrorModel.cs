using MongoDB.Bson.Serialization.Attributes;
using NetCoreRpc.RpcMonitor;
using System;

namespace NetCoreRpc.MongoDB.Models
{
    /// <summary>
    /// 类名：RpcMonitorRequestErrorModel.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/14 10:52:13
    /// </summary>
    public class RpcMonitorRequestErrorModel
    {
        public string RequestID { get; set; }

        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int RequestParameterCount { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestTime { get; set; }

        public string ErrorMsg { get; set; }

        public string RequestTimeMinute { get; set; }

        public static RpcMonitorRequestErrorModel Create(RpcMonitorRequestErrorInfo rpcMonitorRequestErrorInfo)
        {
            return new RpcMonitorRequestErrorModel
            {
                ErrorMsg = rpcMonitorRequestErrorInfo.ErrorMsg,
                RequestID = rpcMonitorRequestErrorInfo.RequestID,
                RequestMethodName = rpcMonitorRequestErrorInfo.RequestMethodName,
                RequestParameterCount = rpcMonitorRequestErrorInfo.RequestParameterCount,
                RequestTimeMinute = rpcMonitorRequestErrorInfo.RequestTime.ToString("yyyyMMddHHdd"),
                RequestTime = rpcMonitorRequestErrorInfo.RequestTime,
                RequestTypeName = rpcMonitorRequestErrorInfo.RequestTypeName
            };
        }
    }
}