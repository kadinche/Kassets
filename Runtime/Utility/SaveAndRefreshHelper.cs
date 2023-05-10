#if UNITY_EDITOR

using UnityEditor;

namespace Kadinche.Kassets
{
    public static class SaveAndRefreshHelper
    {
        private static bool _executed;
        public static void Reset() => _executed = false;
        public static void SaveAndRefresh()
        {
            if (_executed) return;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            _executed = true;
        }
    }
}

#endif