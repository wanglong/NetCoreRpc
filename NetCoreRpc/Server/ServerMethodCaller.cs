using NetCoreRpc.Serializing;
using NetCoreRpc.Transport.Remoting;
using NetCoreRpc.Utils;
using NetCoreRpc;
using System;
using System.Reflection;
using System.Threading.Tasks;
using NRpc;
using System.Collections.Concurrent;
using NetCoreRpc.Extensions;

namespace NetCoreRpc.Server
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ServerMethodCaller.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 14:22:31
    /// </summary>
    public class ServerMethodCaller
    {
        /// <summary>
        /// 异步方法处理
        /// </summary>
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(ServerMethodCaller).GetMethod("ExecuteAsyncResultAction", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly IServerFilter _serverFilter;

        public ServerMethodCaller(IServerFilter serverFilter)
        {
            _serverFilter = serverFilter;
        }

        private void OnActionExecuting(MethodInfo methodInfo, object[] param)
        {
            try
            {
                GetFilter(methodInfo)?.OnActionExecuting(methodInfo, param);
            }
            catch { }
        }

        private void OnActionExecuted(MethodInfo methodInfo)
        {
            try
            {
                GetFilter(methodInfo)?.OnActionExecuted(methodInfo);
            }
            catch { }
        }

        private void HandleException(MethodInfo methodInfo, Exception ex)
        {
            try
            {
                var filter = GetFilter(methodInfo);
                if (filter != null)
                {
                    filter.HandleException(methodInfo, ex);
                }
            }
            catch { }
        }

        /// <summary>
        /// 请求处理
        /// </summary>
        /// <param name="remotingRequest"></param>
        /// <returns></returns>
        public RemotingResponse HandleRequest(RemotingRequest remotingRequest)
        {
            var requestMethodInfo = DependencyManage.Resolve<IMethodCallSerializer>().Deserialize<RpcMethodCallInfo>(remotingRequest.Body);
            var classType = ServerAssemblyUtil.GetType(requestMethodInfo.TypeName);
            using (var scope = DependencyManage.BeginScope())
            {
                var obj = scope.ServiceProvider.GetService(classType);
                try
                {
                    LogUtil.InfoFormat("Begin Deal Rpc Request:{0}-{1}", requestMethodInfo.TypeName, requestMethodInfo.MethodName);
                    var executeMethodInfo = ServerAssemblyUtil.GetMethod(requestMethodInfo.MethodName, classType) as MethodInfo;
                    if (executeMethodInfo == null)
                    {
                        LogUtil.Info($"Not found Method{requestMethodInfo.TypeName}-{requestMethodInfo.MethodName}");
                        return remotingRequest.CreateNotFoundResponse($"{requestMethodInfo.TypeName},{requestMethodInfo.MethodName}");
                    }
                    else
                    {
                        var delegateType = executeMethodInfo.GetMethodReturnType();
                        var executeResult = executeMethodInfo.Invoke(obj, requestMethodInfo.Parameters);
                        RemotingResponse remotingResponse;
                        if (delegateType == MethodType.SyncAction)
                        {
                            remotingResponse = remotingRequest.CreateSuccessResponse(ResponseUtil.NoneBodyResponse);
                        }
                        else if (delegateType == MethodType.SyncFunction)
                        {
                            remotingResponse = remotingRequest.CreateSuccessResponse(GetBody(executeResult, executeMethodInfo));
                        }
                        else if (delegateType == MethodType.AsyncAction)
                        {
                            var task = (Task)executeResult;
                            task.Wait();
                            remotingResponse = remotingRequest.CreateSuccessResponse(ResponseUtil.NoneBodyResponse);
                        }
                        else
                        {
                            var resultType = executeMethodInfo.ReturnType.GetGenericArguments()[0];
                            var mi = HandleAsyncMethodInfo.MakeGenericMethod(resultType);
                            var result = mi.Invoke(this, new[] { executeResult });
                            remotingResponse = remotingRequest.CreateSuccessResponse(GetBody(result, executeMethodInfo));
                        }
                        LogUtil.InfoFormat("Rpc Method Dealed:{0}-{1}", requestMethodInfo.TypeName, requestMethodInfo.MethodName);
                        return remotingResponse;
                    }
                }
                catch (Exception e)
                {
                    LogUtil.Error($"excute{requestMethodInfo.TypeName}-{requestMethodInfo.MethodName} failed", e);
                    return remotingRequest.CreateDealErrorResponse();
                }
            }
        }

        /// <summary>
        /// 执行异步有返回值的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        private T ExecuteAsyncResultAction<T>(Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        private byte[] GetBody(object obj, MethodInfo methodInfo)
        {
            return DependencyManage.Resolve<IResponseSerailizer>().Serialize(obj, methodInfo);
        }
        private static readonly ConcurrentDictionary<RuntimeMethodHandle, IServerFilter> _FilterDic = new ConcurrentDictionary<RuntimeMethodHandle, IServerFilter>();

        private IServerFilter GetFilter(MethodInfo methodInfo)
        {
            return _FilterDic.GetValue(methodInfo.MethodHandle, () =>
            {
                var attributes = methodInfo.GetCustomAttributes();
                if (attributes != null)
                {
                    foreach (Attribute item in attributes)
                    {
                        var attribute = item as IServerFilter;
                        if (attribute != null)
                        {
                            return attribute;
                        }
                    }
                }
                var classAttribute = methodInfo.DeclaringType.GetCustomAttributes();
                if (classAttribute != null)
                {
                    foreach (Attribute item in classAttribute)
                    {
                        var attribute = item as IServerFilter;
                        if (attribute != null)
                        {
                            return attribute;
                        }
                    }
                }
                return _serverFilter;
            });
        }
    }
}