#if KASSETS_UNITASK

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.Variable
{
    public abstract partial class VariableCore<T> : IAsyncReactiveProperty<T>
    {
        public IUniTaskAsyncEnumerable<T> WithoutCurrent() => onEventRaise.WithoutCurrent();

        UniTask<T> IReadOnlyAsyncReactiveProperty<T>.WaitAsync(CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return onEventRaise.WaitAsync(token);
        }
    }
}

#endif