using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：IRouteCoordinator.cs
    /// 类属性：接口
    /// 类功能描述：远程服务协调器
    /// 创建标识：yjq 2018/1/22 19:05:46
    /// </summary>
    public interface IRouteCoordinator
    {
        Task RegisterAsync(IPEndPoint iPEndPoint);

        Task<string> GetAvailableServerListAsync(List<string> currentServerList);

        Task DeleteAsync(IPEndPoint iPEndPoint);

        Task CloseAsync();
    }
}