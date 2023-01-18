namespace Kadinche.Kassets
{
    public interface IVariable { }
    public interface IVariable<T> : IVariable
    {
        T Value { get; set; }
    }
}