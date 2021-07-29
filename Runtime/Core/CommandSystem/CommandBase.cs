namespace Kadinche.Kassets.CommandSystem
{
    public abstract class CommandBase : KassetsBase, ICommand
    {
        public abstract void Execute();
    }

    public abstract class CommandBase<T> : CommandBase, ICommand<T>
    {
        public abstract void Execute(T param);
    }
}