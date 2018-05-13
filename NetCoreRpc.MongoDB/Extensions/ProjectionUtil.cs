using MongoDB.Driver;
using NetCoreRpc.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace NetCoreRpc.MongoDB.Extensions
{
    /// <summary>
    /// 类名：ProjectionUtil.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:19:13
    /// </summary>
    public static partial class ProjectionUtil
    {
        /// <summary>
        /// 投影缓存
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<RuntimeTypeHandle, object>> _ProjectionCache = new ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<RuntimeTypeHandle, object>>();

        /// <summary>
        /// 获取投影关系
        /// </summary>
        /// <typeparam name="TSource">源属性</typeparam>
        /// <typeparam name="TTarget">目标属性</typeparam>
        /// <typeparam name="projectionExpression">转换表达式</typeparam>
        /// <returns>投影关系</returns>
        public static ProjectionDefinition<TSource, TTarget> ToProjection<TSource, TTarget>(this Expression<Func<TSource, TTarget>> projectionExpression)
        {
            var targetType = typeof(TTarget);
            var sourceType = typeof(TSource);
            return _ProjectionCache.GetValue(targetType.TypeHandle, sourceType.TypeHandle, () =>
            {
                return Builders<TSource>.Projection.Expression(projectionExpression);
            }) as ProjectionDefinition<TSource, TTarget>;
        }
    }
}