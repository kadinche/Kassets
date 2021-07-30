#if KASSETS_UNITASK

using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandBase
    {
        public virtual void Execute() => ExecuteAsync().Forget();
        public virtual UniTask ExecuteAsync() { return UniTask.CompletedTask; }
    }
    
    public abstract partial class CommandBase<T>
    {
        public virtual void Execute(T param) => ExecuteAsync(param).Forget();

        public virtual UniTask<T> ExecuteAsync(T param)
        {
            return new UniTask<T>(param);
        }
    }
}

#endif