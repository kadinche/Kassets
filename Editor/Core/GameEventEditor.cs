using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CustomEditor(typeof(GameEvent), true)]
    [CanEditMultipleObjects]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AddCustomButtons();
        }

        protected virtual void AddCustomButtons()
        {
            GUI.enabled = Application.isPlaying;

            if (!(target is GameEvent gameEvent)) return;
            
            if (GUILayout.Button("Raise"))
            {
                gameEvent.Raise();
                Debug.Log($"{target.name} event raised.");
            }
        }
    }
    
    [CustomEditor(typeof(GameEvent<>), true)]
    [CanEditMultipleObjects]
    public class TypedGameEventEditor : GameEventEditor
    {
        private readonly string[] _excludedProperties = { "m_Script", "_value", "instanceSettings" };

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            using var value = serializedObject.FindProperty("_value");
            if (value.propertyType == SerializedPropertyType.Generic && !value.isArray)
                foreach (var child in value.GetChildren()) 
                    EditorGUILayout.PropertyField(child, true);
            else
                EditorGUILayout.PropertyField(value, true);
            
            DrawPropertiesExcluding(serializedObject, _excludedProperties);
            
            using var instanceSettings = serializedObject.FindProperty("instanceSettings");
            if (instanceSettings != null)
                EditorGUILayout.PropertyField(instanceSettings);

            AddCustomButtons();

            if (Application.isPlaying && target is GameEvent gameEvent) 
                gameEvent.Raise();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
