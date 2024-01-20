using Kadinche.Kassets.EventSystem;
using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CustomEditor(typeof(VariableCore<>), true)]
    public class VariableEditor : TypedGameEventEditor
    {
        private bool _showJsonFileOperation;
        private readonly string _jsonOpLabel = "Json File Management";
        
        private readonly string[] _jsonPathType = new[] { "Data Path", "Persistent Data Path", "Custom" };
        private int _selectedType;
        private bool _defaultFilename = true;
        private string _jsonPath;
        private string _filename;
        
        private void OnValidate()
        {
            _showJsonFileOperation = false;
        }

        protected override void AddCustomButtons()
        {
            if (!(target is IVariable variable)) return;

            GUI.enabled = true;
            
            _showJsonFileOperation = EditorGUILayout.Foldout(_showJsonFileOperation, _jsonOpLabel);

            if (_showJsonFileOperation)
            {
                EditorGUI.indentLevel++;

                _selectedType = EditorGUILayout.Popup(
                    label: new GUIContent("Json Path Type"),
                    selectedIndex: _selectedType,
                    displayedOptions: _jsonPathType
                );

                _jsonPath = _selectedType switch
                {
                    2 => EditorGUILayout.TextField(_jsonPath),
                    1 => Application.persistentDataPath,
                    _ => Application.dataPath
                };
                
                EditorGUILayout.BeginHorizontal();
                
                GUILayout.Space(EditorGUI.indentLevel * 15);
                
                GUILayout.Label("File Name", GUILayout.ExpandWidth(false));
                
                GUI.enabled = !_defaultFilename;
                _filename = EditorGUILayout.TextField(_filename);
                
                GUI.enabled = true;
                
                _defaultFilename = EditorGUILayout.Toggle(_defaultFilename, GUILayout.MaxWidth(30));
                GUILayout.Label("Default", GUILayout.ExpandWidth(false));
                
                if (_defaultFilename)
                    _filename = $"{target.name}.json";
                
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Space(EditorGUI.indentLevel * 15);
                
                if (GUILayout.Button("Save to Json"))
                {
                    variable.SaveToJson(_jsonPath, _filename);
                    Debug.Log($"Saved {_filename} to {_jsonPath}");
                    
                    AssetDatabase.Refresh();
                }

                if (GUILayout.Button("Load from Json"))
                {
                    if (variable.IsJsonFileExist(_jsonPath, _filename))
                    {
                        serializedObject.Update();

                        variable.LoadFromJson(_jsonPath, _filename);
                        Debug.Log($"Loaded {_filename} from {_jsonPath}");
                        
                        serializedObject.ApplyModifiedProperties();
                    }
                    else
                    {
                        Debug.Log($"Could not found file {_jsonPath}/{_filename}");
                    }
                }

                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }

            GUILayout.Space(15);
            
            base.AddCustomButtons();
        }
    }
}