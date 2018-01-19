using NetCoreRpc.Utils;
using System;
using System.Reflection;

namespace NetCoreRpc.Client
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RpcProxyImpl.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/18 14:04:26
    /// </summary>
    public class RpcProxyImpl<TProxy> : DispatchProxy
    {
        private Type _proxyType;
        private MethodInfo[] _methods;
        private readonly ClientMethodCaller _clientMethodCaller;

        public RpcProxyImpl()
        {
            _proxyType = typeof(TProxy);
            _methods = _proxyType.GetMethods();
            _clientMethodCaller = new ClientMethodCaller(_proxyType);
        }

        public string TypeName
        {
            get { return _proxyType.Name; }
            set { }
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Type[] argumentTypes = MethodUtil.GetArgTypes(args);
            object objReturnValue = null;
            if (targetMethod != null)
            {
                if (targetMethod.Name.Equals("Equals") && argumentTypes != null && argumentTypes.Length == 1 && argumentTypes[0].IsAssignableFrom((typeof(Object))))
                {
                    objReturnValue = false;
                }
                else if (targetMethod.Name.Equals("GetHashCode") && argumentTypes.Length == 0)
                {
                    objReturnValue = _proxyType.GetHashCode();
                }
                else if (targetMethod.Name.Equals("ToString") && argumentTypes.Length == 0)
                {
                    objReturnValue = "[Proxy " + _proxyType.Name + "]";
                }
                else if (targetMethod.Name.Equals("GetType") && argumentTypes.Length == 0)
                {
                    objReturnValue = _proxyType;
                }
                else
                {
                    objReturnValue = DoMethodCall(args, argumentTypes, targetMethod);
                }
            }
            else
            {
                if (targetMethod.Name.Equals("GetType") && (args.Length == 0))
                {
                    objReturnValue = _proxyType;
                }
            }
            return objReturnValue;
        }

        /// <summary>
        /// 判断方法名字是不是唯一的
        /// </summary>
        /// <param name="name">方法名字</param>
        /// <returns></returns>
        private bool IsMethodNameUnique(string name)
        {
            int count = 0;
            foreach (MethodInfo mi in _methods)
                if (mi.Name.Equals(name))
                    count++;
            return count < 2;
        }

        /// <summary>
        /// 开启远程调用
        /// </summary>
        /// <param name="arrMethodArgs">方法参数</param>
        /// <param name="methodInfo">方法名字</param>
        /// <returns></returns>
        private object DoMethodCall(object[] arrMethodArgs, Type[] argmentTypes, MethodInfo methodInfo)
        {
            return _clientMethodCaller.DoMethodCall(arrMethodArgs, argmentTypes, methodInfo);
        }
    }
}