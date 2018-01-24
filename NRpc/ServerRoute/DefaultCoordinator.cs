using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：DefaultCoordinator.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 19:55:50
    /// </summary>
    public sealed class DefaultCoordinator : IRouteCoordinator
    {
        private int _currentRequestCount = 0;

        public Task CloseAsync()
        {
            return Task.Delay(1);
        }

        public Task DeleteAsync(IPEndPoint iPEndPoint)
        {
            return Task.Delay(1);
        }

        public Task<string> GetAvailableServerListAsync(List<string> currentServerList)
        {
            if (!currentServerList.Any())
            {
                return Task.FromResult("");
            }
            var requestCount = Interlocked.Increment(ref _currentRequestCount);
            return Task.FromResult(currentServerList[requestCount % currentServerList.Count]);
        }

        public Task RegisterAsync(IPEndPoint iPEndPoint)
        {
            return Task.Delay(1);
        }
    }
}