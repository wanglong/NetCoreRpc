using System;

namespace NRpc.AdminManage.Dtos
{
    /// <summary>
    /// 类名：RpcMonitorRequestDto.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/14 14:47:52
    /// </summary>
    public class RpcMonitorRequestDto
    {
        public bool IsSuccess { get; set; }
        public int TotalCount { get; set; }
        public string ExcutedTime { get; set; }
        public double MaxExcutedMillisecond { get; set; }
        public double MinExcutedMillisecond { get; set; }
        public double AvgExcutedMillisecond { get; set; }

        public DateTime ExcutedMinute
        {
            get
            {
                return DateTime.Parse(ExcutedTime).AddHours(8);
            }
        }
    }
}