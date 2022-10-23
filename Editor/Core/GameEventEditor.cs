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
            AddRaiseButton();
        }

        protected void AddRaiseButton()
        {
            GUI.enabled = Application.isPlaying;

            if (target is GameEvent gameEvent && 
                GUILayout.Button("Raise"))
                gameEvent.Raise();
        }
    }
    
    [CustomEditor(typeof(GameEvent<>), true)]
    [CanEditMultipleObjects]
    public class TypedGameEventEditor : GameEventEditor
    {
        private SerializedProperty _value;
        private SerializedProperty _instanceSettings;
        private readonly string[] _excludedProperties = { "m_Script", "_value", "instanceSettings" };
    
        private void OnEnable()
        {
            _value = serializedObject.FindProperty("_value");
            _instanceSettings = serializedObject.FindProperty("instanceSettings");
        }
    
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            if (_value.propertyType == SerializedPropertyType.Generic && !_value.isArray)
                foreach (var child in _value.GetChildren()) 
                    EditorGUILayout.PropertyField(child);
            else
                EditorGUILayout.PropertyField(_value);
            
            DrawPropertiesExcluding(serializedObject, _excludedProperties);
            
            if (_instanceSettings != null)
                EditorGUILayout.PropertyField(_instanceSettings);

            AddRaiseButton();

            if (Application.isPlaying && target is GameEvent gameEvent) 
                gameEvent.Raise();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
