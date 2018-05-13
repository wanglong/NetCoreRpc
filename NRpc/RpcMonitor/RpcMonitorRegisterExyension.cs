namespace NRpc.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRegisterExyension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/11 16:24:33
    /// </summary>
    public static class RpcMonitorRegisterExyension
    {
        public static DependencyManage UseDefaultMonitor(this DependencyManage serviceCollection)
        {
            return serviceCollection.RegisterType<IRpcMonitor, DefaultRpcMonitor>();
        }
    }
}