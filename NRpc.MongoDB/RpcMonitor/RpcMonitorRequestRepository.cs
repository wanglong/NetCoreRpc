using MongoDB.Driver;
using NRpc.MongoDB.Models;
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
    public class RpcMonitorRequestRepository : MongoBaseRepository<RpcMonitorRequestModel>
    {
        public RpcMonitorRequestRepository() : base(new MongoDatabaseProvider(), MonogoDbConfig.GetConfig())
        {
        }

        protected override IMongoCollection<RpcMonitorRequestModel> Collection
        {
            get
            {
                return Database.GetCollection<RpcMonitorRequestModel>("RpcMonitor_Request");
            }
        }

        public void AddOneRequestInfo(RpcMonitorRequestInfo rpcMonitorRequestInfo)
        {
            if (rpcMonitorRequestInfo != null)
            {
                _RpcMonitorRequestMessageQueue.EnqueueMessage(RpcMonitorRequestModel.Create(rpcMonitorRequestInfo));
            }
        }

        #region 利用双缓冲队列进行插入

        private static DoubleBufferQueue<RpcMonitorRequestModel> _RpcMonitorRequestMessageQueue = new DoubleBufferQueue<RpcMonitorRequestModel>(20000, MessageHandle, HaveNoCountHandle);

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<RpcMonitorRequestModel> _RpcMonitorRequestMessageList = new List<RpcMonitorRequestModel>();

        /// <summary>
        /// 信息处理的方法
        /// </summary>
        /// <param name="message">要处理的信息</param>
        private static void MessageHandle(RpcMonitorRequestModel message)
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

        private static void AddRpcMonitorRequestList(List<RpcMonitorRequestModel> rpcMonitorRequestInfos)
        {
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            rpcMonitorRequestRepository.InsertMany(rpcMonitorRequestInfos);
        }

        #endregion 利用双缓冲队列进行插入
    }
}