#if KASSETS_UNITASK

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent : IUniTaskAsyncEnumerable<object>, ISerializationCallbackReceiver
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

        public override void OnAfterDeserialize()
        {
            _ = cts.RefreshToken();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => OnAfterDeserialize();
    }
    
#if UNITY_EDITOR
    public partial class GameEvent
    {
        protected override void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.ExitingPlayMode)
            {
                cts.RefreshToken();
            }
        }
    }
#endif

    public abstract partial class GameEvent<T> : IUniTaskAsyncEnumerable<T>
    {
        private readonly AsyncReactiveProperty<T> _onEventRaise = new AsyncReactiveProperty<T>(default);

        public void Subscribe(Action<T> action, CancellationToken cancellationToken) => _onEventRaise.Subscribe(action, cancellationToken);

        public new UniTask<T> EventAsync(CancellationToken cancellationToken) => _onEventRaise.WaitAsync(cancellationToken);
        public new UniTask<T> EventAsync() => EventAsync(cts.Token);
        
        IUniTaskAsyncEnumerator<T> IUniTaskAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken) =>
            _onEventRaise.GetAsyncEnumerator(cancellationToken);
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
            _onEventRaise.Value = param;
        }

        private IDisposable Subscribe_UniTask(Action<T> action) => _onEventRaise.Subscribe(action);
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
            _onEventRaise.Value = param;
        }

        public IDisposable Subscribe(Action<T> action) => _onEventRaise.Subscribe(action);
    }
#endif
}

#endif
