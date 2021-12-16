using System.IO;
using Kadinche.Kassets.Variable;
using UnityEngine;

namespace Kadinche.Kassets
{
    public static class KassetsJsonExtension
    {
        
#if UNITY_EDITOR
        private static readonly string DefaultPath = Application.dataPath;
#else
        private static readonly string DefaultPath = Application.persistentDataPath;
#endif
        
        private const string DefaultExtension = ".json";
        
        /// <summary>
        /// Load a json file from the given path.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="fullpath">Path to a directory where the json file exist. Path must include the json file.</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this VariableCore<T> data, string fullpath)
        {
            if (!File.Exists(fullpath)) return;
            var jsonString = File.ReadAllText(fullpath);
            JsonUtility.FromJsonOverwrite(jsonString, data.Value);
        }
        
        /// <summary>
        /// Load a json file from Unity's default data directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this VariableCore<T> data)
        {
            LoadFromJson(data, DefaultPath, data.name);
        }
        
        /// <summary>
        /// Load a json file from a directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this VariableCore<T> data, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullpath = Path.Combine(directory, fn);
            LoadFromJson(data, fullpath);
        }
        
        /// <summary>
        /// Load a json file with custom extension from a directory.
        /// </summary>
        /// <param name="data">Reference to data to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this VariableCore<T> data, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            LoadFromJson(data, directory, filenameExt);
        }
        
        /// <summary>
        /// Save a data into json file.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static void SaveToJson<T>(this VariableCore<T> data)
        {
            SaveToJson(data, DefaultPath, data.name);
        }
        
        /// <summary>
        /// Save a data into json file to the given path.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="fullPath">Path to a directory where the json file would exist. Path must include the json filename and extension.</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this VariableCore<T> data, string fullPath)
        {
            var jsonString = JsonUtility.ToJson(data.Value);
            File.WriteAllText(fullPath, jsonString);
        }

        /// <summary>
        /// Save a data into json file to the given directory.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this VariableCore<T> data, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullPath = Path.Combine(directory, fn);
            SaveToJson(data, fullPath);
        }
        
        /// <summary>
        /// Save a data into json file with custom extension into the given directory.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this VariableCore<T> data, string directory, string filename, string extension)
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
            return IsJsonExist(DefaultPath, filename);
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
        
        /// <summary>
        /// Check whether a json file exist in a directory.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsJsonExist<T>(this VariableCore<T> data, string filename)
        {
            return IsJsonExist(filename);
        }
        
        /// <summary>
        /// Check whether a json file exist.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsJsonExist<T>(this VariableCore<T> data)
        {
            return IsJsonExist(DefaultPath, data.name);
        }
    }
}