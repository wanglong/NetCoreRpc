using MongoDB.Bson;
using MongoDB.Driver;
using NRpc.AdminManage.Dtos;
using NRpc.AdminManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NRpc.AdminManage.Infrastructure.MongoUtil
{
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

        public RpcMonitorRequestStatisticTotalDto StatisticsExcute(DateTime startTime, DateTime endTime)
        {
            var timeArray = CalculateTimeArray(startTime, endTime).ToList();
            var monitorRequestList = GetExcuteCount(startTime, endTime);
            var excuteCountStatisticDto = new RpcMonitorRequestStatisticDto<int>("执行数量", timeArray.Select(m => m.ToString("HH:mm")), 1);
            var excuteErrorCountStatisticDto = new RpcMonitorRequestStatisticDto<int>("错误数量", timeArray.Select(m => m.ToString("HH:mm")), 2);
            var excuteAvgSpendStatisticDto = new RpcMonitorRequestStatisticDto<double>("平均耗时", timeArray.Select(m => m.ToString("HH:mm")), 3);
            var excuteMaxSpendStatisticDto = new RpcMonitorRequestStatisticDto<double>("最大耗时", timeArray.Select(m => m.ToString("HH:mm")), 4);
            var excuteMinSpendStatisticDto = new RpcMonitorRequestStatisticDto<double>("最小耗时", timeArray.Select(m => m.ToString("HH:mm")), 5);
            for (int i = 0, count = timeArray.Count(); i < count; i++)
            {
                if (i == count - 1)
                {
                    excuteCountStatisticDto.ValueList.Add(monitorRequestList?.Where(m => m.ExcutedMinute >= timeArray[i])?.Sum(m => m?.TotalCount) ?? 0);
                    excuteErrorCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.IsSuccess == false)?.Sum(m => m?.TotalCount) ?? 0);
                    excuteAvgSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Average(m => m?.AvgExcutedMillisecond) ?? 0);
                    excuteMaxSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Max(m => m?.MaxExcutedMillisecond) ?? 0);
                    excuteMinSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i])?.Min(m => m?.MinExcutedMillisecond) ?? 0);
                }
                else
                {
                    excuteCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Sum(m => m?.TotalCount) ?? 0);
                    excuteErrorCountStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1] && m.IsSuccess == false)?.Sum(m => m?.TotalCount) ?? 0);
                    excuteAvgSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Average(m => m?.AvgExcutedMillisecond) ?? 0);
                    excuteMaxSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Max(m => m?.MaxExcutedMillisecond) ?? 0);
                    excuteMinSpendStatisticDto.ValueList.Add(monitorRequestList.Where(m => m.ExcutedMinute >= timeArray[i] && m.ExcutedMinute < timeArray[i + 1])?.Min(m => m?.MinExcutedMillisecond) ?? 0);
                }
            }
            var totalSeconds = (startTime - endTime).TotalSeconds;
            return new RpcMonitorRequestStatisticTotalDto()
            {
                TotalErrorCount = monitorRequestList.Where(m => m.IsSuccess == false).Sum(m => m?.TotalCount),
                TotalExcuteCount = monitorRequestList.Sum(m => m?.TotalCount),
                AvgExcutedMillisecond = monitorRequestList.Average(m => m?.AvgExcutedMillisecond),
                MaxExcutedMillisecond = monitorRequestList.Max(m => m?.MaxExcutedMillisecond),
                MinExcutedMillisecond = monitorRequestList.Min(m => m?.MinExcutedMillisecond),
                EverySecondExcuteCount = (int)(monitorRequestList.Sum(m => m.TotalCount) / totalSeconds <= 0 ? 1 : totalSeconds),
                StatisticList = new List<RpcMonitorRequestStatisticDto> {
                    excuteCountStatisticDto,excuteErrorCountStatisticDto,excuteAvgSpendStatisticDto,excuteMaxSpendStatisticDto,excuteMinSpendStatisticDto
                }
            };
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
    }
}