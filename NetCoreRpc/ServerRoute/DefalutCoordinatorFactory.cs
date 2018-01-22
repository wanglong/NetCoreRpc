namespace NetCoreRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DefalutCoordinatorFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 20:00:33
    /// </summary>
    public sealed class DefalutCoordinatorFactory : ICoordinatorFactory
    {
        public IRouteCoordinator Create()
        {
            return new DefaultCoordinator();
        }
    }
}