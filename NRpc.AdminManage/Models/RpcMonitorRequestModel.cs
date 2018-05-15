using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NRpc.AdminManage.Models
{
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
    }
}