#if !KASSETS_UNIRX

using System;
using System.Collections.Generic;

namespace Kadinche.Kassets.Utilities
{
    internal class CompositeDisposable : IDisposable
    {
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        public void Add(IDisposable disposable) => _disposables.Add(disposable);

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }

    internal static class DisposableExtension
    {
        internal static void Dispose(this IList<IDisposable> disposables)
        {
            foreach (var disposable in disposables)
            {
                disposable?.Dispose();
            }
            
            disposables.Clear();
        }

        internal static void AddTo(this IDisposable disposable, IList<IDisposable> disposables)
        {
            disposables.Add(disposable);
        }
        
        internal static void AddTo(this IDisposable disposable, CompositeDisposable disposables)
        {
            disposables.Add(disposable);
        }
    }
}

#endif