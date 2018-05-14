using MongoDB.Driver;
using NRpc.MongoDB.Models;
using NRpc.RpcMonitor;
using NRpc.Utils;
using System.Collections.Generic;

namespace NRpc.MongoDB.RpcMonitor
{
    /// <summary>
    /// 类名：RpcMonitorRequestErrorRepository.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:53:37
    /// </summary>
    public class RpcMonitorRequestErrorRepository : MongoBaseRepository<RpcMonitorRequestErrorModel>
    {
        public RpcMonitorRequestErrorRepository() : base(new MongoDatabaseProvider(), MonogoDbConfig.GetConfig())
        {
        }

        protected override IMongoCollection<RpcMonitorRequestErrorModel> Collection
        {
            get
            {
                return Database.GetCollection<RpcMonitorRequestErrorModel>("RpcMonitor_RequestError");
            }
        }

        public void AddOneRequestInfo(RpcMonitorRequestErrorInfo rpcMonitorRequestInfo)
        {
            if (rpcMonitorRequestInfo != null)
            {
                _RpcMonitorRequestMessageQueue.EnqueueMessage(RpcMonitorRequestErrorModel.Create(rpcMonitorRequestInfo));
            }
        }

        #region 利用双缓冲队列进行插入

        private static DoubleBufferQueue<RpcMonitorRequestErrorModel> _RpcMonitorRequestMessageQueue = new DoubleBufferQueue<RpcMonitorRequestErrorModel>(20000, MessageHandle, HaveNoCountHandle);

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<RpcMonitorRequestErrorModel> _RpcMonitorRequestMessageList = new List<RpcMonitorRequestErrorModel>();

        /// <summary>
        /// 信息处理的方法
        /// </summary>
        /// <param name="message">要处理的信息</param>
        private static void MessageHandle(RpcMonitorRequestErrorModel message)
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

        private static void AddRpcMonitorRequestList(List<RpcMonitorRequestErrorModel> rpcMonitorRequestInfos)
        {
            var rpcMonitorRequestErrorRepository = new RpcMonitorRequestErrorRepository();
            rpcMonitorRequestErrorRepository.InsertMany(rpcMonitorRequestInfos);
        }

        #endregion 利用双缓冲队列进行插入
    }
}