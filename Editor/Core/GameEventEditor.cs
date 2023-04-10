using System.Linq;
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
        private readonly string[] _excludedProperties = { "m_Script", "_value" };
        private readonly string[] _instanceSettings = { "defaultSubscribeBehavior", "variableEventType", "autoResetValue" };
        private readonly string _instanceSettingsLabel = "Instance Settings";
        private bool _showInstanceSettings;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            using var value = serializedObject.FindProperty("_value");
            if (value.propertyType == SerializedPropertyType.Generic && !value.isArray)
                foreach (var child in value.GetChildren()) 
                    EditorGUILayout.PropertyField(child, true);
            else
                EditorGUILayout.PropertyField(value, true);

            var toExclude = _excludedProperties.Concat(_instanceSettings).ToArray();
            DrawPropertiesExcluding(serializedObject, toExclude);

            _showInstanceSettings = EditorGUILayout.Foldout(_showInstanceSettings, _instanceSettingsLabel);

            if (_showInstanceSettings)
            {
                EditorGUI.indentLevel++;
                
                foreach (var settingName in _instanceSettings)
                {
                    using var prop = serializedObject.FindProperty(settingName);
                    if (prop == null) continue;
                    EditorGUILayout.PropertyField(prop);
                }
                
                EditorGUI.indentLevel--;
            }

            AddCustomButtons();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
