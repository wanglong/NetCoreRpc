using NetCoreRpc.Utils;
using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreRpc.ServerRoute.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ZkCoordinator.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 19:14:51
    /// </summary>
    public sealed class ZkCoordinator : IRouteCoordinator
    {
        private readonly ZkManager _zkManager;
        private readonly RouteCoordinatorOption _routeCoordinatorOption;
        private List<string> _availableServerList = new List<string>();
        private bool _isHaveWatcher = false;
        private int _currentRequestCount = 0;

        public ZkCoordinator(RouteCoordinatorOption routeCoordinatorOption)
        {
            _routeCoordinatorOption = routeCoordinatorOption;
            _zkManager = new ZkManager(routeCoordinatorOption, NodeChangedAsync);
        }

        public Task RegisterAsync(IPEndPoint iPEndPoint)
        {
            return Retry(async () =>
            {
                return await _zkManager.CreateAsync($"{_routeCoordinatorOption.ParentName}/{iPEndPoint.Address.ToString()}:{iPEndPoint.Port.ToString()}", null, CreateMode.EPHEMERAL);
            }, retryCount: _routeCoordinatorOption.RetryCount, everyTryInterval: _routeCoordinatorOption.OperatingTimeout);
        }

        private async Task NodeChangedAsync(WatchedEvent @event, ZkManager zk)
        {
            LogUtil.InfoFormat("NodeChange:{0},{1},{2}", @event.getPath(), @event.getState(), @event.get_Type());
            if (@event.getState() == Watcher.Event.KeeperState.SyncConnected)
            {
                await UpdateChildrenAsync(zk);
            }
            else if (@event.getState() == Watcher.Event.KeeperState.Disconnected || @event.getState() == Watcher.Event.KeeperState.Expired)
            {
                await Retry(async () =>
                 {
                     //重新连接
                     await zk.ReConnectAsync();
                 }, retryCount: _routeCoordinatorOption.RetryCount, everyTryInterval: _routeCoordinatorOption.OperatingTimeout);
            }
            else if (@event.get_Type() == Watcher.Event.EventType.NodeChildrenChanged)
            {
                await UpdateChildrenAsync(zk);
            }
            else if (@event.get_Type() == Watcher.Event.EventType.NodeCreated || @event.get_Type() == Watcher.Event.EventType.NodeDeleted)
            {
                await UpdateChildrenAsync(zk);
            }
        }

        private async Task UpdateChildrenAsync(ZkManager zk)
        {
            var childrenList = await zk.GetChlidrenListAsync(_routeCoordinatorOption.ParentName);
            lock (this)
            {
                _availableServerList = childrenList;
            }
            LogUtil.Info($"最新全部连接【{string.Join(",", _availableServerList)}】");
        }

        public async Task<T> Retry<T>(Func<Task<T>> callable, int retryCount = 10, int everyTryInterval = 1000, [CallerMemberName]string memberName = null)
        {
            int currentExcuteCount = 0;
            while (currentExcuteCount <= retryCount)
            {
                try
                {
                    return await callable();
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex, memberName);
                    if (currentExcuteCount == retryCount)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(everyTryInterval);
                    }
                }
                currentExcuteCount++;
            }
            throw new Exception("执行错误");
        }

        public async Task Retry(Func<Task> callable, int retryCount = 10, int everyTryInterval = 1000, [CallerMemberName]string memberName = null)
        {
            int currentExcuteCount = 0;
            while (currentExcuteCount <= retryCount)
            {
                try
                {
                    await callable();
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex, memberName);
                    if (currentExcuteCount == retryCount)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(everyTryInterval);
                    }
                }
                currentExcuteCount++;
            }
        }

        public async Task<string> GetAvailableServerListAsync(List<string> currentServerList)
        {
            if (!_isHaveWatcher)
            {
                await UpdateChildrenAsync(_zkManager);
                _isHaveWatcher = true;
            }
            if (!currentServerList.Any())
            {
                return string.Empty;
            }
            var availableList = currentServerList.Where((m => _availableServerList.Contains(m)));
            if (!availableList.Any())
            {
                return string.Empty;
            }
            var requestCount = Interlocked.Increment(ref _currentRequestCount);
            return availableList.ToList()[requestCount % availableList.Count()];
        }

        public Task DeleteAsync(IPEndPoint iPEndPoint)
        {
            return _zkManager.DeleteAsync($"{_routeCoordinatorOption.ParentName}/{iPEndPoint.Address.ToString()}:{iPEndPoint.Port.ToString()}");
        }

        public Task CloseAsync()
        {
            return _zkManager.CloseAsync();
        }
    }
}