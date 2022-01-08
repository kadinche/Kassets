#if KASSETS_UNITASK

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.Variable
{
    public abstract partial class VariableCore<T> : IAsyncReactiveProperty<T>
    {
        public IUniTaskAsyncEnumerable<T> WithoutCurrent() => onEventRaise.WithoutCurrent();
        UniTask<T> IReadOnlyAsyncReactiveProperty<T>.WaitAsync(CancellationToken cancellationToken) => onEventRaise.WaitAsync(cancellationToken);
    }
}

#endif