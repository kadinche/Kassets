using System;

namespace Kadinche.Kassets
{
    public interface IGameEventRaiser
    {
        void Raise();
    }
    
    public interface IGameEventRaiser<in T> : IGameEventRaiser
    {
        void Raise(T param);
    }
    
    public interface IGameEventHandler
    {
        IDisposable Subscribe(Action action);
    }

    public interface IGameEventHandler<out T> : IGameEventHandler
    {
        IDisposable Subscribe(Action<T> action);
    }
}