#if KASSETS_UNITASK

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandCore
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();
        
        public virtual void Execute() => ExecuteAsync(cts.Token).Forget();
        public virtual UniTask ExecuteAsync(CancellationToken cancellationToken) { return UniTask.CompletedTask; }
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
        public virtual void Execute(T param) => ExecuteAsync(param, cts.Token).Forget();
        public virtual UniTask<T> ExecuteAsync(T param, CancellationToken cancellationToken) => new UniTask<T>(param);
        public UniTask<T> ExecuteAsync(T param) => ExecuteAsync(param, cts.Token);
    }
}

#endif