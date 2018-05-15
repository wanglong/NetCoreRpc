using MongoDB.Driver;

namespace NRpc.AdminManage.Infrastructure.MongoUtil
{
    /// <summary>
    /// 类名：MongoDatabaseProvider.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 15:43:04
    /// </summary>
    public sealed class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        /// <summary>
        ///  获取 <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="config">MongoDb配置</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        /// <returns><see cref="IMongoDatabase"/></returns>
        public IMongoDatabase GetDatabase(MonogoDbConfig config)
        {
            Ensure.NotNull(config, "MonogoDbConfig");
            MongoUrl mongoUrl = new MongoUrl(config.ConnectionString);
            var mongoClient = new MongoClient(mongoUrl);
            return mongoClient.GetDatabase(mongoUrl.DatabaseName ?? config.DatabaseName);
        }
    }
}