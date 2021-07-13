using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Kadinche.Kassets.EventSystem
{
    /// <summary>
    /// Game Event System using Asynchronous Pattern (UniTask) as core system.
    /// </summary>
    #if ODIN_INSPECTOR
    [InlineEditor()]
    #endif
    [CreateAssetMenu(fileName = "Event", menuName = MenuHelper.DefaultEventMenu + "GameEvent")]
    public class GameEvent : ScriptableObject, ISerializationCallbackReceiver, IDisposable
    {
        [Tooltip("Whether to listen to previous event on subscribe")]
        [SerializeField] protected bool buffered;
        
        /// <summary>
        /// Asynchronous reactive property (to self) as core event system.
        /// </summary>
        protected readonly AsyncReactiveProperty<GameEvent> onEventRaise = new AsyncReactiveProperty<GameEvent>(default);
        protected CancellationTokenSource cts = new CancellationTokenSource();
        
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise() => onEventRaise.Value = this;

        /// <summary>
        /// Raise event on editor.
        /// </summary>
        public virtual void RaiseEditor() => Raise();
        
        /// <summary>
        /// Wait for event asynchronously.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public UniTask EventAsync(CancellationToken cancellationToken) => onEventRaise.WaitAsync(cancellationToken);
        public UniTask EventAsync() => EventAsync(cts.Token);

        /// <summary>
        /// Use IUniTaskAsyncEnumerable to further make use of UniTask's Asynchronous LINQ
        /// </summary>
        /// <returns></returns>
        public IUniTaskAsyncEnumerable<GameEvent> AsUniTaskAsyncEnumerable() => buffered ? onEventRaise : onEventRaise.WithoutCurrent();
        
        /// <summary>
        /// Manually implemented Subscribe method since GameEvent base class doesn't derived from IUniTaskAsyncEnumerable.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IDisposable Subscribe(Action action) => AsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke());
        public void Subscribe(Action action, CancellationToken cancellationToken) => 
            AsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke(), cancellationToken);

        public override string ToString() => onEventRaise.ToString();

        public virtual void OnBeforeSerialize() { }
        public virtual void OnAfterDeserialize() => Cancel();
        
        public void Cancel(bool reset = true)
        {
            try
            {
                cts?.Cancel();
                cts?.Dispose();

                if (reset)
                    cts = new CancellationTokenSource();
            }
            catch (Exception)
            {
                //
            }
        }

        public virtual void Dispose()
        {
            Cancel(false);
            onEventRaise?.Dispose();
        }

        private void OnDestroy() => Dispose();
    }
    
    /// <summary>
    /// Generic base class for event system with parameter.
    /// </summary>
    /// <typeparam name="T">Parameter type for the event system</typeparam>
    public class GameEvent<T> : GameEvent, IUniTaskAsyncEnumerable<T>
    {
        [SerializeField] private T _inspectorRaiseValue;

        protected new readonly AsyncReactiveProperty<T> onEventRaise = new AsyncReactiveProperty<T>(default);

        public override void Raise() => Raise(onEventRaise.Value);

        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        public void Raise(T param)
        {
            onEventRaise.Value = param;
            base.onEventRaise.Value = this;
        }
        
        public override void RaiseEditor() => Raise(_inspectorRaiseValue);

        /// <summary>
        /// Wait for event asynchronously.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public new UniTask<T> EventAsync(CancellationToken cancellationToken) => onEventRaise.WaitAsync(cancellationToken);
        public new UniTask<T> EventAsync() => EventAsync(cts.Token);
        
        private IUniTaskAsyncEnumerable<T> UniTaskAsyncEnumerable => buffered ? onEventRaise : onEventRaise.WithoutCurrent();
        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken) => UniTaskAsyncEnumerable.GetAsyncEnumerator(cancellationToken);
        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator() => UniTaskAsyncEnumerable.GetAsyncEnumerator(cts.Token);

        public IDisposable Subscribe(Action<T> action) => onEventRaise.WithoutCurrent().Subscribe(action.Invoke);

        public override string ToString() => onEventRaise.ToString();

        public override void Dispose()
        {
            base.Dispose();
            onEventRaise?.Dispose();
        }
    }

    /// <summary>
    /// An event that contains collection of events. Get raised whenever any event is raised.
    /// Made it possible to listen to many events at once.
    /// </summary>
    [Serializable]
    public class GameEventCollection : IDisposable
    {
        [SerializeField] private List<GameEvent> _gameEvents;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public UniTask<int> AnyEventAsync(CancellationToken cancellationToken) => UniTask.WhenAny(_gameEvents.Select(gameEvent => gameEvent.EventAsync(cancellationToken)));
        public UniTask<int> AnyEventAsync() => AnyEventAsync(_cts.Token);
        
        public void SubscribeAnyEvent(Action onAnyEvent, CancellationToken cancellationToken)
        {
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent, cancellationToken);
            }
        }

        public void RaiseAllEvents()
        {
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Raise();
            }
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() => Cancel();
        
        public void Cancel(bool reset = true)
        {
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();

                if (reset)
                    _cts = new CancellationTokenSource();
            }
            catch (Exception)
            {
                //
            }
        }

        public void Dispose() => Cancel(false);
    }
    
    public static class GameEventAsyncExtension
    {
        public static UniTask.Awaiter GetAwaiter(this GameEvent source)
        {
            return source.EventAsync().GetAwaiter();
        }
        
        public static UniTask<T>.Awaiter GetAwaiter<T>(this GameEvent<T> source)
        {
            return source.EventAsync().GetAwaiter();
        }

        public static UniTask<int>.Awaiter GetAwaiter(this GameEventCollection source)
        {
            return source.AnyEventAsync().GetAwaiter();
        }
    } 
}