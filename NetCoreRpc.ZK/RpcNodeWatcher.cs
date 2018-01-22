using org.apache.zookeeper;
using System;
using System.Threading.Tasks;

namespace NetCoreRpc.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcNodeWatcher.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 9:53:22
    /// </summary>
    public sealed class RpcNodeWatcher : Watcher
    {
        public async override Task process(WatchedEvent @event)
        {
            Console.WriteLine($"{@event.getPath()},{@event.getState()},{@event.get_Type()}");
            await Task.Delay(1);
        }
    }
}