using Kadinche.Kassets.EventSystem;
using UnityEditor;

namespace Kadinche.Kassets.Variable
{
    [CustomEditor(typeof(VariableCore<>), true)]
    [CanEditMultipleObjects]
    public class VariableEditor : GameEventEditor
    {
        private SerializedProperty _value;

        private void OnEnable()
        {
            _value = serializedObject.FindProperty("_value");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "m_Script", "_value");
            EditorGUILayout.PropertyField(_value);
            AddRaiseButton();
            serializedObject.ApplyModifiedProperties();
        }
    }
}