namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandBase : KassetsBase, ICommand
    {
#if !KASSETS_UNITASK
        public abstract void Execute();
#endif
    }

    public abstract partial class CommandBase<T> : CommandBase, ICommand<T>
    {
#if !KASSETS_UNITASK
        public abstract void Execute(T param);
#endif
    }
}