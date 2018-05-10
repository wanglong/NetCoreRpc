using System;
using System.Reflection;

namespace NetCoreRpc.Server
{
    /// <summary>
    /// Copyright (C) 2018 yjq 版权所有。
    /// 类名：IServerFilter.cs
    /// 接口属性：接口
    /// 类功能描述：IServerFilter
    /// 创建标识：yjq 2018/2/3 23:18:09
    /// </summary>
    public interface IServerFilter
    {
        void OnActionExecuting(MethodInfo methodInfo, object[] param);

        void OnActionExecuted(MethodInfo methodInfo);

        void HandleException(MethodInfo methodInfo, Exception ex);
        bool Any();
    }
}