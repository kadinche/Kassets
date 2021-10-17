using System;
using UnityEngine;

#if !KASSETS_UNIRX && !KASSETS_UNITASK
using System.Collections.Generic;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kadinche.Kassets
{
    public abstract class KassetsCore : ScriptableObject, ISerializationCallbackReceiver, IDisposable
    {
        public virtual void OnBeforeSerialize() {}
        public virtual void OnAfterDeserialize() {}
        public abstract void Dispose();
        
        protected virtual void OnDestroy()
        {
            Dispose();
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void Awake()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        protected virtual void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
#endif
        }
    }
    
#if !KASSETS_UNIRX && !KASSETS_UNITASK
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
#endif
}