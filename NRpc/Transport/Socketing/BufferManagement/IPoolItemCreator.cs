using System.Collections.Generic;

namespace NRpc.Transport.Socketing.BufferManagement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IPoolItemCreator.cs
    /// 接口属性：接口
    /// 类功能描述：IPoolItemCreator
    /// 创建标识：yjq 2017/11/24 21:37:57
    /// </summary>
    public interface IPoolItemCreator<T>
    {
        /// <summary>
        /// Creates the items of the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        IEnumerable<T> Create(int count);
    }
}