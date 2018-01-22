namespace NetCoreRpc.ServerRoute
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：RouteCoordinatorOption.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/22 19:06:42
    /// </summary>
    public sealed class RouteCoordinatorOption
    {
        /// <summary>
        /// 连接字符串。
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 等待ZooKeeper连接的时间。
        /// </summary>
        public int ConnectionTimeout { get; set; } = 5000;

        /// <summary>
        /// 执行ZooKeeper操作的重试等待时间。
        /// </summary>
        public int OperatingTimeout { get; set; } = 1000;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 10;

        /// <summary>
        /// zookeeper会话超时时间。
        /// </summary>
        public int SessionTimeout { get; set; } = 5000;

        /// <summary>
        /// 是否只读，默认为false。
        /// </summary>
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// 会话Id。
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// 会话密码。
        /// </summary>
        public byte[] SessionPasswd { get; set; }

        /// <summary>
        /// 父级名字
        /// </summary>
        public string ParentName { get; set; }
    }
}