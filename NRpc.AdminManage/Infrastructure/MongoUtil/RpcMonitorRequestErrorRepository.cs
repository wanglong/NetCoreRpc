using MongoDB.Driver;
using NRpc.AdminManage.Dtos;
using NRpc.AdminManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NRpc.AdminManage.Infrastructure.MongoUtil
{
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

        public IEnumerable<RpcMonitorRequestErrorDto> GetErrorList(DateTime startTime, DateTime endTime, int pageIndex, int pageSize, out int TotalCount)
        {
            var skipIndex = (pageIndex - 1) * pageSize;
            pageSize = pageSize < 0 ? 30 : (pageSize > 1000 ? 1000 : pageSize);
            Expression<Func<RpcMonitorRequestErrorModel, RpcMonitorRequestErrorDto>> projectionExpression = m => new RpcMonitorRequestErrorDto
            {
                ErrorMsg = m.ErrorMsg,
                RequestID = m.RequestID,
                RequestMethodName = m.RequestMethodName,
                RequestParameterCount = m.RequestParameterCount,
                RequestTime = m.RequestTime,
                RequestTypeName = m.RequestTypeName
            };
            TotalCount = (int)Collection.Count(m => m.RequestTime >= startTime && m.RequestTime <= endTime);
            return Collection.Find(m => m.RequestTime >= startTime && m.RequestTime <= endTime)
                             .Project(projectionExpression.ToProjection()).Skip(skipIndex < 0 ? 0 : skipIndex).Limit(pageSize).ToEnumerable();
        }
    }
}