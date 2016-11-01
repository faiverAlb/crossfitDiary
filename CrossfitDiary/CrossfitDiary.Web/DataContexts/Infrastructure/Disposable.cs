using System;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public class Disposable : IDisposable
    {
        private bool _isDisposed;
        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeCore()
        {
        }

        private void Dispose(bool calledByUser)
        {
            if (calledByUser && !_isDisposed)
            {
                DisposeCore();
            }
            _isDisposed = true;
        }
    }
}