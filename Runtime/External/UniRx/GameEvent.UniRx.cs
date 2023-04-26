#if KASSETS_UNIRX
using System;
using UniRx;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent : IObserver<object>, IObservable<object>
    {
        private readonly Subject<object> _subject = new Subject<object>();

        void IObserver<object>.OnCompleted() => _subject.OnCompleted();
        void IObserver<object>.OnError(Exception error) => _subject.OnError(error);
        void IObserver<object>.OnNext(object value) => Raise();
        IDisposable IObservable<object>.Subscribe(IObserver<object> observer) => _subject.Subscribe(observer);
    }

    public abstract partial class GameEvent<T> : IObserver<T>, IObservable<T>
    {
        private readonly Subject<T> _valueSubject = new Subject<T>();

        void IObserver<T>.OnCompleted() => _valueSubject.OnCompleted();
        void IObserver<T>.OnError(Exception error) => _valueSubject.OnError(error);
        void IObserver<T>.OnNext(T value) => Raise(value);
        IDisposable IObservable<T>.Subscribe(IObserver<T> observer) => _valueSubject.Subscribe(observer);
    }

    public partial class GameEventCollection
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public IDisposable Subscribe(Action onAnyEvent, bool withBuffer)
        {
            foreach (IGameEventHandler gameEvent in _gameEvents)
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
    
#if KASSETS_UNITASK
    public partial class GameEvent
    {
        protected void Raise_UniRx() => _subject.OnNext(this);
        private IDisposable Subscribe_UniRx(Action action) => this.Subscribe(_ => action.Invoke());
        protected virtual void Dispose_UniRx() => _subject.Dispose();
    }
    
    public abstract partial class GameEvent<T>
    {
        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        private void Raise_UniRx(T param)
        {
            _value = param;
            base.Raise_UniRx();
            _valueSubject.OnNext(param);
        }

        private IDisposable Subscribe_UniRx(Action<T> action) => ObservableExtensions.Subscribe(this, action);
        
        protected override void Dispose_UniRx()
        {
            _valueSubject.Dispose();
            base.Dispose();
        }
    }
#else
    public partial class GameEvent
    {
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => _subject.OnNext(this);
        public IDisposable Subscribe(Action action) => this.Subscribe(_ => action.Invoke());
        public override void Dispose()
        {
            _subject.Dispose();
            base.Dispose();
        }
    }
    
    public abstract partial class GameEvent<T>
    {
        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        public virtual void Raise(T param)
        {
            _value = param;
            base.Raise();
            _valueSubject.OnNext(param);
        }

        public IDisposable Subscribe(Action<T> action) => ObservableExtensions.Subscribe(this, action);
        
        public override void Dispose()
        {
            _valueSubject.Dispose();
            base.Dispose();
        }
    }
#endif
}

#endif
