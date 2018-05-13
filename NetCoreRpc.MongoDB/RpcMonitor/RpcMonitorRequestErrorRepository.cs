using MongoDB.Driver;
using NetCoreRpc.RpcMonitor;
using NetCoreRpc.Utils;
using System.Collections.Generic;

namespace NetCoreRpc.MongoDB.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRequestErrorRepository.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:53:37
    /// </summary>
    public class RpcMonitorRequestErrorRepository : MongoBaseRepository<RpcMonitorRequestErrorInfo>
    {
        public RpcMonitorRequestErrorRepository() : base(new MongoDatabaseProvider(), MonogoDbConfig.GetConfig())
        {
        }

        protected override IMongoCollection<RpcMonitorRequestErrorInfo> Collection
        {
            get
            {
                return Database.GetCollection<RpcMonitorRequestErrorInfo>("RpcMonitor_RequestError");
            }
        }

        public void AddOneRequestInfo(RpcMonitorRequestErrorInfo rpcMonitorRequestInfo)
        {
            if (rpcMonitorRequestInfo != null)
            {
                _RpcMonitorRequestMessageQueue.EnqueueMessage(rpcMonitorRequestInfo);
            }
        }

        #region 利用双缓冲队列进行插入

        private static DoubleBufferQueue<RpcMonitorRequestErrorInfo> _RpcMonitorRequestMessageQueue = new DoubleBufferQueue<RpcMonitorRequestErrorInfo>(20000, MessageHandle, HaveNoCountHandle);

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<RpcMonitorRequestErrorInfo> _RpcMonitorRequestMessageList = new List<RpcMonitorRequestErrorInfo>();

        /// <summary>
        /// 信息处理的方法
        /// </summary>
        /// <param name="message">要处理的信息</param>
        private static void MessageHandle(RpcMonitorRequestErrorInfo message)
        {
            if (_RpcMonitorRequestMessageList.Count > 100)
            {
                AddRpcMonitorRequestList(_RpcMonitorRequestMessageList);
                _RpcMonitorRequestMessageList.Clear();
            }
            _RpcMonitorRequestMessageList.Add(message);
        }

        /// <summary>
        /// 没有新消息的时候处理方法
        /// </summary>
        private static void HaveNoCountHandle()
        {
            AddRpcMonitorRequestList(_RpcMonitorRequestMessageList);
            _RpcMonitorRequestMessageList.Clear();
        }

        private static void AddRpcMonitorRequestList(List<RpcMonitorRequestErrorInfo> rpcMonitorRequestInfos)
        {
            var rpcMonitorRequestErrorRepository = new RpcMonitorRequestErrorRepository();
            rpcMonitorRequestErrorRepository.InsertMany(rpcMonitorRequestInfos);
        }

        #endregion 利用双缓冲队列进行插入
    }
}