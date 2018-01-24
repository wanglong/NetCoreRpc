namespace NRpc.Scheduling
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ScheduleRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:03:41
    /// </summary>
    public static class ScheduleRegisterExtension
    {
        /// <summary>
        /// 使用默认的任务调度
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static DependencyManage UseDefaultSchedule(this DependencyManage dependencyManage)
        {
            return dependencyManage.RegisterType<IScheduleService, ScheduleService>();
        }
    }
}