using NetCoreRpc.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：TypeUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/17 20:21:58
    /// </summary>
    internal static class TypeUtil
    {
        /// <summary>
        /// 字节数组类型
        /// </summary>
        public static readonly Type ByteArrayType = typeof(Byte[]);

        /// <summary>
        /// 同步类型(无返回值)
        /// </summary>
        public static readonly Type SyncActionType = typeof(void);

        /// <summary>
        /// 异步类型(无返回值)
        /// </summary>
        public static readonly Type AsyncActionType = typeof(Task);

        /// <summary>
        /// 异步方法类型(有返回值)
        /// </summary>
        public static readonly Type AsyncFunctionType = typeof(Task<>);

        public static readonly Type NullableType = typeof(Nullable<>);

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, List<FieldInfo>> _FieldListDic = new ConcurrentDictionary<RuntimeTypeHandle, List<FieldInfo>>();

        public static List<FieldInfo> GetRpcFieldList(this Type type)
        {
            return _FieldListDic.GetValue(type.TypeHandle, () =>
            {
                List<FieldInfo> _FieldList = new List<FieldInfo>();
                for (; type != null; type = type.BaseType)
                {
                    FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.GetField |
                        BindingFlags.DeclaredOnly);
                    if (fields != null)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if ((fields[i].Attributes & FieldAttributes.NotSerialized) == 0)
                            {
                                if (!_FieldList.Contains(fields[i]))
                                {
                                    _FieldList.Add(fields[i]);
                                }
                            }
                        }
                    }
                }
                return _FieldList;
            });
        }
    }
}