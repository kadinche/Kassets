using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadinche.Kassets
{
    public abstract class KassetsBase : ScriptableObject, ISerializationCallbackReceiver, IDisposable
    {
        public virtual void OnBeforeSerialize() {}
        public virtual void OnAfterDeserialize() {}
        public abstract void Dispose();
        protected virtual void OnDestroy() => Dispose();
    }
    
    internal class Subscription : IDisposable
    {
        private Action _action;
        private IList<IDisposable> _disposables;
        
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
            
            _action = null;
            _disposables = null;
        }
    }
    
    internal class Subscription<T> : IDisposable
    {
        private Action<T> _action;
        private IList<IDisposable> _disposables;
        
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

            _action = null;
            _disposables = null;
        }
    }

    internal class CompositeDisposable : IDisposable
    {
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        public void Add(IDisposable disposable) => _disposables.Add(disposable);

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }

    internal static class DisposableExtension
    {
        internal static void Dispose(this IList<IDisposable> disposables)
        {
            foreach (var disposable in disposables)
            {
                disposable?.Dispose();
            }
            
            disposables.Clear();
        }
    }
}