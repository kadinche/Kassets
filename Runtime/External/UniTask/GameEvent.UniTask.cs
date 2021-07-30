#if KASSETS_UNITASK

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.Utilities;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent : IUniTaskAsyncEnumerable<object>, ISerializationCallbackReceiver
    {
        private readonly AsyncReactiveProperty<object> _onEventRaise = new AsyncReactiveProperty<object>(default);
        protected CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => _onEventRaise.Value = this;

        IDisposable IEventHandler.Subscribe(Action action) => _onEventRaise.Subscribe(_ => action.Invoke());

        public void Subscribe(Action action) => Subscribe(action, cts.Token);

        public void Subscribe(Action action, CancellationToken cancellationToken)
        {
            _onEventRaise.Subscribe(_ => action.Invoke(), cancellationToken);
        }

        public UniTask EventAsync(CancellationToken cancellationToken) => _onEventRaise.WaitAsync(cancellationToken);
        public UniTask EventAsync() => EventAsync(cts.Token);

        IUniTaskAsyncEnumerator<object> IUniTaskAsyncEnumerable<object>.GetAsyncEnumerator(CancellationToken cancellationToken) =>
            _onEventRaise.GetAsyncEnumerator(cancellationToken);

        public override void OnAfterDeserialize()
        {
            cts.RefreshToken();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => OnAfterDeserialize();

        public override void Dispose()
        {
            cts.CancelAndDispose();
            _onEventRaise.Dispose();
        }
        
#if UNITY_EDITOR
        protected override void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.ExitingPlayMode)
            {
                cts.RefreshToken();
            }
        }
#endif
    }

    public partial class GameEvent<T> : IUniTaskAsyncEnumerable<T>
    {
        protected readonly AsyncReactiveProperty<T> onEventRaise = new AsyncReactiveProperty<T>(default);

        public virtual void Raise(T param)
        {
            _value = param;
            base.Raise();
            onEventRaise.Value = param;
        }

        IDisposable IEventHandler<T>.Subscribe(Action<T> action) => onEventRaise.Subscribe(action);

        public void Subscribe(Action<T> action) => Subscribe(action, cts.Token);
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
}

#endif