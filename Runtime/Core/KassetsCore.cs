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
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        protected virtual void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

        public abstract void Dispose();

        protected virtual void OnDestroy()
        {
            Dispose();
        }
        
#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            switch (stateChange)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    OnEnterPlayMode();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    OnExitPlayMode();
                    break;
            }
        }

        protected virtual void OnEnterPlayMode() { }

        protected virtual void OnExitPlayMode() { }

        protected void SaveAndRefresh()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
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