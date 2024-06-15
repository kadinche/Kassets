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
            base.Dispose_UniRx();
        }
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
