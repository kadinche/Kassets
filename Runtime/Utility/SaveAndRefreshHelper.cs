#if UNITY_EDITOR

using UnityEditor;

namespace Kadinche.Kassets
{
    public static class SaveAndRefreshHelper
    {
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayModeInEditor(EnterPlayModeOptions options)
        {
            UnsubscribeEditorEvents();
            SubscribeToEditorEvents();
        }
        
        private static void SaveAndRefresh()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange playModeState)
        {
            switch (playModeState)
            {
                case PlayModeStateChange.EnteredEditMode:
                    SaveAndRefresh();
                    break;
            }
        }

        private static void SubscribeToEditorEvents()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.quitting += UnsubscribeEditorEvents;
        }

        private static void UnsubscribeEditorEvents()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.quitting -= UnsubscribeEditorEvents;
        }
    }
}

#endif