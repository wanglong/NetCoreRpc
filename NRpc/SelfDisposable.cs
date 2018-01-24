using System;

namespace NRpc
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：SelfDisposable.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/24 10:15:11
    /// </summary>
    public class SelfDisposable : IDisposable
    {
        private bool _isDisposed;

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