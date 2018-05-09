using System;

namespace NetCoreRpc
{
    /// <summary>
    /// 类名：SelfDisposable.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/8 10:11:03
    /// </summary>
    public class SelfDisposable : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeCode()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCode();
            }
            _isDisposed = true;
        }

        ~SelfDisposable()
        {
            Dispose(false);
        }
    }
}