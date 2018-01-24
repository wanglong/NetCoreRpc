using org.apache.zookeeper;
using org.apache.zookeeper.data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NRpc.ServerRoute.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ZkManager.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Zookeeper有两种watches，一种是data watches，另一种是child watches。其中，getData()和exists()以及create()等会添加data watches，getChildren()会添加child watches。而delete()涉及到删除数据和子节点，会同时触发data watches和child watches。
    /// 创建标识：yjq 2018/1/22 19:07:09
    /// </summary>
    public class ZkManager
    {
        private readonly RouteCoordinatorOption _zkOption;
        private ZooKeeper _zooKeeper;
        private readonly Func<WatchedEvent, ZkManager, Task> _changeEvent;

        public ZkManager(RouteCoordinatorOption zkOption, Func<WatchedEvent, ZkManager, Task> changeEvent)
        {
            _zkOption = zkOption;
            _changeEvent = changeEvent;
            _zooKeeper = CreateZk();
        }

        public RouteCoordinatorOption Option
        {
            get
            {
                return _zkOption;
            }
        }

        private ZooKeeper CreateZk()
        {
            var zk = new ZooKeeper(Option.ConnectionString, Option.SessionTimeout, new ZkWatcher(_changeEvent, this), Option.SessionId, Option.SessionPasswd, Option.ReadOnly);
            int currentTryCount = 0;
            while (zk.getState() != ZooKeeper.States.CONNECTED && currentTryCount < Option.RetryCount)
            {
                Thread.Sleep(1000);
            }
            return zk;
        }

        private async Task<bool> ExistsAsync(string path)
        {
            var state = await _zooKeeper.existsAsync(path, watch: false);
            return state != null;
        }

        public Task<string> CreateAsync(string path, byte[] data)
        {
            return CreateAsync(path, data, ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);
        }

        public Task<string> CreateAsync(string path, byte[] data, CreateMode createMode)
        {
            return CreateAsync(path, data, ZooDefs.Ids.OPEN_ACL_UNSAFE, createMode);
        }

        public async Task<string> CreateAsync(string path, byte[] data, List<ACL> acl, CreateMode createMode)
        {
            string currentPath = string.Empty;
            var pathList = path.Split('/');
            for (int i = 0; i < pathList.Length; i++)
            {
                if (!currentPath.EndsWith("/"))
                {
                    currentPath += "/";
                }
                currentPath += pathList[i];
                if (i > 0 && i < pathList.Length)
                {
                    var isExists = await ExistsAsync(currentPath);
                    if (!isExists)
                    {
                        var createdPath = await _zooKeeper.createAsync(currentPath, null, acl, CreateMode.PERSISTENT);
                        if (!string.IsNullOrWhiteSpace(createdPath))
                        {
                        }
                    }
                }
            }
            var isExist = await ExistsAsync(path);
            if (!isExist)
            {
                return await _zooKeeper.createAsync(path, data, acl, createMode);
            }
            return path;
        }

        public async Task<bool> DeleteAsync(string sourcePath)
        {
            var exists = await ExistsAsync(sourcePath);
            if (exists)
            {
                await _zooKeeper.deleteAsync(sourcePath);
            }
            return true;
        }

        public async Task<List<string>> GetChlidrenListAsync(string sourcePath)
        {
            var isExists = await ExistsAsync(sourcePath);
            if (!isExists)
            {
                return new List<string>();
            }
            var childrenResult = await _zooKeeper.getChildrenAsync(sourcePath, watch: true);
            return childrenResult.Children;
        }

        public async Task<byte[]> GetDataAsync(string sourcePath)
        {
            var exists = await ExistsAsync(sourcePath);
            if (exists)
            {
                var dataResult = await _zooKeeper.getDataAsync(sourcePath, watch: true);
                return dataResult.Data;
            }
            return null;
        }

        public async Task ReConnectAsync()
        {
            if (!Monitor.TryEnter(this, Option.ConnectionTimeout))
                return;
            try
            {
                if (_zooKeeper != null)
                {
                    try
                    {
                        await CloseAsync();
                    }
                    catch
                    {
                        // ignored
                    }
                }
                _zooKeeper = CreateZk();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public Task CloseAsync()
        {
            return _zooKeeper?.closeAsync();
        }

        #region ZkWatcher

        public class ZkWatcher : Watcher
        {
            public readonly Func<WatchedEvent, ZkManager, Task> _changeEvent;
            private readonly ZkManager _zkManager;

            public ZkWatcher(Func<WatchedEvent, ZkManager, Task> changeEvent, ZkManager zkManager)
            {
                _changeEvent = changeEvent;
                _zkManager = zkManager;
            }

            public override Task process(WatchedEvent @event)
            {
                return _changeEvent?.Invoke(@event, _zkManager);
            }
        }

        #endregion ZkWatcher
    }
}