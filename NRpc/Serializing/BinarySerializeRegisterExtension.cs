namespace NRpc.Serializing
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BinarySerializeRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/27 17:18:27
    /// </summary>
    internal static class BinarySerializeRegisterExtension
    {
        public static DependencyManage UseDefaultSerializer(this DependencyManage containerManager)
        {
            return containerManager.RegisterType<IBinarySerializer, DefaultBinarySerializer>();
        }

        public static DependencyManage UseDefaultMethodSerializer(this DependencyManage containerManager)
        {
            return containerManager.RegisterType<IMethodCallSerializer, MethodCallSerializer>();
        }

        public static DependencyManage UseDefaultResponseSerializer(this DependencyManage containerManager)
        {
            return containerManager.RegisterType<IResponseSerailizer, ResponseSerializer>();
        }
    }
}