namespace NRpc.Transport.Remoting
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RemotingState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：返回状态码
    /// 创建标识：yjq 2017/11/28 14:57:26
    /// </summary>
    public class ResponseState
    {
        public static short Success = 100;

        public static short NotFound = 404;

        public static short Error = 500;
    }
}