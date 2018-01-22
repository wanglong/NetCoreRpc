using org.apache.zookeeper;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreRpc.ZK
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ZkManager.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 9:59:13
    /// </summary>
    public sealed class ZkManager11
    {
        private ZooKeeper zk = null;

        public async Task Test()
        {
            try
            {
                zk = new ZooKeeper("192.168.100.34:2181", 3000, new RpcNodeWatcher());
                //Console.ReadLine();
                var existsState = await zk.existsAsync("/NetCoreRpc", watch: true);
                if (existsState == null)
                    await zk.createAsync("/NetCoreRpc", Encoding.UTF8.GetBytes("321"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                await zk.deleteAsync("/NetCoreRpc");
                //await zk.getDataAsync("/NetCoreRpc", watch: true);
                //await zk.createAsync("/NetCoreRpc/5896", Encoding.UTF8.GetBytes("5896"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");
                //await zk.createAsync("/NetCoreRpc/1478", Encoding.UTF8.GetBytes("5896"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");

                //var childrenResult = await zk.getChildrenAsync("/NetCoreRpc", watch: true);
                //foreach (var item in childrenResult.Children)
                //{
                //    Console.WriteLine(item);
                //    var dataResult = await zk.getDataAsync("/NetCoreRpc" + "/" + item, true);
                //}
                //await zk.deleteAsync("/NetCoreRpc/5896");
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");
                //await zk.createAsync("/NetCoreRpc/5896", Encoding.UTF8.GetBytes("5896"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");
                //await zk.deleteAsync("/NetCoreRpc/5896");
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");

                //await zk.deleteAsync("/NetCoreRpc/1478");
                //System.Threading.Thread.Sleep(5000);
                //Console.WriteLine("1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Task Close()
        {
            return zk?.closeAsync();
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
                catch
                {
                    //LogUtil.Error(ex, memberName);
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
    }
}