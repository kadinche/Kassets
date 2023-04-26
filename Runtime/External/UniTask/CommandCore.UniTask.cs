#if KASSETS_UNITASK

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandCore
    {
        protected CancellationTokenSource cts;
        
        public abstract void Execute();

        public virtual UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            Execute();
            return UniTask.Yield(cancellationToken);
        }
        
        public UniTask ExecuteAsync() => ExecuteAsync(cts.Token);
        
        protected override void OnEnable()
        {
            cts = new CancellationTokenSource();
            base.OnEnable();
        }

        public override void Dispose()
        {
            cts?.CancelAndDispose();
            cts = null;
        }
    }

    public abstract partial class CommandCore<T>
    {
        public abstract void Execute(T param);

        public virtual UniTask ExecuteAsync(T param, CancellationToken cancellationToken)
        {
            Execute(param);
            return UniTask.Yield(cancellationToken);
        }
        
        public UniTask ExecuteAsync(T param) => ExecuteAsync(param, cts.Token);
    }
}

#endif