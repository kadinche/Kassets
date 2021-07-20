using System;

namespace Kadinche.Kassets
{
    public interface IEventRaiser
    {
        void Raise();
    }
    
    public interface IEventRaiser<in T> : IEventRaiser
    {
        void Raise(T param);
    }
    
    public interface IEventHandler
    {
        IDisposable Subscribe(Action action);
    }

    public interface IEventHandler<out T> : IEventHandler
    {
        IDisposable Subscribe(Action<T> action);
    }
}