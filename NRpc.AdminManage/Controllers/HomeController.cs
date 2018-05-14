using Microsoft.AspNetCore.Mvc;
using NetCoreRpc.MongoDB.RpcMonitor;
using System;
using System.Linq;

namespace NRpc.AdminManage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// 1 指定时间段内 执行的数量统计、错误数量、平均消耗时间、最大消耗时间 最终输出30个统计点(最小单位是分)  执行的方法以及方法次数、最大消耗时间、最小消耗时间、平均消耗时间
        /// 2 错误列表
        ///

        [HttpGet]
        public IActionResult LoadExcuteCount()
        {
            var startTime = DateTime.Now.AddMinutes(-60);
            var endTime = DateTime.Now;
            var rpcMonitorRequestRepository = new RpcMonitorRequestRepository();
            var result = rpcMonitorRequestRepository.StatisticsExcute(startTime, endTime).ToList();
            return Json(result);
        }
    }
}