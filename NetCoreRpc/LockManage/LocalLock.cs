using NetCoreRpc.Extensions;
using NetCoreRpc.Utils;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreRpc.LockManage
{
    /// <summary>
    /// 类名：LocalLock.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/9 14:19:58
    /// </summary>
    public sealed class LocalLock : ILock
    {
        private static ConcurrentDictionary<string, object> _LockCache = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, string> _LockUserCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 获取一个锁(需要自己释放)
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <returns>成功返回true</returns>
        public bool LockTake(string key, string value, TimeSpan span)
        {
            Ensure.NotNullAndNotEmpty(key, "Lockkey");
            Ensure.NotNullAndNotEmpty(value, "Lockvalue");
            var obj = _LockCache.GetValue(key, () => { return new object(); });
            if (Monitor.TryEnter(obj, span))
            {
                _LockUserCache[key] = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 异步获取一个锁(需要自己释放)
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <returns>成功返回true</returns>
        public Task<bool> LockTakeAsync(string key, string value, TimeSpan span)
        {
            return Task.FromResult(LockTake(key, value, span));
        }

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <returns>成功返回true</returns>
        public bool LockRelease(string key, string value)
        {
            Ensure.NotNullAndNotEmpty(key, "Lockkey");
            Ensure.NotNullAndNotEmpty(value, "Lockvalue");
            object obj;
            _LockCache.TryGetValue(key, out obj);
            if (obj != null)
            {
                if (_LockUserCache[key] == value)
                {
                    Monitor.Exit(obj);
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 异步释放一个锁
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <returns>成功返回true</returns>
        public Task<bool> LockReleaseAsync(string key, string value)
        {
            return Task.FromResult(LockRelease(key, value));
        }
    }
}