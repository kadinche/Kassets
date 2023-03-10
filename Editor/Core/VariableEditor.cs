using Kadinche.Kassets.EventSystem;
using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CustomEditor(typeof(VariableCore<>), true)]
    [CanEditMultipleObjects]
    public class VariableEditor : TypedGameEventEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AddJsonOperationButton();
        }

        private void AddJsonOperationButton()
        {
            if (!(target is IVariable variable)) return;

            serializedObject.Update();
            
            GUILayout.BeginHorizontal();

            GUI.enabled = true;
            if (GUILayout.Button("Save to Json"))
            {
                variable.SaveToJson();
                Debug.Log($"Saved {target.name} to json file.");
            }

            GUI.enabled = variable.IsJsonFileExist();
            if (GUILayout.Button("Load from Json"))
            {
                variable.LoadFromJson();
                Debug.Log($"Loaded {target.name} from json file.");
            }
            
            GUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
            
            AssetDatabase.Refresh();
        }
    }
}