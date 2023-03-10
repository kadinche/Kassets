#if KASSETS_UNITASK

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandCore
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();
        
        public abstract void Execute();

        public virtual async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            Execute();
            await UniTask.Yield(cancellationToken);
        }
        
        public UniTask ExecuteAsync() => ExecuteAsync(cts.Token);

        public override void Dispose()
        {
            cts.CancelAndDispose();
        }
    }
    
    public abstract partial class CommandCore
    {
#if !UNITY_EDITOR
        protected override void OnDisable()
        {
            base.OnDisable();
            cts.CancelAndDispose();
            cts = new CancellationTokenSource();
        }
#else
        protected override void OnExitPlayMode()
        {
            base.OnExitPlayMode();
            cts.CancelAndDispose();
            cts = new CancellationTokenSource();
        }
#endif
    }
    
    public abstract partial class CommandCore<T>
    {
        public abstract void Execute(T param);

        public virtual async UniTask ExecuteAsync(T param, CancellationToken cancellationToken)
        {
            Execute(param);
            await UniTask.Yield(cancellationToken);
        }
        
        public UniTask ExecuteAsync(T param) => ExecuteAsync(param, cts.Token);
    }
}

#endif