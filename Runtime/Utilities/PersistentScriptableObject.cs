using UnityEngine;

namespace Kassets.Utilities
{
    public abstract class PersistentScriptableObject : ScriptableObject
    {
        protected virtual string BasePath => Application.dataPath;
        protected virtual string JsonFilename => name + ".json";
        public bool JsonFileExist => ExternalJsonExtension.IsJsonExist(BasePath, JsonFilename);

        public virtual void SaveToJson()
        {
            this.SaveToJson(BasePath, JsonFilename);
        }

        public virtual void LoadFromJson()
        {
            if (JsonFileExist)
                this.LoadFromJson(BasePath, JsonFilename);
            else
                SaveToJson();
        }
    }
}