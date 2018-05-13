using MongoDB.Driver;
using NRpc.Utils;
using System;

namespace NRpc.MongoDB
{
    /// <summary>
    /// 类名：MonogoDbConfig.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 15:39:26
    /// </summary>
    public class MonogoDbConfig
    {
        private static Func<MonogoDbConfig> GetConfigAction;

        public MonogoDbConfig()
        {
        }

        public MonogoDbConfig(string connectionString, string databaseName) : this()
        {
            Ensure.NotNullAndNotEmpty(connectionString, "connectionString");
            Ensure.NotNullAndNotEmpty(databaseName, "databaseName");
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 库名字
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 根据包含数据库名字的连接字符串创建MonogoDb配置
        /// </summary>
        /// <param name="connectionString">包含数据库名字的连接字符串</param>
        /// <returns>MonogoDb配置</returns>
        public static MonogoDbConfig CreateConfig(string connectionString)
        {
            MongoUrl mongoUrl = new MongoUrl(connectionString);
            return new MonogoDbConfig(connectionString, mongoUrl.DatabaseName);
        }

        /// <summary>
        /// 根据连接字符串和数据库名字创建MonogoDb配置
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名字</param>
        /// <returns>MonogoDb配置</returns>
        public static MonogoDbConfig CreateConfig(string connectionString, string databaseName)
        {
            return new MonogoDbConfig(connectionString, databaseName);
        }

        public static void SetConfig(Func<MonogoDbConfig> func)
        {
            Ensure.NotNull(func, "初始化配置文件方法不能为空");
            GetConfigAction = func;
        }

        public static MonogoDbConfig GetConfig()
        {
            return GetConfigAction();
        }
    }
}