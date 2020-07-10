using System.IO;
using UnityEngine;

namespace Kassets.Utilities
{
    public static class ExternalJsonExtension
    {
        #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private static readonly string BasePath = Application.persistentDataPath;
        #else
        private static readonly string BasePath = Application.dataPath;
        #endif
        private const string DefaultExtension = ".json";
        
        /// <summary>
        /// Load a json file from the given path.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="fullpath">Path to a directory where the json file exist. Path must include the json file.</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJsonPath<T>(this T data, string fullpath)
        {
            if (!File.Exists(fullpath)) return;
            var jsonString = File.ReadAllText(fullpath);
            JsonUtility.FromJsonOverwrite(jsonString, data);
        }
        
        /// <summary>
        /// Load a json file from Unity's default data directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this T data, string filename)
        {
            LoadFromJson(data, BasePath, filename);
        }
        
        /// <summary>
        /// Load a json file from a directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this T data, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullpath = Path.Combine(directory, fn);
            LoadFromJsonPath(data, fullpath);
        }
        
        /// <summary>
        /// Load a json file with custom extension from a directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this T data, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            LoadFromJson(data, directory, filenameExt);
        }
        
        /// <summary>
        /// Save a data into json file to the given path.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="fullpath">Path to a directory where the json file would exist. Path must include the json file.</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJsonPath<T>(this T data, string fullpath)
        {
            var jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(fullpath, jsonString);
        }
        
        /// <summary>
        /// Save a data into json file to Unity's default data directory.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this T data, string filename)
        {
            SaveToJson(data, BasePath, filename);
        }

        /// <summary>
        /// Save a data into json file to the given directory.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this T data, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullpath = Path.Combine(directory, fn);
            SaveToJsonPath(data, fullpath);
        }
        
        /// <summary>
        /// Save a data into json file with custom extension into the given directory.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this T data, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            SaveToJson(data, directory, filenameExt);
        }

        /// <summary>
        /// Check whether a json file exist in Unity's default data directory.
        /// </summary>
        /// <param name="filename">Name of the json file</param>
        /// <returns>true if json file exist.</returns>
        public static bool IsJsonExist(string filename)
        {
            return IsJsonExist(BasePath, filename);
        }

        /// <summary>
        /// Check whether a json file exist in a directory.
        /// </summary>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <returns>true if json file exist.</returns>
        public static bool IsJsonExist(string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, filename);
            return File.Exists(path);
        }
    }
}