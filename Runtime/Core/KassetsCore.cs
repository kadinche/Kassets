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
    public abstract class KassetsCore : ScriptableObject, IDisposable
    {
        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            SaveAndRefreshHelper.Reset();
#endif
            Application.quitting += OnQuit;
        }

        protected virtual void OnQuit()
        {
            Dispose();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            SaveAndRefreshHelper.SaveAndRefresh();
#endif
            Application.quitting -= OnQuit;
        }

        public abstract void Dispose();
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
            _action = null;
            if (_disposables == null) return;
            if (_disposables.Contains(this))
            {
                _disposables.Remove(this);
            }

            _disposables = null;
        }
    }
#endif
}