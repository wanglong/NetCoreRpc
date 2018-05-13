using System;
using System.Collections.Concurrent;

namespace NRpc.Extensions
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ConcurrentDictionaryExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 10:18:19
    /// </summary>
    public static class ConcurrentDictionaryExtension
    {
        public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return dict.TryRemove(key, out value);
        }

        /// <summary>
        /// ConcurrentDictionary 获取值的扩展方法
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="conDic">字典集合</param>
        /// <param name="key">键值</param>
        /// <param name="action">获取值的方法(使用时需要执行注意方法闭包)</param>
        /// <returns>指定键的值</returns>
        public static TValue GetValue<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> conDic, TKey key, Func<TValue> action)
        {
            TValue value;
            if (conDic.TryGetValue(key, out value)) return value;
            value = action();
            conDic[key] = value;
            return value;
        }

        public static TValue GetValue<TKey, TKey1, TValue>(this ConcurrentDictionary<TKey, ConcurrentDictionary<TKey1, TValue>> conDic, TKey key, TKey1 key1, Func<TValue> action)
        {
            ConcurrentDictionary<TKey1, TValue> keyValue;
            conDic.TryGetValue(key, out keyValue);
            if (keyValue != null)
            {
                TValue value;
                keyValue.TryGetValue(key1, out value);
                if (value == null)
                {
                    value = action();
                    keyValue.TryAdd(key1, value);
                    conDic[key] = keyValue;
                }
                return value;
            }
            else
            {
                keyValue = new ConcurrentDictionary<TKey1, TValue>();
                TValue value = action();
                keyValue.TryAdd(key1, value);
                conDic[key] = keyValue;
                return value;
            }
        }
    }
}