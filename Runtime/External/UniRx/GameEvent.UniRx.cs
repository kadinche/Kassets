#if KASSETS_UNIRX
using System;
using UniRx;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent : IObserver<object>, IObservable<object>
    {
        private readonly Subject<object> _subject = new Subject<object>();
        
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => _subject.OnNext(this);
        
        public IDisposable Subscribe(Action action) => this.Subscribe(_ => action.Invoke());

        void IObserver<object>.OnCompleted() => _subject.OnCompleted();
        void IObserver<object>.OnError(Exception error) => _subject.OnError(error);
        void IObserver<object>.OnNext(object value) => Raise();
        IDisposable IObservable<object>.Subscribe(IObserver<object> observer) => _subject.Subscribe(observer);

        public override void Dispose() => _subject.Dispose();
    }

    public abstract partial class GameEvent<T> : IObserver<T>, IObservable<T>
    {
        private readonly Subject<T> _subject = new Subject<T>();
        
        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        public virtual void Raise(T param)
        {
            _value = param;
            base.Raise();
            _subject.OnNext(param);
        }

        IDisposable IEventHandler<T>.Subscribe(Action<T> action) => this.Subscribe(action);

        void IObserver<T>.OnCompleted() => _subject.OnCompleted();
        void IObserver<T>.OnError(Exception error) => _subject.OnError(error);
        void IObserver<T>.OnNext(T value) => Raise(value);
        IDisposable IObservable<T>.Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
    }

    public partial class GameEventCollection
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public IDisposable Subscribe(Action onAnyEvent, bool withBuffer)
        {
            foreach (IEventHandler gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent).AddTo(_compositeDisposable);
            }

            if (withBuffer)
            {
                onAnyEvent.Invoke();
            }

            return _compositeDisposable;
        }
    }
}

#endif