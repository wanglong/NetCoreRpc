using System;

namespace NRpc.Scheduling
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：IScheduleService.cs
    /// 类属性：接口
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:01:59
    /// </summary>
    internal interface IScheduleService
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="name">任务名字</param>
        /// <param name="action">执行方法</param>
        /// <param name="dueTime">延迟时间</param>
        /// <param name="period"></param>
        void StartTask(string name, Action action, int dueTime, int period);

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="name">任务名字</param>
        void StopTask(string name);
    }
}