#if KASSETS_R3

using System;
using System.Collections.Generic;
using System.Threading;
using R3;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent
    {
        private readonly Subject<object> _subject = new Subject<object>();
    }

    public abstract partial class GameEvent<T>
    {
        private readonly Subject<T> _valueSubject = new Subject<T>();
        
        public Observable<T> AsObservable() => _valueSubject;
        public IObservable<T> AsSystemObservable() => _valueSubject.AsSystemObservable();
        public IAsyncEnumerable<T> ToAsyncEnumerable(CancellationToken cancellationToken = default) => _valueSubject.ToAsyncEnumerable(cancellationToken: cancellationToken);
    }
    
#if KASSETS_UNIRX
    public partial class GameEvent : IObserver<object>, IObservable<object>
    {
        void IObserver<object>.OnCompleted() => _subject.OnCompleted();
        void IObserver<object>.OnError(Exception error) => _subject.OnCompleted(error);
        void IObserver<object>.OnNext(object value) => Raise();
        IDisposable IObservable<object>.Subscribe(IObserver<object> observer) => _subject.AsSystemObservable().Subscribe(observer);
    }

    public abstract partial class GameEvent<T> : IObserver<T>, IObservable<T>
    {
        void IObserver<T>.OnCompleted() => _valueSubject.OnCompleted();
        void IObserver<T>.OnError(Exception error) => _valueSubject.OnCompleted(error);
        void IObserver<T>.OnNext(T value) => Raise(value);
        IDisposable IObservable<T>.Subscribe(IObserver<T> observer) => AsSystemObservable().Subscribe(observer);
    }
#endif
    
#if KASSETS_UNITASK
    public partial class GameEvent
    {
        protected void Raise_R3() => _subject.OnNext(this);
        private IDisposable Subscribe_R3(Action action) => _subject.Subscribe(_ => action.Invoke());
        protected virtual void Dispose_R3() => _subject.Dispose();
    }
    
    public abstract partial class GameEvent<T>
    {
        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        private void Raise_R3(T param)
        {
            _value = param;
            base.Raise_R3();
            _valueSubject.OnNext(param);
        }

        private IDisposable Subscribe_R3(Action<T> action) => _valueSubject.Subscribe(action);
        
        protected override void Dispose_R3()
        {
            _valueSubject.Dispose();
            base.Dispose_R3();
        }
    }
    
    public partial class GameEvent
    {
        private void Raise_UniRx() => Raise_R3();
        private IDisposable Subscribe_UniRx(Action action) => Subscribe_R3(action);
        private void Dispose_UniRx() => Dispose_R3();
    }

    public abstract partial class GameEvent<T>
    {
        private void Raise_UniRx(T param) => Raise_R3(param);
        private IDisposable Subscribe_UniRx(Action<T> action) => Subscribe_R3(action);
    }
#else
    public partial class GameEvent
    {
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => _subject.OnNext(this);
        public IDisposable Subscribe(Action action) => _subject.Subscribe(_ => action.Invoke());
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

        public IDisposable Subscribe(Action<T> action) => _valueSubject.Subscribe(action);
        
        public override void Dispose()
        {
            _valueSubject.Dispose();
            base.Dispose();
        }
    }
#endif
}

#endif
