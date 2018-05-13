using NRpc.MongoDB.RpcMonitor;
using NRpc.RpcMonitor;
using System;

namespace NRpc.MongoDB
{
    /// <summary>
    /// 类名：MongoDbRegisterExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:57:07
    /// </summary>
    public static class MongoDbRegisterExtension
    {
        public static DependencyManage UseMongoDBMonitor(this DependencyManage dependencyManage, Func<MonogoDbConfig> func)
        {
            MonogoDbConfig.SetConfig(func);
            return dependencyManage.RegisterType<IRpcMonitor, MongoMonitor>();
        }
    }
}