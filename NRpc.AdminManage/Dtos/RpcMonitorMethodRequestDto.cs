namespace NRpc.AdminManage.Dtos
{
    public class RpcMonitorMethodRequestDto
    {
        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int RequestParameterCount { get; set; }

        public int TotalExcuteCount { get; set; }

        public int ErrorCount { get; set; }

        public double MaxExcutedMillisecond { get; set; }

        public double MinExcutedMillisecond { get; set; }

        public double AvgExcutedMillisecond { get; set; }
    }
}