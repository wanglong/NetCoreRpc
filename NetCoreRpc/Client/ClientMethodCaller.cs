using NetCoreRpc.Serializing;
using NetCoreRpc.Transport.Remoting;
using NetCoreRpc.Utils;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static NetCoreRpc.Client.ClientConfig;

namespace NetCoreRpc.Client
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ClientMethodCaller.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:59:47
    /// </summary>
    public class ClientMethodCaller
    {
        /// <summary>
        /// 异步方法处理
        /// </summary>
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(ClientMethodCaller).GetMethod("AsyncFunction", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly IMethodCallSerializer _methodCallSerializer;
        private readonly IResponseSerailizer _responseSerailizer;
        private readonly Type _proxyType;

        public ClientMethodCaller(Type proxyType)
        {
            _proxyType = proxyType;
            _methodCallSerializer = DependencyManage.Resolve<IMethodCallSerializer>();
            _responseSerailizer = DependencyManage.Resolve<IResponseSerailizer>();
        }

        /// <summary>
        /// 发送远程请求
        /// </summary>
        /// <param name="arrMethodArgs"></param>
        /// <param name="argmentTypes"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public object DoMethodCall(object[] arrMethodArgs, Type[] argmentTypes, MethodInfo methodInfo)
        {
            var client = RemotingClientFactory.GetClient(_proxyType);
            var requestInfo = Create(arrMethodArgs, argmentTypes, methodInfo);
            var response = client.InvokeSync(requestInfo, CurrentClientConfig.TimeouMillis);
            return HandleResponse(response, methodInfo);
        }

        private RemotingRequest Create(object[] arrMethodArgs, Type[] argmentTypes, MethodInfo methodInfo)
        {
            //var methodCallInfo = new RpcMethodCallInfo()
            //{
            //    TypeName = _proxyType.FullName,
            //    MethodName = methodInfo.Name,
            //    Parameters = arrMethodArgs
            //};
            var methodCallInfo = RpcMethodCallInfo.Create(arrMethodArgs, methodInfo, _proxyType.FullName);
            var body = _methodCallSerializer.Serialize(methodCallInfo);
            return new RemotingRequest(100, body);
        }

        /// <summary>
        /// 处理反馈结果
        /// </summary>
        /// <param name="remotingResponse"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public object HandleResponse(RemotingResponse remotingResponse, MethodInfo methodInfo)
        {
            if (remotingResponse.ResponseCode != ResponseState.Success)
            {
                throw new Exception(Encoding.UTF8.GetString(remotingResponse.ResponseBody));
            }
            var delegateType = methodInfo.GetMethodReturnType();
            if (delegateType == MethodType.SyncAction)
            {
                return null;
            }
            else if (delegateType == MethodType.SyncFunction)
            {
                return GetSyncFunctionResult(remotingResponse, methodInfo);
            }
            else if (delegateType == MethodType.AsyncAction)
            {
                return AsyncAction();
            }
            else
            {
                return GetAsyncFunctionResult(remotingResponse, methodInfo);
            }
        }

        /// <summary>
        /// 获取同步方法的返回值
        /// </summary>
        /// <param name="remotingResponse"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private object GetSyncFunctionResult(RemotingResponse remotingResponse, MethodInfo methodInfo)
        {
            if (remotingResponse.IsEmptyBody())
            {
                return null;
            }
            else
            {
                return _responseSerailizer.Deserialize(remotingResponse.ResponseBody, methodInfo.ReturnType, methodInfo);
            }
        }

        /// <summary>
        /// 获取异步方法的返回值
        /// </summary>
        /// <param name="remotingResponse"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private object GetAsyncFunctionResult(RemotingResponse remotingResponse, MethodInfo methodInfo)
        {
            var resultType = methodInfo.ReturnType.GetGenericArguments()[0];
            var mi = HandleAsyncMethodInfo.MakeGenericMethod(resultType);
            if (remotingResponse.IsEmptyBody())
            {
                return mi.Invoke(this, new[] { null as object });
            }
            var executeResult = _responseSerailizer.Deserialize(remotingResponse.ResponseBody, resultType, methodInfo);
            var result = mi.Invoke(this, new[] { executeResult });
            return result;
        }

        /// <summary>
        /// 异步无返回值方法
        /// </summary>
        /// <returns></returns>
        private Task AsyncAction()
        {
            return Task.Delay(1);
        }

        /// <summary>
        /// 异步有返回值方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private Task<T> AsyncFunction<T>(T value)
        {
            return Task.FromResult(value);
        }
    }
}