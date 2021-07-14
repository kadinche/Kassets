using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    /// <summary>
    /// Core Game Event System.
    /// </summary>
    public abstract class GameEventBase : KassetsBase, IEventHandler
    {
        [Tooltip("Whether to listen to previous event upon registration")]
        [SerializeField] protected bool buffered;

        protected readonly IList<IDisposable> disposables = new List<IDisposable>();

        public IDisposable Subscribe(Action action)
        {
            var subscriber = new Subscription(action, disposables);
            if (!disposables.Contains(subscriber))
            {
                disposables.Add(subscriber);
                
                if (buffered)
                {
                    subscriber.Invoke();
                }
            }

            return subscriber;
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
    public abstract class GameEventBase<T> : GameEventBase, IEventHandler<T>
    {
        protected T bufferedValue = default;
        
        public virtual IDisposable Subscribe(Action<T> action)
        {
            var subscriber = new Subscription<T>(action, disposables);
            if (!disposables.Contains(subscriber))
            {
                disposables.Add(subscriber);
                
                if (buffered)
                {
                    subscriber.Invoke(bufferedValue);
                }
            }

            return subscriber;
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