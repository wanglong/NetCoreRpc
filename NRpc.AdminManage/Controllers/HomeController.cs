using Microsoft.AspNetCore.Mvc;
using NRpc.AdminManage.Infrastructure.MongoUtil;
using System;

namespace NRpc.AdminManage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.TimeType = 30;
            return View();
        }

        /// 1 指定时间段内 执行的数量统计、错误数量、平均消耗时间、最大消耗时间 最终输出30个统计点(最小单位是分)  执行的方法以及方法次数、最大消耗时间、最小消耗时间、平均消耗时间
        /// 2 错误列表
        ///

        [HttpGet]
        public IActionResult LoadExcuteCount(int? timeType, DateTime? startTime, DateTime? endTime)
        {
            if (timeType != null)
            {
                endTime = DateTime.Now;
                startTime = DateTime.Now.AddMinutes(-(timeType.Value > 120 ? 120 : timeType.Value));
            }
            else
            {
                if (startTime == null || endTime == null)
                {
                    endTime = DateTime.Now;
                    startTime = DateTime.Now.AddMinutes(-30);
                }
                else
                {
                    if ((endTime.Value - startTime.Value).TotalMinutes > 60 * 24)
                    {
                        startTime = endTime.Value.AddMinutes(-60 * 24);
                    }
                }
            }
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            var result = rpcMonitorRequestRepository.StatisticsExcute(startTime.Value, endTime.Value);
            return Json(result);
        }

        [HttpGet]
        public IActionResult LoadExcuteMethod(int? timeType, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize = 10)
        {
            if (timeType != null)
            {
                endTime = DateTime.Now;
                startTime = DateTime.Now.AddMinutes(-(timeType.Value > 120 ? 120 : timeType.Value));
            }
            else
            {
                if (startTime == null || endTime == null)
                {
                    endTime = DateTime.Now;
                    startTime = DateTime.Now.AddMinutes(-30);
                }
                else
                {
                    if ((endTime.Value - startTime.Value).TotalMinutes > 60 * 24)
                    {
                        startTime = endTime.Value.AddMinutes(-60 * 24);
                    }
                }
            }
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 0 ? 30 : (pageSize > 1000 ? 1000 : pageSize);
            var result = rpcMonitorRequestRepository.StatisticsExcuteMethod(startTime.Value, endTime.Value, pageIndex, pageSize, out int totalCount);
            return Json(new { PageInfo = CreatePageInfo(totalCount, pageIndex, pageSize), Data = result });
        }

        [HttpGet]
        public IActionResult LoadExcuteErrorMethod(int? timeType, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize = 10)
        {
            if (timeType != null)
            {
                endTime = DateTime.Now;
                startTime = DateTime.Now.AddMinutes(-(timeType.Value > 120 ? 120 : timeType.Value));
            }
            else
            {
                if (startTime == null || endTime == null)
                {
                    endTime = DateTime.Now;
                    startTime = DateTime.Now.AddMinutes(-30);
                }
                else
                {
                    if ((endTime.Value - startTime.Value).TotalMinutes > 60 * 24)
                    {
                        startTime = endTime.Value.AddMinutes(-60 * 24);
                    }
                }
            }
            var rpcMonitorRequestErrorRepository = new RpcMonitorRequestErrorRepository();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 0 ? 30 : (pageSize > 1000 ? 1000 : pageSize);
            var result = rpcMonitorRequestErrorRepository.GetErrorList(startTime.Value, endTime.Value, pageIndex, pageSize, out int totalCount);
            return Json(new { PageInfo = CreatePageInfo(totalCount, pageIndex, pageSize), Data = result });
        }

        private object CreatePageInfo(int totalCount, int currentPageIndex, int pageSize)
        {
            var totalPage = (totalCount + pageSize - 1) / pageSize;
            return new
            {
                TotalPage = totalPage,
                TotalCount = totalCount,
                CurrentPage = currentPageIndex,
                PageSize = pageSize,
                HaveNextPage = totalPage > currentPageIndex,
                HavePrePage = currentPageIndex > 1
            };
        }
    }
}