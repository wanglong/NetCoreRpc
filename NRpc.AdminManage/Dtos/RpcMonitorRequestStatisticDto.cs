using System.Collections.Generic;

namespace NRpc.AdminManage.Dtos
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

    public class RpcMonitorRequestStatisticTotalDto
    {
        private int? _totalExcuteCount;
        private double? _avgExcutedMillisecond;
        private int? _totalErrorCount;
        private double? _maxExcutedMillisecond;
        private double? _minExcutedMillisecond;

        public int? TotalExcuteCount
        {
            get => _totalExcuteCount ?? 0;
            set => _totalExcuteCount = value;
        }

        public int? TotalErrorCount
        {
            get => _totalErrorCount ?? 0;
            set => _totalErrorCount = value;
        }

        public double? AvgExcutedMillisecond
        {
            get => _avgExcutedMillisecond ?? 0;
            set => _avgExcutedMillisecond = value;
        }

        public double? MaxExcutedMillisecond
        {
            get => _maxExcutedMillisecond ?? 0;
            set => _maxExcutedMillisecond = value;
        }

        public double? MinExcutedMillisecond
        {
            get => _minExcutedMillisecond ?? 0;
            set => _minExcutedMillisecond = value;
        }

        public int EverySecondExcuteCount { get; set; }

        public IEnumerable<RpcMonitorRequestStatisticDto> StatisticList { get; set; }
    }
}