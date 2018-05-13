using MongoDB.Driver;

namespace NetCoreRpc.MongoDB
{
    /// <summary>
    /// 接口名：IMongoDatabaseProvider.cs
    /// 接口功能描述：
    /// 创建标识：yjq 2018/5/13 15:42:23
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        ///  获取 <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="config">MongoDb配置</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        /// <returns><see cref="IMongoDatabase"/></returns>
        IMongoDatabase GetDatabase(MonogoDbConfig config);
    }
}