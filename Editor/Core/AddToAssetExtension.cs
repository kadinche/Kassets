using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets
{
    /// <summary>
    /// Extensions to allow add Scriptable Object inside a Prefab or another ScriptableObject
    /// Reference : https://tsubakit1.hateblo.jp/entry/2017/08/03/233000 (Japanese/日本語)
    /// </summary>
    public static class AddToAssetExtension
    {
        private static T AddInternal<T>(this Object source, string assetName = default) where T : ScriptableObject
        {
            var so = ScriptableObject.CreateInstance<T> ();
            so.name = string.IsNullOrEmpty(assetName) || string.IsNullOrWhiteSpace(assetName)
                ? $"New {typeof(T).Name}"
                : assetName;
            
            AssetDatabase.AddObjectToAsset(so, AssetDatabase.GetAssetPath(source));
            AssetDatabase.SaveAssets();
            return so;
        }

        private static bool AddValidateInternal<T>(this Object source, string assetName = default) where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath(source);
            var checkName = !string.IsNullOrEmpty(assetName) && !string.IsNullOrWhiteSpace(assetName);
            var hasSO = AssetDatabase.LoadAllAssetsAtPath (path).ToList ().Exists (c => c is T && (!checkName || c.name == assetName));
            return !hasSO;
        }
        
        private static void RemoveInternal<T>(this Object source, string assetName = default) where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath(source);
            var checkName = !string.IsNullOrEmpty(assetName) && !string.IsNullOrWhiteSpace(assetName);
            var so = AssetDatabase.LoadAllAssetsAtPath(path).ToList().First(c => c is T && (!checkName || c.name == assetName));
            Object.DestroyImmediate(so, true);
            AssetDatabase.SaveAssets();
        }
        
        private static bool RemoveValidateInternal<T>(this Object source, string assetName = default) where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath (source);
            var checkName = !string.IsNullOrEmpty(assetName) && !string.IsNullOrWhiteSpace(assetName);
            var hasSO = AssetDatabase.LoadAllAssetsAtPath (path).ToList ().Exists (c => c is T && (!checkName || c.name == assetName));

            return hasSO;
        }
        
        // Prefab extension
        public static T Add<T>(this GameObject prefab, string assetName = default) where T : ScriptableObject => AddInternal<T>(prefab, assetName);

        public static bool AddValidate<T>(this GameObject prefab, string assetName = default) where T : ScriptableObject
        {
            var isPrefab = PrefabUtility.GetPrefabAssetType(prefab) != PrefabAssetType.NotAPrefab &&
                           PrefabUtility.GetPrefabInstanceStatus(prefab) == PrefabInstanceStatus.NotAPrefab;
            return isPrefab && AddValidateInternal<T>(prefab, assetName);
        }
        
        public static void Remove<T>(this GameObject prefab, string assetName = default) where T : ScriptableObject => RemoveInternal<T>(prefab, assetName);

        public static bool RemoveValidate<T>(this GameObject prefab, string assetName = default) where T : ScriptableObject
        {
            var isPrefab = PrefabUtility.GetPrefabAssetType(prefab) != PrefabAssetType.NotAPrefab &&
                           PrefabUtility.GetPrefabInstanceStatus(prefab) == PrefabInstanceStatus.NotAPrefab;
            return isPrefab && RemoveValidateInternal<T>(prefab, assetName);
        }
        
        // ScriptableObject extension
        public static T Add<T>(this ScriptableObject baseSO, string assetName = default) where T : ScriptableObject => AddInternal<T>(baseSO, assetName);

        public static bool AddValidate<T>(this ScriptableObject baseSO, string assetName = default) where T : ScriptableObject => AddValidateInternal<T>(baseSO, assetName);

        public static void Remove<T>(this ScriptableObject baseSO, string assetName = default) where T : ScriptableObject => RemoveInternal<T>(baseSO, assetName);

        public static bool RemoveValidate<T>(this ScriptableObject baseSO, string assetName = default) where T : ScriptableObject => RemoveValidateInternal<T>(baseSO, assetName);
    }
}