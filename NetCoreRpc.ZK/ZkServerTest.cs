using System;
using System.Threading.Tasks;

namespace NetCoreRpc.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ZkServerTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 15:41:27
    /// </summary>
    public sealed class ZkServerTest
    {
        public static async Task StartAsync()
        {
            ZkOption zkOption = new ZkOption
            {
                ConnectionString = "192.168.100.34:2181"
            };

            var zkManager = new ZkManager(zkOption, async (@event, zk) =>
             {
                 Console.WriteLine($"NodeChange:{@event.getPath()},{@event.getState()},{@event.get_Type()}");
                 if (@event.getState() == org.apache.zookeeper.Watcher.Event.KeeperState.SyncConnected)
                 {
                     await UpdateChildrenAsync(zk, @event.getPath());
                 }
                 else if (@event.getState() == org.apache.zookeeper.Watcher.Event.KeeperState.Disconnected || @event.getState() == org.apache.zookeeper.Watcher.Event.KeeperState.Expired)
                 {
                     //重新连接
                     await zk.ReConnectAsync();
                 }
                 else if (@event.get_Type() == org.apache.zookeeper.Watcher.Event.EventType.NodeChildrenChanged)
                 {
                     await UpdateChildrenAsync(zk, @event.getPath());
                 }
                 else if (@event.get_Type() == org.apache.zookeeper.Watcher.Event.EventType.NodeCreated || @event.get_Type() == org.apache.zookeeper.Watcher.Event.EventType.NodeDeleted)
                 {
                     await UpdateChildrenAsync(zk, @event.getPath());
                 }
             });
            //await zkManager.DeleteAsync("/NetCoreRpc");
            var childrenList = await zkManager.GetChlidrenListAsync("/NetCoreRpc/Test/ServerGroup");
            foreach (var children in childrenList)
            {
                Console.WriteLine(children + "--1");
            }
            await zkManager.DeleteAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.0");
            await zkManager.DeleteAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.1");
            await zkManager.DeleteAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.2");
            await zkManager.DeleteAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.3");
            //await zkManager.DeleteAsync("/NetCoreRpc/Test/ServerGroup");
            //await zkManager.DeleteAsync("/NetCoreRpc/Test");
            //await zkManager.DeleteAsync("/NetCoreRpc");
            //var childrenList2 = await zkManager.GetChlidrenListAsync("/NetCoreRpc/123");
            //foreach (var children in childrenList2)
            //{
            //    Console.WriteLine(children + "--2");
            //}
            //await zkManager.DeleteAsync("/NetCoreRpc");
            await zkManager.CreateAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.0", null);
            await zkManager.CreateAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.1", null);
            await zkManager.CreateAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.2", null);
            await zkManager.CreateAsync("/NetCoreRpc/Test/ServerGroup/192.168.0.3", null);
        }

        private static async Task UpdateChildrenAsync(ZkManager zk, string path)
        {
            Console.WriteLine(path);
            var childrenList = await zk.GetChlidrenListAsync("/NetCoreRpc/Test/ServerGroup");
            foreach (var children in childrenList)
            {
                Console.WriteLine(children + "--2");
            }
        }
    }
}