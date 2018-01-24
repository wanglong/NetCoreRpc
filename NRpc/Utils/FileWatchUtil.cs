using NRpc.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：FileWatchUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 11:08:20
    /// </summary>
    public class FileWatchUtil : SelfDisposable
    {
        private static List<FileWatchUtil> _fileWatchList = new List<FileWatchUtil>();

        private Timer _timer;
        private FileSystemWatcher _fileWatcher;
        private readonly int _timeOutMillis = 500;//延迟执行时间
        private FileInfo _fileInfo;
        private Action<FileStream> _fileChangedHandle = null;//文件变动后执行方法

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="action">文件改变后的执行的方法</param>
        /// <param name="timeOutMillis">延迟时间</param>
        /// <param name="isStart">是否启动监听</param>
        public FileWatchUtil(string filePath, Action<FileStream> action, int? timeOutMillis = null, bool isStart = false)
        {
            if (FileUtil.IsExistsFile(filePath))
            {
                if (timeOutMillis.HasValue)
                {
                    _timeOutMillis = timeOutMillis.Value;
                }
                Ensure.NotNull(action, "file changed action");
                _fileChangedHandle = action;
                _fileInfo = new FileInfo(filePath);
                if (isStart)
                {
                    OnWatchedFileChange(null);
                    StartWatching();
                }
                _fileWatchList.Add(this);
            }
            else
            {
                LogUtil.Warn($"file not exist【{filePath}】");
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="action">文件改变后的执行的方法</param>
        /// <param name="timeOutMillis">延迟时间</param>
        /// <param name="isStart">是否启动监听</param>
        public FileWatchUtil(FileInfo fileInfo, Action<FileStream> action, int? timeOutMillis = null, bool isStart = false)
        {
            if (timeOutMillis.HasValue)
            {
                _timeOutMillis = timeOutMillis.Value;
            }
            Ensure.NotNull(action, "file changed action");
            Ensure.NotNull(_fileInfo, "watching fileInfo");
            _fileChangedHandle = action;
            _fileInfo = fileInfo;
            if (isStart)
            {
                OnWatchedFileChange(null);
                StartWatching();
            }
        }

        /// <summary>
        /// 开始启动监听文件
        /// </summary>
        public void StartWatching()
        {
            _fileWatcher = new FileSystemWatcher
            {
                Path = _fileInfo.DirectoryName,
                Filter = _fileInfo.Name,
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            _fileWatcher.Changed += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Created += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Deleted += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Renamed += new RenamedEventHandler(ConfigWacherHandler_OnRenamed);
            _fileWatcher.EnableRaisingEvents = true;

            _timer = new Timer(new TimerCallback(OnWatchedFileChange), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void ConfigWacherHandler_OnChanged(object source, FileSystemEventArgs e)
        {
            _timer.Change(_timeOutMillis, Timeout.Infinite);
        }

        private void ConfigWacherHandler_OnRenamed(object source, RenamedEventArgs e)
        {
            _timer.Change(_timeOutMillis, Timeout.Infinite);
        }

        private void OnWatchedFileChange(object state)
        {
            if (_fileInfo == null)
            {
                LogUtil.Error("file not exist【InternalConfigure】");
            }
            else if (FileUtil.IsExistsFile(_fileInfo.FullName))
            {
                FileStream fs = null;
                for (int retry = 5; --retry >= 0;)
                {
                    try
                    {
                        fs = _fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
                        break;
                    }
                    catch (IOException ex)
                    {
                        if (retry == 0)
                        {
                            LogUtil.Error(string.Format("open file failed [{0}],{1}", _fileInfo.FullName, ex.ToErrMsg("OnWatchedFileChange")));
                            fs = null;
                        }
                        Thread.Sleep(250);
                    }
                }
                if (fs != null)
                {
                    try
                    {
                        _fileChangedHandle?.Invoke(fs);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            else
            {
                LogUtil.Warn($"file not exist【{_fileInfo.FullName}】");
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        protected override void DisposeCode()
        {
            _fileWatcher?.Dispose();
            _timer?.Dispose();
        }

        internal static void RegisterUnLoad()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            foreach (var item in _fileWatchList)
            {
                item?.Dispose();
            }
        }
    }
}