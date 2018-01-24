using NRpc.Extensions;
using NRpc.Serializing;
using NRpc.Utils;
using System;
using System.IO;
using System.Text;

namespace NRpc
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：NRpcConfigWatcher.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 11:32:53
    /// </summary>
    public sealed class NRpcConfigWatcher
    {
        private static FileWatchUtil _configFileWacth = null;
        private static NRpcConfig _RemoteEndPointConfig;

        public static void Install()
        {
            if (FileUtil.IsNotExistsFile(GetAppConfigPath))
            {
                throw new Exception("not found file NRpc.json");
            }
            _configFileWacth = new FileWatchUtil(GetAppConfigPath, InternalConfigure, isStart: true);
        }

        internal static NRpcConfig CurrentConfig
        {
            get
            {
                return _RemoteEndPointConfig;
            }
        }

        private static string GetAppConfigPath
        {
            get { return FileUtil.PathCombine(FileUtil.GetDomianPath(), "NRpc.json"); }
        }

        private static void InternalConfigure(FileStream configStream)
        {
            if (configStream == null)
            {
                LogUtil.Error("【configStream】is empty");
            }

            try
            {
                int fsLen = (int)configStream.Length;
                byte[] heByte = new byte[fsLen];
                int r = configStream.Read(heByte, 0, heByte.Length);
                string myStr = Encoding.UTF8.GetString(heByte);
                _RemoteEndPointConfig = new JsonBinarySerializer().Deserialize<NRpcConfig>(heByte);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToErrMsg("InternalConfigure"));
            }
        }
    }
}