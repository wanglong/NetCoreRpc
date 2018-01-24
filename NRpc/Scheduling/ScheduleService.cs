using NRpc.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NRpc.Scheduling
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ScheduleService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:02:34
    /// </summary>
    internal class ScheduleService : IScheduleService
    {
        private readonly object _lockObject = new object();
        private readonly Dictionary<string, TimerBasedTask> _taskDict = new Dictionary<string, TimerBasedTask>();

        public void StartTask(string name, Action action, int dueTime, int period)
        {
            lock (_lockObject)
            {
                if (_taskDict.ContainsKey(name)) return;
                var timer = new Timer(TaskCallback, name, Timeout.Infinite, Timeout.Infinite);
                _taskDict.Add(name, new TimerBasedTask { Name = name, Action = action, Timer = timer, DueTime = dueTime, Period = period, Stopped = false });
                timer.Change(dueTime, period);
            }
        }

        public void StopTask(string name)
        {
            lock (_lockObject)
            {
                if (_taskDict.ContainsKey(name))
                {
                    var task = _taskDict[name];
                    task.Stopped = true;
                    task.Timer.Dispose();
                    _taskDict.Remove(name);
                }
            }
        }

        private void TaskCallback(object obj)
        {
            var taskName = (string)obj;
            TimerBasedTask task;

            if (_taskDict.TryGetValue(taskName, out task))
            {
                try
                {
                    if (!task.Stopped)
                    {
                        task.Timer.Change(Timeout.Infinite, Timeout.Infinite);
                        task.Action();
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    LogUtil.Error(string.Format("Task has exception, name: {0}, due: {1}, period: {2}", task.Name, task.DueTime, task.Period), ex);
                }
                finally
                {
                    try
                    {
                        if (!task.Stopped)
                        {
                            task.Timer.Change(task.Period, task.Period);
                        }
                    }
                    catch (ObjectDisposedException) { }
                    catch (Exception ex)
                    {
                        LogUtil.Error(string.Format("Timer change has exception, name: {0}, due: {1}, period: {2}", task.Name, task.DueTime, task.Period), ex);
                    }
                }
            }
        }

        private class TimerBasedTask
        {
            public string Name;
            public Action Action;
            public Timer Timer;
            public int DueTime;
            public int Period;
            public bool Stopped;
        }
    }
}