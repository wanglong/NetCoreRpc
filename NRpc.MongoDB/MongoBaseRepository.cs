using MongoDB.Bson;
using MongoDB.Driver;
using NRpc.MongoDB.Extensions;
using NRpc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NRpc.MongoDB
{
    /// <summary>
    /// 类名：MongoBaseRepository.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 16:17:28
    /// </summary>
    public class MongoBaseRepository<TEntity> : IMongoDbBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="databaseProvider"></param>
        /// <param name="dbConfig"></param>
        public MongoBaseRepository(IMongoDatabaseProvider databaseProvider, MonogoDbConfig dbConfig)
        {
            Ensure.NotNull(databaseProvider, "IMongoDatabaseProvider");
            Ensure.NotNull(dbConfig, "MonogoDbConfig");
            _databaseProvider = databaseProvider;
            DbConfig = dbConfig;
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        protected MonogoDbConfig DbConfig { get; }

        protected virtual IMongoDatabase Database
        {
            get { return _databaseProvider.GetDatabase(DbConfig); }
        }

        protected virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        /// <summary>
        /// BsonDocument类型的集合，调用MongoDb驱动的某些方法时需要基于该类型的集合
        /// </summary>
        protected virtual IMongoCollection<BsonDocument> BsonCollection
        {
            get
            {
                return Database.GetCollection<BsonDocument>(typeof(TEntity).Name);
            }
        }

        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns>实体信息</returns>
        public virtual TEntity InsertOne(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns>实体信息</returns>
        public virtual async Task<TEntity> InsertOneAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entityList">实体列表</param>
        public virtual void InsertMany(IEnumerable<TEntity> entityList)
        {
            Collection.InsertMany(entityList);
        }

        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entityList">实体列表</param>
        /// <returns></returns>
        public virtual Task InsertManyAsync(IEnumerable<TEntity> entityList)
        {
            return Collection.InsertManyAsync(entityList);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="obj">更新内容</param>
        /// <returns>记录数大于返回true</returns>
        public virtual bool UpdateOne(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = Collection.UpdateOne(filter, obj.ToUpdate<TEntity>());
            return updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 异步更新一条记录
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="obj">更新内容</param>
        /// <returns>记录数大于返回true</returns>
        public async virtual Task<bool> UpdateOneAsync(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = await Collection.UpdateOneAsync(filter, obj.ToUpdate<TEntity>());
            return updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 替换一条记录
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="replacement">待替换内容</param>
        /// <returns>记录数大于返回true</returns>
        public virtual bool ReplaceOne(Expression<Func<TEntity, bool>> filter, TEntity replacement)
        {
            var updateResult = Collection.ReplaceOne(filter, replacement);
            return updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 异步替换一条记录
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="replacement">待替换内容</param>
        /// <returns>记录数大于返回true</returns>
        public async virtual Task<bool> ReplaceOneAsync(Expression<Func<TEntity, bool>> filter, TEntity replacement)
        {
            var updateResult = await Collection.ReplaceOneAsync(filter, replacement);
            return updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 获取自增序列号
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="obj"></param>
        /// <returns>返回当前更新的序列号对象</returns>
        public virtual TEntity GetNextSequence(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = Collection.FindOneAndUpdate(filter, obj.ToUpdateInc<TEntity>(),
                new FindOneAndUpdateOptions<TEntity, TEntity> { ReturnDocument = ReturnDocument.After });
            return updateResult;
        }

        /// <summary>
        /// 异步获取自增序列号
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="obj"></param>
        /// <returns>返回当前更新的序列号对象</returns>
        public async virtual Task<TEntity> GetNextSequenceAsync(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = await Collection.FindOneAndUpdateAsync(filter, obj.ToUpdateInc<TEntity>(),
                new FindOneAndUpdateOptions<TEntity, TEntity> { ReturnDocument = ReturnDocument.After });
            return updateResult;
        }

        /// <summary>
        /// 更新与查询操作，返回当前更新的对象
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="obj"></param>
        /// <returns>返回当前更新的对象</returns>
        public virtual TEntity FindOneAndUpdate(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = Collection.FindOneAndUpdate(filter, obj.ToUpdate<TEntity>(),
                new FindOneAndUpdateOptions<TEntity, TEntity> { ReturnDocument = ReturnDocument.After });
            return updateResult;
        }

        /// <summary>
        /// 异步更新与查询操作，返回当前更新的对象
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="obj"></param>
        /// <returns>返回当前更新的对象</returns>
        public async virtual Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = await Collection.FindOneAndUpdateAsync(filter, obj.ToUpdate<TEntity>(),
                new FindOneAndUpdateOptions<TEntity, TEntity> { ReturnDocument = ReturnDocument.After });
            return updateResult;
        }

        /// <summary>
        /// 聚合操作(目前驱动只支持BsonDocument类型的聚合，因此返回的聚合结果也是BsonDocument类型)
        /// </summary>
        /// <param name="stages">管道操作字符串集合(e.g.  [ { $match : {'basicInfo.createTime':{'$gt':'2017-08-16 15:30:12 256'}} },  { $group: { _id: {decisionTreeNumber:'$decisionTree.decisionTreeNumber', version:'$decisionTree.version'}, count: { $sum: 1 } } }  ] )</param>
        /// <returns></returns>
        public virtual List<BsonDocument> Aggregate(params string[] stages)
        {
            var pipelineStages = GeneratePipelineStages(stages);
            var pipeline = new PipelineStagePipelineDefinition<BsonDocument, BsonDocument>(pipelineStages);
            var aggregatedResult = BsonCollection.Aggregate(pipeline);
            return aggregatedResult.ToList();
        }

        /// <summary>
        /// 聚合操作异步执行(目前驱动只支持BsonDocument类型的聚合，因此返回的聚合结果也是BsonDocument类型)
        /// </summary>
        /// <param name="stages">管道操作字符串集合(e.g.  [ { $match : {'basicInfo.createTime':{'$gt':'2017-08-16 15:30:12 256'}} },  { $group: { _id: {decisionTreeNumber:'$decisionTree.decisionTreeNumber', version:'$decisionTree.version'}, count: { $sum: 1 } } }  ] )</param>
        /// <returns></returns>
        public async virtual Task<List<BsonDocument>> AggregateAsync(params string[] stages)
        {
            var pipelineStages = GeneratePipelineStages(stages);
            var pipeline = new PipelineStagePipelineDefinition<BsonDocument, BsonDocument>(pipelineStages);
            var aggregatedResult = await BsonCollection.AggregateAsync(pipeline);
            return aggregatedResult.ToList();
        }

        /// <summary>
        /// 聚合操作(目前驱动只支持BsonDocument类型的聚合，因此返回的聚合结果也是BsonDocument类型)
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public virtual List<BsonDocument> Aggregate(PipelineDefinition<BsonDocument, BsonDocument> pipeline)
        {
            var aggregatedResult = BsonCollection.Aggregate(pipeline);
            return aggregatedResult.ToList();
        }

        /// <summary>
        /// 聚合操作异步执行(目前驱动只支持BsonDocument类型的聚合，因此返回的聚合结果也是BsonDocument类型)
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public async virtual Task<List<BsonDocument>> AggregateAsync(PipelineDefinition<BsonDocument, BsonDocument> pipeline)
        {
            var aggregatedResult = await BsonCollection.AggregateAsync(pipeline);
            return aggregatedResult.ToList();
        }

        private List<IPipelineStageDefinition> GeneratePipelineStages(params string[] stages)
        {
            if (stages == null || stages.Length == 0)
            {
                return null;
            }

            List<IPipelineStageDefinition> pipelineStages = new List<IPipelineStageDefinition>();
            foreach (string stage in stages)
            {
                pipelineStages.Add(new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(stage));
            }
            return pipelineStages;
        }

        /// <summary>
        /// 更新记录信息
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="obj">更新内容</param>
        /// <returns>更新记录数</returns>
        public long UpdateMany(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = Collection.UpdateMany(filter, obj.ToUpdate<TEntity>());
            return updateResult.ModifiedCount;
        }

        /// <summary>
        /// 异步更新记录信息
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="obj">更新内容</param>
        /// <returns>更新记录数</returns>
        public async Task<long> UpdateManyAsync(Expression<Func<TEntity, bool>> filter, object obj)
        {
            var updateResult = await Collection.UpdateManyAsync(filter, obj.ToUpdate<TEntity>());
            return updateResult.ModifiedCount;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns>true表示删除成功</returns>
        public virtual bool DeleteOne(Expression<Func<TEntity, bool>> filter)
        {
            var deleteResult = Collection.DeleteOne(filter);
            return deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// 异步删除一条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns>true表示删除成功</returns>
        public virtual async Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var deleteResult = await Collection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns>删除的总记录数</returns>
        public virtual long DeleteMany(Expression<Func<TEntity, bool>> filter)
        {
            var deleteResult = Collection.DeleteMany(filter);
            return deleteResult.DeletedCount;
        }

        /// <summary>
        /// 异步删除多条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns>删除的总记录数</returns>
        public virtual async Task<long> DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            var deleteResult = await Collection.DeleteManyAsync(filter);
            return deleteResult.DeletedCount;
        }

        /// <summary>
        /// 获取一条实体信息（获取不到则为空）
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>实体信息</returns>
        public virtual TEntity GetInfo(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 异步获取一条实体信息（获取不到则为空）
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>实体信息</returns>
        public virtual Task<TEntity> GetInfoAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取全部实体
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>列表</returns>
        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }

        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>列表</returns>
        public virtual Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.FindAsync(filter).ContinueWith((m) => { return m.Result.ToEnumerable(); });
        }

        /// <summary>
        /// 获取DTO（获取不到则为空）
        /// </summary>
        /// <typeparam name="TDto">DTO类型</typeparam>
        /// <param name="filter">条件</param>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>DTO信息</returns>
        public virtual TDto GetDto<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TDto>> projectionExpression)
        {
            var projection = projectionExpression.ToProjection();
            return Collection.Find(filter).Project(projection).FirstOrDefault();
        }

        /// <summary>
        /// 异步获取DTO（获取不到则为空）
        /// </summary>
        /// <typeparam name="TDto">DTO类型</typeparam>
        /// <param name="filter">条件</param>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>DTO信息</returns>
        public virtual Task<TDto> GetDtoAsync<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TDto>> projectionExpression)
        {
            return Task.FromResult(GetDto(filter, projectionExpression));
        }

        /// <summary>
        /// 获取全部实体
        /// </summary>
        /// <typeparam name="TDto">类型</typeparam>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>获取全部实体</returns>
        public virtual IEnumerable<TDto> GetDtoList<TDto>(Expression<Func<TEntity, TDto>> projectionExpression)
        {
            var projection = projectionExpression.ToProjection();
            return Collection.Find(new BsonDocument()).Project(projection).ToEnumerable();
        }

        /// <summary>
        /// 异步获取全部实体
        /// </summary>
        /// <typeparam name="TDto">类型</typeparam>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>获取全部实体</returns>
        public virtual Task<IEnumerable<TDto>> GetDtoListAsync<TDto>(Expression<Func<TEntity, TDto>> projectionExpression)
        {
            return Task.FromResult(GetDtoList(projectionExpression));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="TDto">实体类型</typeparam>
        /// <param name="filter">条件</param>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>实体列表</returns>
        public virtual IEnumerable<TDto> GetDtoList<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TDto>> projectionExpression)
        {
            var projection = projectionExpression.ToProjection();
            return Collection.Find(filter).Project(projection).ToEnumerable();
        }

        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <typeparam name="TDto">实体类型</typeparam>
        /// <param name="filter">条件</param>
        /// <param name="projectionExpression">映射表达式</param>
        /// <returns>实体列表</returns>
        public virtual Task<IEnumerable<TDto>> GetDtoListAsync<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TDto>> projectionExpression)
        {
            return Task.FromResult(GetDtoList(filter, projectionExpression));
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns>数量</returns>
        public virtual long Count()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// 异步获取数量
        /// </summary>
        /// <returns>数量</returns>
        public virtual Task<long> CountAsync()
        {
            return Task.FromResult(Count());
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>数量</returns>
        public virtual long Count(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Count(filter);
        }

        /// <summary>
        /// 异步获取数量
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns>数量</returns>
        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.CountAsync(filter);
        }
    }
}