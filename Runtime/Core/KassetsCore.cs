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
        private void OnEnable()
        {
#if UNITY_EDITOR
            UnsubscribeEditorEvents();
            SubscribeToEditorEvents();
#endif
            Initialize();
        }

        protected virtual void Initialize()
        {
#if UNITY_EDITOR
            if (IsDomainReloadDisabled) return;
#endif
            Application.quitting += OnQuit;
        }

        protected virtual void OnQuit()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            if (IsDomainReloadDisabled) return;
#endif
            Dispose();
            Application.quitting -= OnQuit;
        }

        public abstract void Dispose();
    
#if UNITY_EDITOR
        // Handling of special case when user decides to disable domain reload.
        private bool IsDomainReloadDisabled => EditorSettings.enterPlayModeOptionsEnabled &&
                                               EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload);
        
        private void OnPlayModeStateChanged(PlayModeStateChange playModeState)
        {
            // Only handle special case when user decides to disable domain reload.
            if (!IsDomainReloadDisabled) return;
            
            switch (playModeState)
            {
                case PlayModeStateChange.ExitingEditMode:
                    Initialize(); // need to call Initialize due to override calls.
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    OnQuit(); // need to call OnQuit due to override calls.
                    break;
            }
        }
        
        private void SubscribeToEditorEvents()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.quitting += UnsubscribeEditorEvents;
        }

        private void UnsubscribeEditorEvents()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.quitting -= UnsubscribeEditorEvents;
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