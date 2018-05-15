using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NRpc.AdminManage.Models
{
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
    }
}