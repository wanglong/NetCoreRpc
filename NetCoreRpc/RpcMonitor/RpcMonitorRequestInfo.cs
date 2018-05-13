using System;
using System.Reflection;

namespace NetCoreRpc.RpcMonitor
{
    /// <summary>
    /// 类名：RpcRequestInfo.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/11 16:02:51
    /// </summary>
    public class RpcMonitorRequestInfo : BaseRpcMonitorRequestInfo
    {
        public RpcMonitorRequestInfo()
        {
        }

        public RpcMonitorRequestInfo(MethodInfo methodInfo, int argumentCount) : base(ObjectId.GenerateNewStringId(), methodInfo.DeclaringType.FullName, methodInfo.Name, argumentCount)
        {
        }

        public DateTime RequestStartTime { get; set; } = DateTime.Now;

        public DateTime RequestEndTime { get; set; }

        public bool IsSuccess { get; set; } = true;

        public double TotalMillisecond
        {
            get
            {
                return (RequestEndTime - RequestStartTime).TotalMilliseconds;
            }
        }

        public override string ToString()
        {
            return $"{RequestID}-{RequestStartTime.ToString("yyyyMMddHH:mm:sss")}-{RequestTypeName}-{RequestMethodName}-{RequestParameterCount}-{IsSuccess}-{TotalMillisecond}-{RequestEndTime.ToString("yyyyMMddHH:mm:sss")}";
        }
    }
}