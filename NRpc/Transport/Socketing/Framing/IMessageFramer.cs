using System;
using System.Collections.Generic;

namespace NRpc.Transport.Socketing.Framing
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IMessageFramer.cs
    /// 接口属性：公共
    /// 类功能描述：IMessageFramer接口
    /// 创建标识：yjq 2017/11/24 16:28:37
    /// </summary>
    public interface IMessageFramer
    {
        /// <summary>
        /// 将消息内容进行封包
        /// </summary>
        /// <param name="data"></param>
        void Package(IEnumerable<ArraySegment<byte>> data);

        /// <summary>
        /// 将消息内容进行封包
        /// </summary>
        /// <param name="data"></param>
        void Package(ArraySegment<byte> data);

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<ArraySegment<byte>> FrameData(ArraySegment<byte> data);

        /// <summary>
        /// 注册消息解析完成时执行的方法
        /// </summary>
        /// <param name="handler"></param>
        void RegisterMessageArrivedCallback(Action<ArraySegment<byte>> handler);
    }
}