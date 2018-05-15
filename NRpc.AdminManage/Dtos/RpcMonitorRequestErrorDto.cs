using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NRpc.AdminManage.Dtos
{
    /// <summary>
    /// 类名：RpcMonitorRequestErrorDto.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/15 13:13:05
    /// </summary>
    public class RpcMonitorRequestErrorDto
    {
        public string RequestID { get; set; }

        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int RequestParameterCount { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestTime { get; set; }

        public string ErrorMsg { get; set; }
    }
}