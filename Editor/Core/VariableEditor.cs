using Kadinche.Kassets.Variable;
using UnityEditor;

namespace Kadinche.Kassets.EventSystem
{
    [CustomEditor(typeof(VariableCore<>), true)]
    [CanEditMultipleObjects]
    public class VariableEditor : GameEventEditor
    {
        private SerializedProperty _defaultSubscribeBehavior;
        private SerializedProperty _value;
        private SerializedProperty _variableEventType;
        private SerializedProperty _autoResetValue;

        private void OnEnable()
        {
            _defaultSubscribeBehavior = serializedObject.FindProperty("_defaultSubscribeBehavior");
            _value = serializedObject.FindProperty("_value");
            _variableEventType = serializedObject.FindProperty("_variableEventType");
            _autoResetValue = serializedObject.FindProperty("_autoResetValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_defaultSubscribeBehavior);
            EditorGUILayout.PropertyField(_variableEventType);
            EditorGUILayout.PropertyField(_autoResetValue);
            EditorGUILayout.PropertyField(_value);
            serializedObject.ApplyModifiedProperties();
            AddRaiseButton();
        }
    }
}