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
        protected readonly CancellationTokenSource cts = new CancellationTokenSource();

        public void Subscribe(Action action, CancellationToken cancellationToken)
        {
            _onEventRaise.Subscribe(_ => action.Invoke(), cancellationToken);
        }

        public UniTask EventAsync(CancellationToken cancellationToken) => _onEventRaise.WaitAsync(cancellationToken);
        public UniTask EventAsync() => EventAsync(cts.Token);

        IUniTaskAsyncEnumerator<object> IUniTaskAsyncEnumerable<object>.GetAsyncEnumerator(
            CancellationToken cancellationToken) =>
            _onEventRaise.GetAsyncEnumerator(cancellationToken);
    }
    
    public partial class GameEvent
    {
#if !UNITY_EDITOR
        protected override void OnDisable()
        {
            base.OnDisable();
            cts.RefreshToken();
        }
#else
        protected override void OnExitPlayMode()
        {
            base.OnExitPlayMode();
            cts.RefreshToken();
        }
#endif
    }

    public abstract partial class GameEvent<T> : IUniTaskAsyncEnumerable<T>
    {
        protected readonly AsyncReactiveProperty<T> onEventRaise = new AsyncReactiveProperty<T>(default);

        public void Subscribe(Action<T> action, CancellationToken cancellationToken) => onEventRaise.Subscribe(action, cancellationToken);

        public new UniTask<T> EventAsync(CancellationToken cancellationToken) => onEventRaise.WaitAsync(cancellationToken);
        public new UniTask<T> EventAsync() => EventAsync(cts.Token);
        
        IUniTaskAsyncEnumerator<T> IUniTaskAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken) =>
            onEventRaise.GetAsyncEnumerator(cancellationToken);
    }

    public partial class GameEventCollection
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public void Subscribe(Action onAnyEvent, CancellationToken cancellationToken)
        {
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent, cancellationToken);
            }
        }

        public UniTask AnyEventAsync() => AnyEventAsync(cts.Token);
        public UniTask AnyEventAsync(CancellationToken cancellationToken)
        {
            return UniTask.WhenAny(_gameEvents.Select(gameEvent => gameEvent.EventAsync(cancellationToken)));
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
            cts.CancelAndDispose();
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
            cts.CancelAndDispose();
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
