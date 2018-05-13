using MongoDB.Driver;
using NRpc.RpcMonitor;
using NRpc.Utils;
using System.Collections.Generic;

namespace NRpc.MongoDB.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRequestRepository.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:22:42
    /// </summary>
    public class RpcMonitorRequestRepository : MongoBaseRepository<RpcMonitorRequestInfo>
    {
        public RpcMonitorRequestRepository() : base(new MongoDatabaseProvider(), MonogoDbConfig.GetConfig())
        {
        }

        protected override IMongoCollection<RpcMonitorRequestInfo> Collection
        {
            get
            {
                return Database.GetCollection<RpcMonitorRequestInfo>("RpcMonitor_Request");
            }
        }

        public void AddOneRequestInfo(RpcMonitorRequestInfo rpcMonitorRequestInfo)
        {
            if (rpcMonitorRequestInfo != null)
            {
                _RpcMonitorRequestMessageQueue.EnqueueMessage(rpcMonitorRequestInfo);
            }
        }

        #region 利用双缓冲队列进行插入

        private static DoubleBufferQueue<RpcMonitorRequestInfo> _RpcMonitorRequestMessageQueue = new DoubleBufferQueue<RpcMonitorRequestInfo>(20000, MessageHandle, HaveNoCountHandle);

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<RpcMonitorRequestInfo> _RpcMonitorRequestMessageList = new List<RpcMonitorRequestInfo>();

        /// <summary>
        /// 信息处理的方法
        /// </summary>
        /// <param name="message">要处理的信息</param>
        private static void MessageHandle(RpcMonitorRequestInfo message)
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

        private static void AddRpcMonitorRequestList(List<RpcMonitorRequestInfo> rpcMonitorRequestInfos)
        {
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            rpcMonitorRequestRepository.InsertMany(rpcMonitorRequestInfos);
        }

        #endregion 利用双缓冲队列进行插入
    }
}