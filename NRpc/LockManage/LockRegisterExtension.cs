namespace NRpc.LockManage
{
    /// <summary>
    /// 类名：LockRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 15:58:40
    /// </summary>
    public static class LockRegisterExtension
    {
        public static DependencyManage UseDefaultLock(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<ILock, LocalLock>();
        }
    }
}