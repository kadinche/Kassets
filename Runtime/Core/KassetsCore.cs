using System;
using UnityEngine;

#if !KASSETS_UNIRX && !KASSETS_UNITASK
using System.Collections.Generic;
#endif

#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
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
                case PlayModeStateChange.ExitingEditMode:
                    OnExitingEditMode();
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    OnEnteringEditMode();
                    break;
            }
        }

        protected virtual void OnExitingEditMode() { }

        protected virtual void OnEnteringEditMode()
        {
            EditorUtility.SetDirty(this);
            SaveAndRefresh().Forget();
        }

        private async UniTaskVoid SaveAndRefresh()
        {
            SaveAndRefreshHelper.RequestExecute();
            await UniTask.Delay(100);
            SaveAndRefreshHelper.SaveAndRefresh();
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