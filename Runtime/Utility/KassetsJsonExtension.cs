using System;
using System.IO;
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
        /// Load variable from json string
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="jsonString"></param>
        /// <typeparam name="T"></typeparam>
        public static void FromJsonString<T>(this IVariable<T> variable, string jsonString)
        {
            variable.Value = JsonUtility.FromJson<JsonableWrapper<T>>(jsonString).value;
        }
        
        /// <summary>
        /// Load variable from json string. Note that this uses Reflection.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="jsonString"></param>
        public static void FromJsonString(this IVariable variable, string jsonString)
        {
            // Get the type of variable
            var variableType = variable.GetType();
            
            while (!(variableType is { IsGenericType: true })) 
                variableType = variableType?.BaseType;

            // Get the generic type of variable
            var genericType = variableType.GetGenericArguments()[0];
            var propertyInfo = variableType.GetProperty("Value");
            // Get the Value property of variable using reflection
            var value = propertyInfo?.GetValue(variable);
            // Create an instance of JsonableWrapper<T> with the value of variable
            var wrapped = Activator.CreateInstance(typeof(JsonableWrapper<>).MakeGenericType(genericType), value);
            
            // Deserialize json string to wrapped
            JsonUtility.FromJsonOverwrite(jsonString, wrapped);
            // Get the value of wrapped
            value = wrapped.GetType().GetField("value").GetValue(wrapped);
            // Get the Value property of variable using reflection and Set the value of the Value property.
            propertyInfo?.SetValue(variable, value);
        }
        
        /// <summary>
        /// Convert variable to json string
        /// </summary>
        /// <param name="variable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJsonString<T>(this IVariable<T> variable)
        {
            return JsonUtility.ToJson(new JsonableWrapper<T>(variable.Value), Application.isEditor);
        }

        /// <summary>
        /// Convert variable to json string. Note that this uses Reflection.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static string ToJsonString(this IVariable variable)
        {
            // Get the type of variable
            var variableType = variable.GetType();
            
            while (!(variableType is { IsGenericType: true })) 
                variableType = variableType?.BaseType;

            // Get the generic type of variable
            var genericType = variableType.GetGenericArguments()[0];
            // Get the Value property of variable using reflection
            var value = variableType.GetProperty("Value")?.GetValue(variable);
            // Create an instance of JsonableWrapper<T> with the value of variable
            var wrapped = Activator.CreateInstance(typeof(JsonableWrapper<>).MakeGenericType(genericType), value);
            // Convert wrapped to json string
            return JsonUtility.ToJson(wrapped, Application.isEditor);
        }

        /// <summary>
        /// Load a json file from the given path.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="fullpath">Path to a directory where the json file exist. Path must include the json file.</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this IVariable<T> variable, string fullpath)
        {
            if (!File.Exists(fullpath)) return;
            var jsonString = File.ReadAllText(fullpath);
            FromJsonString(variable, jsonString);
        }
        
        /// <summary>
        /// Load a json file from the given path.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="fullpath">Path to a directory where the json file exist. Path must include the json file.</param>
        public static void LoadFromJson(this IVariable variable, string fullpath)
        {
            if (!File.Exists(fullpath)) return;
            var jsonString = File.ReadAllText(fullpath);
            FromJsonString(variable, jsonString);
        }
        
        /// <summary>
        /// Load a json file from Unity's default data directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this IVariable<T> variable)
        {
            if (variable is ScriptableObject so)
                LoadFromJson(variable, DefaultPath, so.name);
        }
        
        /// <summary>
        /// Load a json file from Unity's default data directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        public static void LoadFromJson(this IVariable variable)
        {
            if (variable is ScriptableObject so)
                LoadFromJson(variable, DefaultPath, so.name);
        }
        
        /// <summary>
        /// Load a json file from a directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this IVariable<T> variable, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullpath = Path.Combine(directory, fn);
            LoadFromJson(variable, fullpath);
        }
        
        /// <summary>
        /// Load a json file from a directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        public static void LoadFromJson(this IVariable variable, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullpath = Path.Combine(directory, fn);
            LoadFromJson(variable, fullpath);
        }
        
        /// <summary>
        /// Load a json file with custom extension from a directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to load. Must be serializable.</typeparam>
        public static void LoadFromJson<T>(this IVariable<T> variable, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            LoadFromJson(variable, directory, filenameExt);
        }
        
        /// <summary>
        /// Load a json file with custom extension from a directory.
        /// </summary>
        /// <param name="variable">Reference to variable to load</param>
        /// <param name="directory">Path to a directory where the json file exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        public static void LoadFromJson(this IVariable variable, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            LoadFromJson(variable, directory, filenameExt);
        }
        
        /// <summary>
        /// Save a variable into json file.
        /// </summary>
        /// <param name="variable"></param>
        /// <typeparam name="T"></typeparam>
        public static void SaveToJson<T>(this IVariable<T> variable)
        {
            if (variable is ScriptableObject so)
                SaveToJson(variable, DefaultPath, so.name);
        }

        /// <summary>
        /// Save a variable into json file.
        /// </summary>
        /// <param name="variable"></param>
        public static void SaveToJson(this IVariable variable)
        {
            if (variable is ScriptableObject so)
                SaveToJson(variable, DefaultPath, so.name);
        }
        
        /// <summary>
        /// Save a variable into json file to the given path.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="fullPath">Path to a directory where the json file would exist. Path must include the json filename and extension.</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this IVariable<T> variable, string fullPath)
        {
            var jsonString = ToJsonString(variable);
            File.WriteAllText(fullPath, jsonString);
        }
        
        /// <summary>
        /// Save a variable into json file to the given path.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="fullPath">Path to a directory where the json file would exist. Path must include the json filename and extension.</param>
        public static void SaveToJson(this IVariable variable, string fullPath)
        {
            var jsonString = ToJsonString(variable);
            File.WriteAllText(fullPath, jsonString);
        }

        /// <summary>
        /// Save a variable into json file to the given directory.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this IVariable<T> variable, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullPath = Path.Combine(directory, fn);
            SaveToJson(variable, fullPath);
        }
        
        /// <summary>
        /// Save a variable into json file to the given directory.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        public static void SaveToJson(this IVariable variable, string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fn = filename.Split('.').Length < 2 ? filename + DefaultExtension : filename;
            var fullPath = Path.Combine(directory, fn);
            SaveToJson(variable, fullPath);
        }
        
        /// <summary>
        /// Save a variable into json file with custom extension into the given directory.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        /// <typeparam name="T">Data type to save. Must be serializable.</typeparam>
        public static void SaveToJson<T>(this IVariable<T> variable, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            SaveToJson(variable, directory, filenameExt);
        }

        /// <summary>
        /// Save a variable into json file with custom extension into the given directory.
        /// </summary>
        /// <param name="variable">Variable to save.</param>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <param name="extension">Custom extension of the json file</param>
        public static void SaveToJson(this IVariable variable, string directory, string filename, string extension)
        {
            var ext = extension[0] == '.' ? extension : "." + extension;
            var filenameExt = $"{filename}{ext}";
            SaveToJson(variable, directory, filenameExt);
        }
        
        /// <summary>
        /// Check whether a json file exist in Unity's default data directory.
        /// </summary>
        /// <param name="filename">Name of the json file</param>
        /// <returns>true if json file exist.</returns>
        public static bool IsJsonFileExist(string filename)
        {
            return IsJsonFileExist(DefaultPath, filename);
        }

        /// <summary>
        /// Check whether a json file exist in a directory.
        /// </summary>
        /// <param name="directory">Path to a directory where the json file would exist</param>
        /// <param name="filename">Name of the json file</param>
        /// <returns>true if json file exist.</returns>
        public static bool IsJsonFileExist(string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, filename);
            return File.Exists(path);
        }
        
        /// <summary>
        /// Check whether a json file exist in a directory.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsJsonFileExist(this IVariable variable, string filename)
        {
            return IsJsonFileExist(filename);
        }
        
        /// <summary>
        /// Check whether a json file exist.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static bool IsJsonFileExist(this IVariable variable)
        {
            if (variable is ScriptableObject so)
                return IsJsonFileExist(DefaultPath, so.name);
            return false;
        }

        [Serializable]
        public struct JsonableWrapper<T>
        {
            public T value;
            public JsonableWrapper(T value) => this.value = value;
        }
    }
}