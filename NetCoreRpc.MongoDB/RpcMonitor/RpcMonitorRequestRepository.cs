using MongoDB.Bson;
using MongoDB.Driver;
using NetCoreRpc.MongoDB.Dtos;
using NetCoreRpc.MongoDB.Models;
using NetCoreRpc.RpcMonitor;
using NetCoreRpc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreRpc.MongoDB.RpcMonitor
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

        public IEnumerable<RpcMonitorRequestStatisticDto> StatisticsExcute(DateTime startTime, DateTime endTime)
        {
            var timeArray = CalculateTimeArray(startTime, endTime).ToList();
            var monitorRequestList = GetExcuteCount(startTime, endTime);
            var excuteCountStatisticDto = new RpcMonitorRequestStatisticDto<int?>("执行数量", timeArray.Select(m => m.ToString("HH:mm")), 1);
            var excuteErrorCountStatisticDto = new RpcMonitorRequestStatisticDto<int?>("错误数量", timeArray.Select(m => m.ToString("HH:mm")), 2);
            var excuteAvgSpendStatisticDto = new RpcMonitorRequestStatisticDto<double?>("平均耗时", timeArray.Select(m => m.ToString("HH:mm")), 3);
            var excuteMaxSpendStatisticDto = new RpcMonitorRequestStatisticDto<double?>("最大耗时", timeArray.Select(m => m.ToString("HH:mm")), 4);
            var excuteMinSpendStatisticDto = new RpcMonitorRequestStatisticDto<double?>("最小耗时", timeArray.Select(m => m.ToString("HH:mm")), 5);
            for (int i = 0, count = timeArray.Count(); i < count; i++)
            {
                if (i == count - 1)
                {
                    excuteCountStatisticDto.ValueList.Add(monitorRequestList?.Where(m => m.ExcutedMinute >= timeArray[i])?.Sum(m => m?.TotalCount));
                    excuteErrorCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.IsSuccess == false)?.Sum(m => m?.TotalCount));
                    excuteAvgSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Average(m => m?.AvgExcutedMillisecond));
                    excuteMaxSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Max(m => m?.MaxExcutedMillisecond));
                    excuteMinSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Min(m => m?.MinExcutedMillisecond));
                }
                else
                {
                    excuteCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Sum(m => m?.TotalCount));
                    excuteErrorCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1] && m.IsSuccess == false)?.Sum(m => m?.TotalCount));
                    excuteAvgSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Average(m => m?.AvgExcutedMillisecond));
                    excuteMaxSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Max(m => m?.MaxExcutedMillisecond));
                    excuteMinSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Min(m => m?.MinExcutedMillisecond));
                }
            }
            yield return excuteCountStatisticDto;
            yield return excuteErrorCountStatisticDto;
            yield return excuteAvgSpendStatisticDto;
            yield return excuteMaxSpendStatisticDto;
            yield return excuteMinSpendStatisticDto;
        }

        private List<RpcMonitorRequestDto> GetExcuteCount(DateTime startTime, DateTime endTime)
        {
            #region group

            var group = new BsonDocument
            {
                { "_id", new BsonDocument
                    {
                       { "RequestTime",new BsonDocument{
                           {
                               "$dateToString",new BsonDocument{
                                   { "format","%Y-%m-%d %H:%M"},
                                   {"date", "$RequestStartTime" }
                               }
                           }
                       } },
                       { "Day", new BsonDocument("$dayOfMonth", "$RequestStartTime") },
                       { "Year", new BsonDocument("$year", "$RequestStartTime") },
                       { "RequestMethodName", "$RequestMethodName" },
                       { "RequestTypeName", "$RequestTypeName" },
                       { "IsSuccess", "$IsSuccess" },
                    }
                },
                { "Count", new BsonDocument("$sum", 1) },
                { "MaxExcutedMillisecond", new BsonDocument("$max", "$TotalMillisecond") },
                { "MinExcutedMillisecond", new BsonDocument("$min", "$TotalMillisecond") },
                { "AvgExcutedMillisecond", new BsonDocument("$avg", "$TotalMillisecond") }
            };

            #endregion group

            #region project

            var project = new BsonDocument
            {
                {
                    "_id",0
                },
                {
                    "ExcutedTime","$_id.RequestTime"
                },
                {
                    "RequestMethodName","$_id.RequestMethodName"
                },
                {
                    "RequestTypeName","$_id.RequestTypeName"
                },
                {
                    "IsSuccess","$_id.IsSuccess"
                },
                {
                    "TotalCount","$Count"
                },
                {
                    "MaxExcutedMillisecond","$MaxExcutedMillisecond"
                },
                {
                    "MinExcutedMillisecond","$MinExcutedMillisecond"
                },
                {
                    "AvgExcutedMillisecond","$AvgExcutedMillisecond"
                }
            };

            #endregion project

            return Collection.Aggregate()
                       .Match(m => m.RequestStartTime >= startTime && m.RequestStartTime <= endTime)
                       .Group(group).Project<RpcMonitorRequestDto>(project).ToList();
        }

        private IEnumerable<DateTime> CalculateTimeArray(DateTime startTime, DateTime endTime)
        {
            startTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd HH:mm"));
            endTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd HH:mm"));
            var totalMinuties = (endTime - startTime).TotalMinutes;
            if (totalMinuties <= 30)
            {
                for (DateTime i = startTime; i <= endTime; i = i.AddMinutes(1))
                {
                    yield return i;
                }
            }
            else
            {
                for (DateTime i = startTime; i <= endTime; i = i.AddMinutes((int)(totalMinuties / 30)))
                {
                    yield return i;
                }
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