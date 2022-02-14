namespace Kadinche.Kassets.CommandSystem
{
    public abstract partial class CommandCore : KassetsCore, ICommand
    {
    }

    public abstract partial class CommandCore<T> : CommandCore, ICommand<T>
    {
        public override void Execute() => Execute(default);
    }

#if !KASSETS_UNITASK
    public abstract partial class CommandCore
    {
        public override void Dispose() { }
        public abstract void Execute();
    }

    public abstract partial class CommandCore<T>
    {
        public abstract void Execute(T param);
    }
#endif
}