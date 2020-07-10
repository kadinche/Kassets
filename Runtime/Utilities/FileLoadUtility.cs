using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Kassets.Utilities
{
    public static class FileLoadUtility
    {
        private static string GenerateUri(string url)
        {
            if (url.Contains("http"))
                return url;

            if (Path.IsPathRooted(url))
                return "file://" + url;

            return "file://" + Application.dataPath + url;
        }
        
        public static async UniTask<byte[]> LoadBytes(string url)
        {
            var uri = GenerateUri(url);
            
            var www = UnityWebRequest.Get(uri);
            await www.SendWebRequest();
            return www.downloadHandler.data;
        }
        
        public static async UniTask<Texture2D> LoadTexture(string url)
        {
            var uri = GenerateUri(url);

            var www = UnityWebRequestTexture.GetTexture(uri);
            await www.SendWebRequest();
            return DownloadHandlerTexture.GetContent(www);
        }
    }
}