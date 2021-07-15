using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    /// <summary>
    /// Core Game Event System.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = MenuHelper.DefaultEventMenu + "GameEvent")]
    public class GameEvent : KassetsBase, IEventRaiser, IEventHandler
    {
        [Tooltip("Whether to subscribe to previous event upon registration")]
        [SerializeField] protected bool buffered;
        
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

        public IDisposable Subscribe(Action action)
        {
            var subscription = new Subscription(action, disposables);
            if (!disposables.Contains(subscription))
            {
                disposables.Add(subscription);
                
                if (buffered)
                {
                    subscription.Invoke();
                }
            }

            return subscription;
        }

        public override void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable?.Dispose();
            }
            
            disposables.Clear();
        }
    }

    /// <summary>
    /// Generic base class for event system with parameter.
    /// </summary>
    /// <typeparam name="T">Parameter type for the event system</typeparam>
    public abstract class GameEvent<T> : GameEvent, IEventRaiser<T>, IEventHandler<T>
    {
        [SerializeField] protected T _value;

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
        
        public override void Raise() => Raise(_value);

        public IDisposable Subscribe(Action<T> action)
        {
            var subscription = new Subscription<T>(action, disposables);
            if (!disposables.Contains(subscription))
            {
                disposables.Add(subscription);
                
                if (buffered)
                {
                    subscription.Invoke(_value);
                }
            }

            return subscription;
        }

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
    public class GameEventCollection : IEventRaiser, IEventHandler, IDisposable
    {
        [SerializeField] private List<GameEvent> _gameEvents;

        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public IDisposable Subscribe(Action onAnyEvent)
        {
            foreach (var gameEvent in _gameEvents)
            {
                _compositeDisposable.Add(gameEvent.Subscribe(onAnyEvent));
            }

            return _compositeDisposable;
        }

        public void Raise()
        {
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Raise();
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
    
    internal class Subscription : IDisposable
    {
        private readonly Action _action;
        private readonly IList<IDisposable> _disposables;
        
        public Subscription(
            Action action,
            IList<IDisposable> disposables)
        {
            _action = action;
            _disposables = disposables;
        }

        public void Invoke() => _action.Invoke();
        
        public void Dispose()
        {
            if (_disposables.Contains(this))
            {
                _disposables.Remove(this);
            }
        }
    }
    
    internal class Subscription<T> : IDisposable
    {
        private readonly Action<T> _action;
        private readonly IList<IDisposable> _disposables;
        
        public Subscription(
            Action<T> action,
            IList<IDisposable> disposables)
        {
            _action = action;
            _disposables = disposables;
        }

        public void Invoke(T param) => _action.Invoke(param);
        
        public void Dispose()
        {
            if (_disposables.Contains(this))
            {
                _disposables.Remove(this);
            }
        }
    }

    internal class CompositeDisposable : IDisposable
    {
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        public void Add(IDisposable disposable) => _disposables.Add(disposable);

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}