#if UNITY_EDITOR

using UnityEditor;

namespace Kadinche.Kassets
{
    public static class SaveAndRefreshHelper
    {
        private static int _requestCount;
        public static void RequestExecute() => _requestCount++;
        public static void SaveAndRefresh()
        {
            if (--_requestCount > 0) return;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif