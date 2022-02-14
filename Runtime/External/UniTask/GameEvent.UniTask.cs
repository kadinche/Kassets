#if KASSETS_UNITASK

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent : IUniTaskAsyncEnumerable<object>
    {
        private readonly AsyncReactiveProperty<object> _onEventRaise = new AsyncReactiveProperty<object>(default);

        public void Subscribe(Action action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            _onEventRaise.Subscribe(_ => action.Invoke(), token);
        }

        public UniTask EventAsync(CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return _onEventRaise.WaitAsync(token);
        }

        IUniTaskAsyncEnumerator<object> IUniTaskAsyncEnumerable<object>.GetAsyncEnumerator(
            CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return _onEventRaise.GetAsyncEnumerator(token);
        }
    }

    public abstract partial class GameEvent<T> : IUniTaskAsyncEnumerable<T>
    {
        protected readonly AsyncReactiveProperty<T> onEventRaise = new AsyncReactiveProperty<T>(default);

        public void Subscribe(Action<T> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            onEventRaise.Subscribe(action, token);
        }

        public new UniTask<T> EventAsync(CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return onEventRaise.WaitAsync(token);
        }

        IUniTaskAsyncEnumerator<T> IUniTaskAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return onEventRaise.GetAsyncEnumerator(token);
        }
    }

    public partial class GameEventCollection
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public void Subscribe(Action onAnyEvent, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent, token);
            }
        }

        public UniTask AnyEventAsync(CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return UniTask.WhenAny(_gameEvents.Select(gameEvent => gameEvent.EventAsync(token)));
        }
        
        public void Dispose()
        {
            _compositeDisposable.Dispose();
            cts.CancelAndDispose();
        }
    }
    
#if KASSETS_UNIRX
    public partial class GameEvent
    {
        /// <summary>
        /// Raise the event.
        /// </summary>
        private void Raise_UniTask() => _onEventRaise.Value = this;
        private IDisposable Subscribe_UniTask(Action action) => _onEventRaise.Subscribe(_ => action.Invoke());
        private void Dispose_UniTask()
        {
            base.Dispose();
            _onEventRaise.Dispose();
        }
    }
    
    public abstract partial class GameEvent<T>
    {
        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        private void Raise_UniTask(T param)
        {
            _value = param;
            base.Raise();
            onEventRaise.Value = param;
        }

        private IDisposable Subscribe_UniTask(Action<T> action) => onEventRaise.Subscribe(action);
    }
#else
    public partial class GameEvent
    {
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => _onEventRaise.Value = this;
        public IDisposable Subscribe(Action action) => _onEventRaise.Subscribe(_ => action.Invoke());
        public override void Dispose()
        {
            base.Dispose();
            _onEventRaise.Dispose();
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
            onEventRaise.Value = param;
        }

        public IDisposable Subscribe(Action<T> action) => onEventRaise.Subscribe(action);
    }
#endif
}

#endif
