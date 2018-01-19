using System;

namespace NetCoreRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ExceptionUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:25:54
    /// </summary>
    internal static class ExceptionUtil
    {
        public static void Eat(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
        }

        public static T Eat<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return defaultValue;
            }
        }
    }
}