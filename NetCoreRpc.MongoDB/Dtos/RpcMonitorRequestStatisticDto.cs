using System;
using System.Collections.Generic;

namespace NetCoreRpc.MongoDB.Dtos
{
    /// <summary>
    /// 类名：RpcMonitorRequestStatisticDto.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/14 16:37:28
    /// </summary>
    public class RpcMonitorRequestStatisticDto<T> : RpcMonitorRequestStatisticDto
    {
        public RpcMonitorRequestStatisticDto(string desc, IEnumerable<string> timeArray, int order)
        {
            Description = desc;
            TimeArray = timeArray;
            OrderIndex = order;
        }

        public List<T> ValueList { get; set; } = new List<T>();
    }

    public class RpcMonitorRequestStatisticDto
    {
        public string Description { get; set; }
        public IEnumerable<string> TimeArray { get; set; }

        public int OrderIndex { get; set; }
    }
}