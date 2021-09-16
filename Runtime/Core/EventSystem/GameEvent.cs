using System;
using System.Collections.Generic;
using Kadinche.Kassets.CommandSystem;
using UnityEngine;

#if !KASSETS_UNIRX
using Kadinche.Kassets.Utilities;
#endif

namespace Kadinche.Kassets.EventSystem
{
    /// <summary>
    /// Core Game Event System.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = MenuHelper.DefaultEventMenu + "GameEvent")]
    public partial class GameEvent : CommandBase, IEventRaiser, IEventHandler
    {
        public override void Execute() => Raise();
    }

    /// <summary>
    /// Generic base class for event system with parameter.
    /// </summary>
    /// <typeparam name="T">Parameter type for the event system</typeparam>
    public abstract partial class GameEvent<T> : GameEvent, IEventRaiser<T>, IEventHandler<T>
    {
        [SerializeField] protected T _value;

        public override void Raise() => Raise(_value);
        
        public override void OnAfterDeserialize()
        {
            _value = default;
        }
    }

    /// <summary>
    /// An event that contains collection of events. Get raised whenever any event is raised.
    /// Made it possible to listen to many events at once.
    /// </summary>
    [Serializable]
    public partial class GameEventCollection : IEventRaiser, IEventHandler, IDisposable
    {
        [SerializeField] private List<GameEvent> _gameEvents;
        [SerializeField] protected bool buffered;

        public IDisposable Subscribe(Action action) => Subscribe(action, buffered);

        public void Raise()
        {
            foreach (IEventRaiser gameEvent in _gameEvents)
            {
                gameEvent.Raise();
            }
        }
    }

#if !KASSETS_UNIRX && !KASSETS_UNITASK

    public partial class GameEvent
    {
        [Tooltip("Whether to listen to previous event upon subscription.")]
        [SerializeField] protected bool buffered;
        
        public IDisposable Subscribe(Action action) => Subscribe(action, buffered);
        
        protected readonly IList<IDisposable> disposables = new List<IDisposable>();

        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise()
        {
            foreach (var disposable in disposables)
            {
                if (disposable is Subscription subscription)
                {
                    subscription.Invoke();
                }
            }
        }

        public IDisposable Subscribe(Action action, bool withBuffer)
        {
            var subscription = new Subscription(action, disposables);
            if (!disposables.Contains(subscription))
            {
                disposables.Add(subscription);
                
                if (withBuffer)
                {
                    subscription.Invoke();
                }
            }

            return subscription;
        }

        public override void Dispose() => disposables.Dispose();
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
            foreach (var disposable in disposables)
            {
                if (disposable is Subscription<T> subscription)
                {
                    subscription.Invoke(_value);
                }
            }
        }

        public IDisposable Subscribe(Action<T> action) => Subscribe(action, buffered);

        public IDisposable Subscribe(Action<T> action, bool withBuffer)
        {
            var subscription = new Subscription<T>(action, disposables);
            if (!disposables.Contains(subscription))
            {
                disposables.Add(subscription);

                if (withBuffer)
                {
                    subscription.Invoke(_value);
                }
            }

            return subscription;
        }
    }
#endif

    
#if !KASSETS_UNIRX
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
#endif

#if !KASSETS_UNITASK
    public partial class GameEventCollection
    {
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
#endif
}
