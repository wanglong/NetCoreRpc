namespace NRpc.ConfigManage
{
    /// <summary>
    /// 类名：ConfigProviderRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 16:56:06
    /// </summary>
    public static class ConfigProviderRegisterExtension
    {
        public static DependencyManage UseDefaultConfigProvider(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<IConfigProvider, DefalutConfigProvider>();
        }
    }
}