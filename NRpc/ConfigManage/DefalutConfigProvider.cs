namespace NRpc.ConfigManage
{
    /// <summary>
    /// 类名：DefalutConfigProvider.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 16:53:17
    /// </summary>
    public class DefalutConfigProvider : IConfigProvider
    {
        public NRpcConfig GetConfig()
        {
            return NRpcConfigWatcher.CurrentConfig;
        }
    }
}