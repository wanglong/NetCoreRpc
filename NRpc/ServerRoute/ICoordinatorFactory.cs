namespace NRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ICoordinatorFactory.cs
    /// 类属性：接口
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 19:54:52
    /// </summary>
    public interface ICoordinatorFactory
    {
        IRouteCoordinator Create();
    }
}