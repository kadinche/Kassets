using System;
using UnityEngine;

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
            // NOTE : Unsubscribe then Subscribe ensures the subscription to the editor events only once.
            // Using flags does not work as expected when Domain Reload is disabled.
            // Reference : https://stackoverflow.com/a/7065833
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
            // NOTE : Do not call Dispose when Domain Reload is disabled.
            // When Domain Reload is disabled, Disposed object doesn't get re-instantiated.
            Dispose(); 
            Application.quitting -= OnQuit;
        }

        public abstract void Dispose();
    
#if UNITY_EDITOR
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
}