using MongoDB.Bson;
using MongoDB.Driver;

namespace NetCoreRpc.MongoDB.Extensions
{
    /// <summary>
    /// 类名：UpdateExtension.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:20:41
    /// </summary>
    internal static class UpdateExtension
    {
        /// <summary>
        /// 将object转为UpdateDefinition（字段值必须要与库中一致）
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="obj">要转换的对象</param>
        /// <returns>UpdateDefinition</returns>
        public static UpdateDefinition<TEntity> ToUpdate<TEntity>(this object obj)
        {
            return new BsonDocument { { "$set", obj.ToBsonDocument() } };
        }

        /// <summary>
        /// 将object转为UpdateDefinition（字段值必须要与库中一致）
        /// $inc可以对文档的某个值为数字型（只能为满足要求的数字）的键进行增减的操作
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="obj">要转换的对象</param>
        /// <returns>UpdateDefinition</returns>
        public static UpdateDefinition<TEntity> ToUpdateInc<TEntity>(this object obj)
        {
            return new BsonDocument { { "$inc", obj.ToBsonDocument() } };
        }
    }
}