using System;
using System.Reflection;

namespace NRpc.Server
{
    public interface IServerFilter
    {
        void OnActionExecuting(MethodInfo methodInfo, object[] param);

        void OnActionExecuted(MethodInfo methodInfo);

        void HandleException(MethodInfo methodInfo, Exception ex);
    }
}